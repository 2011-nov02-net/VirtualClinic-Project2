import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { PatientReports } from '../models/patientreport';

@Injectable({
  providedIn: 'root'
})
export class PatientReportsService {
  private baseUrl = environment.urlBase


  constructor(private http: HttpClient) { }

  getPatientReports(id: number): Promise<PatientReports[]> {
    return this.http.get<PatientReports[]>(`${this.baseUrl}/Patients/${id}/Reports`).toPromise();
  }

  addPatientReport(id: number, report: PatientReports): Promise<PatientReports> {
    return this.http.post<PatientReports>(`${this.baseUrl}/Patients/${id}`, report).toPromise();
  }
}
