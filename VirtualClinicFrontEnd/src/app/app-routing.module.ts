import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LogInComponent } from './log-in/log-in.component';
import { OktaCallbackComponent } from '@okta/okta-angular';
import { DoctorsComponent} from '../app/doctors/doctors.component';
import { PatientsComponent } from '../app/patients/patients.component';

const routes: Routes = [
  { path: 'login', component: LogInComponent },
  { path: 'login/callback', component: OktaCallbackComponent },
  { path: 'Doctors/1', component: DoctorsComponent },
  { path:  'Patients/:id', component: PatientsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }