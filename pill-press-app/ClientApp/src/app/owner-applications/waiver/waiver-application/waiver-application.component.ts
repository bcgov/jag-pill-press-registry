import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormArray, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-waiver-application',
  templateUrl: './waiver-application.component.html',
  styleUrls: ['./waiver-application.component.scss']
})
export class WaiverApplicationComponent implements OnInit {
  form: FormGroup;
  busy: Subscription;

  get ownProducts(): FormArray {
    return <FormArray>this.form.get('ownProducts');
  }
  get productsForOthers(): FormArray {
    return <FormArray>this.form.get('productsForOthers');
  }
  constructor(private fb: FormBuilder) { }

  ngOnInit() {

    this.form = this.fb.group({
      id: [],
      currentlyownusepossessequipment: ['', Validators.required],
      intendtopurchaseequipment: ['', Validators.required],
      ownintendtoownequipmentforbusinessuse: ['', Validators.required],
      borrowrentleaseequipment: ['', Validators.required],
      sellequipment: ['', Validators.required],
      producingownproduct: ['', Validators.required],
      ownProducts: this.fb.array([this.createCustomProduct({ type: 'ownProducts' })]),
      providingmanufacturingtoothers: ['', Validators.required],
      productsForOthers: this.fb.array([this.createCustomProduct({ type: 'productsForOthers' })]),
      mainbusinessfocus: ['', Validators.required],
      manufacturingprocessdescription: ['', Validators.required],
      declarationofcorrectinformation: ['', Validators.required],
      foippaconsent: ['', Validators.required],
      bceid: ['', Validators.required],
      bceiduserguid: ['', Validators.required],
      bceidemail: ['', Validators.required],
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

  createCustomProduct(product: any) {
    return this.fb.group({
      id: [],
      type: [product.type],
      purpose: ['', Validators.required]
    });
  }

  addCustomProduct(type: string) {
    if (type === 'ownProducts') {
      const product = this.createCustomProduct({ type });
      this.ownProducts.push(product);
    } else {
      const product = this.createCustomProduct({ type });
      this.productsForOthers.push(product);
    }

  }

  deleteCustomProduct(index: number, type: string) {
    if (type === 'ownProducts') {
      this.ownProducts.removeAt(index);
    } else {
      this.productsForOthers.removeAt(index);
    }
  }

}
