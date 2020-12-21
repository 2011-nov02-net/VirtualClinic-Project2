import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { DoctorsComponent } from '../app/doctors/doctors.component';
import { PatientDetailComponent} from '../app/patient-detail/patient-detail.component';



const routes: Routes = [
  { path: 'Doctors/:id', component: DoctorsComponent },
  { path:  'Patients/:id', component: PatientDetailComponent}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
