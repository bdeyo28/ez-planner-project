import { Component, OnInit } from '@angular/core';
import { Attendee } from 'src/app/interfaces/Attendee';
import { AttendeeService } from 'src/app/services/attendee.service';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { EventService } from 'src/app/services/event.service';
import { Event } from 'src/app/interfaces/Event';

@Component({
  selector: 'app-addattendee',
  templateUrl: './addattendee.component.html',
  styleUrls: ['./addattendee.component.css']
})
export class AddattendeeComponent implements OnInit {

  name: string;
  email: string;

  attendeeEvent: Event;
  eventList: Event[];

  constructor(private attendeeService: AttendeeService,
    private eventService: EventService,
    private router: Router) { }

  ngOnInit(): void {

    this.eventService.getAllEvents().subscribe(list => {
      this.eventList = list;

      this.attendeeEvent = this.eventList[this.eventList.length - 1];
    });
  }

  addAttendee() {
    var element = document.getElementById("addAttendeeError");
    element.innerHTML = "";
    element.style.display = "none";
    if (this.name == null || this.name == undefined ||
      this.email == null || this.email == undefined) {
      element.style.display = "block";
      element.innerHTML += "<i><b>Please fill out all information.</b></i>";
      this.email = "";
      this.name = "";
    }
    else {

      let toAdd: Attendee = {
        name: this.name, email: this.email, eventId: this.attendeeEvent.id
      };

      this.attendeeService.addAttendee(toAdd).subscribe((_) => console.log(_));

      document.getElementById("attendeeAdded").innerHTML += `Added: ${toAdd.name}` + "<br>";
      this.name = '';
      this.email = '';
    }
  }

  nextPage() {
    this.router.navigate(["addActivity"]);
  }

}
