import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OktaAuthService } from '@okta/okta-angular';

@Injectable({
  providedIn: 'root'
})
export class UsertypeService {

  constructor(private http: HttpClient, private oktaAuth: OktaAuthService) { }

  //todo: ask the db what type of user this is
  //0 == not logged in
  //1 == patient
  //2 == doctor
  //note, the higher number the more privlidge generally
  public GetUserEnum() : number{
    //const authenticated = this.oktaAuth.isAuthenticated(1)
    const authenticated = true;
    if(authenticated)
      {
        return 2;
      } else {
        return 0;
      }
  }
}
