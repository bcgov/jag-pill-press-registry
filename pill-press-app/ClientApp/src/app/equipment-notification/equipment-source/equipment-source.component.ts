import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { Subscription, zip } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { ApplicationDataService } from '../../services/adoxio-application-data.service';
import { Application } from '../../models/application.model';

import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
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
  selector: 'app-equipment-source',
  templateUrl: './equipment-source.component.html',
  styleUrls: ['./equipment-source.component.scss']
})
export class EquipmentSourceComponent implements OnInit {
  form: FormGroup;
  busy: Subscription;
  equipmentId: string;
  busyPromise: Promise<void>;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private applicationDataService: ApplicationDataService,
    private fb: FormBuilder) {
    this.equipmentId = this.route.snapshot.params.id;
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
        province: [],
        postalCode: [],
      }),
      addressOfBusinessThatHasRentedorLeased: this.fb.group({
        id: [],
        streetLine1: [],
        streetLine2: [],
        city: [],
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
        province: [],
        postalCode: [],
      }),
      outsideBcSellersAddress: this.fb.group({
        id: [],
        streetLine1: [],
        streetLine2: [],
        city: [],
        province: [],
        postalCode: [],
      }),
      OriginatingSellersAddress: this.fb.group({
        id: [],
        streetLine1: [],
        streetLine2: [],
        city: [],
        province: [],
        postalCode: [],
      }),

    });

    this.reloadData();
    this.clearHiddenFields();
  }

  clearHiddenFields() {

    this.form.get('ownedBeforeJan2019').valueChanges
      .filter(v => v)
      .subscribe(value => {
        this.form.get('purchasedFromBcSeller').reset();
        this.form.get('purchasedFromSellerOutsideOfBc').reset();
        this.form.get('importedToBcByAThirdParty').reset();
        this.form.get('alternativeOwnershipArrangement').reset();
        this.form.get('iAssembledItMyself').reset();
        this.form.get('howCameIntoPossessionOtherCheck').reset();
      });

    this.form.get('purchasedFromBcSeller').valueChanges
      .filter(v => !v)
      .subscribe(value => {
        this.form.get('nameOfBcSeller').reset();
        this.form.get('bcSellersContactPhoneNumber').reset();
        this.form.get('bcSellersContactEmail').reset();
        this.form.get('dateOfPurchaseFromBcSeller').reset();
        this.form.get('bcSellersRegistrationNumber').reset();

        this.form.get('bcSellersAddress').reset();
      });

    this.form.get('purchasedFromSellerOutsideOfBc').valueChanges
      .filter(v => !v)
      .subscribe(value => {
        this.form.get('outsideBcSellersName').reset();
        this.form.get('outsideBcSellersLocation').reset();
        this.form.get('dateOfPurchaseFromOutsideBcSeller').reset();

        this.form.get('outsideBcSellersAddress').reset();
      });

    this.form.get('importedToBcByAThirdParty').valueChanges
      .filter(v => !v)
      .subscribe(value => {
        this.form.get('nameOfImporter').reset();
        this.form.get('importersRegistrationNumber').reset();
        this.form.get('nameOfOriginatingSeller').reset();
        this.form.get('originatingSellersLocation').reset();
        this.form.get('dateOfPurchaseFromImporter').reset();
        this.form.get('importersAddress').reset();
        this.form.get('OriginatingSellersAddress').reset();
      });

    this.form.get('alternativeOwnershipArrangement').valueChanges
      .filter(v => !v)
      .subscribe(value => {
        this.form.get('kindOfAlternateOwnershipOtherCheck').reset();
        this.form.get('possessUntilICanSell').reset();
        this.form.get('giveNorLoanedToMe').reset();
        this.form.get('rentingOrLeasingFromAnotherBusiness').reset();
      });

    this.form.get('kindOfAlternateOwnershipOtherCheck').valueChanges
      .filter(v => !v)
      .subscribe(value => {
        this.form.get('kindOfAlternateOwnershipOther').reset();
      });

    this.form.get('possessUntilICanSell').valueChanges
      .filter(v => !v)
      .subscribe(value => {
        this.form.get('usingToManufactureAProduct').reset();
        this.form.get('areYouARegisteredSeller').reset();
      });

    this.form.get('giveNorLoanedToMe').valueChanges
      .filter(v => !v)
      .subscribe(value => {
        this.form.get('emailOfTheBusinessThatHasGivenOrLoaned').reset();
        this.form.get('phoneofbusinessthathasgivenorloaned').reset();
        this.form.get('whyHaveYouAcceptedOrBorrowed').reset();
        this.form.get('nameOfBusinessThatHasGivenOrLoaned').reset();
        this.form.get('addressofBusinessthathasGivenorLoaned').reset();
      });

    this.form.get('rentingOrLeasingFromAnotherBusiness').valueChanges
      .filter(v => !v)
      .subscribe(value => {
        this.form.get('emailOfBusinessThatHasRentedOrLeased').reset();
        this.form.get('phoneOfBusinessThatHasRentedOrLeased').reset();
        this.form.get('whyHaveYouRentedOrLeased').reset();
        this.form.get('NameOfBusinessThatHasRentedOrLeased').reset();
        this.form.get('addressOfBusinessThatHasRentedorLeased').reset();
      });


    this.form.get('iAssembledItMyself').valueChanges
      .filter(v => !v)
      .subscribe(value => {
        this.form.get('whenDidYouAssembleEquipment').reset();
        this.form.get('whereDidYouObtainParts').reset();
        this.form.get('doYouAssembleForOtherBusinesses').reset();
      });

    this.form.get('doYouAssembleForOtherBusinesses').valueChanges
      .filter(v => !v)
      .subscribe(value => {
        this.form.get('detailsOfAssemblyForOtherBusinesses').reset();
      });

    this.form.get('howCameIntoPossessionOtherCheck').valueChanges
      .filter(v => !v)
      .subscribe(value => {
        this.form.get('detailsOfHowEquipmentCameIntoPossession').reset();
      });

  }

  reloadData() {
    this.busy = this.applicationDataService.getApplicationById(this.equipmentId)
      .subscribe((data: Application) => {
        data.addressofBusinessthathasGivenorLoaned = data.addressofBusinessthathasGivenorLoaned || <any>{};
        data.addressOfBusinessThatHasRentedorLeased = data.addressofBusinessthathasGivenorLoaned || <any>{};
        data.bcSellersAddress = data.bcSellersAddress || <any>{};
        data.importersAddress = data.importersAddress || <any>{};
        data.outsideBcSellersAddress = data.outsideBcSellersAddress || <any>{};
        data.OriginatingSellersAddress = data.OriginatingSellersAddress || <any>{};

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
            this.router.navigateByUrl(`/equipment-notification/location/${this.equipmentId}`);
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
