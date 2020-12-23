import { Component, Input, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Patient } from '../models/patient';
import { Prescription } from '../models/prescription';
import { PatientReports } from '../models/patientreport';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { PatientsService } from '../services/patients.service';


@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.scss']
})

export class PatientsComponent implements OnInit {
   @Input() patients: Patient[] | undefined;
   selectedPrescrition: Prescription | undefined;
   selectedReport: PatientReports | undefined;
  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private patientService: PatientsService
  ) { }

  ngOnInit(): void {
    this.getPatients();
  }



  getPatients(): void {
    this.patientService.getPatients()
      .then(patients => {
        this.patients = patients;
      })
  }

  onSelect(prescription: Prescription): void {
    this.selectedPrescrition = prescription;
  };

  
  goBack(): void {
    this.location.back();
  }
}
