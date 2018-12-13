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
import { ApplicationDataService } from './services/adoxio-application-data.service';
import { AdoxioLegalEntityDataService } from './services/adoxio-legal-entity-data.service';
import { AdoxioLicenseDataService } from './services/adoxio-license-data.service';
import { PaymentDataService } from './services/payment-data.service';
import { AppComponent } from './app.component';
import { BceidConfirmationComponent } from './bceid-confirmation/bceid-confirmation.component';
import { SearchBoxDirective } from './search-box/search-box.directive';
import { GeneralDataService } from './general-data.service';
import { BreadcrumbComponent } from './breadcrumb/breadcrumb.component';
import { DynamicsDataService } from './services/dynamics-data.service';
import { InsertComponent } from './insert/insert.component';
import { InsertService } from './insert/insert.service';
import { StaticComponent } from './static/static.component';
import { HomeComponent } from './home/home.component';
import { PolicyDocumentComponent } from './policy-document/policy-document.component';
import { PolicyDocumentDataService } from './services/policy-document-data.service';
import { PolicyDocumentSidebarComponent } from './policy-document-sidebar/policy-document-sidebar.component';
import { StatusBadgeComponent } from './status-badge/status-badge.component';
import { SurveyComponent } from './survey/survey.component';
import { SurveyPrimaryComponent } from './survey/primary.component';
import { SurveyTestComponent } from './survey/test.component';
import { SurveySidebarComponent } from './survey/sidebar.component';
import { SurveyDataService } from './services/survey-data.service';
import { UserDataService } from './services/user-data.service';
import { VersionInfoDataService } from './services/version-info-data.service';
import { NotFoundComponent } from './not-found/not-found.component';
import { ApplicationsListComponent } from './applications-list/applications-list.component';
import { BusinessProfileComponent } from './business-information/business-profile/business-profile.component';
import { FileDropModule } from 'ngx-file-drop';
import { FileUploaderComponent } from './shared/file-uploader/file-uploader.component';
import { BusinessProfileSummaryComponent } from './business-profile-summary/business-profile-summary.component';
import { NgBusyModule } from 'ng-busy';
import { PaymentConfirmationComponent } from './payment-confirmation/payment-confirmation.component';
import { LicenceFeePaymentConfirmationComponent } from './licence-fee-payment-confirmation/licence-fee-payment-confirmation.component';
import { BsDatepickerModule, AlertModule } from 'ngx-bootstrap';
import { CanDeactivateGuard } from './services/can-deactivate-guard.service';
import { BCeidAuthGuard } from './services/bceid-auth-guard.service';
import { ServiceCardAuthGuard } from './services/service-card-auth-guard.service';
import { metaReducers, reducers } from './app-state/reducers/reducers';
import { StoreModule } from '@ngrx/store';
import { DashboardComponent } from './dashboard/dashboard.component';
import { TermsAndConditionsComponent } from './lite/terms-and-conditions/terms-and-conditions.component';
import { AliasDataService } from './services/alias-data.service';
import { PreviousAddressDataService } from './services/previous-address-data.service';
import { WorkerDataService } from './services/worker-data.service.';
import { FieldComponent } from './shared/field/field.component';
import { ProfileSummaryComponent } from './business-information/profile-summary/profile-summary.component';
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
import { VersionInfoDialog } from './version-info/version-info.component';
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

@NgModule({
  declarations: [
    AppComponent,
    ApplicationsListComponent,
    BceidConfirmationComponent,
    BusinessProfileComponent,
    BreadcrumbComponent,
    HomeComponent,
    InsertComponent,
    NotFoundComponent,
    PolicyDocumentComponent,
    PolicyDocumentSidebarComponent,
    SearchBoxDirective,
    StaticComponent,
    StatusBadgeComponent,
    SurveyComponent,
    SurveyPrimaryComponent,
    SurveySidebarComponent,
    SurveyTestComponent,
    FileUploaderComponent,
    BusinessProfileSummaryComponent,
    PaymentConfirmationComponent,
    DashboardComponent,
    LicenceFeePaymentConfirmationComponent,
    FieldComponent,
    ProfileSummaryComponent,
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
    VersionInfoDialog,
    EquipmentTypeAndUseComponent,
    EquipmentIdentificationComponent,
    EquipmentSourceComponent,
    EquipmentLocationComponent,
    EquipmentReviewComponent,
    EquipmentThankYouComponent,
    EquipmentNotificationComponent,
    WaiverComponent,
    RegisteredSellerComponent,
    AuthorizedOwnerComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
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
    InsertService,
    GeneralDataService,
    PolicyDocumentDataService,
    SurveyDataService,
    UserDataService,
    AliasDataService,
    ApplicationDataService,
    AdoxioLegalEntityDataService,
    AdoxioLicenseDataService,
    AccountDataService,
    ContactDataService,
    PaymentDataService,
    PreviousAddressDataService,
    WorkerDataService,
    Title,
    VersionInfoDataService,
    CanDeactivateGuard,
    BCeidAuthGuard,
    ServiceCardAuthGuard,
  ],
  entryComponents: [
    SellerOwnerDialogComponent,
    VersionInfoDialog
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
