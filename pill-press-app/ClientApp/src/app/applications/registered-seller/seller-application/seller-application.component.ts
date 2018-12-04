import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormArray, FormBuilder, Validators } from '@angular/forms';
import { Subscription, Observable, zip } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { DynamicsDataService } from '../../../services/dynamics-data.service';
import { ApplicationDataService } from '../../../services/adoxio-application-data.service';

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
  selector: 'app-seller-application',
  templateUrl: './seller-application.component.html',
  styleUrls: ['./seller-application.component.scss']
})
export class SellerApplicationComponent implements OnInit {
  form: FormGroup;
  busy: Subscription;
  waiverId: string;

  ownerList: any[] = [];

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

      additionalbusinessinformationaboutseller: ['', Validators.required],
      registeredsellerownermanager: [],
    });


    this.reloadData();
  }

  reloadData() {
    this.applicationDataService.getApplicationById(this.waiverId).subscribe(data => {
      this.form.patchValue(data);
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


  save(gotToReview: boolean) {
    const value = this.form.value;
    const saveList = [this.applicationDataService.updateApplication(value)];
    zip(...saveList)
      .subscribe(res => {
        if (gotToReview) {
          this.router.navigateByUrl(`/application/registered-seller/review/${this.waiverId}`);
        } else {
          this.router.navigateByUrl(`/dashboard`);
        }
      }, err => {
        // todo: show errors;
      });
  }

  addEditOwner(owner: any) {
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
          const i = this.ownerList.indexOf(formData);
          if (i === -1) {
            this.ownerList.push(formData);
          } else {
            this.ownerList[i] = formData;
          }
        }
      }
    );
  }

  isSellerTypeValid() {
    return this.form.get('manufacturerofcontrolledequipment').value === true
      || this.form.get('onetimesellerofowncontrolledequipment').value === true
      || this.form.get('retailerofcontrolledequipment').value === true
      || this.form.get('typeofsellerothercheck').value === true;
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
    const owner = this.data.owner;
    this.form = this.fb.group({
      id: [owner.id || null],
      firstName: [owner.firstName || '', Validators.required],
      lastName: [owner.lastName || '', Validators.required],
      title: [owner.title || ''],
      phoneNumber: [owner.phoneNumber || '', Validators.required],
      email: [owner.email || '', [Validators.required, Validators.email]],
      isOwner: [owner.isOwner, Validators.required],
    });
  }

  save() {
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
