import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Prescription } from '../models/prescription';
import { Location } from '@angular/common';
import { PrescriptionsService} from '../services/prescriptions.service'

@Component({
  selector: 'app-prescriptions',
  templateUrl: './prescriptions.component.html',
  styleUrls: ['./prescriptions.component.scss']
})
export class PrescriptionsComponent implements OnInit {
  prescription: Prescription | undefined;

   
  constructor(
    private route: ActivatedRoute,
    private location:Location,
    private prescriptionService: PrescriptionsService
  ) { }

  ngOnInit(): void {
    this.getPrescriptionById(1);
  }

  getPrescriptionById(id: number): void {
    this.prescriptionService.getPrescriptionById(id)
    .then(prescription => {
      this.prescription = prescription
    })
  }

  goBack(): void {
    this.location.back();
  }
}
