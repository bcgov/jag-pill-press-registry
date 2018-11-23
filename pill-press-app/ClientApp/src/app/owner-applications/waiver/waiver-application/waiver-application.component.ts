import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormArray } from '@angular/forms';
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
      currentlyownusepossessequipment: [],
      intendtopurchaseequipment: [],
      ownintendtoownequipmentforbusinessuse: [],
      borrowrentleaseequipment: [],
      sellequipment: [],
      producingownproduct: [],
      ownProducts: this.fb.array([this.createCustomProduct({type: 'ownProducts'})]),
      providingmanufacturingtoothers: [],
      productsForOthers: this.fb.array([this.createCustomProduct({type: 'productsForOthers'})]),
      mainbusinessfocus: [],
      manufacturingprocessdescription: [],
      declarationofcorrectinformation: [],
      foippaconsent: [],
      bceid: [],
      bceiduserguid: [],
      bceidemail: [],
    });
  }

  createCustomProduct(product: any) {
    return this.fb.group({
      id: [],
      type: [product.type],
      purpose: []
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
