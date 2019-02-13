import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { User } from '../../models/user.model';
import { UserDataService } from '../../services/user-data.service';
import { AccountDataService } from '../../services/account-data.service';
import { DynamicsAccount } from '../../models/dynamics-account.model';
import { DomSanitizer } from '@angular/platform-browser';
import { ValidatorFn, AbstractControl, FormBuilder, FormGroup } from '@angular/forms';
import { Router, Route, ActivatedRouteSnapshot, ActivatedRoute } from '@angular/router';

import { faExclamation, faPencilAlt, faQuestion, faEnvelope } from '@fortawesome/free-solid-svg-icons';
@Component({
  selector: 'app-profile-review',
  templateUrl: './profile-review.component.html',
  styleUrls: ['./profile-review.component.scss']
})
export class ProfileReviewComponent implements OnInit {
  busy: Promise<any>;
  busy2: Promise<any>;
  dataLoaded: boolean;
  account: DynamicsAccount;
  businessInfoData: any[];
  businessAddressData: { label: string; value: string; }[];
  primaryContactData: { label: string; value: string; }[];
  additionalContactData: { label: string; value: string; }[];
  form: FormGroup;
  mode: string;
  id: string;
  nextRoute: string;

  faExclamation = faExclamation;
  faPencilAlt = faPencilAlt;
  faQuestion = faQuestion;
  faEnvelope = faEnvelope;


  constructor(private userDataService: UserDataService,
    private sanitizer: DomSanitizer,
    public router: Router,
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private accountDataService: AccountDataService) {
    this.mode = this.route.snapshot.params.mode;
    this.id = this.route.snapshot.params.id;
    this.nextRoute = this.route.snapshot.data.nextRoute;

  }

  ngOnInit() {
    this.form = this.fb.group({
      id: [],
      declarationofcorrectinformation: []
    });
    this.reloadUser();
  }

  reloadUser() {
    this.busy = this.userDataService.getCurrentUser()
      .toPromise()
      .then((data: User) => {
        this.dataLoaded = true;
        if (data && data.accountid) {
          this.busy =
            this.accountDataService.getAccount(data.accountid)
              .toPromise()
              .then(res => {
                this.account = res;
                this.setSummaryTables();
                this.form.patchValue(res);
              });
        }
      });
  }

  setSummaryTables() {
    this.businessInfoData = [
      { label: 'Business - Legal Name:', value: this.account.businessLegalName },
      { label: 'Business - Doing Business As Name:', value: this.account.businessDBAName },
      { label: 'Business Number :', value: this.account.businessNumber },
      { label: 'Business Type:', value: this.account.businessType }
    ];

    this.account.physicalAddress = this.account.physicalAddress || <any>{};
    this.account.mailingAddress = this.account.mailingAddress || <any>{};
    this.businessAddressData = [
      {
        label: 'Physical Address:',
        value: <any>this.sanitizer.bypassSecurityTrustHtml(`
            <div>${this.account.physicalAddressLine1 || ''}</div>
            <div>${this.account.physicalAddressLine2 || ''}</div>
            <div>${this.account.physicalAddressCity || ''}</div>
            <div>${this.account.physicalAddressProvince || ''}</div>
            <div>${this.account.physicalAddressPostalCode || ''}</div>
            <div>${this.account.physicalAddressCountry || ''}</div>
          `)
      },
      {
        label: 'Mailing Address:',
        value: <any>this.sanitizer.bypassSecurityTrustHtml(`
            <div>${this.account.mailingAddressLine1 || ''}</div>
            <div>${this.account.mailingAddressLine2 || ''}</div>
            <div>${this.account.mailingAddressCity || ''}</div>
            <div>${this.account.mailingAddressProvince || ''}</div>
            <div>${this.account.mailingAddressPostalCode || ''}</div>
            <div>${this.account.mailingAddressCountry || ''}</div>
          `)
      },
      { label: 'Business Phone Number:', value: this.account.businessPhoneNumber },
      { label: 'Business Email:', value: this.account.businessEmail },
      {
        label: 'I authorize the Security Programs Division to use my email address to communicate with me:',
        value: 'Yes' // Todo: persist this value
      },
      { label: 'Website Address:', value: this.account.websiteAddress }
    ];

    this.account.primaryContact = this.account.primaryContact || <any>{};
    this.primaryContactData = [
      { label: 'First Name:', value: this.account.primaryContact.firstName },
      { label: 'Last Name:', value: this.account.primaryContact.lastName },
      { label: 'Title/ Position:', value: this.account.primaryContact.title },
      { label: 'Phone Number (main):', value: this.account.primaryContact.phoneNumber },
      { label: 'Phone Number (alternate):', value: this.account.primaryContact.phoneNumberAlt },
      { label: 'Email', value: this.account.primaryContact.email }
    ];

    this.account.additionalContact = this.account.additionalContact || <any>{};
    this.additionalContactData = [
      { label: 'First Name:', value: this.account.additionalContact.firstName },
      { label: 'Last Name:', value: this.account.additionalContact.lastName },
      { label: 'Title/ Position:', value: this.account.additionalContact.title },
      { label: 'Phone Number (main):', value: this.account.additionalContact.phoneNumber },
      { label: 'Phone Number (alternate):', value: this.account.additionalContact.phoneNumberAlt },
      { label: 'Email', value: this.account.additionalContact.email }
    ];
  }

  declarationsValid() {
    return this.form.get('declarationofcorrectinformation').value === true;
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

  isAdditionalContactPopulated() {
    return (this.account
      && this.account.additionalContact
      && (
      this.account.additionalContact.firstName 
      || this.account.additionalContact.lastName 
      || this.account.additionalContact.title
      || this.account.additionalContact.phoneNumber 
      || this.account.additionalContact.phoneNumberAlt 
      || this.account.additionalContact.email 
      )
    );
  }

  save() {
    if (!!(this.mode || this.declarationsValid())) {
      const value = <DynamicsAccount>this.form.value;
      value.primaryContact = this.account.primaryContact;
      value.additionalContact = this.account.additionalContact;
      if (!this.nextRoute) {
        value.submittedDate = new Date();
      }
      this.busy = this.accountDataService.updateAccount(value)
        .toPromise()
        .then(data => {
          if (this.nextRoute) {
            this.router.navigateByUrl(`${this.nextRoute}/${this.id}`);
          } else {
            this.router.navigateByUrl('/dashboard');
          }
        });
      this.busy2 = Promise.resolve(this.busy);
    }
  }

  customRequiredCheckboxValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      if (control.value === true) {
        return null;
      } else {
        return { 'shouldBeTrue': 'But value is false' };
      }
    };
  }

}
