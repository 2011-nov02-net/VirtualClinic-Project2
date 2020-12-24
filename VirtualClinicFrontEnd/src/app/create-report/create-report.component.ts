import { Component, OnInit } from '@angular/core';
import { PatientReportsService } from '../services/patient-reports.service';
import { PatientReports } from '../models/patientreport';
@Component({
  selector: 'app-create-report',
  templateUrl: './create-report.component.html',
  styleUrls: ['./create-report.component.scss']
})
export class CreateReportComponent implements OnInit {

  info:string = " ";

  constructor(private PatientReport:PatientReportsService) { 

  }

  ngOnInit(): void {

  }


  CreateNewReport(){
    console.log(this.info.toString())
    console.log(this)

    var report : PatientReports = {
      id: -1,
      patientId : 13,
      doctorId : 1,
      time : new Date(),
      info : this.info,
      vitals: null,
      doctor:null,
      patient:null
    }

    this.PatientReport.addPatientReport(-1, report );
  }

}
