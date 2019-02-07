import { Component, OnInit } from '@angular/core';
import { FormBase } from '@shared/form-base';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Subscription, zip } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { ApplicationDataService } from '@services/application-data.service';

@Component({
  selector: 'app-reporting-sales-form',
  templateUrl: './reporting-sales-form.component.html',
  styleUrls: ['./reporting-sales-form.component.scss']
})
export class ReportingSalesFormComponent extends FormBase implements OnInit {
  form: FormGroup;
  busy: Subscription;
  equipmentId: string;
  busyPromise: Promise<any>;
  locations: any;

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
      /* * /
      equipmentType: ['', Validators.required],
      equipmentRegistryNumber: ['', Validators.required],
      howWasEquipmentBuilt: ['', Validators.required],
      dateLost: ['', Validators.required],
      dateReported: ['', Validators.required],
      howWasEquipmentBuiltOther: [],
      howWasEquipmentBuiltOtherCheck: [],
      nameOfManufacturer: ['', this.requiredSelectChildValidator('howWasEquipmentBuilt', ['Commercially Manufactured'])],
      equipmentMake: ['', this.requiredSelectChildValidator('howWasEquipmentBuilt', ['Commercially Manufactured'])],
      equipmentModel: ['', this.requiredSelectChildValidator('howWasEquipmentBuilt', ['Commercially Manufactured'])],
      serialNumber: ['', this.requiredSelectChildValidator('howWasEquipmentBuilt', ['Commercially Manufactured'])],
      howEquipmentBuiltDescription: ['', this.requiredSelectChildValidator('howWasEquipmentBuilt', ['Custom-built', 'Other'])],
      personBusinessThatBuiltEquipment: [''],
      serialNumberForCustomBuilt: ['', this.requiredSelectChildValidator('howWasEquipmentBuilt', ['Custom-built', 'Other'])],
      customBuiltSerialNumber: ['', this.requiredSelectChildValidator('serialNumberForCustomBuilt', [true])],
      serialNumberKeyPartDescription: [],
      address: this.fb.group({
        id: [],
        streetLine1: [''],
        streetLine2: [],
        city: [''],
        province: [],
        postalCode: [''],
      })
      /* */

      dateOfSale: [''],
      typeOfSale: [''],
      typeOfSaleOther: [''],
      rightsToOwnuseOrPossessRetained: [''],
      methodOfPayment: [''],
      methodOfPaymentOther: [''],
      whereWillEquipmentReside: [''],
      civicAddressOfPurchaser: this.fb.group({
        id: [],
        streetLine1: [''],
        streetLine2: [],
        city: [''],
        province: [],
        postalCode: [''],
      }),
      privateDwelling: [''],
      purchasedByIndividualOrBusiness: [''],
      legalNameOfPurchaserIndividual: [''],
      purchasersCivicAddress: this.fb.group({
        id: [],
        streetLine1: [''],
        streetLine2: [],
        city: [''],
        province: [],
        postalCode: [''],
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
        province: [],
        postalCode: [''],
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
      nameOnPurchasersSiteLicense: [''],
      purchasersSiteLicenseExpiryDate: [''],
      purchasersWaiverNumber: [''],
      purchasersRegistrationNumber: [''],
    });
    // this.clearHiddenFields();
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
        data.address = data.address || <any>{};
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
    this.form.get('howWasEquipmentBuilt').valueChanges
      .subscribe((value) => {
        if (value === 'Commercially Manufactured') {
          this.form.get('address').reset();
        }
        for (const field in this.form.controls) {
          if (field !== 'id'
            && field !== 'province'
            && field !== 'howWasEquipmentBuilt') {
            this.form.get(field).reset();
          }
        }
      });
  }

  save(goToReview: boolean) {
    if (this.form.valid || goToReview === false) {
      const value = this.form.value;
      value.address.country = 'Canada';
      const saveList = [this.applicationDataService.updateApplication(value)];
      this.busyPromise = zip(...saveList)
        .toPromise()
        .then(res => {
          if (goToReview) {
            this.router.navigateByUrl(`/equipment-changes/reporting-sales/review/${this.equipmentId}`);
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
  //   let controls = this.form.controls;
  //   for (const c in controls) {
  //     if (typeof (controls[c].markAsTouched) === 'function') {
  //       controls[c].markAsTouched();
  //     }
  //   }

  //   controls = (<FormGroup>this.form.get('address')).controls;
  //   for (const c in controls) {
  //     if (typeof (controls[c].markAsTouched) === 'function') {
  //       controls[c].markAsTouched();
  //     }
  //   }
  }
}
