import { Component, OnInit } from '@angular/core';

import { ActivatedRoute, Router } from '@angular/router';
import { Application } from '../../models/application.model';
import { ApplicationDataService } from '../../services/application-data.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { zip } from 'rxjs';
import { faExclamationCircle } from '@fortawesome/free-solid-svg-icons';
import { faSave } from '@fortawesome/free-regular-svg-icons';
import { FileSystemItem } from '@app/models/file-system-item.model';
import { HttpHeaders, HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-equipment-review',
  templateUrl: './equipment-review.component.html',
  styleUrls: ['./equipment-review.component.scss']
})
export class EquipmentReviewComponent implements OnInit {
  busy: any;
  equipmentId: string;
  reviewOnly: boolean;
  notification: Application;
  form: FormGroup;
  busyPromise: Promise<any>;
  showErrors: boolean;
  faSave = faSave;
  faExclamationCircle = faExclamationCircle;
  files: FileSystemItem[];

  constructor(private applicationDataService: ApplicationDataService,
    private fb: FormBuilder,
    private router: Router,
    private http: HttpClient,
    private route: ActivatedRoute) {
    this.equipmentId = this.route.snapshot.params.id;
    this.reviewOnly = this.route.snapshot.params.reviewOnly === 'true';
  }

  ngOnInit() {
    this.form = this.fb.group({
      id: [],
      declarationOfCorrectInformation: [],
      confirmationOfAuthorizedUse: []
    });

    this.reloadData();
  }

  reloadData() {
    this.busy = this.applicationDataService.getApplicationById(this.equipmentId)
      .subscribe((data: Application) => {
        this.notification = data;
        this.form.patchValue(data);

        this.busy = this.getUploadedFileData().subscribe((data: FileSystemItem[]) => {
          data.forEach((entry) => {
            entry.size = Math.ceil(entry.size / 1024);
          });
          this.files = data;
        });
      }, error => {
        // debugger;
      });
  }

  /**
 * get documents attached to this application
 */
  getUploadedFileData() {
    const attachmentURL = `api/file/${this.equipmentId}/attachments/incident`;
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    const getFileURL = attachmentURL + '/Equipment Documents';
    return this.http.get(getFileURL, { headers: headers });
  }

  save() {
    this.showErrors = true;
    if (this.form.get('declarationOfCorrectInformation').value && this.form.get('confirmationOfAuthorizedUse').value) {
      const value = this.form.value;
      value.statuscode = 'Pending';
      value.submittedDate = new Date();

      this.busyPromise = this.applicationDataService.updateApplication(value)
        .toPromise()
        .then(res => {
          this.router.navigateByUrl(`/equipment-notification/thank-you/${this.equipmentId}`);
        }, err => {
          // todo: show errors;
        });
    }
  }

  saveAndExit() {
    const value = this.form.value;
    this.busyPromise = this.applicationDataService.updateApplication(value)
      .toPromise()
      .then(res => {
        this.router.navigateByUrl(`/dashboard`);
      }, err => {
        // todo: show errors;
      });

  }

  saveAndAddMore() {
    this.showErrors = true;
    if (this.form.get('declarationOfCorrectInformation').value && this.form.get('confirmationOfAuthorizedUse').value) {
      const value = this.form.value;
      value.statuscode = 'Pending';
      value.submittedDate = new Date();

      this.busyPromise = this.applicationDataService.updateApplication(value)
        .toPromise()
        .then(res => {
          this.addEquipment();
        }, err => {
          // todo: show errors;
        });
    }
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
        // this.snackBar.open('Error starting a New Equipment Notificatio Application',
        //   'Fail', { duration: 3500, panelClass: ['red-snackbar'] });
        console.log('Error starting a Registered Seller Application');
      }
    );
  }

}
