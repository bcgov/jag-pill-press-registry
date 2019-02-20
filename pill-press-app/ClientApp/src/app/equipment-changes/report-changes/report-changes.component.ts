import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { StepperSelectionEvent } from '@angular/cdk/stepper';
import { MatStepper } from '@angular/material';
import { ApplicationDataService } from '../../services/application-data.service';
import { Application } from '../../models/application.model';

import { faCheck, faAddressCard } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-report-changes',
  templateUrl: './report-changes.component.html',
  styleUrls: ['./report-changes.component.scss']
})
export class ReportChangesComponent implements OnInit {
  @ViewChild(MatStepper) stepper: MatStepper;
  equipmentId: string;
  tab: string;
  tabList: string[] = [
    'details',
    'review'
  ];
  displayedColumns: string[] = ['equipment', 'status'];
  equipment: Application[];

  faCheck = faCheck;
  faAddressCard = faAddressCard;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private applicationDataService: ApplicationDataService,
    private fb: FormBuilder) {
    this.equipmentId = this.route.snapshot.firstChild.params.id;
    this.tab = this.route.snapshot.firstChild.url[0].path;
  }

  ngOnInit() {
    this.router.events.forEach((event) => {
      if (event instanceof NavigationEnd) {
        if (this.route.snapshot.firstChild.url.length > 0) {
          if (this.equipmentId !== this.route.snapshot.firstChild.params.id) {
            this.equipmentId = this.route.snapshot.firstChild.params.id;
            this.stepper.reset();
          }
          this.tab = this.route.snapshot.firstChild.url[0].path;
          if (this.tabList.indexOf(this.tab) !== -1) {
            this.stepper.selectedIndex = this.tabList.indexOf(this.tab);
          }
        }
      }
    });
  }



  selectionChange(event) {
    this.router.navigateByUrl(`/equipment-changes/reporting-changes/${this.tabList[event.selectedIndex]}/${this.equipmentId}`);
  }

}
