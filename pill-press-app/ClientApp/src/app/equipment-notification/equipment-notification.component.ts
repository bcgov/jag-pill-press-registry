import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { StepperSelectionEvent } from '@angular/cdk/stepper';
import { MatStepper } from '@angular/material';
import { ApplicationDataService } from '../services/application-data.service';
import { Application } from '../models/application.model';

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
  displayedColumns: string[] = ['equipment', 'status'];
  equipment: Application[];

  constructor(private route: ActivatedRoute,
    private router: Router,
    private applicationDataService: ApplicationDataService,
    private fb: FormBuilder) {
    this.equipmentId = this.route.snapshot.firstChild.params.id;
    this.tab = this.route.snapshot.firstChild.url[0].path;
  }

  ngOnInit() {
    this.loadEquipment();

    this.stepper.selectedIndex = this.tabList.indexOf(this.tab);
    this.router.events.forEach((event) => {
      if (event instanceof NavigationEnd) {
        if (this.route.snapshot.firstChild.url.length > 0) {
          if (this.equipmentId !== this.route.snapshot.firstChild.params.id) {
            this.equipmentId = this.route.snapshot.firstChild.params.id;
            this.stepper.reset();
            this.loadEquipment();
          }
          this.tab = this.route.snapshot.firstChild.url[0].path;
          this.stepper.selectedIndex = this.tabList.indexOf(this.tab);
        }
      }
    });
  }

  loadEquipment() {
    this.applicationDataService.getApplications()
      .subscribe((data: Application[]) => {
        this.equipment = data.filter(a => a.applicationtype === 'Equipment Notification' && a.statuscode !== 'Draft');
      });
  }

  selectionChange(event) {
    this.router.navigateByUrl(`/equipment-notification/${this.tabList[event.selectedIndex]}/${this.equipmentId}`);
  }

}
