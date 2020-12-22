import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { OktaAuthModule, OKTA_CONFIG } from '@okta/okta-angular/';

import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DoctorsComponent } from './doctors/doctors.component';
import { LogInComponent } from './log-in/log-in.component';
import { AppRoutingModule } from './app-routing.module';
import { StyletestsComponent } from './styletests/styletests.component';
import { TimeslotsComponent } from './timeslots/timeslots.component';
import { PrescriptionsComponent } from './prescriptions/prescriptions.component';
import { PatientsComponent } from './patients/patients.component';
import { VitalsComponent } from './vitals/vitals.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { UpperCasePipe } from '@angular/common';




//the object to be provided by the DI
const config = {
  // Configuration here
  // https://developer.okta.com/docs/guides/sign-into-spa/angular/configure-the-sdk/
  clientId: '0oa2rlie97FpwhcZV5d6',
  issuer: 'https://dev-7862904.okta.com/oauth2/default',
  //TODO: move this to enviorment vars, or at least the localhost part
  redirectUri: 'http://localhost:4200/login/callback',
  scopes: ['openid', 'profile', 'email'],
  pkce: true
}; 


@NgModule({
  declarations: [
    AppComponent,
    LogInComponent,
    StyletestsComponent,
    TimeslotsComponent,
    PrescriptionsComponent,
    PatientsComponent,
    DoctorsComponent,
    VitalsComponent
  ],
  imports: [
    NgbModule,
    FormsModule,
    BrowserModule,
    AppRoutingModule,
    OktaAuthModule,
    HttpClientModule
  ],
  providers: [ { provide: OKTA_CONFIG, useValue: config }],
  bootstrap: [AppComponent],
})
export class AppModule { }
