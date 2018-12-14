import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Subscription, zip } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { ApplicationDataService } from '../../services/adoxio-application-data.service';
import { Application } from '../../models/application.model';
import { EquipmentLocation } from '../../models/equipment-location.model';

@Component({
  selector: 'app-equipment-location',
  templateUrl: './equipment-location.component.html',
  styleUrls: ['./equipment-location.component.scss']
})
export class EquipmentLocationComponent implements OnInit {
  form: FormGroup;
  busy: Subscription;
  equipmentId: string;
  busyPromise: Promise<any>;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private applicationDataService: ApplicationDataService,
    private fb: FormBuilder) {
    this.equipmentId = this.route.snapshot.params.id;
  }

  ngOnInit() {
    this.form = this.fb.group({
      id: [],
      equipmentLocation: this.fb.group({
        id: [],
        address: this.fb.group({
          id: [],
          streetLine1: [],
          streetLine2: [],
          city: [],
          province: ['British Columbia'],
          postalCode: [],
        }),
        privateDwelling: [],
        settingDescription: [],
      }),
    });

    this.reloadData();
  }

  reloadData() {
    this.busy = this.applicationDataService.getApplicationById(this.equipmentId)
      .subscribe((data: Application) => {
        data.equipmentLocation = data.equipmentLocation || <EquipmentLocation>{ address: {} };
        this.form.patchValue(data);
      }, error => {
        // debugger;
      });
  }

  markAsTouched() {

  }


  save(goToReview: boolean) {
    if (this.form.valid || goToReview === false) {
      const value = this.form.value;
      const saveList = [this.applicationDataService.updateApplication(value)];
      this.busyPromise = zip(...saveList)
        .toPromise()
        .then(res => {
          if (goToReview) {
            this.router.navigateByUrl(`/equipment-notification/review/${this.equipmentId}`);
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
