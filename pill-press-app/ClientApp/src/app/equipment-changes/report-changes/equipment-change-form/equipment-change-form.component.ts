import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subscription, zip } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { ApplicationDataService } from '../../../services/application-data.service';
import { Application } from '../../../models/application.model';
import { FormBase } from '../../../shared/form-base';
import { postalRegex } from '../../../business-profile/business-profile/business-profile.component';
import { faSave } from '@fortawesome/free-regular-svg-icons';
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

  faSave = faSave;

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
      typeOfChange: ['', Validators.required],
      dateOfEquipmentChange: ['', Validators.required],
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
        province: ['British Columbia'],
        postalCode: [''],
      }),
      equipmentRecord: this.fb.group({
        id: [],
        equipmentType: [''],
        equipmentTypeOther: [''],
        name: [''],
        pillpressEncapsulatorSize: [''],
        pillpressEncapsulatorSizeOther: [''],
        levelOfEquipmentAutomation: [''],
        pillpressMaxCapacity: [''],
        howWasEquipmentBuilt: [''],
        HhwWasEquipmentBuiltOther: [''],
        nameOfManufacturer: [''],
        equipmentMake: [''],
        equipmentModel: [''],
        serialNumber: [''],
        encapsulatorMaxCapacity: [''],
        customBuiltSerialNumber: [''],
      })
    });
    this.clearHiddenFields();
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
        data.addressWhereEquipmentWasDestroyed = data.addressWhereEquipmentWasDestroyed || <any>{};
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
    this.form.get('typeOfChange').valueChanges
      .subscribe((value) => {
        this.form.get('circumstancesOfLoss').clearValidators();
        this.form.get('circumstancesOfLoss').reset();
        this.form.get('policeNotified').clearValidators();
        this.form.get('policeNotified').reset();
        this.form.get('circumstancesOfStolenEquipment').clearValidators();
        this.form.get('circumstancesOfStolenEquipment').reset();
        this.form.get('circumstancesOfDestroyedEquipment').clearValidators();
        this.form.get('circumstancesOfDestroyedEquipment').reset();
        this.form.get('whoDestroyedEquipment').clearValidators();
        this.form.get('whoDestroyedEquipment').reset();

        this.form.get('addressWhereEquipmentWasDestroyed.streetLine1').clearValidators();
        this.form.get('addressWhereEquipmentWasDestroyed.city').clearValidators();
        this.form.get('addressWhereEquipmentWasDestroyed.province').clearValidators();
        this.form.get('addressWhereEquipmentWasDestroyed.postalCode').clearValidators();
        this.form.get('addressWhereEquipmentWasDestroyed').reset();
        if (value === 'Lost') {
          this.form.get('circumstancesOfLoss').setValidators([Validators.required]);
          this.form.get('policeNotified').setValidators([Validators.required]);
        } else if (value === 'Sold') {
          this.form.get('circumstancesOfStolenEquipment').setValidators([Validators.required]);
          this.form.get('policeNotified').setValidators([Validators.required]);
        } if (value === 'Destroyed') {
          this.form.get('circumstancesOfDestroyedEquipment').setValidators([Validators.required]);
          this.form.get('addressWhereEquipmentWasDestroyed.streetLine1').setValidators([Validators.required]);
          this.form.get('addressWhereEquipmentWasDestroyed.city').setValidators([Validators.required]);
          this.form.get('addressWhereEquipmentWasDestroyed.province').setValidators([Validators.required]);
          this.form.get('addressWhereEquipmentWasDestroyed.province').setValue('British Columbia');
          this.form.get('addressWhereEquipmentWasDestroyed.postalCode')
            .setValidators([Validators.required, Validators.pattern(postalRegex)]);
        }
      });
    this.form.get('policeNotified').valueChanges
      .subscribe((value) => {
        if (value) {
          this.form.get('policeReportDate').setValidators([Validators.required]);
          this.form.get('policeFileNumber').setValidators([Validators.required]);
        } else {
          this.form.get('policeReportDate').clearValidators();
          this.form.get('policeFileNumber').clearValidators();
          this.form.get('policeReportDate').reset();
          this.form.get('policeFileNumber').reset();
        }
      });
  }

  save(goToReview: boolean) {
    if (this.form.valid || goToReview === false) {
      const value = this.form.value;
      if (value.addressWhereEquipmentWasDestroyed) {
        value.addressWhereEquipmentWasDestroyed.province = 'British Columbia';
        value.addressWhereEquipmentWasDestroyed.country = 'Canada';
      }
      const saveList = [this.applicationDataService.updateApplication(value)];
      this.busyPromise = zip(...saveList)
        .toPromise()
        .then(res => {
          if (goToReview) {
            this.router.navigateByUrl(`/equipment-changes/reporting-changes/review/${this.equipmentId}`);
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
