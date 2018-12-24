import { Component, OnInit } from '@angular/core';
import { UserDataService } from '../services/user-data.service';
import { User } from '../models/user.model';
import { Router } from '@angular/router';
import { Application } from '../models/application.model';
import { DynamicsDataService } from '../services/dynamics-data.service';
import { ApplicationDataService } from '../services/adoxio-application-data.service';
import { DynamicsAccount } from '../models/dynamics-account.model';
import { Subscription } from 'rxjs';
import { MatSnackBar } from '@angular/material';
import { PaymentDataService } from '../services/payment-data.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  public currentUser: User;
  applicationId: string;
  submittedApplications = 8;

  contactId: string = null;
  account: DynamicsAccount;
  busy: Subscription;
  isPaid: Boolean;
  orgType = '';

  inProgressEquipment: Application[] = [];
  completedEquipment: Application[] = [];
  waiverApplication: Application;
  authorizedOwnerApplication: Application;
  registeredSellerApplication: Application;

  constructor(private paymentDataService: PaymentDataService,
    private userDataService: UserDataService, private router: Router,
    private dynamicsDataService: DynamicsDataService,
    private applicationDataService: ApplicationDataService,
    public snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.busy = this.userDataService.getCurrentUser()
      .subscribe((data) => {
        this.currentUser = data;
        if (this.currentUser.accountid != null) {
          // fetch the account to get the primary contact.
          this.busy = this.dynamicsDataService.getRecord('account', this.currentUser.accountid)
            .subscribe((result: DynamicsAccount) => {
              this.account = result;
              if (result.primaryContact) {
                this.contactId = result.primaryContact.id;
              }
            });
        }
      });

    this.applicationDataService.getApplications()
      .subscribe((data: Application[]) => {
        const authorizedOwners = data.filter(a => a.applicationtype === 'Authorized Owner');
        if (authorizedOwners.length > 0) {
          this.authorizedOwnerApplication = authorizedOwners[0];
        }

        const sellers = data.filter(a => a.applicationtype === 'Registered Seller');
        if (sellers.length > 0) {
          this.registeredSellerApplication = sellers[0];
        }

        const waivers = data.filter(a => a.applicationtype === 'Waiver');
        if (waivers.length > 0) {
          this.waiverApplication = waivers[0];
        }

        this.inProgressEquipment = data.filter(a => a.applicationtype === 'Equipment Notification' && a.statuscode !== 'Approved');
        this.completedEquipment = data.filter(a => a.applicationtype === 'Equipment Notification' && a.statuscode === 'Approved');
      });
  }

  isAuthorizedApplicationPending() {
    return this.authorizedOwnerApplication
      && this.authorizedOwnerApplication.statuscode !== 'Draft'
      && this.authorizedOwnerApplication.statuscode !== 'Withdrawn'
      && this.authorizedOwnerApplication.statuscode !== 'Approved'
      && this.authorizedOwnerApplication.statuscode !== 'Cancelled'
      && this.authorizedOwnerApplication.statuscode !== 'Denied';
  }

  isWaiverOrSellerUnderReview(statuscode: string) {
    return statuscode
      && (
        statuscode === 'Under Review'
        || statuscode === 'With Risk Assessment'
        || statuscode === 'With C&E Investigations'
        || statuscode === 'With Deputy Registrar'
      );
  }

  startNewWaiverApplication() {
    const newLicenceApplicationData: Application = <Application>{
      statuscode: 'Draft'
    };
    this.busy = this.applicationDataService.createApplication(newLicenceApplicationData, 'Waiver').subscribe(
      data => {
        this.router.navigateByUrl(`/waiver/profile-review/${data.id}`);
      },
      err => {
        this.snackBar.open('Error starting a New Licence Application', 'Fail', { duration: 3500, panelClass: ['red-snackbar'] });
        console.log('Error starting a New Licence Application');
      }
    );
  }

  startNewAuthorizedOwnerApplication() {
    const newLicenceApplicationData: Application = <Application>{
      statuscode: 'Draft'
    };
    this.busy = this.applicationDataService.createApplication(newLicenceApplicationData, 'Authorized Owner').subscribe(
      data => {
        this.router.navigateByUrl(`/authorized-owner/profile-review/${data.id}`);
      },
      err => {
        this.snackBar.open('Error starting a New Authorized Owner Application', 'Fail', { duration: 3500, panelClass: ['red-snackbar'] });
        console.log('Error starting a New Authorized Owner Application');
      }
    );
  }

  startNewASellerApplication() {
    const newLicenceApplicationData: Application = <Application>{
      statuscode: 'Draft'
    };
    this.busy = this.applicationDataService.createApplication(newLicenceApplicationData, 'Registered Seller').subscribe(
      data => {
        this.router.navigateByUrl(`/registered-seller/profile-review/${data.id}`);
      },
      err => {
        this.snackBar.open('Error starting a New Registered Seller Application', 'Fail', { duration: 3500, panelClass: ['red-snackbar'] });
        console.log('Error starting a Registered Seller Application');
      }
    );
  }

  showEquipmentTables() {
    const show = (this.authorizedOwnerApplication && this.authorizedOwnerApplication.statuscode === 'Approved')
      || (this.waiverApplication && this.waiverApplication.statuscode === 'Approved')
      || (this.registeredSellerApplication && this.registeredSellerApplication.statuscode === 'Approved');
    return show;
  }

  addEquipment() {
    const newLicenceApplicationData: Application = <Application>{
      statuscode: 'Draft'
    };
    this.busy = this.applicationDataService.createApplication(newLicenceApplicationData, 'Equipment Notification').subscribe(
      data => {
        this.router.navigateByUrl(`/equipment-notification/profile-review/${data.id}`);
      },
      err => {
        this.snackBar.open('Error starting a New Equipment Notificatio Application',
          'Fail', { duration: 3500, panelClass: ['red-snackbar'] });
        console.log('Error starting a Registered Seller Application');
      }
    );
  }

}
