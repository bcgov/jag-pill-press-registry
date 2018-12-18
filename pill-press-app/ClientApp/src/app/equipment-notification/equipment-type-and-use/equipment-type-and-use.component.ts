import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Subscription, zip } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { ApplicationDataService } from '../../services/adoxio-application-data.service';
import { Application } from '../../models/application.model';

@Component({
  selector: 'app-equipment-type-and-use',
  templateUrl: './equipment-type-and-use.component.html',
  styleUrls: ['./equipment-type-and-use.component.scss']
})
export class EquipmentTypeAndUseComponent implements OnInit {
  form: FormGroup;
  busy: Subscription;
  equipmentId: string;
  busyPromise: any;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private applicationDataService: ApplicationDataService,
    private fb: FormBuilder) {
    this.equipmentId = this.route.snapshot.params.id;
  }

  ngOnInit() {
    this.form = this.fb.group({
      id: [],
      equipmentType: [],
      equipmentTypeOther: [],
      levelOfEquipmentAutomation: [],
      pillpressEncapsulatorsize: [],
      pillpressencapsulatorsizeothercheck: [],
      pillpressencapsulatorsizeother: [],
      pillpressmaxcapacity: [],
      encapsulatorMaxCapacity: [],
      explanationOfEquipmentuse: [],
    });

    this.reloadData();
    this.clearHiddenFields();
  }

  reloadData() {
    this.busy = this.applicationDataService.getApplicationById(this.equipmentId)
      .subscribe((data: Application) => {
        this.form.patchValue(data);
      }, error => {
        // debugger;
      });
  }

  clearHiddenFields() {
    this.form.get('equipmentType').valueChanges
      .subscribe(() => {
        for (const field in this.form.controls) {
          if (field != 'id' 
            && field != 'equipmentType' 
            && field != 'explanationOfEquipmentuse') {
            this.form.get(field).reset();
          }
        }

      });
  }

  markAsTouched() {
    this.form.markAsTouched();
    const controls = this.form.controls;
    for (const c in controls) {
      if (typeof (controls[c].markAsTouched) === 'function') {
        controls[c].markAsTouched();
      }
    }
  }


  save(goToReview: boolean) {
    if (this.form.valid || goToReview === false) {
      const value = this.form.value;
      const saveList = [this.applicationDataService.updateApplication(value)];
      this.busyPromise = zip(...saveList)
        .toPromise()
        .then(res => {
          if (goToReview) {
            this.router.navigateByUrl(`/equipment-notification/identification/${this.equipmentId}`);
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
