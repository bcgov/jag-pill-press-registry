import { Component, OnInit } from '@angular/core';
import { FormGroup, FormArray, FormBuilder, Validators } from '@angular/forms';
import { Subscription, Observable, zip } from 'rxjs';
import { PRODUCING_OWN_PRODUCT, MANUFACTURING_FOR_OTHERS } from '../../waiver/waiver-application/waiver-application.component';
import { ActivatedRoute, Router } from '@angular/router';
import { DynamicsDataService } from '../../../services/dynamics-data.service';
import { ApplicationDataService } from '../../../services/adoxio-application-data.service';
import { FormBase } from './../../../shared/form-base';

import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import * as _moment from 'moment';
// tslint:disable-next-line:no-duplicate-imports
import { defaultFormat as _rollupMoment } from 'moment';
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
  selector: 'app-authorized-application',
  templateUrl: './authorized-application.component.html',
  styleUrls: ['./authorized-application.component.scss'],
  providers: [
    // `MomentDateAdapter` can be automatically provided by importing `MomentDateModule` in your
    // application's root module. We provide it at the component level here, due to limitations of
    // our example generation script.
    { provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE] },

    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
  ],
})
export class AuthorizedApplicationComponent extends FormBase implements OnInit {
  form: FormGroup;
  busy: Subscription;
  waiverId: string;

  deletedProducts: any[] = [];
  PRODUCING_OWN_PRODUCT = PRODUCING_OWN_PRODUCT;
  MANUFACTURING_FOR_OTHERS = MANUFACTURING_FOR_OTHERS;

  get ownProducts(): FormArray {
    return <FormArray>this.form.get('ownProducts');
  }
  get productsForOthers(): FormArray {
    return <FormArray>this.form.get('productsForOthers');
  }

  constructor(private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private dynamicsDataService: DynamicsDataService,
    private applicationDataService: ApplicationDataService) {
    super();
    this.waiverId = this.route.snapshot.params.id;
  }

