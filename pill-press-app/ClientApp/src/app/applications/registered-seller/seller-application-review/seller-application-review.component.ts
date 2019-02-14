import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormArray, FormBuilder, Validators } from '@angular/forms';
import { Subscription, Observable, zip } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { DynamicsDataService } from '../../../services/dynamics-data.service';
import { ApplicationDataService } from '../../../services/application-data.service';
import { faSave, faExclamationTriangle } from '@fortawesome/free-solid-svg-icons';

import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import * as _moment from 'moment';
// tslint:disable-next-line:no-duplicate-imports
import { defaultFormat as _rollupMoment } from 'moment';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material';
import { DynamicsContact } from '../../../models/dynamics-contact.model';
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
  selector: 'app-seller-application-review',
  templateUrl: './seller-application-review.component.html',
  styleUrls: ['./seller-application-review.component.scss']
})
export class SellerApplicationReviewComponent implements OnInit {
  form: FormGroup;
  formData: any;
  busy: Subscription;
  waiverId: string;

  faSave = faSave;
  faExclamationTriangle = faExclamationTriangle;

  ownersAndManagers: any[] = [];
  equipmentInformation: string[];
  purposeOfEquipment: { text: string; }[];
  sellerBusinessDetails: string[];

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
      declarationOfCorrectInformation: []
    });

    this.reloadData();
  }

  reloadData() {
    this.busy = this.applicationDataService.getApplicationById(this.waiverId).subscribe(application => {
      this.form.patchValue(application);
      this.formData = application;

      this.ownersAndManagers = application.businessContacts || [];

      // Equipment information list
      this.equipmentInformation = [
        'Do you currently own, use, or possess Controlled Equipment?',
        application.currentlyownusepossessequipment ? 'Yes' : 'No',
        'Do you intend on purchasing Controlled Equipment in the future?',
        application.intendtopurchaseequipment ? 'Yes' : 'No'
      ];

      this.purposeOfEquipment = [
        { text: 'Do you intend on owning, using, or possessing Controlled Equipment for the purposes of producing a product?' },
        { text: application.ownusepossesstoproduceaproduct ? 'Yes' : 'No' },
        {
          text: 'Do you intend on renting or leasing Controlled Equipment to others?'
        },
        { text: application.intendonrentingleasingtoothers ? 'Yes' : 'No' },
      ];


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
  }


  save(goToThankYouPage: boolean) {
    const value = this.form.value;
    if (goToThankYouPage) {
      value.statuscode = 'Pending';
      value.submittedDate = new Date();
    }
    if (goToThankYouPage && !this.form.get('declarationOfCorrectInformation').value) {
      return;
    }

    this.busy = this.applicationDataService.updateApplication(value)
      .subscribe(res => {
        if (goToThankYouPage) {
          this.router.navigateByUrl(`/registered-seller/thank-you/${this.waiverId}`);
        } else {
          this.router.navigateByUrl(`/dashboard`);
        }
      }, err => {
        // todo: show errors;
      });
  }


}
