import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LogInComponent } from './log-in/log-in.component';
import { OktaCallbackComponent } from '@okta/okta-angular';

const routes: Routes = [
  { path: 'login', component: LogInComponent },
  { path: 'login/callback', component: OktaCallbackComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }