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

  get ownProducts() {
    return <FormArray>this.form.get('ownProducts');
  }
  get productsForOthers() {
    return <FormArray>this.form.get('productsForOthers p');
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
      text: []
    });
  }

  addCustomProduct(type: string) {
    if (type === 'ownProducts') {
      const product = this.createCustomProduct({ type });
      (<FormArray>this.form.get('ownProducts')).push(product);
    } else {
      const product = this.createCustomProduct({ type });
      (<FormArray>this.form.get('productsForOthers')).push(product);
    }

  }

  deleteCustomProduct() {

  }

}
