import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OktaAuthService} from '@okta/okta-angular';
import { environment } from 'src/environments/environment';
import { User } from './models/user'
import { UsertypeService } from './usertype.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  
})
export class AppComponent implements OnInit {
  title = 'VirtualClinicFrontEnd';
  isAuthenticated = false;
  username:string = "Unknown"


  //https://ng-bootstrap.github.io/#/components/nav/overview#routing
  //gives the component a ref to the active route for the nav
  constructor(
    public route: ActivatedRoute,
    private oktaAuth: OktaAuthService,
    private userType: UsertypeService
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
      }
    }

    setUsername(user : UserClaims){
      this.username = user.email;
    }
  
    login() {
      this.oktaAuth.signInWithRedirect();
    }
  
    logout() {
      this.oktaAuth.signOut();
    }



      /*{title: link display text, }
     * fragment: url fragment. shows up as "#fragment" appended at the end,
     * page: the page to route to}
     */
    getLinks(){
      if(this.userType.IsDoctor()){
        return [{ title: 'Patients', fragment: '', page:"Patients" },
                { title: 'Doctors', fragment: '', page:"Doctors" }] ;
      } else {
        return [{ title: 'Doctors', fragment: '', page:"Doctors" }];
      }
    }
}
