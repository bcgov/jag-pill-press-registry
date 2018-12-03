import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormArray, FormBuilder, Validators } from '@angular/forms';
import { Subscription, Observable, zip } from 'rxjs';
import { PRODUCTING_OWN_PRODUCT, MANUFACTURING_FOR_OTHERS } from '../../waiver/waiver-application/waiver-application.component';
import { ActivatedRoute, Router } from '@angular/router';
import { DynamicsDataService } from '../../../services/dynamics-data.service';
import { ApplicationDataService } from '../../../services/adoxio-application-data.service';

import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import * as _moment from 'moment';
// tslint:disable-next-line:no-duplicate-imports
import { defaultFormat as _rollupMoment } from 'moment';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material';
import { DynamicsContact } from 'src/app/models/dynamics-contact.model';
const moment = _rollupMoment || _moment;

// See the Moment.js docs for the meaning of these formats:
// https://momentjs.com/docs/#/displaying/format/
export const MY_FORMATS = {
  parse: {
    dateInput: 'LL',
  },
  display: {
    dateInput: 'YYYY-MM-DD',
    monthYearLabel: 'MMM YYYY',
    dateA11yLabel: 'YYYY-MM-DD',
    monthYearA11yLabel: 'MMMM YYYY',
  },
};

@Component({
  selector: 'app-seller-application',
  templateUrl: './seller-application.component.html',
  styleUrls: ['./seller-application.component.scss']
})
export class SellerApplicationComponent implements OnInit {
  form: FormGroup;
  busy: Subscription;
  waiverId: string;

  deletedProducts: any[] = [];
  PRODUCTING_OWN_PRODUCT = PRODUCTING_OWN_PRODUCT;
  MANUFACTURING_FOR_OTHERS = MANUFACTURING_FOR_OTHERS;
  ownerList: any[];

  get ownProducts(): FormArray {
    return <FormArray>this.form.get('ownProducts');
  }
  get productsForOthers(): FormArray {
    return <FormArray>this.form.get('productsForOthers');
  }

  constructor(private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog,
    private dynamicsDataService: DynamicsDataService,
    private applicationDataService: ApplicationDataService) {
    this.waiverId = this.route.snapshot.params.id;
  }

  ngOnInit() {
    this.form = this.fb.group({
      id: [],
      currentlyownusepossessequipment: ['', Validators.required],
      intendtopurchaseequipment: [''],
      ownusepossesstoproduceaproduct: ['', Validators.required],
      intendonrentingleasingtoothers: ['', Validators.required],
      intendonsellingequipmenttoothers: ['', Validators.required],

      manufacturerofcontrolledequipment: [],
      retailerofcontrolledequipment: [],
      onetimesellerofowncontrolledequipment: [],
      typeofsellerothercheck: [],
      typeofsellerother: [],

      intendtosellpillpress: [],
      intendtosellencapsulator: [],
      intendtoselldiemouldorpunch: [],
      intendtosellpharmaceuticalmixerorblender: [],
      intendtosellothercheck: [],
      intendtosellother: [],

      additionalbusinessinformationaboutseller: [],
      registeredsellerownermanager: ['', Validators.required],
    });

    this.form.get('producingownproduct').valueChanges
      .subscribe(value => {
        if (value === false) {
          while (this.ownProducts.controls.length > 0) {
            this.deleteCustomProduct(0, this.ownProducts.controls[0].value.purpose);
          }
        } else {
          this.addCustomProduct(<CustomProduct>{ purpose: PRODUCTING_OWN_PRODUCT });
        }
      });

    this.form.get('providingmanufacturingtoothers').valueChanges
      .subscribe(value => {
        if (value === false) {
          while (this.productsForOthers.controls.length > 0) {
            this.deleteCustomProduct(0, this.productsForOthers.controls[0].value.purpose);
          }
        } else {
          this.addCustomProduct(<CustomProduct>{ purpose: MANUFACTURING_FOR_OTHERS });
        }
      });

    this.reloadData();
  }

  reloadData() {
    this.applicationDataService.getApplicationById(this.waiverId).subscribe(data => {
      this.form.patchValue(data);

      // process custom products
      data.customProducts = data.customProducts || [];
      this.clearCustomProducts();
      const ownProducts = data.customProducts.filter(p => p.purpose === PRODUCTING_OWN_PRODUCT);
      ownProducts.forEach(p => {
        this.addCustomProduct(p);
      });

      const productsForOthers = data.customProducts.filter(p => p.purpose === MANUFACTURING_FOR_OTHERS);
      productsForOthers.forEach(p => {
        this.addCustomProduct(p);
      });
    }, error => {
      // todo: show errors;
    });
  }

  markAsTouched() {
    this.form.markAsTouched();
    const controls = this.form.controls;
    for (const c in controls) {
      if (typeof (controls[c].markAsTouched) === 'function') {
        controls[c].markAsTouched();
      }
    }

    this.productsForOthers.controls
      .concat(this.ownProducts.controls)
      .forEach((fa: FormGroup) => {
        const arrayControls = fa.controls;
        for (const c in arrayControls) {
          if (typeof (arrayControls[c].markAsTouched) === 'function') {
            arrayControls[c].markAsTouched();
          }
        }
      });
  }

