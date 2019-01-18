import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subscription, zip } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { ApplicationDataService } from '../../services/application-data.service';
import { Application } from '../../models/application.model';
import { FormBase } from './../../shared/form-base';

@Component({
  selector: 'app-equipment-type-and-use',
  templateUrl: './equipment-type-and-use.component.html',
  styleUrls: ['./equipment-type-and-use.component.scss']
})
export class EquipmentTypeAndUseComponent extends FormBase implements OnInit {
  form: FormGroup;
  busy: Subscription;
  equipmentId: string;
  busyPromise: any;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private applicationDataService: ApplicationDataService,
    private fb: FormBuilder) {
    super();
    this.equipmentId = this.route.snapshot.params.id;
  }

  ngOnInit() {
    this.form = this.fb.group({
      id: [],
      equipmentType: ['', Validators.required],
      equipmentTypeOther: ['', this.requiredSelectChildValidator('equipmentType', ['Other'])],
      levelOfEquipmentAutomation: ['', Validators.required],
      pillpressEncapsulatorsize: ['', this.requiredSelectChildValidator('equipmentType', ['Pill Press', 'Encapsulator'])],
      pillpressencapsulatorsizeothercheck: [],
      pillpressencapsulatorsizeother: [],
      pillpressmaxcapacity: ['', this.requiredSelectChildValidator('equipmentType', ['Pill Press'])],
      encapsulatorMaxCapacity: ['', this.requiredSelectChildValidator('equipmentType', ['Encapsulator'])],
      explanationOfEquipmentuse: ['', Validators.required],
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
          if (field !== 'id'
            && field !== 'equipmentType'
            && field !== 'explanationOfEquipmentuse') {
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
      this.busy = this.applicationDataService.updateApplication(value)
        .subscribe(res => {
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
