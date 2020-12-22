import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Doctor } from '../models/doctor';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DoctorsService {

  private baseUrl = 'https://localhost:44317/api'

  constructor(private http: HttpClient) { }

  getDoctorByID(id: number): Promise<Doctor> {

    return this.http.get<Doctor>(`${this.baseUrl}/Doctors/${id}`)
    .toPromise();

    return this.http.get<Doctor>(`${this.baseUrl}/Doctors/${id}`, {headers: new HttpHeaders({'Access-Control-Allow-Origin':'*'})})
      .toPromise();
  }
}
