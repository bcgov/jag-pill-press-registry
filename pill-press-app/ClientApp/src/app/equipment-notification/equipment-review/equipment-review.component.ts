import { Component, OnInit } from '@angular/core';

import { ActivatedRoute, Router } from '@angular/router';
import { Application } from '../../models/application.model';
import { ApplicationDataService } from '../../services/adoxio-application-data.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { zip } from 'rxjs';

@Component({
  selector: 'app-equipment-review',
  templateUrl: './equipment-review.component.html',
  styleUrls: ['./equipment-review.component.scss']
})
export class EquipmentReviewComponent implements OnInit {
  busy: any;
  equipmentId: string;
  notification: Application;
  form: FormGroup;
  busyPromise: Promise<any>;

  constructor(private applicationDataService: ApplicationDataService,
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute) {
    this.equipmentId = this.route.snapshot.params.id;
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
      }, error => {
        // debugger;
      });
  }

  save(goToReview: boolean) {
    if (this.form.valid || goToReview === false) {
      const value = this.form.value;
      if (goToReview) {
        value.statuscode = 'Pending';
        value.submittedDate = new Date();
      }
      const saveList = [this.applicationDataService.updateApplication(value)];
      this.busyPromise = zip(...saveList)
        .toPromise()
        .then(res => {
          if (goToReview) {
            this.router.navigateByUrl(`/equipment-notification/thank-you/${this.equipmentId}`);
          } else {
            this.router.navigateByUrl(`/dashboard`);
            // this.reloadData();
          }
        }, err => {
          // todo: show errors;
        });
    }
  }

}
