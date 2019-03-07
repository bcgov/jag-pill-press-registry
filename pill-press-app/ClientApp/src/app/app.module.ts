import { BrowserModule, Title } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { HttpClientModule } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { AppRoutingModule } from './app-routing.module';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
  MatAutocompleteModule,
  MatButtonModule,
  MatButtonToggleModule,
  MatCardModule,
  MatCheckboxModule,
  MatChipsModule,
  MatDatepickerModule,
  MatDialogModule,
  MatDividerModule,
  MatExpansionModule,
  MatGridListModule,
  MatIconModule,
  MatInputModule,
  MatListModule,
  MatMenuModule,
  MatNativeDateModule,
  MatPaginatorModule,
  MatProgressBarModule,
  MatProgressSpinnerModule,
  MatRadioModule,
  MatRippleModule,
  MatSelectModule,
  MatSidenavModule,
  MatSliderModule,
  MatSlideToggleModule,
  MatSnackBarModule,
  MatSortModule,
  MatStepperModule,
  MatTableModule,
  MatTabsModule,
  MatToolbarModule,
  MatTooltipModule
} from '@angular/material';
import { CdkTableModule } from '@angular/cdk/table';

import { AccountDataService } from './services/account-data.service';
import { ContactDataService } from './services/contact-data.service';
import { ApplicationDataService } from './services/application-data.service';
import { AppComponent } from './app.component';
import { BceidConfirmationComponent } from './bceid-confirmation/bceid-confirmation.component';
import { SearchBoxDirective } from './search-box/search-box.directive';
import { GeneralDataService } from './general-data.service';
import { BreadcrumbComponent } from './breadcrumb/breadcrumb.component';
import { DynamicsDataService } from './services/dynamics-data.service';
import { HomeComponent } from './home/home.component';
import { PolicyDocumentComponent } from './policy-document/policy-document.component';
import { PolicyDocumentDataService } from './services/policy-document-data.service';
import { PolicyDocumentSidebarComponent } from './policy-document-sidebar/policy-document-sidebar.component';
import { StatusBadgeComponent } from './status-badge/status-badge.component';
import { UserDataService } from './services/user-data.service';
import { VersionInfoDataService } from './services/version-info-data.service';
import { NotFoundComponent } from './not-found/not-found.component';
import { BusinessProfileComponent } from './business-profile/business-profile/business-profile.component';
import { FileDropModule } from 'ngx-file-drop';
import { FileUploaderComponent } from './shared/file-uploader/file-uploader.component';
import { NgBusyModule } from 'ng-busy';
import { BsDatepickerModule, AlertModule  } from 'ngx-bootstrap';
import { ModalModule, BsModalService } from 'ngx-bootstrap/modal';
import { CanDeactivateGuard } from './services/can-deactivate-guard.service';
import { BCeidAuthGuard } from './services/bceid-auth-guard.service';
import { ServiceCardAuthGuard } from './services/service-card-auth-guard.service';
import { metaReducers, reducers } from './app-state/reducers/reducers';
import { StoreModule } from '@ngrx/store';
import { DashboardComponent } from './dashboard/dashboard.component';
import { TermsAndConditionsComponent } from './terms-and-conditions/terms-and-conditions.component';
import { PreviousAddressDataService } from './services/previous-address-data.service';
import { FieldComponent } from './shared/field/field.component';
import { ProfileReviewComponent } from './business-profile/profile-review/profile-review.component';
import { WaiverApplicationComponent } from './applications/waiver/waiver-application/waiver-application.component';
import { WaiverReviewComponent } from './applications/waiver/waiver-review/waiver-review.component';
import { ThankYouComponent } from './applications/waiver/thank-you/thank-you.component';
import {
  SellerApplicationComponent, SellerOwnerDialogComponent
} from './applications/registered-seller/seller-application/seller-application.component';
import {
  SellerApplicationReviewComponent
} from './applications/registered-seller/seller-application-review/seller-application-review.component';
import {
  SellerApplicationThanksComponent
} from './applications/registered-seller/seller-application-thanks/seller-application-thanks.component';
import {
  AuthorizedApplicationComponent
} from './applications/authorized-owner/authorized-application/authorized-application.component';
import {
  AuthorizedApplicationReviewComponent
} from './applications/authorized-owner/authorized-application-review/authorized-application-review.component';
import {
  AuthorizedApplicationThanksComponent
} from './applications/authorized-owner/authorized-application-thanks/authorized-application-thanks.component';
import { EquipmentTypeAndUseComponent } from './equipment-notification/equipment-type-and-use/equipment-type-and-use.component';
import { EquipmentIdentificationComponent } from './equipment-notification/equipment-identification/equipment-identification.component';
import { EquipmentSourceComponent } from './equipment-notification/equipment-source/equipment-source.component';
import { EquipmentLocationComponent } from './equipment-notification/equipment-location/equipment-location.component';
import { EquipmentReviewComponent } from './equipment-notification/equipment-review/equipment-review.component';
import { EquipmentThankYouComponent } from './equipment-notification/equipment-thank-you/equipment-thank-you.component';
import { EquipmentNotificationComponent } from './equipment-notification/equipment-notification.component';
import { WaiverComponent } from './applications/waiver/waiver.component';
import { RegisteredSellerComponent } from './applications/registered-seller/registered-seller.component';
import { AuthorizedOwnerComponent } from './applications/authorized-owner/authorized-owner.component';
import { LocationChangeComponent } from './equipment-changes/location-change/location-change.component';
import { ReportChangesComponent } from './equipment-changes/report-changes/report-changes.component';
import { EquipmentChangeFormComponent } from './equipment-changes/report-changes/equipment-change-form/equipment-change-form.component';
import { EquipmentChangeReviewComponent } from './equipment-changes/report-changes/equipment-change-review/equipment-change-review.component';
import { EquipmentChangeThankYouComponent } from './equipment-changes/report-changes/equipment-change-thank-you/equipment-change-thank-you.component';
import { ReportingSalesFormComponent } from './equipment-changes/reporting-sales/reporting-sales-form/reporting-sales-form.component';
import { ReportingSalesReviewComponent } from './equipment-changes/reporting-sales/reporting-sales-review/reporting-sales-review.component';
import { ReportingSalesThankYouComponent } from './equipment-changes/reporting-sales/reporting-sales-thank-you/reporting-sales-thank-you.component';
import { ReportingSalesComponent } from './equipment-changes/reporting-sales/reporting-sales.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';



