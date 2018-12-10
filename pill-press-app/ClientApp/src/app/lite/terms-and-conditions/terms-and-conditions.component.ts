import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-terms-and-conditions',
  templateUrl: './terms-and-conditions.component.html',
  styleUrls: ['./terms-and-conditions.component.scss']
})
export class TermsAndConditionsComponent implements OnInit {

  @Input() parentBusy: Promise<any>;
  childBusy: Promise<any>;
  @Output() termsAccepted = new EventEmitter<boolean>();
  window = window
  _termsAccepted: boolean;

  constructor() { }

  ngOnChanges() {
    this.childBusy = Promise.resolve(this.parentBusy);
  }

  ngOnInit() {
  }
  
}
