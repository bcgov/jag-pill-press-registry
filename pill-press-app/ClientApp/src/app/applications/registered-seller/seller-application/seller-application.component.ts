import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup, FormArray, FormBuilder, Validators } from '@angular/forms';
import { Subscription, Observable, zip } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { DynamicsDataService } from '../../../services/dynamics-data.service';
import { ApplicationDataService } from '../../../services/application-data.service';

import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import * as _moment from 'moment';
// tslint:disable-next-line:no-duplicate-imports
import { defaultFormat as _rollupMoment } from 'moment';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material';
import { BusinessContact } from '../../../models/business-contact.model';
import { FormBase } from '../../../shared/form-base';
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
export class SellerApplicationComponent extends FormBase implements OnInit {

  constructor(private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog,
    private dynamicsDataService: DynamicsDataService,
    private applicationDataService: ApplicationDataService) {
    super();
    this.waiverId = this.route.snapshot.params.id;
  }
  form: FormGroup;
  busy: Subscription;
  busyPromise: Promise<any>;
  waiverId: string;

  ownersAndManagers: any[] = [];
  showErrorMessages: boolean;
  form2: FormGroup;

  currentOwner: any;

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

    this.form2 = this.fb.group({
      id: [null],
      jobTitle: [''],
      contactType: ['Additional'],
      registeredSellerOwnerManager: ['', Validators.required],
      contact: this.fb.group({
        id: [''],
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        phoneNumber: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
      })
    });

    this.reloadData();
    this.clearHiddenFields();
  }

  reloadData() {
    this.busy = this.applicationDataService.getApplicationById(this.waiverId).subscribe(data => {
      this.form.patchValue(data);
      this.ownersAndManagers = data.businessContacts || [];
    }, error => {
      // todo: show errors;
    });
  }

  clearHiddenFields() {
    this.form.get('currentlyownusepossessequipment').valueChanges
      .filter(value => value)
      .subscribe(() => {
        this.form.get('intendtopurchaseequipment').reset();
      });
    this.form.get('typeofsellerothercheck').valueChanges
      .filter(value => !value)
      .subscribe(() => {
        this.form.get('typeofsellerother').reset();
      });
    this.form.get('intendtosellothercheck').valueChanges
      .filter(value => !value)
      .subscribe(() => {
        this.form.get('intendtosellother').reset();
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

  markForm2AsTouched() {
    this.form2.markAsTouched();
    let controls = this.form2.controls;
    for (const c in controls) {
      if (typeof (controls[c].markAsTouched) === 'function') {
        controls[c].markAsTouched();
      }
    }

    controls = (<FormGroup>this.form2.get('contact')).controls;
    for (const c in controls) {
      if (typeof (controls[c].markAsTouched) === 'function') {
        controls[c].markAsTouched();
      }
    }
  }

  isFormValid() {
    return this.ownersAndManagers.length > 0
      && this.isSellerTypeValid()
      && this.isEquipmentTypeValid()
      && this.form.valid
      && (
        this.form.get('typeofsellerothercheck').value === false
        || this.form.get('typeofsellerother').value
      )
      && (
        this.form.get('intendtosellothercheck').value === false
        || this.form.get('intendtosellother').value
      );
  }

  save(goToReview: boolean) {
    this.showErrorMessages = true;
    if (this.isFormValid() || goToReview === false) {
      const value = this.form.value;
      value.businessContacts = this.ownersAndManagers;
      this.busy = this.applicationDataService.updateApplication(value)
        .subscribe(res => {
          if (goToReview) {
            this.router.navigateByUrl(`/registered-seller/review/${this.waiverId}`);
          } else {
            this.router.navigateByUrl(`/dashboard`);
          }
        }, err => {
          // todo: show errors;
        });
    }
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
          const i = this.ownersAndManagers.indexOf(formData);
          if (i === -1) {
            this.ownersAndManagers.push(formData);
          } else {
            this.ownersAndManagers[i] = formData;
          }
        }
      }
    );
  }

  addOwner(data, modal) {
    this.currentOwner = this.currentOwner || {};
    data = Object.assign(this.currentOwner, data);


    this.markForm2AsTouched();
    if (this.form2.valid && data) {
      const i = this.ownersAndManagers.indexOf(data);
      if (i === -1) {
        this.ownersAndManagers.push(data);
      } else {
        this.ownersAndManagers[i] = data;
      }
      modal.hide();
    }
  }

  deleteOwnerOrManager(owner: any) {
    const index = this.ownersAndManagers.indexOf(owner);
    this.ownersAndManagers.splice(index, 1);
  }

  isSellerTypeValid() {
    return this.form.get('manufacturerofcontrolledequipment').value === true
      || this.form.get('onetimesellerofowncontrolledequipment').value === true
      || this.form.get('retailerofcontrolledequipment').value === true
      || this.form.get('typeofsellerothercheck').value === true;
  }

  isEquipmentTypeValid() {
    return this.form.get('intendtosellpillpress').value === true
      || this.form.get('intendtosellencapsulator').value === true
      || this.form.get('intendtoselldiemouldorpunch').value === true
      || this.form.get('intendtosellpharmaceuticalmixerorblender').value === true
      || this.form.get('intendtosellothercheck').value === true;
  }

}

@Component({
  selector: 'app-seller-owner-dialog',
  templateUrl: './owner-dialog.html',
})
export class SellerOwnerDialogComponent extends FormBase implements OnInit {
  form: FormGroup;
  showErrors: boolean;

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<SellerOwnerDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { owner: BusinessContact }) {
    super();
  }

  ngOnInit(): void {
    const owner = this.data.owner;
    owner.contact = owner.contact || <any>{};
    this.form = this.fb.group({
      id: [owner.id || null],
      jobTitle: [owner.jobTitle || ''],
      contactType: ['Additional'],
      registeredSellerOwnerManager: [owner.registeredSellerOwnerManager || '', Validators.required],
      contact: this.fb.group({
        id: [owner.contact.id],
        firstName: [owner.contact.firstName || '', Validators.required],
        lastName: [owner.contact.lastName || '', Validators.required],
        phoneNumber: [owner.contact.phoneNumber || '', Validators.required],
        email: [owner.contact.email || '', [Validators.required, Validators.email]],
      })
    });
  }

  save() {
    this.showErrors = true;
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
