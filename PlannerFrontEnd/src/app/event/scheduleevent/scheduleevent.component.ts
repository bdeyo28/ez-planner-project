import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/auth.service';
import { Event } from 'src/app/interfaces/Event';
import { Organizer } from 'src/app/interfaces/Organizer';
import { EventService } from 'src/app/services/event.service';
import { OrganizerService } from 'src/app/services/organizer.service';

@Component({
  selector: 'app-scheduleevent',
  templateUrl: './scheduleevent.component.html',
  styleUrls: ['./scheduleevent.component.css']
})
export class ScheduleeventComponent implements OnInit {

  category: string;
  organizer : Organizer;
  location: string;
  signedIn : boolean = false;
  time: string;
  eventDate: Date;
  eventName: string;

  constructor(private eventService: EventService,
    private organizerService: OrganizerService,
    private router: Router,
    private authService : AuthService) { }

  ngOnInit(): void {
    this.organizerService.getUserAsOrganizer().subscribe(org => this.organizer = org);

    this.signedIn = this.authService.isSignedIn();

    this.authService.loginChangedEvent.subscribe((signedIn) => this.signedIn = signedIn);

    if(!this.signedIn)
    {
      this.router.navigate([""]);
    }

  }

  addEvent() {
    if (this.category == null || this.category == undefined
      || this.eventDate == null || this.eventDate == undefined
      || this.eventName == null || this.eventName == undefined
      || this.location == null || this.location == undefined
      || this.time == null || this.time == undefined) {
      var element = document.getElementById("addEventError");
      element.style.display = "block";
      element.innerHTML += "<i><b>Please fill out all information.</b></i>";
      this.category = "";
      this.eventDate = null;
      this.eventName = "";
      this.location = "";
      this.time = "";
    }
    else {
      let addEvent: Event = {
        date: this.eventDate, eventName: this.eventName, category: this.category,
        organizerId: this.organizer.id, location : this.location, time : this.time
      }

      this.eventService.addEvent(addEvent).subscribe((_) => this.router.navigate(["addAttendee"]));
    }
  }

}
