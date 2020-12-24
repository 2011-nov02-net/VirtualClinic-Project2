import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Patient } from '../models/patient';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PatientDetailsService {
  private baseUrl = environment.urlBase;
  constructor(private http: HttpClient) { }

   /** GET patient from the server */
 getPatientByID(id: number): Promise<Patient>{
  return this.http.get<Patient>(`${this.baseUrl}/Patients/${id}`).toPromise();
}

/** POST Patient to the server */
postPatient(patient: Patient): Promise<Patient> {
  return this.http.post<Patient>(`${this.baseUrl}/Patients/`, patient).toPromise();
}

/**Update Patient */
async updatePatient(patient: Patient): Promise<Patient>{
  return this.http.put<Patient>(`${this.baseUrl}/Patients/${patient.id}`, patient).toPromise()
}

}
