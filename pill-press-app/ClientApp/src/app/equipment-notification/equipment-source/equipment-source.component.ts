import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Subscription, zip } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { ApplicationDataService } from '../../services/application-data.service';
import { Application } from '../../models/application.model';
import { faSave } from '@fortawesome/free-regular-svg-icons';

import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import * as _moment from 'moment';
// tslint:disable-next-line:no-duplicate-imports
import { defaultFormat as _rollupMoment } from 'moment';
import { FormBase } from '../../../app/shared/form-base';
import { postalRegex } from '../../../app/business-profile/business-profile/business-profile.component';
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
export class EquipmentSourceComponent extends FormBase implements OnInit {
  form: FormGroup;
  busy: Subscription;
  equipmentId: string;
  busyPromise: Promise<void>;
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

    });

    this.reloadData();
    this.clearHiddenFields();
  }

  clearHiddenFields() {

    this.form.get('ownedBeforeJan2019').valueChanges
      .subscribe(value => {
        const group = ['purchasedFromBcSeller', 'purchasedFromSellerOutsideOfBc', 'importedToBcByAThirdParty',
          'alternativeOwnershipArrangement', 'iAssembledItMyself', 'howCameIntoPossessionOtherCheck'];
        if (value) {
          group.forEach(field => {
            this.form.get(field).clearValidators();
            this.form.get(field).reset();
          });
        } else {
          group.forEach(field => {
            this.form.get(field).setValidators([this.requiredCheckboxGroupValidator(group)]);
          });
        }
      });

    this.form.get('purchasedFromBcSeller').valueChanges
      .subscribe(value => {
        if (!value) {
          this.form.get('nameOfBcSeller').clearValidators();
          this.form.get('nameOfBcSeller').reset();
          this.form.get('bcSellersContactPhoneNumber').clearValidators();
          this.form.get('bcSellersContactPhoneNumber').reset();
          this.form.get('bcSellersContactEmail').clearValidators();
          this.form.get('bcSellersContactEmail').reset();
          this.form.get('dateOfPurchaseFromBcSeller').clearValidators();
          this.form.get('dateOfPurchaseFromBcSeller').reset();
          this.form.get('bcSellersRegistrationNumber').clearValidators();
          this.form.get('bcSellersRegistrationNumber').reset();

          this.form.get('bcSellersAddress.streetLine1').clearValidators();
          this.form.get('bcSellersAddress.city').clearValidators();
          this.form.get('bcSellersAddress.province').clearValidators();
          this.form.get('bcSellersAddress.postalCode').clearValidators();
          this.form.get('bcSellersAddress').reset();
          this.form.get('bcSellersAddress.province').setValue('British Columbia');
        } else {
          this.form.get('nameOfBcSeller').setValidators([Validators.required]);
          this.form.get('bcSellersContactPhoneNumber').setValidators([]);
          this.form.get('bcSellersContactEmail').setValidators([]);
          this.form.get('dateOfPurchaseFromBcSeller').setValidators([Validators.required]);
          this.form.get('bcSellersRegistrationNumber').setValidators([Validators.required]);

          this.form.get('bcSellersAddress.streetLine1').setValidators([Validators.required]);
          this.form.get('bcSellersAddress.city').setValidators([Validators.required]);
          this.form.get('bcSellersAddress.province').setValidators([Validators.required]);
          this.form.get('bcSellersAddress.postalCode').setValidators([Validators.required, Validators.pattern(postalRegex)]);
        }
      });

    this.form.get('purchasedFromSellerOutsideOfBc').valueChanges
      .subscribe(value => {
        if (!value) {
          this.form.get('outsideBcSellersName').clearValidators();
          this.form.get('outsideBcSellersName').reset();
          this.form.get('outsideBcSellersLocation').clearValidators();
          this.form.get('outsideBcSellersLocation').reset();
          this.form.get('dateOfPurchaseFromOutsideBcSeller').clearValidators();
          this.form.get('dateOfPurchaseFromOutsideBcSeller').reset();

          this.form.get('outsideBcSellersAddress.streetLine1').clearValidators();
          this.form.get('outsideBcSellersAddress.city').clearValidators();
          this.form.get('outsideBcSellersAddress.country').clearValidators();
          this.form.get('outsideBcSellersAddress.province').clearValidators();
          this.form.get('outsideBcSellersAddress.postalCode').clearValidators();
          this.form.get('outsideBcSellersAddress').reset();
        } else {
          this.form.get('outsideBcSellersName').setValidators([Validators.required]);
          this.form.get('dateOfPurchaseFromOutsideBcSeller').setValidators([Validators.required]);

          this.form.get('outsideBcSellersAddress.streetLine1').setValidators([Validators.required]);
          this.form.get('outsideBcSellersAddress.city').setValidators([Validators.required]);
          this.form.get('outsideBcSellersAddress.country').setValidators([Validators.required]);
          this.form.get('outsideBcSellersAddress.province').setValidators([Validators.required]);
          this.form.get('outsideBcSellersAddress.postalCode').setValidators([Validators.required]);
        }
      });

    this.form.get('importedToBcByAThirdParty').valueChanges
      .subscribe(value => {
        if (!value) {
          this.form.get('nameOfImporter').clearValidators();
          this.form.get('nameOfImporter').reset();
          this.form.get('importersRegistrationNumber').clearValidators();
          this.form.get('importersRegistrationNumber').reset();
          this.form.get('nameOfOriginatingSeller').clearValidators();
          this.form.get('nameOfOriginatingSeller').reset();
          this.form.get('originatingSellersLocation').clearValidators();
          this.form.get('originatingSellersLocation').reset();
          this.form.get('dateOfPurchaseFromImporter').clearValidators();
          this.form.get('dateOfPurchaseFromImporter').reset();

          this.form.get('importersAddress.streetLine1').clearValidators();
          this.form.get('importersAddress.city').clearValidators();
          this.form.get('importersAddress.country').clearValidators();
          this.form.get('importersAddress.province').clearValidators();
          this.form.get('importersAddress.postalCode').clearValidators();
          this.form.get('importersAddress').reset();

          this.form.get('OriginatingSellersAddress').reset();
        } else {
          this.form.get('nameOfImporter').setValidators([Validators.required]);
          this.form.get('dateOfPurchaseFromImporter').setValidators([Validators.required]);

          this.form.get('importersAddress.streetLine1').setValidators([Validators.required]);
          this.form.get('importersAddress.city').setValidators([Validators.required]);
          this.form.get('importersAddress.country').setValidators([Validators.required]);
          this.form.get('importersAddress.province').setValidators([Validators.required]);
          this.form.get('importersAddress.postalCode').setValidators([Validators.required]);
        }
      });

    this.form.get('alternativeOwnershipArrangement').valueChanges
      .subscribe(value => {
        const group = ['kindOfAlternateOwnershipOtherCheck', 'possessUntilICanSell',
          'giveNorLoanedToMe', 'rentingOrLeasingFromAnotherBusiness'];
        if (!value) {
          group.forEach(field => {
            this.form.get(field).clearValidators();
            this.form.get(field).reset();
          });
        } else {
          group.forEach(field => {
            this.form.get(field).setValidators([this.requiredCheckboxGroupValidator(group)]);
          });
        }
      });

    this.form.get('kindOfAlternateOwnershipOtherCheck').valueChanges
      .subscribe(value => {
        if (!value) {
          this.form.get('kindOfAlternateOwnershipOther').clearValidators();
          this.form.get('kindOfAlternateOwnershipOther').reset();
        } else {
          this.form.get('kindOfAlternateOwnershipOther').setValidators([Validators.required]);
        }
      });

    this.form.get('possessUntilICanSell').valueChanges
      .subscribe(value => {
        if (!value) {
          this.form.get('usingToManufactureAProduct').clearValidators();
          this.form.get('usingToManufactureAProduct').reset();
          this.form.get('areYouARegisteredSeller').clearValidators();
          this.form.get('areYouARegisteredSeller').reset();
        } else {
          this.form.get('usingToManufactureAProduct').setValidators([Validators.required]);
          this.form.get('areYouARegisteredSeller').setValidators([Validators.required]);
        }
      });

    this.form.get('giveNorLoanedToMe').valueChanges
      .subscribe(value => {
        if (!value) {
          this.form.get('emailOfTheBusinessThatHasGivenOrLoaned').clearValidators();
          this.form.get('emailOfTheBusinessThatHasGivenOrLoaned').reset();
          this.form.get('phoneofbusinessthathasgivenorloaned').clearValidators();
          this.form.get('phoneofbusinessthathasgivenorloaned').reset();
          this.form.get('whyHaveYouAcceptedOrBorrowed').clearValidators();
          this.form.get('whyHaveYouAcceptedOrBorrowed').reset();
          this.form.get('nameOfBusinessThatHasGivenOrLoaned').clearValidators();
          this.form.get('nameOfBusinessThatHasGivenOrLoaned').reset();

          this.form.get('addressofBusinessthathasGivenorLoaned.streetLine1').clearValidators();
          this.form.get('addressofBusinessthathasGivenorLoaned.city').clearValidators();
          this.form.get('addressofBusinessthathasGivenorLoaned.country').clearValidators();
          this.form.get('addressofBusinessthathasGivenorLoaned.province').clearValidators();
          this.form.get('addressofBusinessthathasGivenorLoaned.postalCode').clearValidators();
          this.form.get('addressofBusinessthathasGivenorLoaned').reset();
        } else {
          this.form.get('nameOfBusinessThatHasGivenOrLoaned').setValidators([Validators.required]);

          this.form.get('addressofBusinessthathasGivenorLoaned.streetLine1').setValidators([Validators.required]);
          this.form.get('addressofBusinessthathasGivenorLoaned.city').setValidators([Validators.required]);
          this.form.get('addressofBusinessthathasGivenorLoaned.country').setValidators([Validators.required]);
          this.form.get('addressofBusinessthathasGivenorLoaned.province').setValidators([Validators.required]);
          this.form.get('addressofBusinessthathasGivenorLoaned.postalCode').setValidators([Validators.required]);
        }
      });

    this.form.get('rentingOrLeasingFromAnotherBusiness').valueChanges
      .subscribe(value => {
        if (!value) {
          this.form.get('emailOfBusinessThatHasRentedOrLeased').clearValidators();
          this.form.get('emailOfBusinessThatHasRentedOrLeased').reset();
          this.form.get('phoneOfBusinessThatHasRentedOrLeased').clearValidators();
          this.form.get('phoneOfBusinessThatHasRentedOrLeased').reset();
          this.form.get('whyHaveYouRentedOrLeased').clearValidators();
          this.form.get('whyHaveYouRentedOrLeased').reset();
          this.form.get('NameOfBusinessThatHasRentedOrLeased').clearValidators();
          this.form.get('NameOfBusinessThatHasRentedOrLeased').reset();

          this.form.get('addressOfBusinessThatHasRentedorLeased.streetLine1').clearValidators();
          this.form.get('addressOfBusinessThatHasRentedorLeased.city').clearValidators();
          this.form.get('addressOfBusinessThatHasRentedorLeased.country').clearValidators();
          this.form.get('addressOfBusinessThatHasRentedorLeased.province').clearValidators();
          this.form.get('addressOfBusinessThatHasRentedorLeased.postalCode').clearValidators();
          this.form.get('addressOfBusinessThatHasRentedorLeased').reset();
        } else {
          this.form.get('NameOfBusinessThatHasRentedOrLeased').setValidators([Validators.required]);

          this.form.get('addressOfBusinessThatHasRentedorLeased.streetLine1').setValidators([Validators.required]);
          this.form.get('addressOfBusinessThatHasRentedorLeased.city').setValidators([Validators.required]);
          this.form.get('addressOfBusinessThatHasRentedorLeased.country').setValidators([Validators.required]);
          this.form.get('addressOfBusinessThatHasRentedorLeased.province').setValidators([Validators.required]);
          this.form.get('addressOfBusinessThatHasRentedorLeased.postalCode').setValidators([Validators.required]);
        }
      });


    this.form.get('iAssembledItMyself').valueChanges
      .subscribe(value => {
        if (!value) {
          this.form.get('whenDidYouAssembleEquipment').clearValidators();
          this.form.get('whenDidYouAssembleEquipment').reset();
          this.form.get('whereDidYouObtainParts').clearValidators();
          this.form.get('whereDidYouObtainParts').reset();
          this.form.get('doYouAssembleForOtherBusinesses').clearValidators();
          this.form.get('doYouAssembleForOtherBusinesses').reset();
        } else {
          this.form.get('whenDidYouAssembleEquipment').setValidators([Validators.required]);
          this.form.get('whereDidYouObtainParts').setValidators([Validators.required]);
          this.form.get('doYouAssembleForOtherBusinesses').setValidators([Validators.required]);
        }
      });

    this.form.get('doYouAssembleForOtherBusinesses').valueChanges
      .subscribe(value => {
        if (!value) {
          this.form.get('detailsOfAssemblyForOtherBusinesses').clearValidators();
          this.form.get('detailsOfAssemblyForOtherBusinesses').reset();
        } else {
          this.form.get('detailsOfAssemblyForOtherBusinesses').setValidators([Validators.required]);
        }
      });

    this.form.get('howCameIntoPossessionOtherCheck').valueChanges
      .subscribe(value => {
        if (!value) {
          this.form.get('detailsOfHowEquipmentCameIntoPossession').clearValidators();
          this.form.get('detailsOfHowEquipmentCameIntoPossession').reset();
        } else {
          this.form.get('detailsOfHowEquipmentCameIntoPossession').setValidators([Validators.required]);
        }
      });

  }

  reloadData() {
    this.busy = this.applicationDataService.getApplicationById(this.equipmentId)
      .subscribe((data: Application) => {
        data.addressofBusinessthathasGivenorLoaned = data.addressofBusinessthathasGivenorLoaned || <any>{};
        data.addressOfBusinessThatHasRentedorLeased = data.addressofBusinessthathasGivenorLoaned || <any>{};
        data.bcSellersAddress = data.bcSellersAddress || <any>{};
        data.bcSellersAddress.province = data.bcSellersAddress.province || 'British Columbia';
        data.importersAddress = data.importersAddress || <any>{};
        data.outsideBcSellersAddress = data.outsideBcSellersAddress || <any>{};
        data.OriginatingSellersAddress = data.OriginatingSellersAddress || <any>{};

        this.form.patchValue(data);
      }, error => {
        // debugger;
      });
  }

  arePossessionCheckValid() {
    const result = (
      this.form.get('purchasedFromBcSeller').valid
      || this.form.get('purchasedFromSellerOutsideOfBc').valid
      || this.form.get('alternativeOwnershipArrangement').valid
      || this.form.get('iAssembledItMyself').valid
      || this.form.get('howCameIntoPossessionOtherCheck').valid
    )
      || !(
        this.form.get('purchasedFromBcSeller').touched
        || this.form.get('purchasedFromSellerOutsideOfBc').touched
        || this.form.get('alternativeOwnershipArrangement').touched
        || this.form.get('iAssembledItMyself').touched
        || this.form.get('howCameIntoPossessionOtherCheck').touched
      );
    return result;
  }

  areAltArrangementChecksValid() {
    const result = (
      this.form.get('kindOfAlternateOwnershipOtherCheck').valid
      || this.form.get('possessUntilICanSell').valid
      || this.form.get('giveNorLoanedToMe').valid
      || this.form.get('rentingOrLeasingFromAnotherBusiness').valid
    )
      || !(
        this.form.get('kindOfAlternateOwnershipOtherCheck').touched
        || this.form.get('possessUntilICanSell').touched
        || this.form.get('giveNorLoanedToMe').touched
        || this.form.get('rentingOrLeasingFromAnotherBusiness').touched
      );
    return result;
  }

  markAsTouched() {
    let controls = this.form.controls;
    // tslint:disable-next-line:forin
    for (const c in controls) {
      controls[c].markAsTouched();
      controls[c].updateValueAndValidity();
    }

    controls = (<FormGroup>this.form.get('addressofBusinessthathasGivenorLoaned')).controls;
    // tslint:disable-next-line:forin
    for (const c in controls) {
      controls[c].markAsTouched();
      controls[c].updateValueAndValidity();
    }
    controls = (<FormGroup>this.form.get('addressOfBusinessThatHasRentedorLeased')).controls;
    // tslint:disable-next-line:forin
    for (const c in controls) {
      controls[c].markAsTouched();
      controls[c].updateValueAndValidity();
    }
    controls = (<FormGroup>this.form.get('bcSellersAddress')).controls;
    // tslint:disable-next-line:forin
    for (const c in controls) {
      controls[c].markAsTouched();
      controls[c].updateValueAndValidity();
    }
    controls = (<FormGroup>this.form.get('importersAddress')).controls;
    // tslint:disable-next-line:forin
    for (const c in controls) {
      controls[c].markAsTouched();
      controls[c].updateValueAndValidity();
    }
    controls = (<FormGroup>this.form.get('outsideBcSellersAddress')).controls;
    // tslint:disable-next-line:forin
    for (const c in controls) {
      controls[c].markAsTouched();
      controls[c].updateValueAndValidity();
    }
    controls = (<FormGroup>this.form.get('OriginatingSellersAddress')).controls;
    // tslint:disable-next-line:forin
    for (const c in controls) {
      controls[c].markAsTouched();
      controls[c].updateValueAndValidity();
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
