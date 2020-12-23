import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Patient } from '../models/patient';

@Injectable({
  providedIn: 'root'
})
export class PatientDetailsService {
  private baseUrl = 'https://localhost:44317/api/';
  constructor(private http: HttpClient) { }

   /** GET patient from the server */
 getPatientByID(id: number): Promise<Patient>{
  return this.http.get<Patient>(`${this.baseUrl}/Patients/${id}`).toPromise();
}

/** POST Patient to the server */
postPatient(patient: Patient): Promise<Patient> {
  return this.http.post<Patient>(`${this.baseUrl}/Patients/`, patient).toPromise();
}

}
