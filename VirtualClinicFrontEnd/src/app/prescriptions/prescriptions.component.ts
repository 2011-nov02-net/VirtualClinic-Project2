import { Component, OnInit, Input } from '@angular/core';
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
  @Input() prescriptions: Prescription[] | undefined;
  selectedPrescrition: Prescription | undefined;
   
  constructor(
    private route: ActivatedRoute,
    private location:Location,
    private prescriptionService: PrescriptionsService
  ) { }

  ngOnInit(): void {
    this.getPatientsPrescriptions(1);
  }

  getPatientsPrescriptions(id: number): void {
    this.prescriptionService.getPatientPrescriptions(id)
    .then(prescriptions => {
      this.prescriptions = prescriptions
    })
  }

  onSelect(prescription: Prescription): void {
    this.selectedPrescrition = prescription;
  };

  goBack(): void {
    this.location.back();
  }
}
