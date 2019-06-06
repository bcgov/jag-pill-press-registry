import { Component, OnInit } from '@angular/core';
import { UserDataService } from '../../services/user-data.service';
import { User } from '../../models/user.model';
import { ContactDataService } from '../../services/contact-data.service';
import { DynamicsContact } from '../../models/dynamics-contact.model';
import { AppState } from '../../app-state/models/app-state';
import * as CurrentUserActions from '../../app-state/actions/current-user.action';
import { Store } from '@ngrx/store';
import { Subscription, Observable, Subject, forkJoin } from 'rxjs';
import { FormBuilder, FormGroup, Validators, FormArray, ValidatorFn, AbstractControl, FormControl } from '@angular/forms';
import { PreviousAddressDataService } from '../../services/previous-address-data.service';
import { ActivatedRoute, Router } from '@angular/router';
import { COUNTRIES } from './country-list';

import {
  faTrashAlt,
  faExclamationTriangle,
  faChevronRight,
  faAddressCard,
  faPhone,
  faEnvelope
} from '@fortawesome/free-solid-svg-icons';

import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import * as _moment from 'moment';
// tslint:disable-next-line:no-duplicate-imports
import { defaultFormat as _rollupMoment } from 'moment';
import { zip } from 'rxjs/operators';
import { AccountDataService } from '../../services/account-data.service';
import { DynamicsAccount } from '../../models/dynamics-account.model';
import { FormBase } from '../../shared/form-base';
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


export const postalRegex = '(^\\d{5}([\-]\\d{4})?$)|(^[A-Za-z][0-9][A-Za-z]\\s?[0-9][A-Za-z][0-9]$)';

@Component({
  selector: 'app-business-profile',
  templateUrl: './business-profile.component.html',
  styleUrls: ['./business-profile.component.scss'],
  providers: [
    // `MomentDateAdapter` can be automatically provided by importing `MomentDateModule` in your
    // application's root module. We provide it at the component level here, due to limitations of
    // our example generation script.
    { provide: DateAdapter, useClass: MomentDateAdapter, deps: [MAT_DATE_LOCALE] },

    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
  ],
})
export class BusinessProfileComponent extends FormBase implements OnInit {
  currentUser: User;
  dataLoaded = false;
  busy: Promise<any>;
  busy2: Promise<any>;
  busy3: Promise<any>;
  form: FormGroup;
  countryList = COUNTRIES;

  accountId: string;
  saveFormData: any;
  _showAdditionalAddress: boolean;
  _showAdditionalContact: boolean;

  faTrashAlt = faTrashAlt;
  faExclamationTriangle = faExclamationTriangle;
  faChevronRight = faChevronRight;
  faAddressCard = faAddressCard;
  faPhone = faPhone;
  faEnvelope = faEnvelope;


  public get contacts(): FormArray {
    return this.form.get('otherContacts') as FormArray;
  }

  constructor(private userDataService: UserDataService,
    private store: Store<AppState>,
    private accountDataService: AccountDataService,
    private contactDataService: ContactDataService,
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,

  ) {
    super();
  }

