import { Component, OnInit } from '@angular/core';
import { FormBase } from '@shared/form-base';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subscription, zip } from 'rxjs';
import { faSave, faExclamationCircle, faExclamationTriangle } from '@fortawesome/free-solid-svg-icons';
import { ActivatedRoute, Router } from '@angular/router';
import { ApplicationDataService } from '@services/application-data.service';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { FileSystemItem } from '@app/models/file-system-item.model';


@Component({
  selector: 'app-equipment-change-review',
  templateUrl: './equipment-change-review.component.html',
  styleUrls: ['./equipment-change-review.component.scss']
})
export class EquipmentChangeReviewComponent extends FormBase implements OnInit {
  form: FormGroup;
  busy: Subscription;
  applicationId: string;
  busyPromise: Promise<any>;
  locations: any;
  files: FileSystemItem[];

  faSave = faSave;
  faExclamationCircle  = faExclamationCircle ;
  faExclamationTriangle  = faExclamationTriangle ;
  application: any;
  showErrors: boolean;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private applicationDataService: ApplicationDataService,
    private http: HttpClient,
    private fb: FormBuilder) {
    super();
    this.applicationId = this.route.snapshot.params.id;
  }

  ngOnInit() {
    this.form = this.fb.group({
      id: [],
      declarationOfCorrectInformation: [''],
    });

    this.reloadData();

  }

  reloadData() {
    this.busy = this.applicationDataService.getApplicationById(this.applicationId)
      .subscribe((data: any) => {

        data.certificates = data.certificates || [];
        if (data.certificates.length > 0) {
          data.certificates.sort(this.dateSort);
          data.equipmentRegistryNumber = data.certificates[0].name;
        }
        this.application = data;
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
    const attachmentURL = `api/file/${this.application.id}/attachments/incident`;
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    const getFileURL = attachmentURL + '/Equipment Change Documents';
    return this.http.get(getFileURL, { headers: headers });
  }

  dateSort(a, b) {
    if (a.issueDate > b.issueDate) {
      return 1;
    } else {
      return -1;
    }
  }

  save(goToReview: boolean) {
    if (this.form.valid || goToReview === false) {
      const value = this.form.value;
      value.statuscode = 'Pending';
      value.submittedDate = new Date();
      const saveList = [this.applicationDataService.updateApplication(value)];
      this.busyPromise = zip(...saveList)
        .toPromise()
        .then(res => {
          if (goToReview) {
            this.router.navigateByUrl(`/equipment-changes/reporting-changes/thank-you/${this.applicationId}`);
          } else {
            this.router.navigateByUrl(`/dashboard`);
            // this.reloadData();
          }
        }, err => {
          // todo: show errors;
        });
    } else {
      this.showErrors = true;
    }
  }

  markAsTouched() {
    const controls = this.form.controls;
    for (const c in controls) {
      if (typeof (controls[c].markAsTouched) === 'function') {
        controls[c].markAsTouched();
      }
    }
  }
}
