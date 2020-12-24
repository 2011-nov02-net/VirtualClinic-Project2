import { HttpClient } from '@angular/common/http';
import { stringify } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OktaAuthService} from '@okta/okta-angular';
import { User } from './models/user'



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  
})
export class AppComponent implements OnInit {
  title = 'VirtualClinicFrontEnd';
  isAuthenticated = false;
  username:string = "Unknown"
  //0 == not logged in
  //1 == patient
  //2 == doctor
  usetype: number = 0;

  links:NavBarLink[] = [];


  //https://ng-bootstrap.github.io/#/components/nav/overview#routing
  //gives the component a ref to the active route for the nav
  constructor(
    public route: ActivatedRoute,
    private oktaAuth: OktaAuthService,
    private http: HttpClient
    /*, private api service*/) 
    {    
      this.oktaAuth.$authenticationState.subscribe((isAuthenticated) =>
        this.updateAuthState(isAuthenticated)
      );
    }

    ngOnInit(): void {
      this.oktaAuth
        .isAuthenticated()
        .then((isAuthenticated) => this.updateAuthState(isAuthenticated));


      if(this.isAuthenticated){

        var response = this.http.get('https://localhost:44317/api/Authentication', {
          headers: {
            Authorization: 'Bearer ' + this.oktaAuth.getAccessToken(),
          }
        }).toPromise();
    
        response.then(console.log)

        response.then( responseval =>
            this.translateUserTypeAndUpdate( responseval.toString())            
            );


      }
    }


    updateAuthState(isAuthenticated: boolean) {
      this.isAuthenticated = isAuthenticated;
      this.updateuserinfo();
    }

    async updateuserinfo(){
      if (this.isAuthenticated) {
        this.oktaAuth.getUser().then(console.log);
        const userClaims = this.oktaAuth.getUser();
        const realClaims  = (await userClaims);

        this.setUsername(realClaims)

        var response = this.http.get('https://localhost:44317/api/Authentication', {
          headers: {
            Authorization: 'Bearer ' + this.oktaAuth.getAccessToken(),
          }
        }).toPromise();
    
        response.then( responseval =>
            this.translateUserTypeAndUpdate( responseval.toString())            
            );

      }
    }

    setUsername(user:any){
      this.username = user.email;
    }
  
    login() {
      this.oktaAuth.signInWithRedirect();
    }
  
    logout() {
      this.oktaAuth.signOut();
    }


    translateUserTypeAndUpdate(typeResponse:string){
      if(typeResponse && typeResponse == 'doctor'){
        this.usetype = 2;
      } else if(typeResponse && typeResponse == 'patient') {
        this.usetype = 1;
      } else {
        this.usetype = 0;
      }

      this.updateLinks();
    }


      /*{title: link display text, }
     * fragment: url fragment. shows up as "#fragment" appended at the end,
     * page: the page to route to}
     */
    updateLinks(){
      if(this.isAuthenticated){
        if(this.usetype === 2){
          //doctor, 2
          this.links = [{ title: 'Patients', fragment: '', page:"Patients" },
          { title: 'Doctors', fragment: '', page:"Doctors" }] ;
        } else if(this.usetype === 1){
          //patient, 1
          this.links = [{ title: 'Doctors', fragment: '', page:"Doctors" }];
        } else {
          this.links = [] ;
        } 
      }

      //not logged in, or not authenticated
      //else
      return[{title: "Please log in!", fragment:'', page:''}]
    }
}


interface NavBarLink{
  title: string,
  fragment: string | undefined
  page: string | undefined
}

