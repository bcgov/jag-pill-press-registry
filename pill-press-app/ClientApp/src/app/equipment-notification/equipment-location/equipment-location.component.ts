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
import { faExclamationCircle } from '@fortawesome/free-solid-svg-icons';
import { faSave } from '@fortawesome/free-regular-svg-icons';

@Component({
  selector: 'app-equipment-location',
  templateUrl: './equipment-location.component.html',
  styleUrls: ['./equipment-location.component.scss']
})
export class EquipmentLocationComponent extends FormBase implements OnInit {
  form: FormGroup;
  busy: Subscription;
  equipmentId: string;
  busyPromise: Promise<any>;
  faExclamationCircle = faExclamationCircle;
  faSave = faSave;

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
      ownedBeforeJan2019: [],

      // First level checkboxes
      purchasedFromBcSeller: [],
      purchasedFromSellerOutsideOfBc: [],
      importedToBcByAThirdParty: [],
      alternativeOwnershipArrangement: [],
      iAssembledItMyself: [],
      howCameIntoPossessionOtherCheck: [],

      // BC Seller fields
      nameOfBcSeller: [],
      bcSellersContactPhoneNumber: [],
      bcSellersContactEmail: [],
      dateOfPurchaseFromBcSeller: [],
      bcSellersRegistrationNumber: [],

      // Outside BC seller fields
      outsideBcSellersName: [],
      outsideBcSellersLocation: [],
      dateOfPurchaseFromOutsideBcSeller: [],

      // Imported to BC by a third party
      nameOfImporter: [],
      importersRegistrationNumber: [],
      nameOfOriginatingSeller: [],
      originatingSellersLocation: [],
      dateOfPurchaseFromImporter: [],

      // Alternative Ownership Arrangement fields
      kindOfAlternateOwnershipOtherCheck: [],
      kindOfAlternateOwnershipOther: [],
      possessUntilICanSell: [],
      giveNorLoanedToMe: [],
      rentingOrLeasingFromAnotherBusiness: [],
      usingToManufactureAProduct: [],
      areYouARegisteredSeller: [],
      emailOfTheBusinessThatHasGivenOrLoaned: [],
      phoneofbusinessthathasgivenorloaned: [],
      emailOfBusinessThatHasRentedOrLeased: [],
      phoneOfBusinessThatHasRentedOrLeased: [],
      whyHaveYouRentedOrLeased: [],
      whyHaveYouAcceptedOrBorrowed: [],
      nameOfBusinessThatHasGivenOrLoaned: [],
      NameOfBusinessThatHasRentedOrLeased: [],

      // I assembled it myself fields
      whenDidYouAssembleEquipment: [],
      whereDidYouObtainParts: [],
      doYouAssembleForOtherBusinesses: [],
      detailsOfAssemblyForOtherBusinesses: [],
      detailsOfHowEquipmentCameIntoPossession: [],

      addressofBusinessthathasGivenorLoaned: this.fb.group({
        id: [],
        streetLine1: [],
        streetLine2: [],
        city: [],
        country: [],
        province: [],
        postalCode: [],
      }),
      addressOfBusinessThatHasRentedorLeased: this.fb.group({
        id: [],
        streetLine1: [],
        streetLine2: [],
        city: [],
        country: [],
        province: [],
        postalCode: [],
      }),
      bcSellersAddress: this.fb.group({
        id: [],
        streetLine1: [],
        streetLine2: [],
        city: [],
        province: [],
        postalCode: [],
      }),
      importersAddress: this.fb.group({
        id: [],
        streetLine1: [],
        streetLine2: [],
        city: [],
        country: [],
        province: [],
        postalCode: [],
      }),
      outsideBcSellersAddress: this.fb.group({
        id: [],
        streetLine1: [],
        streetLine2: [],
        city: [],
        country: [],
        province: [],
        postalCode: [],
      }),
      OriginatingSellersAddress: this.fb.group({
        id: [],
        streetLine1: [],
        streetLine2: [],
        city: [],
        country: [],
        province: [],
        postalCode: [],
      }),
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
      //settingDescription needs to be defined twice, at the application level and the equipmentLocation
      settingDescription: []
    });
   

    //this.form.get('equipmentLocation.id').valueChanges
    //  .subscribe(value => {
    //    if (value) {
    //      this.form.get('equipmentLocation.privateDwelling').disable();
    //    } else {
    //      this.form.get('equipmentLocation.privateDwelling').enable();
    //    }
    //  });

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
              //application.equipmentLocation = application.equipmentLocation || <EquipmentLocation>{ address: {} };              
              this.locations = <EquipmentLocation[]>result[1];
              this.form.patchValue(application);
              const setdesc = this.form.get('settingDescription').value;
              this.form.get('equipmentLocation.settingDescription').setValue(setdesc);
            });
        }
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


  save(goToReview: boolean) { //|| goToReview === false
    if (this.form.valid) {
      //copy the value from equipmentLocation.settingDescription to settingDescription
      const setdesc = this.form.get('equipmentLocation.settingDescription').value; 
      this.form.get('settingDescription').setValue(setdesc);
      const value = this.form.value;
      //set the address type to "Location"
      value.equipmentLocation.address.addresstype = "Location";
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
