import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DoctorsComponent } from '../app/doctors/doctors.component';
import { OktaCallbackComponent } from '@okta/okta-angular';
import { PatientsComponent } from '../app/patients/patients.component';
import { PatientDetailsComponent} from '../app/patient-details/patient-details.component';
import { PrescriptionsComponent } from '../app/prescriptions/prescriptions.component';
<<<<<<< HEAD
import { ReportsComponent } from '../app/patient-reports/patient-reports.component';
import { ReportDetailsComponent } from './report-details/report-details.component';
=======
import { PrescriptionDetailsComponent} from '../app/prescription-details/prescription-details.component';
>>>>>>> frontend



const routes: Routes = [
  { path: 'Doctors', component: DoctorsComponent },
  { path: 'Patients', component: PatientsComponent},
  { path: 'Patients/:id', component: PatientDetailsComponent},
<<<<<<< HEAD
  { path: 'Patients/:id/Prescriptions', component: PrescriptionsComponent},
  { path: 'Patients/:id/Reports', component: ReportsComponent},
  { path: 'login/callback', component: OktaCallbackComponent},
  { path: 'Reports/:id', component: ReportDetailsComponent}
=======
  { path: 'Prescriptions', component: PrescriptionsComponent},
  { path: 'login/callback', component: OktaCallbackComponent },
  { path: 'Prescriptions/:id', component: PrescriptionDetailsComponent}
>>>>>>> frontend

];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }