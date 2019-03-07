import { Component, OnInit, Renderer2, TemplateRef } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { BreadcrumbComponent } from './breadcrumb/breadcrumb.component';
import { UserDataService } from './services/user-data.service';
import { VersionInfoDataService } from './services/version-info-data.service';
import { User } from './models/user.model';
import { VersionInfo } from './models/version-info.model';
import { isDevMode } from '@angular/core';
import { MatDialog } from '@angular/material';
import { AdoxioLegalEntity } from './models/adoxio-legalentities.model';
import { Store } from '@ngrx/store';
import { AppState } from './app-state/models/app-state';
import { Observable } from '../../node_modules/rxjs';
import 'rxjs/add/operator/filter';
import { BsModalService } from 'ngx-bootstrap/modal';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';

import * as CurrentUserActions from './app-state/actions/current-user.action';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  businessProfiles: AdoxioLegalEntity[];
  title = '';
  previousUrl: string;
  public currentUser: User;
  public versionInfo: VersionInfo;
  public isNewUser: boolean;
  public isDevMode: boolean;
  isAssociate = false;
  modalRef: BsModalRef;


  constructor(
    private renderer: Renderer2,
    private router: Router,
    private userDataService: UserDataService,
    private versionInfoDataService: VersionInfoDataService,
    private store: Store<AppState>,
    private modalService: BsModalService,
    private dialog: MatDialog
  ) {
    this.isDevMode = isDevMode();
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        const prevSlug = this.previousUrl;
        let nextSlug = event.url.slice(1);
        if (!nextSlug) {
          nextSlug = 'home';
        }
        if (prevSlug) {
          this.renderer.removeClass(document.body, 'ctx-' + prevSlug);
        }
        if (nextSlug) {
          this.renderer.addClass(document.body, 'ctx-' + nextSlug);
        }
        this.previousUrl = nextSlug;
      }
    });
  }

  ngOnInit(): void {
    this.reloadUser();
    this.loadVersionInfo();

    this.store.select(state => state.legalEntitiesState)
      .pipe(filter(state => !!state))
      .subscribe(state => {
        this.businessProfiles = state.legalEntities;
      });

  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }


  loadVersionInfo() {
    this.versionInfoDataService.getVersionInfo()
      .subscribe((versionInfo: VersionInfo) => {
        this.versionInfo = versionInfo;
      });
  }

  reloadUser() {
    this.userDataService.getCurrentUser()
      .subscribe((data: User) => {
        this.currentUser = data;
        this.isNewUser = this.currentUser.isNewUser;

        this.store.dispatch(new CurrentUserActions.SetCurrentUserAction(data));
      });
  }

  isIE10orLower() {
    let result, jscriptVersion;
    result = false;

    var ua = window.navigator.userAgent;    

    var msie = ua.indexOf('MSIE ');
    if (msie > 0) {
      // IE 10 or older => return version number
      var version = parseInt(ua.substring(msie + 5, ua.indexOf('.', msie)), 10);

      if (version !== undefined && version > 11) {
        result = true;
      }
    }
    
    return result;
  }

  showVersionInfo(): void {
    //this.dialog.open(VersionInfoDialog, {
    //  data: this.versionInfo
    //});
  }
}
