import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BusinessProfileComponent } from './business-information/business-profile/business-profile.component';
import { HomeComponent } from './home/home.component';
import { PolicyDocumentComponent } from './policy-document/policy-document.component';
import { SurveyPrimaryComponent } from './survey/primary.component';
import { SurveyTestComponent } from './survey/test.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { CanDeactivateGuard } from './services/can-deactivate-guard.service';
import { BCeidAuthGuard } from './services/bceid-auth-guard.service';
import { DashboardLiteComponent } from './dashboard-lite/dashboard-lite.component';
import { ProfileSummaryComponent } from './business-information/profile-summary/profile-summary.component';
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

const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'business-profile',
    component: BusinessProfileComponent,
    canDeactivate: [CanDeactivateGuard],
    canActivate: [BCeidAuthGuard]
  },
  {
    path: 'business-profile-review/:mode',
    component: ProfileSummaryComponent,
    canActivate: [BCeidAuthGuard]
  },
  {
    path: 'business-profile-review',
    component: ProfileSummaryComponent,
    canActivate: [BCeidAuthGuard]
  },
  {
    path: 'application',
    canActivate: [BCeidAuthGuard],
    children: [
      {
        path: 'waiver',
        children: [
          {
            path: ':id',
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
        children: [
          {
            path: ':id',
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
        children: [
          {
            path: ':id',
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

    ]
  },
  {
    path: 'dashboard',
    component: DashboardLiteComponent,
    canActivate: [BCeidAuthGuard]
  },
  {
    path: 'policy-document/:slug',
    component: PolicyDocumentComponent
  },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { scrollPositionRestoration: 'top' })],
  exports: [RouterModule],
  providers: []
})
export class AppRoutingModule { }
