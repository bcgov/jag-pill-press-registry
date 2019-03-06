import { Component, OnInit } from '@angular/core';
import { FormBase } from '../../../shared/form-base';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subscription, zip } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { ApplicationDataService } from '../../../services/application-data.service';
import { postalRegex } from '../../../business-profile/business-profile/business-profile.component';
import { faInfoCircle, faExclamationCircle, faExclamationTriangle } from '@fortawesome/free-solid-svg-icons';

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
        province: [''],
        postalCode: ['', [Validators.required, Validators.pattern(postalRegex)]],
        country: [''],
      }),
      privateDwelling: [''],
      purchasedByIndividualOrBusiness: ['', Validators.required],
      legalNameOfPurchaserIndividual: [''],
      purchasersCivicAddress: this.fb.group({
        id: [],
        streetLine1: [''],
        streetLine2: [],
        city: [''],
        province: [''],
        postalCode: [''],
        country: [''],
      }),
      purchasersTelephoneNumber: [''],
      purchasersEmailAddress: [''],
      idNumberCollected: [''],
      typeOfIdNumberCollected: [''],
      nameOfPurchaserBusiness: [''],
      purchaserRegistrationNumber: [''],
      purchaserdBaName: [''],
      purchasersBusinessAddress: this.fb.group({
        id: [],
        streetLine1: [''],
        streetLine2: [],
        city: [''],
        province: [''],
        postalCode: [''],
        country: [''],
      }),
      legalNameOfPersonResponsibleForBusiness: [''],
      phoneNumberOfPersonResponsibleForBusiness: [''],
      emailOfPersonResponsibleForBusiness: [''],
      geographicalLocationOfBusinessPurchaser: [''],
      isPurchaserAPersonOfBC: [''],
      howIsPurchaseAuthorizedAO: [''],
      howIsPurchaserAuthorizedWaiver: [''],
      howIsPurchaserAuthorizedRegisteredSeller: [''],
      howIsPurchaserAuthorizedOther: [''],
      healthCanadaLicenseDEL: [''],
      healthCanadaLicenseSiteLicense: [''],
      nameOnPurchasersDEL: [''],
      purchasersDELNumber: [''],
      purchasersDELExpiryDate: [''],
      nameOnPurchasersSiteLicense: [''],
      purchasersSiteLicenseNumber: [''],
      purchasersSiteLicenseExpiryDate: [''],
      purchasersWaiverNumber: [''],
      purchasersRegistrationNumber: [''],
      purchasersOther: [''],
    });

    this.clearHiddenFields();
    this.reloadData();
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

    const individualGroup = ['legalNameOfPurchaserIndividual', 'purchasersCivicAddress', 'purchasersTelephoneNumber',
      'purchasersEmailAddress', 'idNumberCollected', 'typeOfIdNumberCollected'];

    const businessGroup = ['nameOfPurchaserBusiness',
      'purchaserRegistrationNumber', 'purchaserdBaName', 'purchasersBusinessAddress', 'legalNameOfPersonResponsibleForBusiness',
      'phoneNumberOfPersonResponsibleForBusiness', 'emailOfPersonResponsibleForBusiness', 'geographicalLocationOfBusinessPurchaser'];

    const purchaserAPersonOfBCGroup = ['howIsPurchaseAuthorizedAO', 'howIsPurchaserAuthorizedWaiver',
      'howIsPurchaserAuthorizedRegisteredSeller', 'howIsPurchaserAuthorizedOther'];

    const purchaseAuthorizedAOGroup = ['healthCanadaLicenseDEL', 'healthCanadaLicenseSiteLicense'];

    const childPurchaseAuthorizedAOGroup = ['nameOnPurchasersDEL', 'purchasersDELNumber', 'purchasersDELExpiryDate',
      'nameOnPurchasersSiteLicense', 'purchasersSiteLicenseNumber', 'purchasersSiteLicenseExpiryDate'];

    const healthCanadaLicenseDELGroup = ['nameOnPurchasersDEL', 'purchasersDELNumber', 'purchasersDELExpiryDate'];

    const healthCanadaLicenseSiteLicenseGroup = ['nameOnPurchasersSiteLicense', 'purchasersSiteLicenseNumber', 'purchasersSiteLicenseExpiryDate'];


    this.form.get('whereWillEquipmentReside').valueChanges
      .subscribe(value => {
        if (value === "true") { //BC --> true
          this.form.get('civicAddressOfPurchaser.province').reset();
          this.form.get('civicAddressOfPurchaser.province').setValue('British Columbia');
          this.form.get('civicAddressOfPurchaser.province').disable();
        } else {
          this.form.get('civicAddressOfPurchaser.province').reset();
          this.form.get('civicAddressOfPurchaser.province').enable();
        }
      });

    this.form.get('purchasedByIndividualOrBusiness').valueChanges
      .subscribe(value => {

        individualGroup.forEach(field => {
          this.form.get(field).clearValidators();
          this.form.get(field).reset();
        });

        businessGroup.forEach(field => {
          this.form.get(field).clearValidators();
          this.form.get(field).reset();
        });

        if (value === true) { //business = false, individual = true
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
        } else {
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
        }
      });

    this.form.get('isPurchaserAPersonOfBC').valueChanges
      .subscribe(value => {
        purchaserAPersonOfBCGroup.forEach(field => {
          this.form.get(field).clearValidators();
          this.form.get(field).reset();
        });
      });

    this.form.get('howIsPurchaseAuthorizedAO').valueChanges
      .subscribe(value => {
        purchaseAuthorizedAOGroup.forEach(field => {
          this.form.get(field).clearValidators();
          this.form.get(field).reset();
          if (value) {
            //this.form.get(field).setValidators([this.requiredCheckboxGroupValidator(group)]);
          }
        });
        childPurchaseAuthorizedAOGroup.forEach(field => {
          this.form.get(field).clearValidators();
          this.form.get(field).reset();
          if (value) {
            //this.form.get(field).setValidators([this.requiredCheckboxGroupValidator(group)]);
          }
        });
      });

    this.form.get('healthCanadaLicenseDEL').valueChanges
      .subscribe(value => {
        healthCanadaLicenseDELGroup.forEach(field => {
            this.form.get(field).clearValidators();
          this.form.get(field).reset();
          if (value) {
            //this.form.get(field).setValidators([Validators.required]);
          }
        });
      });

    this.form.get('healthCanadaLicenseSiteLicense').valueChanges
      .subscribe(value => {
        healthCanadaLicenseSiteLicenseGroup.forEach(field => {
            this.form.get(field).clearValidators();
          this.form.get(field).reset();
          if (value) {
            //this.form.get(field).setValidators([Validators.required]);
          }
        });
      });

    this.form.get('howIsPurchaserAuthorizedWaiver').valueChanges
      .subscribe(value => {
        this.form.get('purchasersWaiverNumber').clearValidators();
        this.form.get('purchasersWaiverNumber').reset();
        if (value) {
          this.form.get('purchasersWaiverNumber').setValidators(Validators.required);
        }
      });

    this.form.get('howIsPurchaserAuthorizedRegisteredSeller').valueChanges
      .subscribe(value => {
        this.form.get('purchasersRegistrationNumber').clearValidators();
        this.form.get('purchasersRegistrationNumber').reset();
        if (value) {
          this.form.get('purchasersRegistrationNumber').setValidators(Validators.required);
        }
      });

    this.form.get('howIsPurchaserAuthorizedOther').valueChanges
      .subscribe(value => {
        this.form.get('purchasersOther').clearValidators();
        this.form.get('purchasersOther').reset();
        if (value) {
          this.form.get('purchasersOther').setValidators([Validators.required]);
        };
      });

    this.form.get('typeOfSale').valueChanges
      .subscribe(value => {
        if (value === 'Other') {
          this.form.get('typeOfSaleOther').setValidators(Validators.required);
        } else {
          this.form.get('typeOfSaleOther').clearValidators();
          this.form.get('typeOfSaleOther').reset();
        }
      });

    this.form.get('methodOfPayment').valueChanges
      .subscribe(value => {
        if (value === 'Other') {
          this.form.get('methodOfPaymentOther').setValidators(Validators.required);
        } else {
          this.form.get('methodOfPaymentOther').clearValidators();
          this.form.get('methodOfPaymentOther').reset();
        }
      });

    this.form.get('idNumberCollected').valueChanges
      .subscribe(value => {
        if (value === true) {
          this.form.get('typeOfIdNumberCollected').setValidators(Validators.required);
        } else {
          this.form.get('typeOfIdNumberCollected').clearValidators();
          this.form.get('typeOfIdNumberCollected').reset();
        }
      });
  }

  /**
   * Save form values in dynamics
   * @param goToReview
   */
  save(goToReview: boolean) {
    if (this.form.valid || goToReview === false) {
      const value = this.form.value;
      console.log('valid');
      ///* 
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
        //*/
    } else {
      this.markAsTouched();
      for (var c in this.form.controls) { if (!this.form.get(c).valid) { console.log('Invalid: ' + c) } };
    }
  }

  markAsTouched() {
    let controls = this.form.controls;
    for (const c in controls) {
      if (typeof (controls[c].markAsTouched) === 'function') {
        controls[c].markAsTouched();
      }
    }

    controls = (<FormGroup>this.form.get('purchasersCivicAddress')).controls;
    for (const c in controls) {
      if (typeof (controls[c].markAsTouched) === 'function') {
        controls[c].markAsTouched();
      }
    }
    controls = (<FormGroup>this.form.get('civicAddressOfPurchaser')).controls;
    for (const c in controls) {
      if (typeof (controls[c].markAsTouched) === 'function') {
        controls[c].markAsTouched();
      }
    }
    controls = (<FormGroup>this.form.get('purchasersBusinessAddress')).controls;
    for (const c in controls) {
      if (typeof (controls[c].markAsTouched) === 'function') {
        controls[c].markAsTouched();
      }
    }
    this.form.updateValueAndValidity();
  }
}
