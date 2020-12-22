import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Vitals } from '../models/vitals';

@Injectable({
  providedIn: 'root'
})
export class VitalsService {

  private baseUrl = environment.urlBase

  constructor(private http: HttpClient) { }

  getVitalsById(id: number): Promise<Vitals> {
    return this.http.get<Vitals>(`${this.baseUrl}/Vitals/${id}`).toPromise();
  }
  addVitals(vitals: Vitals): Promise<Vitals> {
    return this.http.post<Vitals>(`${this.baseUrl}/Vitals`, vitals).toPromise();
  }
}
