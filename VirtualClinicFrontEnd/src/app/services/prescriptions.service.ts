import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Prescription } from '../models/prescription';

@Injectable({
  providedIn: 'root'
})
export class PrescriptionsService {
  private baseUrl = environment.urlBase
  selectedPrescription: Prescription | undefined;

  constructor(private http: HttpClient) { }
}
