import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OktaAuthService } from '@okta/okta-angular';

@Injectable({
  providedIn: 'root'
})
export class UsertypeService {

  constructor(private http: HttpClient, private oktaAuth: OktaAuthService) { }

  //todo: ask the db what type of user this is
  public IsDoctor(){
    return true;
  }
}
