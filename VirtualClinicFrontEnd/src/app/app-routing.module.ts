import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DoctorsComponent } from '../app/doctors/doctors.component';
import { OktaCallbackComponent } from '@okta/okta-angular';
import { PatientsComponent } from '../app/patients/patients.component';



const routes: Routes = [
  { path: 'Doctors/:id', component: DoctorsComponent },
  { path: 'Patients/:id', component: PatientsComponent},
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