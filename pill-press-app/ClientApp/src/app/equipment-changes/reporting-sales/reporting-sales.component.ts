import { Component, OnInit, ViewChild } from '@angular/core';
import { MatStepper } from '@angular/material';
import { Application } from '@models/application.model';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import { ApplicationDataService } from '@services/application-data.service';
import { FormBuilder } from '@angular/forms';
import { faCheck, faAddressCard } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-reporting-sales',
  templateUrl: './reporting-sales.component.html',
  styleUrls: ['./reporting-sales.component.scss']
})
export class ReportingSalesComponent implements OnInit {
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

    //if (this.tabList.indexOf(this.tab) !== -1) {
    //  this.stepper.selectedIndex = this.tabList.indexOf(this.tab);
    //}
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
    this.router.navigateByUrl(`/equipment-changes/reporting-sales/${this.tabList[event.selectedIndex]}/${this.equipmentId}`);
  }

}
