import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-reporting-sales-review',
  templateUrl: './reporting-sales-review.component.html',
  styleUrls: ['./reporting-sales-review.component.scss']
})
export class ReportingSalesReviewComponent implements OnInit {
  form: FormGroup;
  formData: any;
  busy: Subscription;
  waiverId: string;

  ownersAndManagers: any[] = [];
  equipmentIdentification: string[];

  constructor(private fb: FormBuilder,
    private route: ActivatedRoute) {
    this.waiverId = this.route.snapshot.params.id;
  }

  ngOnInit() {
    this.form = this.fb.group({
      id: [],
      declarationOfCorrectInformation: ['']
    });

    this.reloadData();
  }

  reloadData() {
      this.formData = {
        id: 0,
        dateOfSale: '10-25-2019',
        typeOfSale: 'Rented',
        typeOfSaleOther: 'Other type of sale',
        rightsToOwnuseOrPossessRetained: true,
        methodOfPayment: 'Visa',
        methodOfPaymentOther: 'Other method of payment',
        whereWillEquipmentReside: 'Outside of BC',
        civicAddressOfPurchaser: {
          id: 0,
          streetLine1: '123 civicaddressofpurchaser st.',
          streetLine2: '',
          city: 'city civicaddressofpurchaser',
          province: 'BC civicaddressofpurchaser',
          postalCode: 'v8p-3l9 civicaddressofpurchaser',
          country: 'Canada civicaddressofpurchaser',
        },
        privateDwelling: 'Yes',
        purchasedByIndividualOrBusiness: 'Business',
        legalNameOfPurchaserIndividual: 'Purchaser Business legal name',
        purchasersCivicAddress: {
          id: '',
          streetLine1: '123 purchasersCivicAddress st.',
          streetLine2: '',
          city: 'city purchasersCivicAddress',
          province: 'province purchasersCivicAddress',
          postalCode: 'v8p-3l0 purchasersCivicAddress',
          country: 'Canada purchasersCivicAddress',
        },
        purchasersTelephoneNumber: '250-360-6111',
        purchasersEmailAddress: 'purchaser@test.com',
        idNumberCollected: 'Yes',
        typeOfIdNumberCollected: 'Drivers Licence',
        nameOfPurchaserBusiness: 'name of purchaser business',
        purchaserRegistrationNumber: 'purchaser reg number 1234',
        purchaserdBaName: 'dba name',
        purchasersBusinessAddress: {
          id: '',
          streetLine1: '123 purchasersBusinessAddress st.',
          streetLine2: '',
          city: 'city purchasersBusinessAddress',
          province: 'province purchasersBusinessAddress',
          postalCode: 'v8p-3l0 purchasersBusinessAddress',
          country: 'Canada purchasersBusinessAddress',
        },
        legalNameOfPersonResponsibleForBusiness: 'legal name of person responsible',
        phoneNumberOfPersonResponsibleForBusiness: '250-360-6222',
        emailOfPersonResponsibleForBusiness: 'personresponsible@test.com',
        geographicalLocationOfBusinessPurchaser: 'Asia',
        isPurchaserAPersonOfBC: 'Yes',
        howIsPurchaseAuthorizedAO: true,
        howIsPurchaserAuthorizedWaiver: true,
        howIsPurchaserAuthorizedRegisteredSeller: true,
        howIsPurchaserAuthorizedOther: true,
        healthCanadaLicenseDEL: true,
        healthCanadaLicenseSiteLicense: true,
        nameOnPurchasersDEL: 'Elias Petterson',
        purchasersDELNumber: '132654798',
        purchasersDELExpiryDate: 'mm-dd-yyyy',
        nameOnPurchasersSiteLicense: 'Elias Petterson',
        purchasersSiteLicenseNumber: '132654798',
        purchasersSiteLicenseExpiryDate: 'mm-dd-yyyy',
        purchasersWaiverNumber: '11112222',
        purchasersRegistrationNumber: '11112223',
        purchasersOther: 'purchaserOther',
      };

      this.equipmentIdentification = [
        'Equipment Type',
        'Equipment type will pre-populate here, i.e. Pill Press',
        'Make',
        'Make will pre-populate here',
        'Model',
        'Model will pre-populate here',
        'Serial Number',
        'Serial Number will pre-populate here',
        'Key Part Serial Number',
        'Key Part Serial Number will pre-populate here',
        'Equipment Registry Number',
        'Equipment Registry Number will pre-populate here'
      ];

      this.form.patchValue(this.formData);
  }

}
