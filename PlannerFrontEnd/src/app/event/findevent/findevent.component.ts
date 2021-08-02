import { listLazyRoutes } from '@angular/compiler/src/aot/lazy_routes';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/auth.service';
import { Activity } from 'src/app/interfaces/Activity';
import { Attendee } from 'src/app/interfaces/Attendee';
import { Event } from 'src/app/interfaces/Event';
import { Organizer } from 'src/app/interfaces/Organizer';
import { EventService } from 'src/app/services/event.service';
import { OrganizerService } from 'src/app/services/organizer.service';
import 'src/assets/smtp.js';
declare let Email: any;

@Component({
  selector: 'app-findevent',
  templateUrl: './findevent.component.html',
  styleUrls: ['./findevent.component.css']
})
export class FindeventComponent implements OnInit {

  eventName: string;
  selectEvent: Event;
  organizer: Organizer;

  signedIn: boolean = false;

  checkDelete: boolean = false;

  currentUserList: Event[];

  activities: Activity[];
  attendees: Attendee[];

  constructor(private eventService: EventService,
    private organizerService: OrganizerService,
    private router: Router,
    private authService: AuthService) { }

  ngOnInit(): void {
    this.eventService.getAllEvents().subscribe((list) => {
      this.currentUserList = list;
    })

    this.signedIn = this.authService.isSignedIn();

    this.authService.loginChangedEvent.subscribe((signedIn) => this.signedIn = signedIn);
  }

  submit() {
    this.eventService.getEventByName(this.eventName).subscribe(event => {
      this.selectEvent = event;

      this.organizerService.getOrganizerById(this.selectEvent.organizerId).subscribe(org => {
        this.organizer = org

        this.eventService.getEventActivities(this.selectEvent.id).subscribe(actList => {
          this.activities = actList;

          this.eventService.getEventAttendees(this.selectEvent.id).subscribe(attList => {
            this.attendees = attList;

            var element = document.getElementById("eventDetails");

            element.innerHTML = "";

            element.style.display = 'block';


            element.innerHTML += `<h1><i>EVENT SUMMARY</i><h1>`;
            element.innerHTML += `<h3>Event Name: ${this.selectEvent.eventName}<br>`;
            element.innerHTML += `<h3>Event Date: ${this.selectEvent.date.toString().substring(0, 10)}<br>`;
            element.innerHTML += `<h3>Event Location: ${this.selectEvent.location}<br>`;
            element.innerHTML += `<h3>Event Time: ${this.selectEvent.time}<br>`
            element.innerHTML += `<h3>Organizer: ${this.organizer.name}<br>`;

            var editElement = document.getElementById("editEvent");

            editElement.style.display = 'inline-block';

            var attendeeDiv = document.getElementById("attendees");
            var activityDiv = document.getElementById("activities");

            attendeeDiv.style.display = 'block';
            activityDiv.style.display = 'block';

          })
        })
      })
    });
  }

  navigate() {
    this.router.navigate(["editEvent/" + this.selectEvent.id]);
  }

  navigateLogin() {
    this.router.navigate(["login"]);
  }

  getDetails() {
    this.router.navigate(["eventDetails/" + this.selectEvent.id]);
  }

  renavigate() {
    this.router.navigate(["findEvent"]);
  }

  delete() {
    this.checkDelete = true;
  }

  confirm() {
    var eventDetails = document.getElementById("eventDetails");
    eventDetails.innerHTML = "";
    eventDetails.style.display = "none";
    var att = document.getElementById("attendees");
    att.style.display = "none";
    var act = document.getElementById("activities");
    act.style.display = "none";
    var editEvent = document.getElementById("editEvent");
    editEvent.style.display = "none";
    this.eventService.deleteEvent(this.selectEvent.id).subscribe((_) => console.log(_));
    this.checkDelete = false;
    this.eventName = null;
  }

  unconfirm() {
    this.checkDelete = false;
  }

  // sendEmail() {

  //   // 9E7E4CB499A2D7E174E92DFC269A08E7D870

  //   for (var attendee of this.attendees) {
  //     Email.send({
  //       Host: "smtp.elasticemail.com",
  //       Username: "ezplanner2021@gmail.com",
  //       Password: "9E7E4CB499A2D7E174E92DFC269A08E7D870",
  //       To: attendee.email,
  //       From: "ezplanner2021@gmail.com",
  //       Subject: "You're Invited!",
  //       Body: ""
  //     })
  //   }
  
}
