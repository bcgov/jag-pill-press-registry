import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-equipment-type-and-use',
  templateUrl: './equipment-type-and-use.component.html',
  styleUrls: ['./equipment-type-and-use.component.scss']
})
export class EquipmentTypeAndUseComponent implements OnInit {
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

    });

  }

  markAsTouched() {

  }

  save(goToNextForm: boolean) {
    if (goToNextForm) {
      this.router.navigateByUrl(`/equipment-notification/identification/${this.equipmentId}`);
    } else {
      this.router.navigateByUrl('/dashboard');
    }
  }

}