  ngOnInit() {
    this.form = this.fb.group({
      businessProfile: this.fb.group({
        id: [''],
        _mailingSameAsPhysicalAddress: [],
        businessLegalName: [{ value: '', disabled: true }],
        businessDBAName: [{ value: '', disabled: true }],
        businessNumber: ['', Validators.required],
        businessType: ['', Validators.required],
        businessPhoneNumber: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(10)]],
        businessEmail: ['', [Validators.required, Validators.email]],
        consentForEmailCommunication: [false, this.customRequiredCheckboxValidator()],
        websiteAddress: [''],
        physicalAddressLine1: ['', Validators.required],
        physicalAddressLine2: [''],
        physicalAddressCity: ['', Validators.required],
        physicalAddressPostalCode: ['', [Validators.required, Validators.pattern(postalRegex)]],
        physicalAddressProvince: [{ value: 'British Columbia', disabled: true }],
        physicalAddressCountry: [{ value: 'Canada', disabled: true }],
        mailingAddressLine1: ['', this.requiredCheckboxChildValidator('_mailingSameAsPhysicalAddress')],
        mailingAddressLine2: [''],
        mailingAddressCity: ['', this.requiredCheckboxChildValidator('_mailingSameAsPhysicalAddress')],
        mailingAddressPostalCode: ['', this.requiredCheckboxChildValidator('_mailingSameAsPhysicalAddress')],
        mailingAddressProvince: [''],
        mailingAddressCountry: ['Canada'],
      }),
      primaryContact: this.fb.group({
        id: [],
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        title: [''],
        phoneNumber: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(10)]],
        phoneNumberAlt: [''],
        email: ['', [Validators.required, Validators.email]],
      }),
      additionalContact: this.fb.group({
        id: [],
        firstName: [''],
        lastName: [''],
        title: [''],
        phoneNumber: [''],
        phoneNumberAlt: [''],
        email: [''],
      })
    });
    this.reloadUser();

    this.form.get('businessProfile._mailingSameAsPhysicalAddress').valueChanges
      .filter(value => value === true)
      .subscribe(value => {
        this.copyPhysicalToMailingAddress();
      });

    this.form.get('businessProfile.physicalAddressLine1').valueChanges
      .filter(v => this.form.get('businessProfile._mailingSameAsPhysicalAddress').value)
      .subscribe(value => {
        this.copyPhysicalToMailingAddress();
      });
    this.form.get('businessProfile.physicalAddressLine2').valueChanges
      .filter(v => this.form.get('businessProfile._mailingSameAsPhysicalAddress').value)
      .subscribe(value => {
        this.copyPhysicalToMailingAddress();
      });
    this.form.get('businessProfile.physicalAddressCity').valueChanges
      .filter(v => this.form.get('businessProfile._mailingSameAsPhysicalAddress').value)
      .subscribe(value => {
        this.copyPhysicalToMailingAddress();
      });
    this.form.get('businessProfile.physicalAddressPostalCode').valueChanges
      .filter(v => this.form.get('businessProfile._mailingSameAsPhysicalAddress').value)
      .subscribe(value => {
        this.copyPhysicalToMailingAddress();
      });
    this.form.get('businessProfile.physicalAddressProvince').valueChanges
      .filter(v => this.form.get('businessProfile._mailingSameAsPhysicalAddress').value)
      .subscribe(value => {
        this.copyPhysicalToMailingAddress();
      });
    this.form.get('businessProfile.physicalAddressCountry').valueChanges
      .filter(v => this.form.get('businessProfile._mailingSameAsPhysicalAddress').value)
      .subscribe(value => {
        this.copyPhysicalToMailingAddress();
      });

  }

  copyPhysicalToMailingAddress() {
    this.form.get('businessProfile.mailingAddressLine1').patchValue(this.form.get('businessProfile.physicalAddressLine1').value);
    this.form.get('businessProfile.mailingAddressLine2').patchValue(this.form.get('businessProfile.physicalAddressLine2').value);
    this.form.get('businessProfile.mailingAddressCity').patchValue(this.form.get('businessProfile.physicalAddressCity').value);
    this.form.get('businessProfile.mailingAddressPostalCode').patchValue(this.form.get('businessProfile.physicalAddressPostalCode').value);
    this.form.get('businessProfile.mailingAddressProvince').patchValue(this.form.get('businessProfile.physicalAddressProvince').value);
    this.form.get('businessProfile.mailingAddressCountry').patchValue(this.form.get('businessProfile.physicalAddressCountry').value);
  }

  hideAdditionalContact() {
    this._showAdditionalContact = false;
    const controls = (<FormGroup>this.form.get('additionalContact')).controls;
    // tslint:disable-next-line:forin
    for (const c in controls) {
      controls[c].clearValidators();
      controls[c].reset();
    }
  }

  reloadUser() {
    this.busy = this.userDataService.getCurrentUser()
      .toPromise()
      .then((data: User) => {
        this.currentUser = data;

        this.store.dispatch(new CurrentUserActions.SetCurrentUserAction(data));
        this.dataLoaded = true;
        if (this.currentUser && this.currentUser.accountid) {
          this.busy2 = forkJoin(
            this.accountDataService.getAccount(this.currentUser.accountid)
          ).toPromise().then(res => {
            const account: any = res[0];

            this.form.patchValue({
              businessProfile: account,
              primaryContact: account.primaryContact || {},
              additionalContact: account.additionalContact || {}
            });



            this.form.patchValue({
              businessProfile: account,
              primaryContact: account.primaryContact || {},
              additionalContact: account.additionalContact || {}
            });

            if (account.additionalContact && (
              account.additionalContact.email
              || account.additionalContact.firstName
              || account.additionalContact.lastName
              || account.additionalContact.phoneNumber
              || account.additionalContact.phoneNumberAlt
              || account.additionalContact.title)) {
              this._showAdditionalContact = true;
            }

            this.saveFormData = this.form.value;
          });
        }
      });
  }

  confirmContact(confirm: boolean) {
    if (confirm) {
      // create contact here
      const contact = new DynamicsContact();
      contact.firstName = this.currentUser.firstname;
      contact.lastName = this.currentUser.lastname;
      contact.email = this.currentUser.email;
      this.busy = this.contactDataService.createWorkerContact(contact)
        .toPromise()
        .then(res => {
          this.reloadUser();
        }, error => alert('Failed to create contact'));
    } else {
      window.location.href = 'logout';
    }
  }

  canDeactivate(): Observable<boolean> | boolean {
    if (// this.workerStatus !== 'Application Incomplete' ||
      JSON.stringify(this.saveFormData) === JSON.stringify(this.form.value)) {
      return true;
    } else {
      return this.save();
    }
  }

  save(): Subject<boolean> {
    const subResult = new Subject<boolean>();
    const value = <DynamicsAccount>{
      ...this.form.get('businessProfile').value,
      primaryContact: this.form.get('primaryContact').value,
      additionalContact: this.form.get('additionalContact').value
    };

    this.busy = this.accountDataService.updateAccount(value)
      .toPromise()
      .then(res => {
        subResult.next(true);
        this.reloadUser();
      }, err => subResult.next(false));
    this.busy3 = Promise.resolve(this.busy);

    return subResult;
  }

  gotoReview() {
    if (this.form.valid) {
      this.save().subscribe(data => {
        this.router.navigate(['/business-profile-review']);
      });
    } else {
      this.markAsTouched();
    }
  }

  // marking the form as touched makes the validation messages show
  markAsTouched() {
    this.form.markAsTouched();

    const businessProfileControls = (<FormGroup>(this.form.get('businessProfile'))).controls;
    for (const c in businessProfileControls) {
      if (typeof (businessProfileControls[c].markAsTouched) === 'function') {
        businessProfileControls[c].markAsTouched();
      }
    }

    const additionalContactControls = (<FormGroup>(this.form.get('additionalContact'))).controls;
    for (const c in additionalContactControls) {
      if (typeof (additionalContactControls[c].markAsTouched) === 'function') {
        additionalContactControls[c].markAsTouched();
      }
    }

    const primaryContactControls = (<FormGroup>(this.form.get('primaryContact'))).controls;
    for (const c in primaryContactControls) {
      if (typeof (primaryContactControls[c].markAsTouched) === 'function') {
        primaryContactControls[c].markAsTouched();
      }
    }

  }
}
