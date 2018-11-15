import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { User } from '../../models/user.model';
import { UserDataService } from '../../services/user-data.service';
import { AccountDataService } from '../../services/account-data.service';
import { DynamicsAccount } from '../../models/dynamics-account.model';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-profile-summary',
  templateUrl: './profile-summary.component.html',
  styleUrls: ['./profile-summary.component.scss']
})
export class ProfileSummaryComponent implements OnInit {
  busy: Subscription;
  dataLoaded: boolean;
  account: DynamicsAccount;
  businessInfoData: any[];
  businessAddressData: { label: string; value: string; }[];
  primaryContactData: { label: string; value: string; }[];
  additionalContactData: { label: string; value: string; }[];

  constructor(private userDataService: UserDataService,
    private sanitizer: DomSanitizer,
    private accountDataService: AccountDataService) { }

  ngOnInit() {
    this.reloadUser();
  }

  reloadUser() {
    this.busy = this.userDataService.getCurrentUser()
      .subscribe((data: User) => {
        this.dataLoaded = true;
        if (data && data.accountid) {
          this.busy =
            this.accountDataService.getAccount(data.accountid)
              .subscribe(res => {
                this.account = res;
                this.setSummaryTables();
              });
        }
      });
  }

  setSummaryTables() {
    this.businessInfoData = [
      { label: 'Business - Legal Name:', value: this.account.businessLegalname },
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
            <div>${this.account.physicalAddress.streetLine1 || ''}</div>
            <div>${this.account.physicalAddress.streetLine2 || ''}</div>
            <div>${this.account.physicalAddress.city || ''}</div>
            <div>${this.account.physicalAddress.province || ''}</div>
            <div>${this.account.physicalAddress.postalCode || ''}</div>
            <div>${this.account.physicalAddress.country || ''}</div>
          `)
      },
      {
        label: 'Mailing Address:',
        value: <any>this.sanitizer.bypassSecurityTrustHtml(`
            <div>${this.account.mailingAddress.streetLine1 || ''}</div>
            <div>${this.account.mailingAddress.streetLine2 || ''}</div>
            <div>${this.account.mailingAddress.city || ''}</div>
            <div>${this.account.mailingAddress.province || ''}</div>
            <div>${this.account.mailingAddress.postalCode || ''}</div>
            <div>${this.account.mailingAddress.country || ''}</div>
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

}
