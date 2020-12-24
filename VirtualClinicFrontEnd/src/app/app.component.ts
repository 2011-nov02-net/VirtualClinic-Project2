import { HttpClient } from '@angular/common/http';
import { stringify } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { OktaAuthService} from '@okta/okta-angular';
import { observable } from 'rxjs';
import { Doctor } from './models/doctor';
import { Patient } from './models/patient';
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

  links:NavBarLink[] = [{ title: 'Home', fragment: '', page:"/" },
  {title: "Please log in!", fragment:'', page:''}];


  //https://ng-bootstrap.github.io/#/components/nav/overview#routing
  //gives the component a ref to the active route for the nav
  constructor(
    public route: ActivatedRoute,
    private oktaAuth: OktaAuthService,
    private http: HttpClient,
    private router: Router
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
        }).toPromise()
        .catch( (error: any) => {
          if(error.status === 404){

          //could be changed into just a redirect and asking what 
          //send request to make new user
          var putResponse = this.http.put('https://localhost:44317/api/Authentication', 
              {},
              {
              headers: {
                Authorization: 'Bearer ' + this.oktaAuth.getAccessToken(),
              }
          })

            this.router.navigate(["UpdatePatient"], {skipLocationChange: false, fragment:"UpdatePatient"} );     
          } else {
            throw error;
          }
        });
    
        console.log("hi from update user info")
        response.then(console.log)

        response.then( responseval =>
              {
                if(responseval){
                  this.translateUserTypeAndUpdate( responseval.toString())   
                } 
              }               
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
        }).toPromise()
        .catch( (error: any) => {
          if(error.status === 404){

          //could be changed into just a redirect and asking what 
          //send request to make new user
          var putResponse = this.http.put('https://localhost:44317/api/Authentication', 
              {},
              {
              headers: {
                Authorization: 'Bearer ' + this.oktaAuth.getAccessToken(),
              }
          })

            this.router.navigate(["UpdatePatient"], {skipLocationChange: false, fragment:"UpdatePatient"} );     
          } else {
            throw error;
          }
        });
    
        console.log("hi from update user info")
        response.then(console.log)

        response.then( responseval =>
              {
                if(responseval){
                  this.translateUserTypeAndUpdate( responseval.toString())   
                } 
              }               
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
          this.links = [{ title: 'Home', fragment: '', page:"/" },
            { title: 'Profile', fragment: '', page:"Doctors" },
          //{ title: 'Patients', fragment: '', page:"Patients" }
          ] ;
        } else if(this.usetype === 1){
          //patient, 1
          this.links = [{ title: 'Home', fragment: '', page:"/" },
            { title: 'Doctor', fragment: '', page:"Doctors" },

          { title: 'Prescriptions', fragment: '', page:"Prescriptions" }];
        } else {
          this.links = [{ title: 'Home', fragment: '', page:"/" }] ;
        } 
      }
      //not logged in, or not authenticated
      //else
      return[{ title: 'Home', fragment: '', page:"/" },
      {title: "Please log in!", fragment:'', page:''}]
    }
}


interface NavBarLink{
  title: string,
  fragment: string | undefined
  page: string | undefined
}

