import { Component, OnInit } from '@angular/core';
import { FormBase } from '../../../shared/form-base';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subscription, zip } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { ApplicationDataService } from '../../../services/application-data.service';
import { postalRegex } from '../../../business-profile/business-profile/business-profile.component';
import { faInfoCircle, faExclamationCircle, faExclamationTriangle } from '@fortawesome/free-solid-svg-icons';
import { faSave } from '@fortawesome/free-regular-svg-icons';
import { startWith, tap, delay } from 'rxjs/operators';

@Component({
  selector: 'app-reporting-sales-form',
  templateUrl: './reporting-sales-form.component.html',
  styleUrls: ['./reporting-sales-form.component.scss']
})
export class ReportingSalesFormComponent extends FormBase implements OnInit {
  form: FormGroup;
  busy: Subscription;
  applicationId: string;
  busyPromise: Promise<any>;
  locations: any;

  faInfoCircle = faInfoCircle;
  faExclamationCircle = faExclamationCircle;
  faExclamationTriangle = faExclamationTriangle;
  faSave = faSave;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private applicationDataService: ApplicationDataService,
    private fb: FormBuilder) {
    super();
    this.applicationId = this.route.snapshot.params.id;
  }

  ngOnInit() {
    this.form = this.fb.group({
      id: [],
      equipmentRecord: this.fb.group({
        id: [],
        equipmentType: [],
        equipmentTypeOther: [],
        name: [],
        pillpressEncapsulatorSize: [],
        pillpressEncapsulatorSizeOther: [],
        levelOfEquipmentAutomation: [],
        pillpressMaxCapacity: [],
        howWasEquipmentBuilt: [],
        HhwWasEquipmentBuiltOther: [],
        nameOfManufacturer: [],
        equipmentMake: [],
        equipmentModel: [],
        serialNumber: [],
        encapsulatorMaxCapacity: [],
        customBuiltSerialNumber: [],
      }),
      dateOfSale: ['', Validators.required],
      typeOfSale: ['', Validators.required],
      typeOfSaleOther: ['', this.requiredSelectChildValidator('typeOfSale', ['Other'])],
      rightsToOwnuseOrPossessRetained: ['', Validators.required],
      methodOfPayment: ['', Validators.required],
      methodOfPaymentOther: ['', this.requiredSelectChildValidator('methodOfPayment', ['Other'])],
      whereWillEquipmentReside: ['', Validators.required],
      civicAddressOfPurchaser: this.fb.group({
        id: [],
        streetLine1: ['', Validators.required],
        streetLine2: [],
        city: ['', Validators.required],
        province: [],
        postalCode: ['', [Validators.required, Validators.pattern(postalRegex)]],
        country: [],
      }),
      privateDwelling: [],
      purchasedByIndividualOrBusiness: ['', Validators.required],
      legalNameOfPurchaserIndividual: [],
      purchasersCivicAddress: this.fb.group({
        id: [],
        streetLine1: [],
        streetLine2: [],
        city: [],
        province: [],
        postalCode: [],
        country: [],
      }),
      purchasersTelephoneNumber: [],
      purchasersEmailAddress: [],
      idNumberCollected: [],
      typeOfIdNumberCollected: [],
      nameOfPurchaserBusiness: [],
      purchaserRegistrationNumber: [],
      purchaserdBaName: [],
      purchasersBusinessAddress: this.fb.group({
        id: [],
        streetLine1: [],
        streetLine2: [],
        city: [],
        province: [],
        postalCode: [],
        country: [],
      }),
      legalNameOfPersonResponsibleForBusiness: [],
      phoneNumberOfPersonResponsibleForBusiness: [],
      emailOfPersonResponsibleForBusiness: [],
      geographicalLocationOfBusinessPurchaser: [],
      isPurchaserAPersonOfBC: [],
      howIsPurchaseAuthorizedAO: [],
      howIsPurchaserAuthorizedWaiver: [],
      howIsPurchaserAuthorizedRegisteredSeller: [],
      howIsPurchaserAuthorizedOtherCheck: [],
      howIsPurchaserAuthorizedOther: [],
      healthCanadaLicenseDEL: [],
      healthCanadaLicenseSiteLicense: [],
      nameOnPurchasersDEL: [],
      purchasersDELNumber: [],
      purchasersDELExpiryDate: [],
      nameOnPurchasersSiteLicense: [],
      purchasersSiteLicenseNumber: [],
      purchasersSiteLicenseExpDate: [],
      purchasersWaiverNumber: [],
      purchasersRegistrationNumber: [],
    });

    this.reloadData();
    this.clearHiddenFields();
  }

  reloadData() {
    this.busy = this.applicationDataService.getApplicationById(this.applicationId)
      .subscribe((data: any) => {

        data.certificates = data.certificates || [];
        if (data.certificates.length > 0) {
          data.certificates.sort(this.dateSort);
          data.equipmentRegistryNumber = data.certificates[0].name;
        }
        data.civicAddressOfPurchaser = data.civicAddressOfPurchaser || <any>{};
        data.purchasersCivicAddress = data.purchasersCivicAddress || <any>{};
        data.purchasersBusinessAddress = data.purchasersBusinessAddress || <any>{};
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

  /**
   * Clear child and hidden fields
   */
  clearHiddenFields() {

    this.form.get('whereWillEquipmentReside').valueChanges
      .subscribe(value => {
        const field = 'civicAddressOfPurchaser.province';
        if (value === "true" || value === true) { //BC --> true
          this.form.get(field).reset();
          this.form.get(field).setValue('British Columbia');
          this.form.get(field).markAsTouched();
          //this.form.get(field).disable();
          //this.form.get(field).clearValidators();
          //this.form.get(field).clearAsyncValidators();
          this.form.get(field).updateValueAndValidity();
        } else {
          this.form.get(field).reset();
          //this.form.get(field).enable();
          this.form.get(field).setValidators(Validators.required);
          this.form.get(field).updateValueAndValidity();
        }
      });

    //**********************
    this.form.get('purchasedByIndividualOrBusiness').valueChanges
      .subscribe(value => {

        const individualGroup = ['legalNameOfPurchaserIndividual', 'purchasersCivicAddress', 'purchasersTelephoneNumber',
          'purchasersEmailAddress', 'idNumberCollected', 'typeOfIdNumberCollected'];
        const businessGroup = ['nameOfPurchaserBusiness', 'purchaserRegistrationNumber', 'purchaserdBaName',
          'purchasersBusinessAddress', 'legalNameOfPersonResponsibleForBusiness',
          'phoneNumberOfPersonResponsibleForBusiness', 'emailOfPersonResponsibleForBusiness',
          'geographicalLocationOfBusinessPurchaser'];
        const purchasersCivicAddressGroup = ['purchasersCivicAddress.streetLine1', 'purchasersCivicAddress.city',
          'purchasersCivicAddress.postalCode', 'purchasersCivicAddress.province', 'purchasersCivicAddress.country'];
        const purchasersBusinessAddressGroup = ['purchasersBusinessAddress.streetLine1', 'purchasersBusinessAddress.city',
          'purchasersBusinessAddress.postalCode', 'purchasersBusinessAddress.province', 'purchasersBusinessAddress.country'];

        if (value === true) { // individual = true, business = false
          // clear all business fields and set required by individual
          businessGroup.forEach(field => {
            this.form.get(field).clearValidators();
            this.form.get(field).reset();
            this.form.get(field).updateValueAndValidity();
          });
          purchasersBusinessAddressGroup.forEach(field => {
            this.form.get(field).clearValidators();
            this.form.get(field).reset();
            this.form.get(field).updateValueAndValidity();
          });
          this.form.get('legalNameOfPurchaserIndividual').setValidators([Validators.required]);
          this.form.get('purchasersCivicAddress').get('streetLine1').setValidators(Validators.required);
          this.form.get('purchasersCivicAddress').get('city').setValidators(Validators.required);
          this.form.get('purchasersCivicAddress').get('postalCode').setValidators([Validators.required, Validators.pattern(postalRegex)]);
          this.form.get('purchasersCivicAddress').get('province').setValidators(Validators.required);
          this.form.get('purchasersCivicAddress').get('country').setValidators(Validators.required);
          this.form.get('purchasersTelephoneNumber').setValidators([Validators.required, Validators.minLength(10), Validators.maxLength(10)]);
          this.form.get('purchasersEmailAddress').setValidators([Validators.required, Validators.email]);
          this.form.get('idNumberCollected').setValidators([Validators.required]);
          this.form.get('typeOfIdNumberCollected').setValidators([Validators.required]);
          individualGroup.forEach(field => {
            this.form.get(field).updateValueAndValidity();
          });
          purchasersCivicAddressGroup.forEach(field => {
            this.form.get(field).updateValueAndValidity();
          });
        } else {
          // clear all individual fields and set required by business
          individualGroup.forEach(field => {
            this.form.get(field).clearValidators();
            this.form.get(field).reset();
            this.form.get(field).updateValueAndValidity();
          });
          purchasersCivicAddressGroup.forEach(field => {
            this.form.get(field).clearValidators();
            this.form.get(field).reset();
            this.form.get(field).updateValueAndValidity();
          });
          this.form.get('nameOfPurchaserBusiness').setValidators([Validators.required]);
          this.form.get('purchaserRegistrationNumber').setValidators([Validators.required]);
          this.form.get('purchaserdBaName').setValidators([Validators.required]);
          this.form.get('purchasersBusinessAddress').get('streetLine1').setValidators(Validators.required);
          this.form.get('purchasersBusinessAddress').get('city').setValidators(Validators.required);
          this.form.get('purchasersBusinessAddress').get('postalCode').setValidators([Validators.required, Validators.pattern(postalRegex)]);
          this.form.get('purchasersBusinessAddress').get('province').setValidators(Validators.required);
          this.form.get('purchasersBusinessAddress').get('country').setValidators(Validators.required);
          this.form.get('legalNameOfPersonResponsibleForBusiness').setValidators([Validators.required]);
          this.form.get('phoneNumberOfPersonResponsibleForBusiness').setValidators([Validators.required, Validators.minLength(10), Validators.maxLength(10)]);
          this.form.get('emailOfPersonResponsibleForBusiness').setValidators([Validators.required, Validators.email]);
          this.form.get('geographicalLocationOfBusinessPurchaser').setValidators([Validators.required]);
          businessGroup.forEach(field => {
            this.form.get(field).updateValueAndValidity();
          });
          purchasersBusinessAddressGroup.forEach(field => {
            this.form.get(field).updateValueAndValidity();
          });
        }
      });

    //**********************
    this.form.get('isPurchaserAPersonOfBC').valueChanges
      .subscribe(value => {

        const purchaserAPersonOfBCGroup = ['howIsPurchaseAuthorizedAO', 'howIsPurchaserAuthorizedWaiver',
          'howIsPurchaserAuthorizedRegisteredSeller', 'howIsPurchaserAuthorizedOtherCheck'];

        if (!value) {
          purchaserAPersonOfBCGroup.forEach(field => {
            this.form.get(field).clearValidators();
            this.form.get(field).reset();
            this.form.get(field).updateValueAndValidity();
          });
        } else {
          purchaserAPersonOfBCGroup.forEach(field => {
            this.form.get(field).setValidators([this.requiredCheckboxGroupValidator(purchaserAPersonOfBCGroup)]);
            this.form.get(field).updateValueAndValidity();
          });
        }
      });

    //**********************
    this.form.get('howIsPurchaseAuthorizedAO').valueChanges
      .subscribe(value => {

        const purchaseAuthorizedAOGroup = ['healthCanadaLicenseDEL', 'healthCanadaLicenseSiteLicense'];

        if (!value) {
          purchaseAuthorizedAOGroup.forEach(field => {
            this.form.get(field).clearValidators();
            this.form.get(field).reset();
            this.form.get(field).updateValueAndValidity();
          });
        } else {
          purchaseAuthorizedAOGroup.forEach(field => {
            this.form.get(field).setValidators(this.requiredCheckboxGroupValidator(purchaseAuthorizedAOGroup));
            this.form.get(field).updateValueAndValidity();
          });
        }

      });

    //**********************
    this.form.get('healthCanadaLicenseDEL').valueChanges
      .subscribe(value => {

        const healthCanadaLicenseDELGroup = ['nameOnPurchasersDEL', 'purchasersDELNumber', 'purchasersDELExpiryDate'];

        if (!value) {
          healthCanadaLicenseDELGroup.forEach(field => {
            this.form.get(field).clearValidators();
            this.form.get(field).reset();
            this.form.get(field).updateValueAndValidity();
          });
        } else {
          healthCanadaLicenseDELGroup.forEach(field => {
            this.form.get(field).setValidators(Validators.required);
            this.form.get(field).updateValueAndValidity();
          });
        }

      });

    //**********************
    this.form.get('healthCanadaLicenseSiteLicense').valueChanges
      .subscribe(value => {

        const healthCanadaLicenseSiteLicenseGroup = ['nameOnPurchasersSiteLicense', 'purchasersSiteLicenseNumber',
          'purchasersSiteLicenseExpDate'];

        if (!value) {
          healthCanadaLicenseSiteLicenseGroup.forEach(field => {
            this.form.get(field).clearValidators();
            this.form.get(field).reset();
            this.form.get(field).updateValueAndValidity();
          });
        } else {
          healthCanadaLicenseSiteLicenseGroup.forEach(field => {
            this.form.get(field).setValidators(Validators.required);
            this.form.get(field).updateValueAndValidity();
          });
        }

      });

    //**********************
    this.form.get('howIsPurchaserAuthorizedWaiver').valueChanges
      .subscribe(value => {
        const field = 'purchasersWaiverNumber';
        if (!value) {
          this.form.get(field).clearValidators();
          this.form.get(field).reset();
          this.form.get(field).updateValueAndValidity();
        } else {
          this.form.get(field).setValidators(Validators.required);
          this.form.get(field).updateValueAndValidity();
        }
      });

    //**********************
    this.form.get('howIsPurchaserAuthorizedRegisteredSeller').valueChanges
      .subscribe(value => {
        const field = 'purchasersRegistrationNumber';
        if (!value) {
          this.form.get(field).clearValidators();
          this.form.get(field).reset();
          this.form.get(field).updateValueAndValidity();
        } else {
          this.form.get(field).setValidators(Validators.required);
          this.form.get(field).updateValueAndValidity();
        }
      });

    //**********************
    this.form.get('howIsPurchaserAuthorizedOtherCheck').valueChanges
      .subscribe(value => {
        const field = 'howIsPurchaserAuthorizedOther';
        if (!value) {
          this.form.get(field).clearValidators();
          this.form.get(field).reset();
          this.form.get(field).updateValueAndValidity();
        } else {
          this.form.get(field).setValidators([Validators.required]);
          this.form.get(field).updateValueAndValidity();
        }

      });

    //**********************
    this.form.get('typeOfSale').valueChanges
      .subscribe(value => {
        const field = 'typeOfSaleOther';
        if (value === 'Other') {
          this.form.get(field).setValidators([Validators.required]);
          this.form.get(field).updateValueAndValidity();
        } else {
          this.form.get(field).clearValidators();
          this.form.get(field).reset();
          this.form.get(field).updateValueAndValidity();
        }
      });

    //**********************
    this.form.get('methodOfPayment').valueChanges
      .subscribe(value => {
        if (value === 'Other') {
          this.form.get('methodOfPaymentOther').setValidators(Validators.required);
          this.form.get('methodOfPaymentOther').updateValueAndValidity();
        } else {
          this.form.get('methodOfPaymentOther').clearValidators();
          this.form.get('methodOfPaymentOther').reset();
          this.form.get('methodOfPaymentOther').updateValueAndValidity();
        }
      });

    //**********************
    this.form.get('idNumberCollected').valueChanges
      .subscribe(value => {
        if (value === true) {
          this.form.get('typeOfIdNumberCollected').setValidators(Validators.required);
          this.form.get('typeOfIdNumberCollected').updateValueAndValidity();
        } else {
          this.form.get('typeOfIdNumberCollected').clearValidators();
          this.form.get('typeOfIdNumberCollected').reset();
          this.form.get('typeOfIdNumberCollected').updateValueAndValidity();
        }
      });
  }

  /**
   * Validate that at least one checkbox has been selected
   */
  howIsPurchaserAuthorizedValid() {
    const result = (
      this.form.get('howIsPurchaseAuthorizedAO').valid
      || this.form.get('howIsPurchaserAuthorizedWaiver').valid
      || this.form.get('howIsPurchaserAuthorizedRegisteredSeller').valid
      || this.form.get('howIsPurchaserAuthorizedOtherCheck').valid
    )
      ||
      !(this.form.get('howIsPurchaseAuthorizedAO').touched
      || this.form.get('howIsPurchaserAuthorizedWaiver').touched
      || this.form.get('howIsPurchaserAuthorizedRegisteredSeller').touched
      || this.form.get('howIsPurchaserAuthorizedOtherCheck').touched);
    //console.log('howIsPurchaserAuthorizedValid: ' + result);
    return result;
  }

  /**
   * Validate that at least one checkbox has been selected
   */
  isPurchaserHealthCanadaLicenceValid() {
    const result = (
      this.form.get('healthCanadaLicenseDEL').valid
      || this.form.get('healthCanadaLicenseSiteLicense').valid
    )
      ||
      !(this.form.get('healthCanadaLicenseDEL').touched
      || this.form.get('healthCanadaLicenseSiteLicense').touched);
    //console.log('isPurchaserHealthCanadaLicenceValid: ' + result);
    return result;
  }

  /**
   * Save form values in dynamics
   * @param goToReview
   */
  save(goToReview: boolean) {
    if (this.form.valid || goToReview === false) {
      //console.log('valid');
      //for (var c in this.form.controls) { console.log(c + '= ' + this.form.get(c).value) };
      const value = this.form.value;
      const saveList = [this.applicationDataService.updateApplication(value)];
      this.busyPromise = zip(...saveList)
        .toPromise()
        .then(res => {
          if (goToReview) {
            this.router.navigateByUrl(`/equipment-changes/reporting-sales/review/${this.applicationId}`);
          } else {
            this.router.navigateByUrl(`/dashboard`);
          }
        }, err => {
          // todo: show errors;
        });
        
    } else {
      //for (var c in this.form.controls) { if (!this.form.get(c).valid) { console.log('Invalid: ' + c) } };
    }
  }

  /**
   * Close the application without saving any data and navigate to dashboard
   */
  cancelAndclose() {
    this.router.navigateByUrl(`/dashboard`);
  }

  /**
   *
   */
  markAsTouched() {
    
    let controls = this.form.controls;
    for (const c in controls) {
      controls[c].markAsTouched();
      controls[c].updateValueAndValidity();
    }

    controls = (<FormGroup>this.form.get('civicAddressOfPurchaser')).controls;
    for (const c in controls) {
      controls[c].markAsTouched();
      controls[c].updateValueAndValidity();
    }
    controls = (<FormGroup>this.form.get('purchasersCivicAddress')).controls;
    for (const c in controls) {
      controls[c].markAsTouched();
      controls[c].updateValueAndValidity();
    }
    controls = (<FormGroup>this.form.get('purchasersBusinessAddress')).controls;
    for (const c in controls) {
      controls[c].markAsTouched();
      controls[c].updateValueAndValidity();
    }

  }

}
