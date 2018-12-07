import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { StepperSelectionEvent } from '@angular/cdk/stepper';
import { MatStepper } from '@angular/material';

@Component({
  selector: 'app-equipment-notification',
  templateUrl: './equipment-notification.component.html',
  styleUrls: ['./equipment-notification.component.scss']
})
export class EquipmentNotificationComponent implements OnInit {
  @ViewChild(MatStepper) stepper: MatStepper;
  equipmentId: string;
  tab: string;
  tabList: string[] = [
    'profile-review',
    'type-and-use',
    'identification',
    'source',
    'location',
    'review'
  ];

  constructor(private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder) {
    this.equipmentId = this.route.snapshot.firstChild.params.id;
    this.tab = this.route.snapshot.firstChild.url[0].path;
  }

  ngOnInit() {
    this.stepper.selectedIndex = this.tabList.indexOf(this.tab);
    this.router.events.forEach((event) => {
      if (event instanceof NavigationEnd) {
        if (this.route.snapshot.firstChild.url.length > 0) {
          this.equipmentId = this.route.snapshot.firstChild.params.id;
          this.tab = this.route.snapshot.firstChild.url[0].path;
          this.stepper.selectedIndex = this.tabList.indexOf(this.tab);
        }
      }
    });
  }

  selectionChange(event) {
    if (event.selectedIndex === 0) {
      this.router.navigateByUrl(`/equipment-notification/${this.tabList[event.selectedIndex]}/type-and-use/${this.equipmentId}`);
    } else {
      this.router.navigateByUrl(`/equipment-notification/${this.tabList[event.selectedIndex]}/${this.equipmentId}`);
    }
  }

}
