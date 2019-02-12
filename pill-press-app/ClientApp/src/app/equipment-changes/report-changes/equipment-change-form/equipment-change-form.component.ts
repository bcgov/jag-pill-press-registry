import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subscription, zip } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { ApplicationDataService } from '../../../services/application-data.service';
import { Application } from '../../../models/application.model';
import { FormBase } from '../../../shared/form-base';
import { postalRegex } from '../../../business-profile/business-profile/business-profile.component';
import * as _moment from 'moment';
// tslint:disable-next-line:no-duplicate-imports
import { defaultFormat as _rollupMoment } from 'moment';
const moment = _rollupMoment || _moment;

// See the Moment.js docs for the meaning of these formats:
// https://momentjs.com/docs/#/displaying/format/
export const MY_FORMATS = {
  parse: {
    dateInput: 'LL',
  },
  display: {
    dateInput: 'YYYY-MM-DD',
    monthYearLabel: 'MMM YYYY',
    dateA11yLabel: 'YYYY-MM-DD',
    monthYearA11yLabel: 'MMMM YYYY',
  },
};
@Component({
  selector: 'app-equipment-change-form',
  templateUrl: './equipment-change-form.component.html',
  styleUrls: ['./equipment-change-form.component.scss']
})
export class EquipmentChangeFormComponent extends FormBase implements OnInit {
  form: FormGroup;
  busy: Subscription;
  equipmentId: string;
  busyPromise: Promise<any>;
  locations: any;

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
      typeOfChange: [''],
      dateOfEquipmentChange: [''],
      circumstancesOfLoss: [''],
      policeNotified: [''],
      policeReportDate: [''],
      policeFileNumber: [''],
      circumstancesOfStolenEquipment: [''],
      circumstancesOfDestroyedEquipment: [''],
      whoDestroyedEquipment: [''],
      addressWhereEquipmentWasDestroyed: this.fb.group({
        id: [],
        streetLine1: [''],
        streetLine2: [],
        city: [''],
        province: [],
        postalCode: [''],
      }),
      equipmentRecord: this.fb.group({
        id: [],
        equipmentType: ['', Validators.required],
        equipmentRegistryNumber: ['', Validators.required],
        howWasEquipmentBuilt: ['', Validators.required],
        dateLost: ['', Validators.required],
        dateReported: ['', Validators.required],
        howWasEquipmentBuiltOther: [],
        howWasEquipmentBuiltOtherCheck: [],
        nameOfManufacturer: [''],
        equipmentMake: [''],
        equipmentModel: [''],
        serialNumber: [''],
        howEquipmentBuiltDescription: [''],
        personBusinessThatBuiltEquipment: [''],
        serialNumberForCustomBuilt: [''],
        customBuiltSerialNumber: [''],
        serialNumberKeyPartDescription: []
      }),

    });
    // this.clearHiddenFields();
    this.reloadData();

  }

  reloadData() {
    this.busy = this.applicationDataService.getApplicationById(this.equipmentId)
      .subscribe((data: any) => {

        data.certificates = data.certificates || [];
        if (data.certificates.length > 0) {
          data.certificates.sort(this.dateSort);
          data.equipmentRegistryNumber = data.certificates[0].name;
        }
        data.address = data.address || <any>{};
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
  clearHiddenFields() {
    this.form.get('howWasEquipmentBuilt').valueChanges
      .subscribe((value) => {
        if (value === 'Commercially Manufactured') {
          this.form.get('address').reset();
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
      value.address.country = 'Canada';
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
    const controls = this.form.controls;
    for (const c in controls) {
      if (typeof (controls[c].markAsTouched) === 'function') {
        controls[c].markAsTouched();
      }
    }

    // controls = (<FormGroup>this.form.get('address')).controls;
    // for (const c in controls) {
    //   if (typeof (controls[c].markAsTouched) === 'function') {
    //     controls[c].markAsTouched();
    //   }
    // }
  }
}
