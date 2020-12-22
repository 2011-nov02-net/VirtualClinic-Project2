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
   @Input() patient: Patient | undefined;
   selectedPrescrition: Prescription | undefined;
   selectedReport: PatientReports | undefined;
  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private patientService: PatientsService
  ) { }

  ngOnInit(): void {
  }
  getPatientByID(id: number): void {
    this.patientService.getPatientByID(id)
      .then(patient => {
        this.patient = patient;
      })
  }

  onSelect(prescription: Prescription): void {
    this.selectedPrescrition = prescription;
  };

  // onSelect(report: PatientReports): void {
  //   this.selectedReport = report;
  // };

  goBack(): void {
    this.location.back();
  }
}
