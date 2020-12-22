import { Component, OnInit, Input } from '@angular/core';
import { UpperCasePipe } from '@angular/common';
import { DoctorsComponent } from '../doctors/doctors.component';
import { Patient } from '../models/patient';
import { Doctor } from '../models/doctor';

@Component({
  selector: 'app-patient-detail',
  templateUrl: './patient-detail.component.html',
  styleUrls: ['./patient-detail.component.css']
})
export class PatientDetailComponent implements OnInit {
  @Input() patient: Patient;
  constructor() { 
    this.patient = {
      id : -1,
      name: "name",
      insuranceProvider: "na",
      reports: [],
      prescriptions: [],
      doctor: {
        id: -1,
        name: "does not exist",
        patients: [],
        title: "dr dr",
        timeslots: [],
      },
      birthday: new Date(),
      ssn: "1234-45-678",
    };
  }

  ngOnInit(): void {
  }

}
