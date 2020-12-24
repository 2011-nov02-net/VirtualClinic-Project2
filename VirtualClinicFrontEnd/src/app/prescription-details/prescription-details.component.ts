import { Component, OnInit, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Prescription } from '../models/prescription';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { PrescriptionsService } from '../services/prescriptions.service';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-prescription-details',
  templateUrl: './prescription-details.component.html',
  styleUrls: ['./prescription-details.component.scss']
})
export class PrescriptionDetailsComponent implements OnInit {

  @Input() prescription: Prescription | undefined;
   prescriptionId : number;


  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private prescriptionsService : PrescriptionsService 
  ) { 
    this.prescriptionId = this.route.snapshot.params['id'];
  }

  ngOnInit(): void {
    this.getPrescriptionById(this.prescriptionId);
  }

  getPrescriptionById(id: number) : void {
    this.prescriptionsService.getPrescriptionById(id)
    .then(prescription => {
      this.prescription = prescription;
    })
  }

  goBack(): void {
    this.location.back();
  }
    

}
