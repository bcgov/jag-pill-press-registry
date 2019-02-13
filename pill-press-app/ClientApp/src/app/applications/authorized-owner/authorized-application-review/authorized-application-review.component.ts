import { Component, OnInit } from '@angular/core';
import { FormGroup, FormArray, FormBuilder, Validators } from '@angular/forms';
import { Subscription, zip, Observable } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { DynamicsDataService } from '../../../services/dynamics-data.service';
import { ApplicationDataService } from '../../../services/application-data.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { PRODUCING_OWN_PRODUCT, MANUFACTURING_FOR_OTHERS } from '../../waiver/waiver-application/waiver-application.component';
import { Application } from '../../../models/application.model';

import { faSave } from '@fortawesome/free-regular-svg-icons';
import { faExclamation } from '@fortawesome/free-solid-svg-icons';


@Component({
  selector: 'app-authorized-application-review',
  templateUrl: './authorized-application-review.component.html',
  styleUrls: ['./authorized-application-review.component.scss']
})
export class AuthorizedApplicationReviewComponent implements OnInit {
  faSave = faSave;
  faExclamation = faExclamation;
  form: FormGroup;
  waiverId: string;

  equipmentInformation: any[] = [];
  purposeOfEquipment: any[] = [];
  legislativeAuthorization: any[] = [];
  attachmentURL: string;
  formData: Application = <Application>{};
  busy: Subscription;
  busyPromise: Promise<any>;

  get ownProducts(): FormArray {
    return <FormArray>this.form.get('ownProducts');
  }
  get productsForOthers(): FormArray {
    return <FormArray>this.form.get('productsForOthers');
  }
  constructor(private fb: FormBuilder,
    private http: HttpClient,
    private route: ActivatedRoute,
    private router: Router,
    private dynamicsDataService: DynamicsDataService,
    private applicationDataService: ApplicationDataService) {
    this.waiverId = this.route.snapshot.params.id;
    this.attachmentURL = `api/file/${this.waiverId}/attachments/incident`;
  }

  ngOnInit() {
    this.form = this.fb.group({
      id: [],
      declarationOfCorrectInformation: ['']
    });

    this.reloadData();
  }

  reloadData() {
    this.busy = this.applicationDataService.getApplicationById(this.waiverId)
      .subscribe(data => {
        const application = data;
        this.formData = application;
        // const files = data[1];

        this.form.patchValue(application);

        // Equipment information list
        this.equipmentInformation = [
          'Do you currently own, use, or possess Controlled Equipment?',
          application.currentlyownusepossessequipment ? 'Yes' : 'No',
          'Do you intend on purchasing Controlled Equipment in the future?',
          application.intendtopurchaseequipment ? 'Yes' : 'No',
          'Do you own or intend to own Controlled Equipment for the sole use of your business?',
          application.ownintendtoownequipmentforbusinessuse ? 'Yes' : 'No',
          'Do you borrow, rent, or lease Controlled Equipment from someone else?',
          application.borrowrentleaseequipment ? 'Yes' : 'No',
          'Do you sell Controlled Equipment to others?',
          application.sellequipment ? 'Yes' : 'No'
        ];

        // process custom products
        application.customProducts = application.customProducts || [];
        application.customProducts = application.customProducts.map(i => {
          return { ...i, text: i.productdescriptionandintendeduse };
        });

        const productsForSelfProcessed = [];
        const productsForSelf = application.customProducts.filter(p => p.purpose === PRODUCING_OWN_PRODUCT);
        for (let i = 0; i < productsForSelf.length; i++) {
          productsForSelfProcessed.push({
            text: `Product ${i === 0 ? '' : i + 1} Description and Intended Use`
          });
          productsForSelfProcessed.push(productsForSelf[i]);
        }
        const productsForOthersProcessed = [];
        const productsForOthers = application.customProducts.filter(p => p.purpose === MANUFACTURING_FOR_OTHERS);
        for (let i = 0; i < productsForOthers.length; i++) {
          productsForOthersProcessed.push({
            text: `Product ${i === 0 ? '' : i + 1} Description and Intended Use`
          });
          productsForOthersProcessed.push(productsForOthers[i]);
        }

        this.purposeOfEquipment = [
          { text: 'Do you own, use, or possess (or intend to own) Controlled Equipment for the purposes of producing your own product?' },
          { text: application.producingownproduct ? 'Yes' : 'No' },
          ...productsForSelfProcessed,
          {
            text: 'Do you own, use, or possess (or intend to own) Controlled ' +
              'Equipment for the purposes of providing manufacturing services to others?'
          },
          { text: application.providingmanufacturingtoothers ? 'Yes' : 'No' },
          ...productsForOthersProcessed
        ];

        this.legislativeAuthorization = [
          'Please explain the main focus of your business and why that requires Controlled Equipment.',
          application.mainbusinessfocus,
          'Please describe the manufacturing process you use to produce the above-noted products.' +
          ' Please include specific information on how you utilize the Controlled Equipment throughout the manufacturing process.',
          application.manufacturingprocessdescription
        ];

      }, error => {
        // todo: show errors;
      });
  }

  getUploadedFileData() {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    const getFileURL = this.attachmentURL + '/Waiver Documents';
    return this.http.get(getFileURL, { headers: headers });
    // .pipe(map((data: Response) => <FileSystemItem[]>data.json()))
    // .subscribe((data) => {
    //   // convert bytes to KB
    //   data.forEach((entry) => {
    //     entry.size = Math.ceil(entry.size / 1024);
    //     entry.downloadUrl = `api/file/${this.entityId}/download-file/${this.entityName}/${entry.name}`;
    //     entry.downloadUrl += `?serverRelativeUrl=${encodeURIComponent(entry.serverrelativeurl)}&documentType=${this.documentType}`;
    //   });
    //   this.files = data;
    // },
    //   err => alert('Failed to get files'));
  }

  markAsTouched() {
    this.form.markAsTouched();
    const controls = this.form.controls;
    for (const c in controls) {
      if (typeof (controls[c].markAsTouched) === 'function') {
        controls[c].markAsTouched();
      }
    }
  }

  save(goToThankYouPage: boolean) {
    const value = this.form.value;
    if (goToThankYouPage) {
      value.statuscode = 'Pending';
      value.submittedDate = new Date();
    }
    this.form.markAsTouched();
    if (value.declarationOfCorrectInformation !== false) {
      const saveList = [this.applicationDataService.updateApplication(value)];
      this.busyPromise = zip(...saveList)
        .toPromise()
        .then(res => {
          if (goToThankYouPage) {
            this.router.navigateByUrl(`/authorized-owner/thank-you/${this.waiverId}`);
          } else {
            this.router.navigateByUrl(`dashboard`);
          }
        }, err => {
          // todo: show errors;
        });
    }
  }
}

