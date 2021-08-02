import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Activity } from 'src/app/interfaces/Activity';
import { Attendee } from 'src/app/interfaces/Attendee';
import {Event} from 'src/app/interfaces/Event';
import { Organizer } from 'src/app/interfaces/Organizer';
import { ActivityService } from 'src/app/services/activity.service';
import { AttendeeService } from 'src/app/services/attendee.service';
import { EventService } from 'src/app/services/event.service';
import { OrganizerService } from 'src/app/services/organizer.service';

@Component({
  selector: 'app-eventdetails',
  templateUrl: './eventdetails.component.html',
  styleUrls: ['./eventdetails.component.css']
})
export class EventdetailsComponent implements OnInit {

  eventID : number;
  eventToView : Event;
  activities : Activity[];
  attendees : Attendee[];
  organizer : Organizer;
  count : number = 0;

  constructor(private activatedRoute : ActivatedRoute,
    private router: Router,
    private eventService : EventService,
    private organizerService: OrganizerService) { }

  ngOnInit(): void {
    let idName: string = this.activatedRoute.snapshot.paramMap.get("id");
    this.eventID = parseInt(idName);

    this.eventService.getEventById(this.eventID).subscribe(event => {
      this.eventToView = event;

      if (this.eventToView == null) {
        this.router.navigate([""]);
      }

      this.eventService.getEventActivities(this.eventID).subscribe(list => {
        this.activities = list;
      })

      this.eventService.getEventAttendees(this.eventID).subscribe(list => {
        this.attendees = list;
      })

      this.organizerService.getOrganizerById(this.eventToView.organizerId).subscribe(org => {
        this.organizer = org;
      })
    })
  }

  print() {
    if (this.count == 0)
    {
      this.count = 1;
      setTimeout(() => {
        window.print();
      }
      , 2500);
    }
  }

}
