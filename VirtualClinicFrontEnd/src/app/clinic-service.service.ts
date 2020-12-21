import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OktaAuthService } from '@okta/okta-angular';

@Injectable({
  providedIn: 'root'
})
export class ClinicServiceService {

  constructor(private http: HttpClient, private oktaAuth: OktaAuthService) { }
}
