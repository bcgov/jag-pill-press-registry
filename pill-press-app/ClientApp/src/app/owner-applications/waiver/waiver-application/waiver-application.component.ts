import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-waiver-application',
  templateUrl: './waiver-application.component.html',
  styleUrls: ['./waiver-application.component.scss']
})
export class WaiverApplicationComponent implements OnInit {
  form: FormGroup;
  busy: Subscription;

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
      providingmanufacturingtoothers: [],
      mainbusinessfocus: [],
      manufacturingprocessdescription: [],
      declarationofcorrectinformation: [],
      foippaconsent: [],
      bceid: [],
      bceiduserguid: [],
      bceidemail: [],
    });
  }

}