  clearCustomProducts() {
    while (this.ownProducts.controls.length > 0) {
      this.ownProducts.removeAt(0);
    }

    while (this.productsForOthers.controls.length > 0) {
      this.productsForOthers.removeAt(0);
    }
  }

  createCustomProduct(product: CustomProduct) {
    return this.fb.group({
      id: [product.id],
      purpose: [product.purpose],
      incidentId: [this.waiverId],
      productdescriptionandintendeduse: [product.productdescriptionandintendeduse, Validators.required]
    });
  }

  addCustomProduct(product: CustomProduct) {
    if (product.purpose === PRODUCTING_OWN_PRODUCT) {
      const control = this.createCustomProduct(product);
      this.ownProducts.push(control);
    } else if (product.purpose === MANUFACTURING_FOR_OTHERS) {
      const control = this.createCustomProduct(product);
      this.productsForOthers.push(control);
    }

  }

  deleteCustomProduct(index: number, type: string) {
    if (type === PRODUCTING_OWN_PRODUCT) {
      const product = this.ownProducts.at(index).value;
      if (product.id) {
        this.deletedProducts.push(product);
      }
      this.ownProducts.removeAt(index);
    } else if (type === MANUFACTURING_FOR_OTHERS) {
      const product = this.productsForOthers.at(index).value;
      if (product.id) {
        this.deletedProducts.push(product);
      }
      this.productsForOthers.removeAt(index);
    }
  }

  save(gotToReview: boolean) {
    const value = this.form.value;
    const saveList = [this.applicationDataService.updateApplication(value), ...this.saveCustomProducts()];
    zip(...saveList)
      .subscribe(res => {
        if (gotToReview) {
          this.router.navigateByUrl(`/application/authorized-owner/review/${this.waiverId}`);
        } else {
          this.router.navigateByUrl(`/dashboard`);
          // this.reloadData();
        }
      }, err => {
        // todo: show errors;
      });
  }

  saveCustomProducts(): Observable<any>[] {
    const saveList: Observable<any>[] = [];
    const products: any[] = [...this.form.value.ownProducts, ...this.form.value.productsForOthers];
    const existingProducts = products.filter(i => !!i.id);
    const newProducts = products.filter(i => !i.id);

    // save observables for updates
    existingProducts.forEach(p => {
      const save = this.dynamicsDataService.updateRecord('customProduct', p.id, p);
      saveList.push(save);
    });

    // save observables for creates
    newProducts.forEach(p => {
      const save = this.dynamicsDataService.createRecord('customProduct', p);
      saveList.push(save);
    });

    // save observables for deletes
    this.deletedProducts.forEach(p => {
      const save = this.dynamicsDataService.deleteRecord('customProduct', p.id);
      saveList.push(save);
    });

    return saveList;
  }

  addOwner(owner: any) {
    // set dialogConfig settings
    const dialogConfig: any = {
      disableClose: true,
      autoFocus: true,
      width: '470px',
      data: { owner }
    };


    // open dialog, get reference and process returned data from dialog
    const dialogRef = this.dialog.open(SellerOwnerDialogComponent, dialogConfig);
    dialogRef.afterClosed().subscribe(
      formData => {
        if (formData) {
          let save = this.dynamicsDataService.createRecord('contact', formData);
          if (formData.id) {
            save = this.dynamicsDataService.updateRecord('contact', formData.id, formData);
          }
          this.busy = save.subscribe(
            res => {
              // this.snackBar.open('Shareholder Details have been saved', 'Success', { duration: 2500, panelClass: ['green-snackbar'] });
              this.getOwnersAndManagers();
            },
            err => {
              // this.snackBar.open('Error saving Shareholder Details', 'Fail', { duration: 3500, panelClass: ['red-snackbar'] });
              // this.handleError(err);
            }
          );
        }
      }
    );
  }

  getOwnersAndManagers() {

  }

}

@Component({
  selector: 'app-seller-owner-dialog',
  templateUrl: './owner-dialog.html',
})
export class SellerOwnerDialogComponent implements OnInit {
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<SellerOwnerDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { owner: DynamicsContact }) { }

  ngOnInit(): void {
    this.form = this.fb.group({
      id: [],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      title: [''],
      phoneNumber: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  save() {
    // console.log('shareholderForm', this.shareholderForm.value, this.shareholderForm.valid);
    if (!this.form.valid) {
      Object.keys(this.form.controls).forEach(field => {
        const control = this.form.get(field);
        control.markAsTouched({ onlySelf: true });
      });
    } else {
      let formData = this.data.owner || {};
      formData = (<any>Object).assign(formData, this.form.value);
      this.dialogRef.close(formData);
    }
  }

  close() {
    this.dialogRef.close();
  }

}
