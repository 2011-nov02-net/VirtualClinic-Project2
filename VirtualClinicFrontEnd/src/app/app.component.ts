import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  
})
export class AppComponent {
  title = 'VirtualClinicFrontEnd';

  links = [
    /*{titel: link display text, }
     * fragment: url fragment. shows up as "#fragment" appended at the end,
     * page: the page to route to}
     */
    { title: 'One', fragment: 'one', page:"." },
    { title: 'Two', fragment: 'two', page:"." },
    { title: 'Log In', fragment: '', page:"login" }
  ];

  //https://ng-bootstrap.github.io/#/components/nav/overview#routing
  //gives the component a ref to the active route for the nav
  constructor(public route: ActivatedRoute) {}
}
