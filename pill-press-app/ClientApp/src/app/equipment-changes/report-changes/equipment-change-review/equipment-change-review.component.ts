import { Component, OnInit } from '@angular/core';
import { FormBase } from '@shared/form-base';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subscription, zip } from 'rxjs';
import { faSave, faExclamationCircle, faExclamationTriangle } from '@fortawesome/free-solid-svg-icons';
import { ActivatedRoute, Router } from '@angular/router';
import { ApplicationDataService } from '@services/application-data.service';


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

  faSave = faSave;
  faExclamationCircle  = faExclamationCircle ;
  faExclamationTriangle  = faExclamationTriangle ;
  application: any;
  showErrors: boolean;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private applicationDataService: ApplicationDataService,
    private fb: FormBuilder) {
    super();
    this.applicationId = this.route.snapshot.params.id;
  }

  ngOnInit() {
    this.form = this.fb.group({
      id: [],
      declarationOfCorrectInformation: [''],
      addressWhereEquipmentWasDestroyed: this.fb.group({
        id: [],
        streetLine1: [''],
        streetLine2: [],
        city: [''],
        province: ['British Columbia'],
        postalCode: [''],
      }),
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
      }, error => {
        // debugger;
      });
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
