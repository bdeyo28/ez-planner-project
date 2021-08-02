import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomepageComponent } from './homepage/homepage.component';
import { ContactpageComponent } from './contactpage/contactpage.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatIconModule } from '@angular/material/icon';
import {MatMenuModule} from '@angular/material/menu'
import {MatButtonModule} from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar'
import { MatCardModule } from '@angular/material/card'
import { FormsModule } from '@angular/forms';
import { CarouselComponent } from './carousel/carousel.component';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { EventComponent } from './event/event.component';
import { FindeventComponent } from './event/findevent/findevent.component';
import { ScheduleeventComponent } from './event/scheduleevent/scheduleevent.component';
import { ActivityComponent } from './activity/activity.component';
import { AddactivityComponent } from './activity/addactivity/addactivity.component';
import { EditactivityComponent } from './activity/editactivity/editactivity.component';
import { AttendeeComponent } from './attendee/attendee.component';
import { AddattendeeComponent } from './attendee/addattendee/addattendee.component';
import { EditattendeeComponent } from './attendee/editattendee/editattendee.component';
import { EditeventComponent } from './event/editevent/editevent.component';
import { UserregistrationComponent } from './userregistration/userregistration.component';
import { LoginComponent } from './login/login.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from './token.interceptor';
import { EventdetailsComponent } from './event/eventdetails/eventdetails.component';

@NgModule({
  declarations: [
    AppComponent,
    HomepageComponent,
    ContactpageComponent,
    CarouselComponent,
    EventComponent,
    FindeventComponent,
    ScheduleeventComponent,
    ActivityComponent,
    AddactivityComponent,
    EditactivityComponent,
    AttendeeComponent,
    AddattendeeComponent,
    EditattendeeComponent,
    EditeventComponent,
    UserregistrationComponent,
    LoginComponent,
    EventdetailsComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatSidenavModule,
    MatToolbarModule,
    MatIconModule,
    MatCardModule,
    MatMenuModule,
    HttpClientModule,
    NgbModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: TokenInterceptor,
    multi : true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