  ngOnInit() {
    this.form = this.fb.group({
      id: [],
      borrowrentleaseequipment: ['', Validators.required],
      currentlyownusepossessequipment: ['', Validators.required],
      declarationOfCorrectInformation: ['', Validators.required],
      foippaconsent: ['', Validators.required],
      intendtopurchaseequipment: [''],
      ownProducts: this.fb.array([this.createCustomProduct(<CustomProduct>{ purpose: PRODUCING_OWN_PRODUCT })]),
      ownintendtoownequipmentforbusinessuse: ['', Validators.required],
      producingownproduct: ['', Validators.required],
      productsForOthers: this.fb.array([this.createCustomProduct(<CustomProduct>{ purpose: MANUFACTURING_FOR_OTHERS })]),
      providingmanufacturingtoothers: ['', Validators.required],
      sellequipment: ['', Validators.required],
      foodanddrugact: [''],
      legislativeauthorityother: ['', this.requiredCheckboxChildValidator('legislativeauthorityothercheck')],
      kindsofproductsdrugs: [''],
      kindsofproductsnaturalhealthproducts: [''],
      kindsofproductsother: ['', this.requiredCheckboxChildValidator('kindsofproductsothercheck')],
      drugestablishmentlicence: ['', Validators.required],
      sitelicence: [''],
      otherlicence: ['', this.requiredCheckboxChildValidator('otherlicencecheck')],
      delbusinessname: ['', this.requiredCheckboxChildValidator('drugestablishmentlicence')],
      drugestablishmentlicencenumber: ['', this.requiredCheckboxChildValidator('drugestablishmentlicence')],
      drugestablishmentlicenceexpirydate: [''],
      sitelicencebusinessname: ['', this.requiredCheckboxChildValidator('sitelicence')],
      sitelicencenumber: ['', this.requiredCheckboxChildValidator('sitelicence')],
      sitelicenceexpirydate: [''],
      otherlicencebusinessname: ['', this.requiredCheckboxChildValidator('otherlicencecheck')],
      otherlicencenumber: ['', this.requiredCheckboxChildValidator('otherlicencecheck')],
      otherlicenceexpirydate: [''],
      legislativeauthorityothercheck: [''],
      kindsofproductsothercheck: [''],
      otherlicencecheck: [''],

    });

    this.form.get('producingownproduct').valueChanges
      .subscribe(value => {
        if (value === false) {
          while (this.ownProducts.controls.length > 0) {
            this.deleteCustomProduct(0, this.ownProducts.controls[0].value.purpose);
          }
        } else {
          this.addCustomProduct(<CustomProduct>{ purpose: PRODUCING_OWN_PRODUCT });
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
    this.clearHiddenFields();
  }

  reloadData() {
    this.applicationDataService.getApplicationById(this.waiverId).subscribe(data => {
      this.form.patchValue(data);

      // process custom products
      data.customProducts = data.customProducts || [];
      this.clearCustomProducts();
      const ownProducts = data.customProducts.filter(p => p.purpose === PRODUCING_OWN_PRODUCT);
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

  clearHiddenFields() {
    this.form.get('currentlyownusepossessequipment').valueChanges
      .filter(value => value)
      .subscribe((value) => {
        if (value) {
          this.form.get('intendtopurchaseequipment').clearValidators();
          this.form.get('intendtopurchaseequipment').reset();
        } else {
          this.form.get('intendtopurchaseequipment').setValidators([Validators.required]);
        }
      });
    this.form.get('producingownproduct').valueChanges
      .filter(value => !value)
      .subscribe(() => {
        while (this.ownProducts.controls.length > 0) {
          this.ownProducts.removeAt(0);
        }
      });
    this.form.get('providingmanufacturingtoothers').valueChanges
      .filter(value => !value)
      .subscribe(() => {
        while (this.productsForOthers.controls.length > 0) {
          this.productsForOthers.removeAt(0);
        }
      });

    this.form.get('legislativeauthorityothercheck').valueChanges
      .filter(value => !value)
      .subscribe(() => {
        this.form.get('legislativeauthorityother').reset();
      });
    this.form.get('kindsofproductsothercheck').valueChanges
      .filter(value => !value)
      .subscribe(() => {
        this.form.get('kindsofproductsother').reset();
      });

    this.form.get('drugestablishmentlicence').valueChanges
      .filter(value => !value)
      .subscribe(() => {
        this.form.get('delbusinessname').reset();
        this.form.get('drugestablishmentlicencenumber').reset();
        this.form.get('drugestablishmentlicenceexpirydate').reset();
      });
    this.form.get('sitelicence').valueChanges
      .filter(value => !value)
      .subscribe(() => {
        this.form.get('sitelicencebusinessname').reset();
        this.form.get('sitelicencenumber').reset();
        this.form.get('sitelicenceexpirydate').reset();
      });
    this.form.get('otherlicencecheck').valueChanges
      .filter(value => !value)
      .subscribe(() => {
        this.form.get('otherlicence').reset();
        this.form.get('otherlicencebusinessname').reset();
        this.form.get('otherlicencenumber').reset();
        this.form.get('otherlicenceexpirydate').reset();
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
    let validator = this.requiredCheckboxChildValidator('producingownproduct');
    if (product.purpose === MANUFACTURING_FOR_OTHERS) {
      validator = this.requiredCheckboxChildValidator('providingmanufacturingtoothers');
    }
    return this.fb.group({
      id: [product.id],
      purpose: [product.purpose],
      incidentId: [this.waiverId],
      productdescriptionandintendeduse: [product.productdescriptionandintendeduse, validator]
    });
  }

  isTypeOfAuthorizationValid() {
    const valid = this.form.get('kindsofproductsdrugs').value === true
      || this.form.get('kindsofproductsnaturalhealthproducts').value === true
      || this.form.get('kindsofproductsothercheck').value === true
      || !(this.form.get('kindsofproductsdrugs').touched
        || this.form.get('kindsofproductsnaturalhealthproducts').touched
        || this.form.get('kindsofproductsothercheck').touched);
    return valid;
  }

  isLegislativeAuthorityValid() {
    const valid = this.form.get('foodanddrugact').value === true
      || this.form.get('legislativeauthorityothercheck').value === true
      || !(this.form.get('foodanddrugact').touched
        || this.form.get('legislativeauthorityothercheck').touched);
    return valid;
  }

  isLicenceTypeValid() {
    const valid = this.form.get('drugestablishmentlicence').value === true
      || this.form.get('sitelicence').value === true
      || this.form.get('otherlicencecheck').value === true
      || !(this.form.get('drugestablishmentlicence').touched
        || this.form.get('sitelicence').touched
        || this.form.get('otherlicencecheck').touched);
    return valid;
  }

  addCustomProduct(product: CustomProduct) {
    if (product.purpose === PRODUCING_OWN_PRODUCT) {
      const control = this.createCustomProduct(product);
      this.ownProducts.push(control);
    } else if (product.purpose === MANUFACTURING_FOR_OTHERS) {
      const control = this.createCustomProduct(product);
      this.productsForOthers.push(control);
    }

  }

  deleteCustomProduct(index: number, type: string) {
    if (type === PRODUCING_OWN_PRODUCT) {
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

  save(goToReview: boolean) {
    this.form.updateValueAndValidity();
    if (this.form.valid || goToReview === false) {
      const value = { ...this.form.value };
      const saveList = [this.applicationDataService.updateApplication(value), ...this.saveCustomProducts()];
      zip(...saveList)
        .subscribe(res => {
          if (goToReview) {
            this.router.navigateByUrl(`/authorized-owner/review/${this.waiverId}`);
          } else {
            this.router.navigateByUrl(`/dashboard`);
            // this.reloadData();
          }
        }, err => {
          // todo: show errors;
        });
    }
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

}

