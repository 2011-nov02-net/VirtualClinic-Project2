import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PatientReportsService {
  private baseUrl = environment.urlBase


  constructor(private http: HttpClient) { }
}
