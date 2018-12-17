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

  save() {
    if (this.form.valid) {
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
    if (this.form.valid) {
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
