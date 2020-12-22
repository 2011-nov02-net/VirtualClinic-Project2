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

  getPrescriptionById(id: number): Promise<Prescription> {
    return this.http.get<Prescription>(`${this.baseUrl}/Prescriptions/${id}`).toPromise();
  }
  getPatientPrescriptions(patientID: number): Promise<Prescription[]> {
    return this.http.get<Prescription[]>(`${this.baseUrl}/Patients/${patientID}/Prescriptions`).toPromise();
  }
  addPrescription(patientID: number, prescription: Prescription): Promise<Prescription> {
    return this.http.post<Prescription>(`${this.baseUrl}/Patients/${patientID}`, prescription).toPromise();
  }
}
