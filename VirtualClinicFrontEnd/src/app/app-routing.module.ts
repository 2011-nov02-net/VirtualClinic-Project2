import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { DoctorsComponent } from '../app/doctors/doctors.component';
import { LogInComponent } from './log-in/log-in.component';
import { OktaCallbackComponent } from '@okta/okta-angular';
import { PatientsComponent } from '../app/patients/patients.component';
import { PatientDetailsComponent} from '../app/patient-details/patient-details.component';
import { PrescriptionsComponent } from '../app/prescriptions/prescriptions.component';



const routes: Routes = [
  { path: 'Patients/', component: PatientsComponent},
  { path: 'Prescritions/', component: PrescriptionsComponent},
  { path: 'Doctors/:id', component: DoctorsComponent },
  { path: 'Patients/:id', component: PatientDetailsComponent},
  { path: 'login', component: LogInComponent },
  { path: 'login/callback', component: OktaCallbackComponent }

];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }