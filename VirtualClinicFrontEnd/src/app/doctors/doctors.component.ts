import { Component, OnInit } from '@angular/core';
import { Doctor } from '../models/doctor';
import { Patient }  from '../models/patient';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { DoctorsService } from '../services/doctors.service';


@Component({
  selector: 'app-doctors',
  templateUrl: './doctors.component.html',
  styleUrls: ['./doctors.component.css']
})
export class DoctorsComponent implements OnInit {
  doctor: Doctor | undefined;
  selectedPatient : Patient | undefined;

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private doctorService: DoctorsService
    ) { }

  ngOnInit(): void {
    this.getDoctorByID(1);

  }

  getDoctorByID(id: number): void {
    this.doctorService.getDoctorByID(id)
      .then(doctor => {
        this.doctor = doctor;
      })
  }

  onSelect(patient: Patient): void {
    this.selectedPatient = patient;
  }
  
  goBack(): void {
    this.location.back();
  }
  // getDoctor(id) : void {
  //   this.sharedService.getDoctor(id).subscribe(doctor => this.doctor = doctor);
  // }
}
