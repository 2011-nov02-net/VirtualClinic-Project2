import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Patient } from '../models/patient';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PatientsService {
  private baseUrl = environment.urlBase
  constructor(private http: HttpClient) { }

 /** GET patients from the server */
 getPatients(): Promise<Patient[]>{
  return this.http.get<Patient[]>(`${this.baseUrl}/Patients/`).toPromise();
}

/** GET Doctors's patients from the server */
getDoctorsPatients(doctorId: number): Promise<Patient[]>{
  return this.http.get<Patient[]>(`${this.baseUrl}/Doctors/${doctorId}/Patients/`).toPromise();
}

/** POST Patient to the server */
postPatient(patient: Patient): Promise<Patient> {
  return this.http.post<Patient>(`${this.baseUrl}/Patients/`, patient).toPromise();
}

}
