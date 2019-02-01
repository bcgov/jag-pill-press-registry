import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BusinessProfileComponent } from './business-profile/business-profile/business-profile.component';
import { HomeComponent } from './home/home.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { CanDeactivateGuard } from './services/can-deactivate-guard.service';
import { BCeidAuthGuard } from './services/bceid-auth-guard.service';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ProfileReviewComponent } from './business-profile/profile-review/profile-review.component';
import { WaiverApplicationComponent } from './applications/waiver/waiver-application/waiver-application.component';
import { WaiverReviewComponent } from './applications/waiver/waiver-review/waiver-review.component';
import { ThankYouComponent } from './applications/waiver/thank-you/thank-you.component';
import { SellerApplicationComponent } from './applications/registered-seller/seller-application/seller-application.component';
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
import { EquipmentThankYouComponent } from './equipment-notification/equipment-thank-you/equipment-thank-you.component';
import { EquipmentReviewComponent } from './equipment-notification/equipment-review/equipment-review.component';
import { EquipmentLocationComponent } from './equipment-notification/equipment-location/equipment-location.component';
import { EquipmentIdentificationComponent } from './equipment-notification/equipment-identification/equipment-identification.component';
import { EquipmentTypeAndUseComponent } from './equipment-notification/equipment-type-and-use/equipment-type-and-use.component';
import { EquipmentSourceComponent } from './equipment-notification/equipment-source/equipment-source.component';
import { EquipmentNotificationComponent } from './equipment-notification/equipment-notification.component';
import { AuthorizedOwnerComponent } from './applications/authorized-owner/authorized-owner.component';
import { RegisteredSellerComponent } from './applications/registered-seller/registered-seller.component';
import { WaiverComponent } from './applications/waiver/waiver.component';
import { LocationChangeComponent } from './equipment-changes/location-change/location-change.component';
import { ReportChangesComponent } from './equipment-changes/report-changes/report-changes.component';
import { EquipmentChangeFormComponent } from './equipment-changes/report-changes/equipment-change-form/equipment-change-form.component';
import { EquipmentChangeReviewComponent } from './equipment-changes/report-changes/equipment-change-review/equipment-change-review.component';
import { EquipmentChangeThankYouComponent } from './equipment-changes/report-changes/equipment-change-thank-you/equipment-change-thank-you.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'business-profile',
    component: BusinessProfileComponent,
    canActivate: [BCeidAuthGuard]
  },
  {
    path: 'business-profile-review',
    component: ProfileReviewComponent,
    canActivate: [BCeidAuthGuard]
  },
  {
    path: 'waiver',
    component: WaiverComponent,
    canActivate: [BCeidAuthGuard],
    children: [
      {
        path: 'profile-review/:id',
        component: ProfileReviewComponent,
        data: {
          nextRoute: 'waiver/application'
        }
      },
      {
        path: 'application/:id',
        component: WaiverApplicationComponent,
      },
      {
        path: 'review/:id',
        component: WaiverReviewComponent,
      },
      {
        path: 'thank-you/:id',
        component: ThankYouComponent,
      },
    ]
  },
  {
    path: 'registered-seller',
    component: RegisteredSellerComponent,
    canActivate: [BCeidAuthGuard],
    children: [
      {
        path: 'profile-review/:id',
        component: ProfileReviewComponent,
        data: {
          nextRoute: 'registered-seller/application'
        }
      },
      {
        path: 'application/:id',
        component: SellerApplicationComponent,
      },
      {
        path: 'review/:id',
        component: SellerApplicationReviewComponent,
      },
      {
        path: 'thank-you/:id',
        component: SellerApplicationThanksComponent,
      },
    ]
  },
  {
    path: 'authorized-owner',
    component: AuthorizedOwnerComponent,
    canActivate: [BCeidAuthGuard],
    children: [
      {
        path: 'profile-review/:id',
        component: ProfileReviewComponent,
        data: {
          nextRoute: 'authorized-owner/application'
        }
      },
      {
        path: 'application/:id',
        component: AuthorizedApplicationComponent,
      },
      {
        path: 'review/:id',
        component: AuthorizedApplicationReviewComponent,
      },
      {
        path: 'thank-you/:id',
        component: AuthorizedApplicationThanksComponent,
      },
    ]
  },
  {
    path: 'equipment-notification',
    canActivate: [BCeidAuthGuard],
    component: EquipmentNotificationComponent,
    children: [
      {
        path: 'type-and-use/:id',
        component: EquipmentTypeAndUseComponent,
      },
      {
        path: 'identification/:id',
        component: EquipmentIdentificationComponent,
      },
      {
        path: 'source/:id',
        component: EquipmentSourceComponent,
      },
      {
        path: 'location/:id',
        component: EquipmentLocationComponent,
      },
      {
        path: 'review/:id',
        component: EquipmentReviewComponent,
      },
      {
        path: 'thank-you/:id',
        component: EquipmentThankYouComponent,
      },
      {
        path: 'profile-review/:id',
        component: ProfileReviewComponent,
        canActivate: [BCeidAuthGuard],
        data: {
          nextRoute: 'equipment-notification/type-and-use'
        }
      },
    ]
  },
  {
    path: 'equipment-changes/location-change/:id',
    canActivate: [BCeidAuthGuard],
    component: LocationChangeComponent
  },
  {
    path: 'equipment-changes/report-changes',
    canActivate: [BCeidAuthGuard],
    component: ReportChangesComponent,
    children: [
      {
        path: 'details/:id',
        component: EquipmentChangeFormComponent
      },
      {
        path: 'review/:id',
        component: EquipmentChangeReviewComponent
      },
      {
        path: 'thank-you/:id',
        component: EquipmentChangeThankYouComponent
      }
    ]
  },
  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [BCeidAuthGuard]
  },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { scrollPositionRestoration: 'top' })],
  exports: [RouterModule],
  providers: []
})
export class AppRoutingModule { }
