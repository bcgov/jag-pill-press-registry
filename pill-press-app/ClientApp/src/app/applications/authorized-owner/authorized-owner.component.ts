import { Component, OnInit, ViewChild } from '@angular/core';
import { MatStepper } from '@angular/material';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import { FormBuilder } from '@angular/forms';

import { faCheck, faAddressCard } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-authorized-owner',
  templateUrl: './authorized-owner.component.html',
  styleUrls: ['./authorized-owner.component.scss']
})
export class AuthorizedOwnerComponent implements OnInit {
  @ViewChild(MatStepper) stepper: MatStepper;
  equipmentId: string;
  tab: string;
  tabList: string[] = [
    'profile-review',
    'application',
    'review'
  ];

  faCheck = faCheck;
  faAddressCard = faAddressCard;

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
    // disable navigation to other pages
    //this.router.navigateByUrl(`/authorized-owner/${this.tabList[event.selectedIndex]}/${this.equipmentId}`);
  }

}

