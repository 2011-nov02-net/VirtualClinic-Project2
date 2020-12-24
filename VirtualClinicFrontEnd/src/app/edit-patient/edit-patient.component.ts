import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Router, ActivatedRoute  } from '@angular/router';
import { Patient } from '../../app/models/patient';
import { Location } from '@angular/common';
import { PatientDetailsService } from '../services/patient-details.service';



@Component({
  selector: 'app-edit-patient',
  templateUrl: './edit-patient.component.html',
  styleUrls: ['./edit-patient.component.scss']
})
export class EditPatientComponent implements OnInit {
  
    constructor(
     private patientDetailsService: PatientDetailsService,
      private formBuilder : FormBuilder,
      private router: Router,
      private route: ActivatedRoute,
      private location: Location,
    
  
      ) { 
      this.patientId = this.route.snapshot.params['id'];
    }
    patientId: number;

    myForm!: FormGroup;
     name!: FormControl;
     ssn!: FormControl;
     dob!: FormControl;
     insuranceProvider!: FormControl;

     patient!: Patient;
     id!: number;
     nameString!: string;
     emailString!: string;
     ssnString!: string;
     dobstring!: Date;
     insuranceString!: String;


  ngOnInit(): void {

     this.name = new FormControl('');
     this.ssn =  new FormControl('');
     this.dob =  new FormControl('');
     this.insuranceProvider = new FormControl('');

     this.myForm = this.formBuilder.group(
      {
        'name': this.name,
        'ssn': this.ssn,
        'dob': this.dob,
        'ininsuranceProvider': this.insuranceProvider

      }
    )
  }

  async updatePatient(){
    this.patient = await this.patientDetailsService.getPatientByID(this.patientId).then(p => this.patient = p);
    this.patient.name = this.myForm.get('name')?.value;
    this.patient.ssn = this.myForm.get('ssn')?.value;
    this.patient.birthday = this.myForm.get('dob')?.value;
    this.insuranceProvider = this.myForm.get('insuranceProvider')?.value;
    await this.patientDetailsService.updatePatient(this.patient);
    this.router.navigate(['/Patients/'+ this.patient.id]);
  }
  goBack(): void {
    this.location.back();
  }

}

