import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subscription, zip, forkJoin } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { ApplicationDataService } from '../../services/application-data.service';
import { Application } from '../../models/application.model';
import { EquipmentLocation } from '../../models/equipment-location.model';
import { idLocale } from 'ngx-bootstrap';
import { UserDataService } from './../../services/user-data.service';
import { DynamicsDataService } from './../../services/dynamics-data.service';
import { FormBase } from './../../shared/form-base';
import { postalRegex } from '../../business-profile/business-profile/business-profile.component';
import { faExclamationCircle, faFileAlt, faTimes } from '@fortawesome/free-solid-svg-icons';
@Component({
  selector: 'app-location-change',
  templateUrl: './location-change.component.html',
  styleUrls: ['./location-change.component.scss']
})
export class LocationChangeComponent extends FormBase implements OnInit {
  form: FormGroup;
  busy: Subscription;
  equipmentId: string;
  busyPromise: Promise<any>;
  faExclamationCircle = faExclamationCircle;
  faFileAlt = faFileAlt;
  faTimes = faTimes;

  locations: EquipmentLocation[] = [
    // <any>{
    //   id: '1',
    //   address: <any>{
    //     id: '1',
    //     streetLine1: '880 Douglas',
    //     streetLine2: 'suite 102',
    //     city: 'Victoria',
    //     province: 'British Columbia',
    //     postalCode: 'V8B 6F1',
    //   }
    // }
  ];

  constructor(private route: ActivatedRoute,
    private router: Router,
    private applicationDataService: ApplicationDataService,
    private dynamicsDataService: DynamicsDataService,
    private userDataService: UserDataService,
    private fb: FormBuilder) {
    super();
    this.equipmentId = this.route.snapshot.params.id;
  }

  updateLocation(event) {
    const loc = this.locations.filter(i => i.id === event.target.value)[0];
    loc.address.province = 'British Columbia';
    this.form.get('equipmentLocation').patchValue(loc);
  }

  ngOnInit() {
    this.form = this.fb.group({
      id: [],
      equipmentLocation: this.fb.group({
        id: [],
        address: this.fb.group({
          id: [],
          streetLine1: ['', Validators.required],
          streetLine2: [],
          city: ['', Validators.required],
          province: ['British Columbia'],
          postalCode: ['', [Validators.required, Validators.pattern(postalRegex)]],
        }),
        privateDwelling: ['', Validators.required],
      }),
      settingDescription: ['', Validators.required]
    });

    this.form.get('equipmentLocation.id').valueChanges
      .subscribe(value => {
        if (value) {
          this.form.get('equipmentLocation.privateDwelling').disable();
        } else {
          this.form.get('equipmentLocation.privateDwelling').enable();
        }
      });

    this.reloadData();
  }

  reloadData() {
    this.busy = this.userDataService.getCurrentUser()
      .subscribe((data) => {
        if (data.accountid != null) {
          this.busyPromise = forkJoin([
            this.applicationDataService.getApplicationById(this.equipmentId),
            this.dynamicsDataService.getRecord(`account/${data.accountid}/locations`, ''),
          ])
            .toPromise()
            .then((result) => {
              const application = <Application>result[0];
              application.equipmentLocation = application.equipmentLocation || <EquipmentLocation>{ address: {} };
              this.form.patchValue(application);
              this.locations = <EquipmentLocation[]>result[1];
            });
        }
      });
  }

  markAsTouched() {
    this.form.get('settingDescription').markAsTouched();
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


  save(goToReview: boolean) {
    if (this.form.valid || goToReview === false) {
      const value = this.form.value;
      const saveList = [this.applicationDataService.updateApplication(value)];
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


