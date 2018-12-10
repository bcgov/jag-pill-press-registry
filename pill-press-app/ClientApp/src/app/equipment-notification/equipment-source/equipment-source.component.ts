import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-equipment-source',
  templateUrl: './equipment-source.component.html',
  styleUrls: ['./equipment-source.component.scss']
})
export class EquipmentSourceComponent implements OnInit {
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
      ownedBefore1019: [],
      purchasedFromBCSeller: [],
      purchasedFromOutsideBC: [],
      importedToBC: [],
      alternaticeOwnership: [],
      selfAssembled: [],
      purchasedOther: [],
      assembleEQForOtherUses: [],

    });

  }

  markAsTouched() {

  }

  save(goToNextForm: boolean) {
    if (goToNextForm) {
      this.router.navigateByUrl(`/equipment-notification/location/${this.equipmentId}`);
    } else {
      this.router.navigateByUrl('/dashboard');
    }
  }

}
