import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subscription, zip } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { ApplicationDataService } from '../../services/application-data.service';
import { Application } from '../../models/application.model';
import { FormBase } from '../../shared/form-base';
import { postalRegex } from '../../business-information/business-profile/business-profile.component';

@Component({
  selector: 'app-equipment-identification',
  templateUrl: './equipment-identification.component.html',
  styleUrls: ['./equipment-identification.component.scss']
})
export class EquipmentIdentificationComponent extends FormBase implements OnInit {
  form: FormGroup;
  busy: Subscription;
  equipmentId: string;
  busyPromise: Promise<any>;

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
      howWasEquipmentBuilt: ['', Validators.required],
      howWasEquipmentBuiltOther: [],
      howWasEquipmentBuiltOtherCheck: [],
      nameOfManufacturer: ['', this.requiredSelectChildValidator('howWasEquipmentBuilt', ['Commercially Manufactured'])],
      equipmentMake: ['', this.requiredSelectChildValidator('howWasEquipmentBuilt', ['Commercially Manufactured'])],
      equipmentModel: ['', this.requiredSelectChildValidator('howWasEquipmentBuilt', ['Commercially Manufactured'])],
      serialNumber: ['', this.requiredSelectChildValidator('howWasEquipmentBuilt', ['Commercially Manufactured'])],
      howEquipmentBuiltDescription: ['', this.requiredSelectChildValidator('howWasEquipmentBuilt', ['Custom-built', 'Other'])],
      personBusinessThatBuiltEquipment: [''],
      serialNumberForCustomBuilt: ['', this.requiredSelectChildValidator('howWasEquipmentBuilt', ['Custom-built', 'Other'])],
      customBuiltSerialNumber: ['', this.requiredSelectChildValidator('serialNumberForCustomBuilt', [true])],
      serialNumberKeyPartDescription: [],
      addressofPersonBusiness: this.fb.group({
        id: [],
        streetLine1: [''],
        streetLine2: [],
        city: [''],
        province: [],
        postalCode: [''],
      })

    });
    this.clearHiddenFields();
    this.reloadData();

  }

  reloadData() {
    this.busy = this.applicationDataService.getApplicationById(this.equipmentId)
      .subscribe((data: Application) => {
        data.addressofPersonBusiness = data.addressofPersonBusiness || <any>{};
        this.form.patchValue(data);
      }, error => {
        // debugger;
      });
  }

  clearHiddenFields() {
    this.form.get('howWasEquipmentBuilt').valueChanges
      .subscribe((value) => {
        if (value === 'Commercially Manufactured') {
          this.form.get('addressofPersonBusiness').reset();
        }
        for (const field in this.form.controls) {
          if (field !== 'id'
            && field !== 'province'
            && field !== 'howWasEquipmentBuilt') {
            this.form.get(field).reset();
          }
        }
      });
  }

  save(goToReview: boolean) {
    if (this.form.valid || goToReview === false) {
      const value = this.form.value;
      value.addressofPersonBusiness.country = 'Canada';
      const saveList = [this.applicationDataService.updateApplication(value)];
      this.busyPromise = zip(...saveList)
        .toPromise()
        .then(res => {
          if (goToReview) {
            this.router.navigateByUrl(`/equipment-notification/source/${this.equipmentId}`);
          } else {
            this.router.navigateByUrl(`/dashboard`);
            // this.reloadData();
          }
        }, err => {
          // todo: show errors;
        });
    }
  }

  markAsTouched() {
    let controls = this.form.controls;
    for (const c in controls) {
      if (typeof (controls[c].markAsTouched) === 'function') {
        controls[c].markAsTouched();
      }
    }

    controls = (<FormGroup>this.form.get('addressofPersonBusiness')).controls;
    for (const c in controls) {
      if (typeof (controls[c].markAsTouched) === 'function') {
        controls[c].markAsTouched();
      }
    }
  }
}
