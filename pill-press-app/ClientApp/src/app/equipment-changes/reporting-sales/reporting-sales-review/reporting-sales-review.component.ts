import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Subscription, zip } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { ApplicationDataService } from '@services/application-data.service';
import { faSave, faExclamationTriangle } from '@fortawesome/free-solid-svg-icons';
@Component({
  selector: 'app-reporting-sales-review',
  templateUrl: './reporting-sales-review.component.html',
  styleUrls: ['./reporting-sales-review.component.scss']
})
export class ReportingSalesReviewComponent implements OnInit {
  form: FormGroup;
  formData: any;
  busy: Subscription;
  applicationId: string;

  ownersAndManagers: any[] = [];
  equipmentIdentification: string[];
  application: any;
  busyPromise: any;
  showErrors: boolean;
  faSave = faSave;
  faExclamationTriangle = faExclamationTriangle;

  constructor(private fb: FormBuilder,
    private applicationDataService: ApplicationDataService,
    private router: Router,
    private route: ActivatedRoute) {
    this.applicationId = this.route.snapshot.params.id;
  }

  ngOnInit() {
    this.form = this.fb.group({
      id: [],
      declarationOfCorrectInformation: ['']
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
        data.civicAddressOfPurchaser = data.civicAddressOfPurchaser || <any>{};
        data.purchasersCivicAddress = data.purchasersCivicAddress || <any>{};
        data.purchasersBusinessAddress = data.purchasersBusinessAddress || <any>{};
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

  save() {
    if (this.form.get('declarationOfCorrectInformation').value) {
      const value = {...this.application, ...this.form.value};
      value.statuscode = 'Pending';
      value.submittedDate = new Date();
      const saveList = [this.applicationDataService.updateApplication(value)];
      this.busyPromise = zip(...saveList)
        .toPromise()
        .then(res => {
            this.router.navigateByUrl(`/equipment-changes/reporting-sales/thank-you/${this.applicationId}`);
        }, err => {
          // todo: show errors;
        });
    } else {
      this.showErrors = true;
    }
  }

}
