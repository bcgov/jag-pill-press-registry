import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-equipment-location',
  templateUrl: './equipment-location.component.html',
  styleUrls: ['./equipment-location.component.scss']
})
export class EquipmentLocationComponent implements OnInit {
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

    });

  }

  markAsTouched() {

  }

  save(goToNextForm: boolean) {
    if (goToNextForm) {
      this.router.navigateByUrl(`/equipment-notification/review/${this.equipmentId}`);
    } else {
      this.router.navigateByUrl('/dashboard');
    }
  }

}
