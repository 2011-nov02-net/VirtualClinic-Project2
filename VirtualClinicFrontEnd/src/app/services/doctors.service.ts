import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Doctor } from '../models/doctor';

@Injectable({
  providedIn: 'root'
})
export class DoctorsService {

  private baseUrl = 'http://localhost:44317/api'

  constructor(private http: HttpClient) { }

  getDoctorByID(id: number): Promise<Doctor> {
    return this.http.get<Doctor>(`/doctors/${id}`)
      .toPromise();
  }
}
