import { Component, Input, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Patient } from '../models/patient';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { PatientDetailsService } from '../services/patient-details.service';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-patient-details',
  templateUrl: './patient-details.component.html',
  styleUrls: ['./patient-details.component.scss']
})
export class PatientDetailsComponent implements OnInit {
  
  @Input() patient: Patient | undefined;
  patientID: number;

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private patientDetailsService: PatientDetailsService
  ) {
      this.patientID = this.route.snapshot.params['id'];
   }

  ngOnInit(): void {
    this.getPatientByID(this.patientID);
  }

   getPatientByID(id: number): void{
    this.patientDetailsService.getPatientByID(id)
      .then(patient => {
        this.patient = patient;
      })
  }

  goBack(): void {
    this.location.back();
  }

}
