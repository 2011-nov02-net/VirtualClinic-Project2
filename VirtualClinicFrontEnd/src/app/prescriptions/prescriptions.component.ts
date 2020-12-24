import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Prescription } from '../models/prescription';
import { Location } from '@angular/common';
import { PrescriptionsService} from '../services/prescriptions.service'
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-prescriptions',
  templateUrl: './prescriptions.component.html',
  styleUrls: ['./prescriptions.component.scss']
})
export class PrescriptionsComponent implements OnInit {

  prescriptions: Prescription[] | undefined;
  singlePrescription: Prescription | undefined;
  selectedPrescription: Prescription | undefined;
  patientID: number;

   
  constructor(
    private route: ActivatedRoute,
    private location:Location,
    private prescriptionService: PrescriptionsService
  ) { 
    this.patientID = this.route.snapshot.params['id'];
   }

  ngOnInit(): void {
    this.getPatientsPrescriptions(this.patientID);
  }

  getPatientsPrescriptions(id: number): void {
    this.prescriptionService.getPatientPrescriptions(id)
    .then(prescriptions => {
      this.prescriptions = prescriptions
    })
  }

  getPrescriptionById(id: number): void {
    this.prescriptionService.getPrescriptionById(id)
    .then(prescription => {this.singlePrescription = prescription});
  }

  onSelect(prescription: Prescription): void {
    this.selectedPrescription = prescription;
  }

  goBack(): void {
    this.location.back();
  }
}
