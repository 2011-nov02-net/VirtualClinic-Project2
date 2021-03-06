import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { OktaAuthModule, OKTA_CONFIG } from '@okta/okta-angular/';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { DoctorsComponent } from './doctors/doctors.component';
import { StyletestsComponent } from './styletests/styletests.component';
import { TimeslotsComponent } from './timeslots/timeslots.component';
import { PrescriptionsComponent } from './prescriptions/prescriptions.component';
import { PatientsComponent } from './patients/patients.component';
import { VitalsComponent } from './vitals/vitals.component';
import { PatientDetailsComponent } from './patient-details/patient-details.component';
import { PrescriptionDetailsComponent } from './prescription-details/prescription-details.component'
import { environment } from './../environments/environment';
import { EditPatientComponent } from './edit-patient/edit-patient.component';
import { ReportsComponent } from './patient-reports/patient-reports.component';
import { ReportDetailsComponent } from './report-details/report-details.component';
import { CreateReportComponent } from './create-report/create-report.component';



//the object to be provided by the DI
const config = {
  // Configuration here
  // https://developer.okta.com/docs/guides/sign-into-spa/angular/configure-the-sdk/
  clientId: '0oa2rlie97FpwhcZV5d6',
  issuer: 'https://dev-7862904.okta.com/oauth2/default',
  redirectUri: '/login/callback',
  scopes: ['openid', 'profile', 'email'],
  pkce: true
}; 


@NgModule({
  declarations: [
    AppComponent,
    StyletestsComponent,
    TimeslotsComponent,
    PrescriptionsComponent,
    PrescriptionDetailsComponent,
    PatientsComponent,
    DoctorsComponent,
    VitalsComponent,
    PatientDetailsComponent,
    EditPatientComponent,
    ReportsComponent,
    ReportDetailsComponent,
    CreateReportComponent
  ],
  imports: [
    NgbModule,
    FormsModule,
    BrowserModule,
    AppRoutingModule,
    OktaAuthModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [ { provide: OKTA_CONFIG, useValue: config }],
  bootstrap: [AppComponent],
})
export class AppModule { 
  static globalBaseURL: string = 'https://virtual-clinic-backend.azurewebsites.net/';
}
