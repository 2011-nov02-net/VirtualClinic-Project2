import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PatientReports } from '../models/patientreport';
import { Location } from '@angular/common';
import { PatientReportsService } from '../services/patient-reports.service';

@Component({
  selector: 'app-report-details',
  templateUrl: './report-details.component.html',
  styleUrls: ['./report-details.component.scss']
})
export class ReportDetailsComponent implements OnInit {
  @Input() report: PatientReports | undefined;
  reportID: number;

  constructor(    
    private route: ActivatedRoute,
    private location: Location,
    private patientReportsService: PatientReportsService) {
      this.reportID = this.route.snapshot.params['id'];
     }

  ngOnInit(): void {
    this.patientReportsService.getPatientReportByID(this.reportID)
    .then(report => { this.report = report;})
  }

  goBack(): void {
    this.location.back();
  }

}
