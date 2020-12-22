import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Appointment } from '../models/appointment';

@Injectable({
  providedIn: 'root'
})
export class AppointmentsService {
  private baseUrl = environment.urlBase
  constructor(private http: HttpClient) { }

  getPatientAppointments(patientID: number): Promise<Appointment[]> {
    return this.http.get<Appointment[]>(`${this.baseUrl}/Patients/${patientID}/Appointments`).toPromise();
  }
  getDoctorAppointments(doctorID: number): Promise<Appointment[]> {
    return this.http.get<Appointment[]>(`${this.baseUrl}/Doctors/${doctorID}/Appointments`).toPromise();
  }
  AddAppointment(id: number, appointment: Appointment): Promise<Appointment> {
    return this.http.post<Appointment>(`${this.baseUrl}/Appointments/${id}`, appointment).toPromise();
  }
}
