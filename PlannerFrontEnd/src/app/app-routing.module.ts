import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddactivityComponent } from './activity/addactivity/addactivity.component';
import { AddattendeeComponent } from './attendee/addattendee/addattendee.component';
import { ContactpageComponent } from './contactpage/contactpage.component';
import { EditeventComponent } from './event/editevent/editevent.component';
import { EventComponent } from './event/event.component';
import { EventdetailsComponent } from './event/eventdetails/eventdetails.component';
import { FindeventComponent } from './event/findevent/findevent.component';
import { ScheduleeventComponent } from './event/scheduleevent/scheduleevent.component';
import { HomepageComponent } from './homepage/homepage.component';
import { LoginComponent } from './login/login.component';
import { UserregistrationComponent } from './userregistration/userregistration.component';

const routes: Routes = [
  {path: "", component : HomepageComponent},
  {path: "contactMe", component : ContactpageComponent},
  {path: "scheduleEvent", component : ScheduleeventComponent},
  {path: "findEvent", component: FindeventComponent},
  {path: "addAttendee", component: AddattendeeComponent},
  {path: "addActivity", component: AddactivityComponent},
  {path: "confirmEvent", component: EventComponent},
  {path: "editEvent/:id", component: EditeventComponent},
  {path: "register", component: UserregistrationComponent},
  {path: "login", component : LoginComponent},
  {path: "eventDetails/:id", component: EventdetailsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