@NgModule({
  declarations: [
    AppComponent,
    BceidConfirmationComponent,
    BusinessProfileComponent,
    BreadcrumbComponent,
    HomeComponent,
    NotFoundComponent,
    PolicyDocumentComponent,
    PolicyDocumentSidebarComponent,
    SearchBoxDirective,
    StatusBadgeComponent,
    FileUploaderComponent,
    DashboardComponent,
    FieldComponent,
    ProfileReviewComponent,
    WaiverApplicationComponent,
    WaiverReviewComponent,
    ThankYouComponent,
    TermsAndConditionsComponent,
    SellerApplicationComponent,
    SellerApplicationReviewComponent,
    SellerApplicationThanksComponent,
    AuthorizedApplicationComponent,
    AuthorizedApplicationReviewComponent,
    AuthorizedApplicationThanksComponent,
    SellerOwnerDialogComponent,
    EquipmentTypeAndUseComponent,
    EquipmentIdentificationComponent,
    EquipmentSourceComponent,
    EquipmentLocationComponent,
    EquipmentReviewComponent,
    EquipmentThankYouComponent,
    EquipmentNotificationComponent,
    WaiverComponent,
    RegisteredSellerComponent,
    AuthorizedOwnerComponent,
    LocationChangeComponent,
    ReportChangesComponent,
    EquipmentChangeFormComponent,
    EquipmentChangeReviewComponent,
    EquipmentChangeThankYouComponent,
    ReportingSalesFormComponent,
    ReportingSalesReviewComponent,
    ReportingSalesThankYouComponent,
    ReportingSalesComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    FontAwesomeModule,
    ReactiveFormsModule,
    HttpModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    NgBusyModule,
    CdkTableModule,
    MatAutocompleteModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatCardModule,
    MatCheckboxModule,
    MatChipsModule,
    MatDatepickerModule,
    MatDialogModule,
    MatDividerModule,
    MatExpansionModule,
    MatGridListModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatNativeDateModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatRadioModule,
    MatRippleModule,
    MatSelectModule,
    MatSidenavModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatSnackBarModule,
    MatSortModule,
    MatStepperModule,
    MatTableModule,
    MatTabsModule,
    MatToolbarModule,
    MatTooltipModule,
    FileDropModule,
    ModalModule.forRoot(),
    BsDatepickerModule.forRoot(),
    StoreModule.forRoot(reducers, { metaReducers }),
    AlertModule.forRoot()
  ],
  exports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    CdkTableModule,
    MatAutocompleteModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatCardModule,
    MatCheckboxModule,
    MatChipsModule,
    MatDatepickerModule,
    MatDialogModule,
    MatDividerModule,
    MatExpansionModule,
    MatGridListModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatNativeDateModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatRadioModule,
    MatRippleModule,
    MatSelectModule,
    MatSidenavModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatSnackBarModule,
    MatSortModule,
    MatStepperModule,
    MatTableModule,
    MatTabsModule,
    MatToolbarModule,
    FileDropModule,
    MatTooltipModule
  ],
  providers: [
    CookieService,
    DynamicsDataService,
    GeneralDataService,
    PolicyDocumentDataService,
    UserDataService,
    ApplicationDataService,
    AccountDataService,
    ContactDataService,
    PreviousAddressDataService,
    Title,
    VersionInfoDataService,
    CanDeactivateGuard,
    BCeidAuthGuard,
    ServiceCardAuthGuard,
    BsModalService,
  ],
  entryComponents: [
    SellerOwnerDialogComponent,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
