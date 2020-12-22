import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OktaAuthService } from '@okta/okta-angular';
import { environment } from 'src/environments/environment';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  
})
export class AppComponent implements OnInit {
  title = 'VirtualClinicFrontEnd';
  isAuthenticated = false;

  username = "Not Logged In"

  links = [
    /*{titel: link display text, }
     * fragment: url fragment. shows up as "#fragment" appended at the end,
     * page: the page to route to}
     */
    { title: 'Doctors', fragment: '', page:"Doctors" },
    { title: 'Patients', fragment: '', page:"Patients" }
  ];

  //https://ng-bootstrap.github.io/#/components/nav/overview#routing
  //gives the component a ref to the active route for the nav
  constructor(
    public route: ActivatedRoute,
    private oktaAuth: OktaAuthService
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
      if (isAuthenticated) {
        this.oktaAuth.getUser().then(console.log);
        this.username = this.oktaAuth.getUser().then();
      }
    }

    setUsername(user){
      this.username = user.email;
    }
  
    login() {
      this.oktaAuth.signInWithRedirect();
    }
  
    logout() {
      this.oktaAuth.signOut();
    }
}
