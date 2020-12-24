import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DoctorsComponent } from '../app/doctors/doctors.component';
import { OktaCallbackComponent } from '@okta/okta-angular';
import { PatientsComponent } from '../app/patients/patients.component';
import { PatientDetailsComponent} from '../app/patient-details/patient-details.component';
import { PrescriptionsComponent } from '../app/prescriptions/prescriptions.component';



const routes: Routes = [
  { path: 'Doctors', component: DoctorsComponent },
  { path: 'Patients', component: PatientsComponent},
  { path: 'Patients/:id', component: PatientDetailsComponent},
  { path: 'Prescritions', component: PrescriptionsComponent},
  { path: 'login/callback', component: OktaCallbackComponent },
  //placeholder redirect to update patient information
  { path: 'UpdatePatient', component: PatientsComponent }
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }