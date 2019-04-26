import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subscription, zip, forkJoin } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { ApplicationDataService } from '../../services/application-data.service';
import { EquipmentDataService } from '../../services/equipment-data-service';
import { Application } from '../../models/application.model';
import { EquipmentLocation } from '../../models/equipment-location.model';
import { idLocale } from 'ngx-bootstrap';
import { UserDataService } from './../../services/user-data.service';
import { DynamicsDataService } from './../../services/dynamics-data.service';
import { FormBase } from './../../shared/form-base';
import { postalRegex } from '../../business-profile/business-profile/business-profile.component';
import { faExclamationCircle, faFileAlt, faTimes } from '@fortawesome/free-solid-svg-icons';
import { Equipment } from '@app/models/equipment.model';
@Component({
  selector: 'app-location-change',
  templateUrl: './location-change.component.html',
  styleUrls: ['./location-change.component.scss']
})
export class LocationChangeComponent extends FormBase implements OnInit {
  form: FormGroup;
  busy: Subscription;
  applicationId: string;
  busyPromise: Promise<any>;
  faExclamationCircle = faExclamationCircle;
  faFileAlt = faFileAlt;
  faTimes = faTimes;
  application: Application;
  locations: EquipmentLocation[] = [];
  equipment: Equipment;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private applicationDataService: ApplicationDataService,
    private dynamicsDataService: DynamicsDataService,
    private userDataService: UserDataService,
    private equipmentDataService: EquipmentDataService,
    private fb: FormBuilder) {
    super();
    this.applicationId = this.route.snapshot.params.id;
  }

  updateLocation(event) {
    const loc = this.locations.filter(i => i.id === event.target.value)[0];
    if (loc) {
      loc.address.province = 'British Columbia';
      this.form.get('equipmentLocation').patchValue(loc);
    } else {
      var x = this.form.get('equipmentLocation');
      this.form.get('equipmentLocation').reset;
    }
  }

  ngOnInit() {
    this.form = this.fb.group({
      id: [],
      equipmentLocation: this.fb.group({
        id: [],
        privateDwelling: ['', Validators.required],
        settingDescription: ['', Validators.required],
        address: this.fb.group({
          id: [],
          streetLine1: ['', Validators.required],
          streetLine2: [],
          city: ['', Validators.required],
          province: ['British Columbia'],
          postalCode: ['', [Validators.required, Validators.pattern(postalRegex)]],
        }),
      }),
    });

    //this.form.get('equipmentLocation.id').valueChanges
    //  .subscribe(value => {
    //    if (value || value === '') {
    //      this.form.get('equipmentLocation.privateDwelling').disable();
    //      this.form.get('equipmentLocation.settingDescription').disable();
    //    } else {
    //      this.form.get('equipmentLocation.privateDwelling').enable();
    //      this.form.get('equipmentLocation.settingDescription').enable();
    //    }
    //  });

    this.reloadData();
  }

  reloadData() {
    this.busy = this.userDataService.getCurrentUser()
      .subscribe((data) => {
        if (data.accountid != null) {
          this.busyPromise = forkJoin([
            this.applicationDataService.getApplicationById(this.applicationId),
            this.dynamicsDataService.getRecord(`account/${data.accountid}/locations`, ''),
          ])
            .toPromise()
            .then((result) => {
              this.application = <Application>result[0];
              this.locations = <EquipmentLocation[]>result[1];
              this.application.equipmentLocation = this.application.equipmentLocation || <EquipmentLocation>{ address: {} };
              this.form.patchValue(this.application);
            })
            //.then((data2) => {
            //  this.equipmentDataService.getEquipmentCurrentEquipmentlocation(this.application.equipmentRecord.id).subscribe((currEquipLoca) => {
            //    this.form.patchValue(currEquipLoca);
            //  });
            //});
        };
      });
  }

  markAsTouched() {
    this.form.get('equipmentLocation.settingDescription').markAsTouched();
    this.form.get('equipmentLocation.privateDwelling').markAsTouched();

    const controls = (<FormGroup>this.form.get('equipmentLocation.address')).controls;
    // tslint:disable-next-line:forin
    for (const c in controls) {
      controls[c].markAsTouched();
    }

  }

  tabChanged(event: any) {
    if (event.tab.textLabel === 'ADD A NEW LOCATION') {
      this.form.get('equipmentLocation').reset();
      this.form.get('equipmentLocation.address.province').setValue('British Columbia');
    }
  }


  save() {
    if (this.form.valid) {
      const value = this.form.value;
      //const saveList = [this.applicationDataService.updateApplication(value)];
      this.application.equipmentLocation = value.equipmentLocation;
      const saveList = [this.equipmentDataService.changeEquipmentLocation(this.application)];
      this.busyPromise = zip(...saveList)
        .toPromise()
        .then(res => {
            this.router.navigateByUrl(`/dashboard`);
        }, err => {
          // todo: show errors;
        });
    }
  }

}


