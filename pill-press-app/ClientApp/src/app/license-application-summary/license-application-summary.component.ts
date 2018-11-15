import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatPaginator, MatTableDataSource, MatSort } from '@angular/material';
import { ApplicationDataService } from '../services/adoxio-application-data.service';
import { AdoxioLicenseDataService } from '../services/adoxio-license-data.service';
import { LicenseApplicationSummary } from '../models/license-application-summary.model';
import { Application } from '../models/adoxio-application.model';
import { AdoxioLicense } from '../models/adoxio-license.model';
import { Observable, Subscription } from 'rxjs';
import { PaymentDataService } from '../services/payment-data.service';

const ACTIVE = 'Active';
const PAYMENT_REQUIRED = 'Payment Required';
const RENEWAL_DUE = 'Renewal Due';

@Component({
  selector: 'app-license-application-summary',
  templateUrl: './license-application-summary.component.html',
  styleUrls: ['./license-application-summary.component.css']
})
export class LicenseApplicationSummaryComponent implements OnInit {
  readonly ACTIVE = ACTIVE;
  readonly PAYMENT_REQUIRED = PAYMENT_REQUIRED;
  readonly RENEWAL_DUE = RENEWAL_DUE;
  displayedColumns = ['modifiedOn', 'establishmentName', 'status', 'actions'];
  dataSource = new MatTableDataSource<LicenseApplicationSummary>();

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  busy: Subscription;

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // MatTableDataSource defaults to lowercase matches
    this.dataSource.filter = filterValue;
  }

  constructor(private ApplicationDataService: ApplicationDataService,
    private adoxioLicenseDataService: AdoxioLicenseDataService,
    private paymentService: PaymentDataService,
    private route: ActivatedRoute) {
  }

  ngOnInit() {
    const licenseApplicationSummary: LicenseApplicationSummary[] = [];

    this.busy = this.ApplicationDataService.getApplications()
      .subscribe(applications => {
        applications.forEach((entry) => {
          if (entry.assignedLicence) {
            const licAppSum = new LicenseApplicationSummary();
            licAppSum.id = entry.id;
            licAppSum.assignedLicence = entry.assignedLicence;
            licAppSum.name = entry.assignedLicence.licenseNumber;
            licAppSum.establishmentName = entry.establishmentName;
            licAppSum.establishmentAddress = entry.establishmentAddress;
            licAppSum.licenseType = entry.licenseType;
            licAppSum.modifiedon = entry.modifiedon;
            licAppSum.status = this.getLicenceStatus(entry);
            licAppSum.licenseNumber = entry.assignedLicence.licenseNumber;
            licenseApplicationSummary.push(licAppSum);
          }
        });

        this.dataSource.data = licenseApplicationSummary;
        setTimeout(() => {
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
        });
      });
  }

  getLicenceStatus(application: Application): string {
    let status = ACTIVE;
    if (application.licenceFeeInvoicePaid !== true) {
      status = PAYMENT_REQUIRED;
    }
    if (application.assignedLicence && (new Date() > new Date(application.assignedLicence.expiryDate))) {
      status = RENEWAL_DUE;
    }

    return status;
  }

  downloadLicence(application) {

  }

  payLicenceFee(application) {
    this.busy = this.paymentService.getInvoiceFeePaymentSubmissionUrl(application.id).subscribe(res => {
      const data = res.json();
      window.location.href = data.url;
    });
  }

  renewLicence(application) {

  }

}
