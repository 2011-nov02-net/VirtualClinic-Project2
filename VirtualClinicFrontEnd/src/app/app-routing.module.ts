import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { DoctorsComponent } from '../app/doctors/doctors.component';
import { PatientDetailComponent} from '../app/patient-detail/patient-detail.component';
import { LogInComponent } from './log-in/log-in.component';
import { OktaCallbackComponent } from '@okta/okta-angular';
import { PatientsComponent } from '../app/patients/patients.component';



const routes: Routes = [
  { path: 'Doctors/:id', component: DoctorsComponent },
  { path:  'Patients/:id', component: PatientDetailComponent},
  { path: 'login', component: LogInComponent },
  { path: 'login/callback', component: OktaCallbackComponent },
  { path: 'Doctors/1', component: DoctorsComponent }
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }