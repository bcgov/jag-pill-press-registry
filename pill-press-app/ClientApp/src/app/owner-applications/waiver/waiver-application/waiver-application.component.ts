import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormArray, Validators } from '@angular/forms';
import { Subscription, Observable, zip } from 'rxjs';
import { ApplicationDataService } from '../../../services/adoxio-application-data.service';
import { ActivatedRoute, Router } from '@angular/router';
import { DynamicsDataService } from '../../../services/dynamics-data.service';

export const PRODUCTING_OWN_PRODUCT = 'Producing Own Product';
export const MANUFACTURING_FOR_OTHERS = 'Manufacturing For Others';
@Component({
  selector: 'app-waiver-application',
  templateUrl: './waiver-application.component.html',
  styleUrls: ['./waiver-application.component.scss']
})
export class WaiverApplicationComponent implements OnInit {
  form: FormGroup;
  busy: Subscription;
  waiverId: string;

  deletedProducts: any[] = [];
  PRODUCTING_OWN_PRODUCT = PRODUCTING_OWN_PRODUCT;
  MANUFACTURING_FOR_OTHERS = MANUFACTURING_FOR_OTHERS;

  get ownProducts(): FormArray {
    return <FormArray>this.form.get('ownProducts');
  }
  get productsForOthers(): FormArray {
    return <FormArray>this.form.get('productsForOthers');
  }
  constructor(private fb: FormBuilder,
    private route: ActivatedRoute,
    private dynamicsDataService: DynamicsDataService,
    private router: Router,
    private applicationDataService: ApplicationDataService) {
    this.waiverId = this.route.snapshot.params.id;
  }

  ngOnInit() {
    this.form = this.fb.group({
      id: [],
      borrowrentleaseequipment: ['', Validators.required],
      currentlyownusepossessequipment: ['', Validators.required],
      declarationofcorrectinformation: ['', Validators.required],
      foippaconsent: ['', Validators.required],
      intendtopurchaseequipment: ['', Validators.required],
      mainbusinessfocus: ['', Validators.required],
      manufacturingprocessdescription: ['', Validators.required],
      ownProducts: this.fb.array([this.createCustomProduct(<CustomProduct>{ purpose: PRODUCTING_OWN_PRODUCT })]),
      ownintendtoownequipmentforbusinessuse: ['', Validators.required],
      producingownproduct: ['', Validators.required],
      productsForOthers: this.fb.array([this.createCustomProduct(<CustomProduct>{ purpose: MANUFACTURING_FOR_OTHERS })]),
      providingmanufacturingtoothers: ['', Validators.required],
      sellequipment: ['', Validators.required],
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
      debugger;
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
          this.router.navigateByUrl(`/waiver-application-review/${this.waiverId}`);
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


  gotoReview() {
    this.router.navigate(['/waiver-application-review/' + this.waiverId]);    
  }

}
