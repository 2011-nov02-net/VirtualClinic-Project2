import { Component, OnInit } from '@angular/core';
import { Doctor } from '../doctor';
import { Patient}  from '../patient';
import { SharedService} from '../shared.service';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-doctors',
  templateUrl: './doctors.component.html',
  styleUrls: ['./doctors.component.css']
})
export class DoctorsComponent implements OnInit {
  doctor: Doctor | undefined;
   
  selectedPatient : Patient | undefined;

  constructor(
    private sharedService : SharedService,
    private route: ActivatedRoute,
    private location: Location
    ) { }

  ngOnInit(): void {
    this.getDoctor();

  }


  getDoctor(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.sharedService.getDoctor(id)
      .subscribe(doctor => this.doctor = doctor);
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
