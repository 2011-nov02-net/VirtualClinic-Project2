import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Doctor } from '../models/doctor';
import { Observable } from 'rxjs';
import { AppModule } from '../app.module';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DoctorsService {

  private baseUrl = environment.urlBase

  constructor(private http: HttpClient) { }

  getDoctorByID(id: number): Promise<Doctor> {
    return this.http.get<Doctor>(`${this.baseUrl}/Doctors/${id}`).toPromise();
  }
  
  getDocotrs(): Promise<Doctor[]> {
    return this.http.get<Doctor[]>(`${this.baseUrl}/Doctors`).toPromise();
  }
}
