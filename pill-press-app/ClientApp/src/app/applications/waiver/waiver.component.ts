import { Component, OnInit, ViewChild } from '@angular/core';
import { MatStepper } from '@angular/material';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-waiver',
  templateUrl: './waiver.component.html',
  styleUrls: ['./waiver.component.scss']
})
export class WaiverComponent implements OnInit {
  @ViewChild(MatStepper) stepper: MatStepper;
  equipmentId: string;
  tab: string;
  tabList: string[] = [
    'profile-review',
    'application',
    'review',
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
        this.equipmentId = this.route.snapshot.firstChild.params.id;
        this.tab = this.route.snapshot.firstChild.url[0].path;
        this.stepper.selectedIndex = this.tabList.indexOf(this.tab);
      }
    });
  }

  selectionChange(event) {
    this.router.navigateByUrl(`/waiver/${this.tabList[event.selectedIndex]}/${this.equipmentId}`);
  }

}

