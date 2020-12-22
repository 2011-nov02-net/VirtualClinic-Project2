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

 /** GET patient from the server */
 getPatientByID(id: number): Promise<Patient>{
  return this.http.get<Patient>(`${this.baseUrl}/Patients/${id}`).toPromise();
}

/** POST Patient to the server */
postPatient(patient: Patient): Promise<Patient> {
  return this.http.post<Patient>(`${this.baseUrl}/Patients/`, patient).toPromise();
}

}
