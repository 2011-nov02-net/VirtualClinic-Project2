import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PatientReports } from '../models/patientreport';
import { Location } from '@angular/common';
import { PatientReportsService } from '../services/patient-reports.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-patient-reports',
  templateUrl: './patient-reports.component.html',
  styleUrls: ['./patient-reports.component.scss']
})
export class ReportsComponent implements OnInit {
  reports: PatientReports[] | undefined;
  singleReport: PatientReports | undefined;
  selectedReport: PatientReports | undefined;
  patientID: number;
   
  constructor(
    private route: ActivatedRoute,
    private location:Location,
    private reportsService: PatientReportsService
  ) { 
    this.patientID = this.route.snapshot.params['id'];
   }

  ngOnInit(): void {
    this.getPatientsReports(this.patientID);
  }

  getPatientsReports(id: number): void {
    this.reportsService.getPatientReports(id)
    .then(reports => {
      this.reports = reports
    })
  }

  getPrescriptionById(id: number): void {
    this.reportsService.getPatientReportByID(id)
    .then(report => {this.singleReport = report});
  }

  onSelect(report: PatientReports): void {
    this.selectedReport = report;
  }

  goBack(): void {
    this.location.back();
  }
}
