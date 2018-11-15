import { Component, OnInit, Input, ViewChild, Inject } from '@angular/core';
import { Subscription } from 'rxjs';
import { MatPaginator, MatTableDataSource, MatSort, MatDialog, MatDialogConfig, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ApplicationDataService } from '../services/adoxio-application-data.service';
import { LicenseApplicationSummary } from '../models/license-application-summary.model';
import { FileSystemItem } from '../models/file-system-item.model';
import { saveAs } from 'file-saver';
import { Application } from '../models/adoxio-application.model';



@Component({
  selector: 'app-lite-application-dashboard',
  templateUrl: './lite-application-dashboard.component.html',
  styleUrls: ['./lite-application-dashboard.component.scss']
})
export class LiteApplicationDashboardComponent implements OnInit {

  busy: Subscription;
  @Input() applicationInProgress: boolean;
  dataLoaded = false;

  // displayedColumns = ['name', 'establishmentName', 'establishmentAddress', 'status', 'licenseType', 'licenseNumber'];
  displayedColumns = ['establishmentName', 'name', 'cancel'];
  dataSource = new MatTableDataSource<LicenseApplicationSummary>();
  @ViewChild(MatPaginator) paginator: MatPaginator;
  // @ViewChild(MatSort) sort: MatSort;

  constructor(private ApplicationDataService: ApplicationDataService, public dialog: MatDialog) { }

  ngOnInit() {
    this.displayApplications();
  }

  /**
   *
   * */
  private displayApplications() {
    const licenseApplicationSummary: LicenseApplicationSummary[] = [];
    this.busy = this.ApplicationDataService.getAllCurrentApplications().subscribe((Applications: Application[]) => {

      // for Applications in progress display the ones not paid
      // for Applications submitted display the ones paid
      if (this.applicationInProgress) {
        this.displayedColumns = ['lastUpdated', 'establishmentName', 'cancel'];
      } else {
        this.displayedColumns = ['name'];
      }

      Applications.forEach((entry) => {
        const licAppSum = new LicenseApplicationSummary();
        licAppSum.id = entry.id;
        licAppSum.name = entry.name;
        licAppSum.establishmentName = entry.establishmentName;
        licAppSum.establishmentAddress = entry.establishmentAddress;
        licAppSum.licenseType = entry.licenseType;
        licAppSum.status = entry.applicationStatus;
        licAppSum.isPaid = entry.isPaid;
        licAppSum.paymentreceiveddate = entry.paymentreceiveddate;
        licAppSum.createdon = entry.createdon;
        licAppSum.modifiedon = entry.modifiedon;

        if (this.applicationInProgress) {
          if (!licAppSum.isPaid) {
            licenseApplicationSummary.push(licAppSum);
          }
        } else {
          if (licAppSum.isPaid && !entry.assignedLicence) {
            this.busy = this.ApplicationDataService.getFileListAttachedToApplication(entry.id, 'Licence Application Main')
              .subscribe((files: FileSystemItem[]) => {
                if (files && files.length) {
                  licAppSum.applicationFormFileUrl = files[0].serverrelativeurl;
                  licAppSum.fileName = files[0].name;
                }
              });
            licenseApplicationSummary.push(licAppSum);
          }
        }
      });

      this.dataSource.data = licenseApplicationSummary;
      this.dataLoaded = true;
      setTimeout(() => {
        this.dataSource.paginator = this.paginator;
      });
    });
  }

  /**
   *
   * @param applicationId
   * @param establishmentName
   * @param applicationName
   */
  cancelApplication(applicationId: string, establishmentName: string, applicationName: string) {

    const dialogConfig = {
      disableClose: true,
      autoFocus: true,
      width: '400px',
      height: '200px',
      data: {
        establishmentName: establishmentName,
        applicationName: applicationName
      }
    };

    // open dialog, get reference and process returned data from dialog
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, dialogConfig);
    dialogRef.afterClosed().subscribe(
      cancelApplication => {
        if (cancelApplication) {
          // delete the application.
          this.busy = this.ApplicationDataService.cancelApplication(applicationId).subscribe(
            res => {
              this.displayApplications();
            });
        }
      }
    );

  }

}

@Component({
  selector: 'app-lite-application-dashboard-dialog',
  templateUrl: 'lite-application-dashboard-dialog.html',
})
export class ConfirmationDialogComponent {

  establishmentName: string;
  applicationName: string;

  constructor(
    public dialogRef: MatDialogRef<ConfirmationDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {
    this.applicationName = data.applicationName;
    this.establishmentName = data.establishmentName;
  }

  close() {
    this.dialogRef.close(false);
  }

  cancel() {
    this.dialogRef.close(true);
  }

}
