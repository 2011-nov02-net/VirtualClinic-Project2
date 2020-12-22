import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Prescription } from '../models/prescription';

@Injectable({
  providedIn: 'root'
})
export class PrescriptionsService {

  private baseUrl = 'https://localhost:44317/api/'
  constructor(private http: HttpClient) { }

 /** GET Prescription  from the server */
 getPrescriptionById(id: number): Promise<Prescription>{
  return this.http.get<Prescription>(`${this.baseUrl}/Prescriptions/${id}`).toPromise();
}

/** POST Prescription to the server */
postPatient(prescription: Prescription): Promise<Prescription> {
  return this.http.post<Prescription>(`${this.baseUrl}/Prescriptions/`, prescription).toPromise();
}
}
