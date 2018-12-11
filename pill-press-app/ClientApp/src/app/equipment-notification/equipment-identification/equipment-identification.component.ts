import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-equipment-identification',
  templateUrl: './equipment-identification.component.html',
  styleUrls: ['./equipment-identification.component.scss']
})
export class EquipmentIdentificationComponent implements OnInit {
  form: FormGroup;
  busy: Subscription;
  equipmentId: string;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder) {
    this.equipmentId = this.route.snapshot.params.id;
  }

  ngOnInit() {
    this.form = this.fb.group({
      id: [],
      howWasEquipmentBuilt: [],
      howWasEquipmentBuiltOther: [],
      howWasEquipmentBuiltOtherCheck: [],
      nameOfManufacturer: [],
      equipmentMake: [],
      equipmentModel: [],
      serialNumber: [],
      howEquipmentBuiltDescription: [],
      personBusinessThatBuiltEquipment: [],
      serialNumberForCustomBuilt: [],
      customBuiltSerialNumber: [],
      serialNumberKeyPartDescription: [],

    });

  }

  markAsTouched() {

  }

  save(goToNextForm: boolean) {
    if (goToNextForm) {
      this.router.navigateByUrl(`/equipment-notification/source/${this.equipmentId}`);
    } else {
      this.router.navigateByUrl('/dashboard');
    }
  }

}
