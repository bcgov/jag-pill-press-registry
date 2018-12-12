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

  constructor(private paymentDataService: PaymentDataService,
    private userDataService: UserDataService, private router: Router,
    private dynamicsDataService: DynamicsDataService,
    private applicationDataService: ApplicationDataService,
    public snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.userDataService.getCurrentUser()
      .subscribe((data) => {
        this.currentUser = data;
        if (this.currentUser.accountid != null) {
          // fetch the account to get the primary contact.
          this.dynamicsDataService.getRecord('account', this.currentUser.accountid)
            .subscribe((result: DynamicsAccount) => {
              this.account = result;
              if (result.primaryContact) {
                this.contactId = result.primaryContact.id;
              }
            });
        }
      });

      this.applicationDataService.getApplications()
      .subscribe(data => {
        debugger;
      });
  }

  applyForWaiver() {

  }

  startNewWaiverApplication() {
    const newLicenceApplicationData: Application = new Application();
    this.busy = this.applicationDataService.createApplication(newLicenceApplicationData, 'Waiver').subscribe(
      data => {
        this.router.navigateByUrl(`/application/profile-review/waiver/${data.id}`);
      },
      err => {
        this.snackBar.open('Error starting a New Licence Application', 'Fail', { duration: 3500, panelClass: ['red-snackbar'] });
        console.log('Error starting a New Licence Application');
      }
    );
  }

  startNewAuthorizedOwnerApplication() {
    const newLicenceApplicationData: Application = new Application();
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
    const newLicenceApplicationData: Application = new Application();
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

  addEquipment() {
    const newLicenceApplicationData: Application = new Application();
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
