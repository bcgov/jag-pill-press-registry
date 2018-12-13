import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Subscription, zip } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { ApplicationDataService } from '../../services/adoxio-application-data.service';
import { Application } from '../../models/application.model';

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

      // BC Seller fields
      purchasedFromBcSeller: [],
      purchasedFromSellerOutsideOfBc: [],
      importedToBcByAThirdParty: [],
      alternativeOwnershipArrangement: [],
      iAssembledItMyself: [],
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

      // I assembled it myself fields
      whenDidYouAssembleEquipment: [],
      whereDidYouObtainParts: [],
      doYouAssembleForOtherBusinesses: [],
      detailsOfAssemblyForOtherBusinesses: [],
      howCameIntoPossessionOtherCheck: [],
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
  }

  reloadData() {
    this.busy = this.applicationDataService.getApplicationById(this.equipmentId)
      .subscribe((data: Application) => {
        this.form.patchValue(data);
      }, error => {
        // debugger;
      });
  }
  markAsTouched() {

  }

  save(gotToReview: boolean) {
    if (this.form.valid || gotToReview === false) {
      const value = this.form.value;
      const saveList = [this.applicationDataService.updateApplication(value)];
      this.busyPromise = zip(...saveList)
        .toPromise()
        .then(res => {
          if (gotToReview) {
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
