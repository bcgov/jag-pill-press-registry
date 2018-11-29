import { Component, OnInit } from '@angular/core';
import { FormGroup, FormArray, FormBuilder, Validators } from '@angular/forms';
import { Subscription, zip, Observable } from 'rxjs';
import { PRODUCTING_OWN_PRODUCT, MANUFACTURING_FOR_OTHERS } from '../waiver-application/waiver-application.component';
import { ActivatedRoute, Router } from '@angular/router';
import { DynamicsDataService } from '../../../services/dynamics-data.service';
import { ApplicationDataService } from '../../../services/adoxio-application-data.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-waiver-review',
  templateUrl: './waiver-review.component.html',
  styleUrls: ['./waiver-review.component.scss']
})
export class WaiverReviewComponent implements OnInit {
  form: FormGroup;
  busy: Subscription;
  waiverId: string;

  equipmentInformation: any[] = [];
  purposeOfEquipment: any[] = [];
  purposeExplanation: any[] = [];
  attachmentURL: string;

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
    this.attachmentURL = `api/file/${this.waiverId}/attachments/application`;
  }

  ngOnInit() {
    this.form = this.fb.group({
      id: [],
      declarationofcorrectinformation: ['']
    });

    this.reloadData();
  }

  reloadData() {
    zip(this.applicationDataService.getApplicationById(this.waiverId),
      this.getUploadedFileData())
      .subscribe(data => {
        const waiver = data[0];
        const files = data[1];

        this.form.patchValue(waiver);

        // Equipment information list
        this.equipmentInformation = [
          'Do you currently own, use, or possess Controlled Equipment?',
          waiver.currentlyownusepossessequipment ? 'Yes' : 'No',
          'Do you intend on purchasing Controlled Equipment in the future?',
          waiver.intendtopurchaseequipment ? 'Yes' : 'No',
          'Do you own or intend to own Controlled Equipment for the sole use of your business?',
          waiver.ownintendtoownequipmentforbusinessuse ? 'Yes' : 'No',
          'Do you borrow, rent, or lease Controlled Equipment from someone else?',
          waiver.borrowrentleaseequipment ? 'Yes' : 'No',
          'Do you sell Controlled Equipment to others?',
          waiver.sellequipment ? 'Yes' : 'No'
        ];

        // process custom products
        waiver.customProducts = waiver.customProducts || [];
        waiver.customProducts = waiver.customProducts.map(i => {
          return { ...i, text: i.productdescriptionandintendeduse };
        });

        const productsForSelfProcessed = [];
        const productsForSelf = waiver.customProducts.filter(p => p.purpose === PRODUCTING_OWN_PRODUCT);
        for (let i = 0; i < productsForSelf.length; i++) {
          productsForSelfProcessed.push({
            text: `Product ${i === 0 ? '' : i + 1} Description and Intended Use`
          });
          productsForSelfProcessed.push(productsForSelf[i]);
        }
        const productsForOthersProcessed = [];
        const productsForOthers = waiver.customProducts.filter(p => p.purpose === MANUFACTURING_FOR_OTHERS);
        for (let i = 0; i < productsForOthers.length; i++) {
          productsForOthersProcessed.push({
            text: `Product ${i === 0 ? '' : i + 1} Description and Intended Use`
          });
          productsForOthersProcessed.push(productsForOthers[i]);
        }

        this.purposeOfEquipment = [
          { text: 'Do you own, use, or possess (or intend to own) Controlled Equipment for the purposes of producing your own product?' },
          { text: waiver.producingownproduct ? 'Yes' : 'No' },
          ...productsForSelfProcessed,
          {
            text: 'Do you own, use, or possess (or intend to own) Controlled ' +
              'Equipment for the purposes of providing manufacturing services to others?'
          },
          { text: waiver.providingmanufacturingtoothers ? 'Yes' : 'No' },
          ...productsForOthersProcessed
        ];

        this.purposeExplanation = [
          'Please explain the main focus of your business and why that requires Controlled Equipment.',
          waiver.mainbusinessfocus,
          'Please describe the manufacturing process you use to produce the above-noted products.' +
          ' Please include specific information on how you utilize the Controlled Equipment throughout the manufacturing process.',
          waiver.manufacturingprocessdescription
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

  save() {
    const value = this.form.value;
    this.form.markAsTouched();
    if (value.declarationofcorrectinformation !== false) {
      const saveList = [this.applicationDataService.updateApplication(value)];
      zip(...saveList)
        .subscribe(res => {
          this.router.navigateByUrl(`/waiver-thank-you/${this.waiverId}`);
        }, err => {
         // todo: show errors;
        });
    }
  }
}
