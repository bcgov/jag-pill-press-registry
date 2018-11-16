import { Component, OnInit } from '@angular/core';
import { UserDataService } from '../../services/user-data.service';
import { User } from '../../models/user.model';
import { ContactDataService } from '../../services/contact-data.service';
import { DynamicsContact } from '../../models/dynamics-contact.model';
import { AppState } from '../../app-state/models/app-state';
import * as CurrentUserActions from '../../app-state/actions/current-user.action';
import { Store } from '@ngrx/store';
import { Subscription } from 'rxjs/Subscription';
import { FormBuilder, FormGroup, Validators, FormArray, ValidatorFn, AbstractControl, FormControl } from '@angular/forms';
import { AliasDataService } from '../../services/alias-data.service';
import { PreviousAddressDataService } from '../../services/previous-address-data.service';
import { Observable, Subject } from 'rxjs';
import { WorkerDataService } from '../../services/worker-data.service.';
import { Alias } from '../../models/alias.model';
import { PreviousAddress } from '../../models/previous-address.model';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin } from 'rxjs/observable/forkJoin';
import { COUNTRIES } from './country-list';

import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import * as _moment from 'moment';
// tslint:disable-next-line:no-duplicate-imports
import { defaultFormat as _rollupMoment } from 'moment';
import { zip } from 'rxjs/operators';
import { AccountDataService } from '../../services/account-data.service';
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


const postalRegex = '(^\\d{5}([\-]\\d{4})?$)|(^[A-Za-z][0-9][A-Za-z]\\s?[0-9][A-Za-z][0-9]$)';

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
export class BusinessProfileComponent implements OnInit {
  currentUser: User;
  dataLoaded = false;
  busy: Subscription;
  busy2: Promise<any>;
  form: FormGroup;
  countryList = COUNTRIES;

  accountId: string;
  saveFormData: any;
  _mailingDifferentFromPhysicalAddress: boolean;
  _showAdditionalAddress: boolean;


  public get contacts(): FormArray {
    return this.form.get('otherContacts') as FormArray;
  }

  constructor(private userDataService: UserDataService,
    private store: Store<AppState>,
    private accountDataService: AccountDataService,
    private previousAddressDataService: PreviousAddressDataService,
    private contactDataService: ContactDataService,
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,

  ) {
    // this.route.params.subscribe(params => {
    //   this.accountId = params.id;
    // });
  }

  ngOnInit() {
    this.form = this.fb.group({
      businessProfile: this.fb.group({
        id: [''],
        businessLegalname: [{ value: '', disabled: true }],
        businessDBAName: [{ value: '', disabled: true }],
        businessNumber: ['', Validators.required],
        businessType: ['', Validators.required],
        businessPhoneNumber: ['', Validators.required],
        businessEmail: ['', [Validators.required, Validators.email]],
        websiteAddress: [''],
      }),
      physicalAddress: this.fb.group({
        id: [],
        streetLine1: ['', Validators.required],
        streetLine2: [''],
        city: ['', Validators.required],
        postalCode: ['', Validators.required],
        province: [{ value: 'British Columbia', disabled: true }],
        country: [{ value: 'Canada', disabled: true }],
      }),
      mailingAddress: this.fb.group({
        id: [],
        streetLine1: ['', Validators.required],
        streetLine2: [''],
        city: ['', Validators.required],
        postalCode: ['', Validators.required],
        province: ['British Columbia', Validators.required],
        country: ['Canada', Validators.required],
      }),
      primaryContact: this.fb.group({
        id: [],
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        title: [''],
        phoneNumber: ['', Validators.required],
        phoneNumberAlt: [''],
        email: ['', [Validators.required, Validators.email]],
      }),
      additionalContact: this.fb.group({
        id: [],
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        title: [''],
        phoneNumber: ['', Validators.required],
        phoneNumberAlt: [''],
        email: ['', [Validators.required, Validators.email]],
      }),
    });
    this.reloadUser();
  }

  reloadUser() {
    this.busy = this.userDataService.getCurrentUser()
      .subscribe((data: User) => {
        this.currentUser = data;

        this.store.dispatch(new CurrentUserActions.SetCurrentUserAction(data));
        this.dataLoaded = true;
        if (this.currentUser && this.currentUser.accountid) {
          this.busy2 = forkJoin(
            this.accountDataService.getAccount(this.currentUser.accountid)
          ).toPromise().then(res => {
            const account = res[0];

            this.form.patchValue({
              businessProfile: account,
              primaryContact: account.primaryContact
            });



            this.saveFormData = this.form.value;
            // this.workerStatus = worker.status;
            // if (worker.status !== 'Application Incomplete') {
            //   this.form.disable();
            // }
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
      this.busy = this.contactDataService.createWorkerContact(contact).subscribe(res => {
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
    const value = { ...this.form.value };
    // Make sure the contact email and phone number are in sync with worker
    value.contact.emailaddress1 = value.worker.email;
    value.contact.telephone1 = value.worker.phonenumber;

    const saves = [
      // this.contactDataService.updateContact(value.contact),
      // this.workerDataService.updateWorker(value.worker, value.worker.id)
    ];


    this.busy2 = Observable.zip(...saves).toPromise().then(res => {
      subResult.next(true);
      this.reloadUser();
    }, err => subResult.next(false));

    return subResult;
  }


  // marking the form as touched makes the validation messages show
  markAsTouched() {
    this.form.markAsTouched();

    const workerControls = (<FormGroup>(this.form.get('worker'))).controls;
    for (const c in workerControls) {
      if (typeof (workerControls[c].markAsTouched) === 'function') {
        workerControls[c].markAsTouched();
      }
    }

    const contactControls = (<FormGroup>(this.form.get('contact'))).controls;
    for (const c in contactControls) {
      if (typeof (contactControls[c].markAsTouched) === 'function') {
        contactControls[c].markAsTouched();
      }
    }

    (<FormGroup[]>this.contacts.controls).forEach(address => {
      for (const c in address.controls) {
        if (typeof (address.controls[c].markAsTouched) === 'function') {
          address.controls[c].markAsTouched();
        }
      }
    });
  }

  rejectIfNotDigitOrBackSpace(event) {
    const acceptedKeys = ['Backspace', 'Tab', 'End', 'Home', 'ArrowLeft', 'ArrowRight', 'Control',
      '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'];
    if (acceptedKeys.indexOf(event.key) === -1) {
      event.preventDefault();
    }
  }

  customZipCodeValidator(pattern: RegExp, countryField: string): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      if (!control.parent) {
        return null;
      }
      const country = control.parent.get(countryField).value;
      if (country !== 'Canada' && country !== 'United States of America') {
        return null;
      }
      const valueMatchesPattern = pattern.test(control.value);
      return valueMatchesPattern ? null : { 'regex-missmatch': { value: control.value } };
    };
  }

  trimValue(control: FormControl) {
    const value = control.value;
    control.setValue('');
    control.setValue(value.trim());
  }
}
