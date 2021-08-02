import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EventService } from 'src/app/services/event.service';
import { Event } from 'src/app/interfaces/Event';
import { Activity } from 'src/app/interfaces/Activity';
import { Attendee } from 'src/app/interfaces/Attendee';
import { AttendeeService } from 'src/app/services/attendee.service';
import { ActivityService } from 'src/app/services/activity.service';
import { AuthService } from 'src/app/auth.service';

@Component({
  selector: 'app-editevent',
  templateUrl: './editevent.component.html',
  styleUrls: ['./editevent.component.css']
})
export class EditeventComponent implements OnInit {

  eventID: number;
  eventToEdit: Event;
  attendees: Attendee[];
  activities: Activity[];

  attendeeId: number;
  activityId: number;

  addedAttendeeName: string;
  addedAttendeeEmail: string;

  addedActivityName: string;
  addedActivityDuration: number;

  checkDeleteAttendee: boolean = false;
  checkDeleteActivity: boolean = false;

  newActivityName: string;
  newActivityDuration: number;

  newAttendeeName: string;
  newAttendeeEmail: string;

  newCategory: string;
  newName: string;
  newDate: Date;
  newTime: string;
  newLocation: string;

  constructor(private activatedRoute: ActivatedRoute,
    private router: Router,
    private eventService: EventService,
    private attendeeService: AttendeeService,
    private activityService: ActivityService,
    private authService: AuthService) { }

  ngOnInit(): void {

    let idName: string = this.activatedRoute.snapshot.paramMap.get("id");
    this.eventID = parseInt(idName);

    this.eventService.getEventById(this.eventID).subscribe(event => {
      this.eventToEdit = event;

      if (this.eventToEdit == null) {
        this.router.navigate([""]);
      }

      this.eventService.getEventActivities(this.eventID).subscribe(list => {
        this.activities = list;
      })

      this.eventService.getEventAttendees(this.eventID).subscribe(list => {
        this.attendees = list;
      })
    })
  }

  editActivity() {
    if (this.newActivityDuration == null || this.newActivityName == null ||
      this.newActivityName == undefined || this.newActivityDuration == undefined) {
      alert("Please enter valid information to edit.");
    }
    else {
      let editedActivity: Activity = {
        name: this.newActivityName, duration: this.newActivityDuration, id: this.activityId, eventId: this.eventID
      }

      this.activityService.editActivity(editedActivity).subscribe((_) => console.log(_));

      document.getElementById("activityEdited").innerHTML += `<i>Successfully Edited: ${editedActivity.name}</i>` + "<br>";
      this.newActivityName = '';
      this.newActivityDuration = null;
      this.attendeeId = null;

    }
  }

  editAttendee() {
    if (this.newAttendeeEmail == null || this.newAttendeeEmail == undefined || this.newAttendeeName == null
      || this.newAttendeeName == undefined) {
      alert("Please enter valid information to edit.")
    }
    else {
      let editedAttendee: Attendee = {
        id: this.attendeeId, name: this.newAttendeeName, email: this.newAttendeeEmail, eventId: this.eventID
      }

      this.attendeeService.editAttendee(editedAttendee).subscribe((_) => console.log(_));

      document.getElementById("attendeeEdited").innerHTML += `<i>Successfully Edited: ${editedAttendee.name}</i>` + "<br>";
      this.newAttendeeName = '';
      this.newAttendeeEmail = '';
      this.attendeeId = null;
    }
  }

  editEvent() {
    if (this.newName == null || this.newName == undefined
      || this.newDate == null || this.newDate == undefined) {
      alert("Please enter valid information to edit.")
    }
    else {
      let editedEvent: Event = {
        category: this.newCategory, eventName: this.newName, date: this.newDate,
        organizerId: this.eventToEdit.organizerId, duration: this.eventToEdit.duration,
        attendees: this.eventToEdit.attendees, activities: this.eventToEdit.activities,
        id: this.eventID, location: this.newLocation, time: this.newTime
      };

      this.eventService.editEvent(editedEvent).subscribe((_) => console.log(_));

      document.getElementById("eventEdited").innerHTML += `<i>Successfully Edited: ${editedEvent.eventName}</i>` + "<br>";
      this.newName = '';
      this.newDate = null;
      this.newTime = '';
      this.newLocation = '';

    }
  }

  deleteAttendee() {
    this.checkDeleteAttendee = true;

    var addNewAttendee = document.getElementById("addNewAttendee");
    addNewAttendee.style.display = "none";

    var deleteElement = document.getElementById("attendeeDeleted");
    deleteElement.innerHTML = "";

    var editElement = document.getElementById("attendeeEdited");
    editElement.innerHTML = "";

    var addElement = document.getElementById("attendeeAdded");
    addElement.innerHTML = "";
  }

  deleteActivity() {
    this.checkDeleteActivity = true;

    var addNewActivity = document.getElementById("addNewActivity");
    addNewActivity.style.display = "none";

    var deleteElement = document.getElementById("activityDeleted");
    deleteElement.innerHTML = "";

    var editElement = document.getElementById("activityEdited");
    editElement.innerHTML = "";

    var addElement = document.getElementById("activityAdded");
    addElement.innerHTML = "";
  }

  addAttendee() {
    this.checkDeleteAttendee = false;

    var deleteElement = document.getElementById("attendeeDeleted");
    deleteElement.innerHTML = "";

    var editElement = document.getElementById("attendeeEdited");
    editElement.innerHTML = "";

    var addElement = document.getElementById("attendeeAdded");
    addElement.innerHTML = "";

    var thisElement = document.getElementById("addNewAttendee");
    thisElement.style.display = "inline-block";
  }

  addActivity() {
    this.checkDeleteActivity = false;

    var deleteElement = document.getElementById("activityDeleted");
    deleteElement.innerHTML = "";

    var editElement = document.getElementById("activityEdited");
    editElement.innerHTML = "";

    var addElement = document.getElementById("activityAdded");
    addElement.innerHTML = "";

    var thisElement = document.getElementById("addNewActivity");
    thisElement.style.display = "inline-block";
  }

  confirmDeleteActivity() {

    if (this.activityId == null) {
      alert("You have not selected an activity");
    }
    else {
      this.activityService.deleteActivity(this.activityId).subscribe((_) => console.log(_));

      document.getElementById("activityDeleted").innerHTML += `<i>Successfully Deleted!</i>` + "<br>";
      this.newActivityName = '';
      this.newActivityDuration = null;
      this.activityId = null;
      this.checkDeleteActivity = false;
    }
  }

  confirmDeleteAttendee() {

    if (this.attendeeId == null) {
      alert("You have not selected an attendee");
    }
    else {
      this.attendeeService.deleteAttendee(this.attendeeId).subscribe((_) => console.log(_));

      document.getElementById("attendeeDeleted").innerHTML += `<i>Successfully Deleted!</i>` + "<br>";
      this.newActivityName = '';
      this.newActivityDuration = null;
      this.attendeeId = null;
      this.checkDeleteAttendee = false;
    }

  }

  unconfirmDeleteAttendee() {
    this.checkDeleteAttendee = false;
  }

  unconfirmDeleteActivity() {
    this.checkDeleteActivity = false;
  }

  navigateFindEvent() {
    this.router.navigate(["findEvent"]);
  }

  confirmAddAttendee() {

    var deleteElement = document.getElementById("attendeeDeleted");
    deleteElement.innerHTML = "";

    var editElement = document.getElementById("attendeeEdited");
    editElement.innerHTML = "";

    var addElement = document.getElementById("attendeeAdded");
    addElement.innerHTML = "";

    if (this.addedAttendeeName == null || this.addedAttendeeName == undefined
      || this.addedAttendeeEmail == null || this.addedAttendeeEmail == undefined) {
      alert("Please fill out all information for adding new attendee.");
    }
    else {

      let toAdd: Attendee = {
        name: this.addedAttendeeName, email: this.addedAttendeeEmail, eventId: this.eventID
      }

      this.attendeeService.addAttendee(toAdd).subscribe((_) => console.log(_));

      document.getElementById("attendeeAdded").innerHTML += `<i>Successfully Added ${this.addedAttendeeName}!</i>` + "<br>";
      this.addedAttendeeName = '';
      this.addedAttendeeEmail = '';
    }
  }

  confirmAddActivity() {

    var deleteElement = document.getElementById("activityDeleted");
    deleteElement.innerHTML = "";

    var editElement = document.getElementById("activityEdited");
    editElement.innerHTML = "";

    var addElement = document.getElementById("activityAdded");
    addElement.innerHTML = "";

    if (this.addedActivityDuration == null || this.addedActivityDuration == undefined
      || this.addedActivityName == null || this.addedActivityName == undefined) {
      alert("Please fill out all information for adding new activity.");
    }
    else {

      let toAdd: Activity = {
        name: this.addedActivityName, duration: this.addedActivityDuration, eventId: this.eventID
      }

      this.activityService.addActivity(toAdd).subscribe((_) => console.log((_)));

      document.getElementById("activityAdded").innerHTML += `<i>Successfully Added ${this.addedActivityName}!</i>` + "<br>";
      this.addedActivityName = '';
      this.addedActivityDuration = null;
    }
  }

  closeAddAttendee() {
    var thisElement = document.getElementById("addNewAttendee");
    thisElement.style.display = "none";
    var anotherElement = document.getElementById("attendeeAdded");
    anotherElement.innerHTML = "";
  }

  closeAddActivity() {
    var thisElement = document.getElementById("addNewActivity");
    thisElement.style.display = "none";
    var anotherElement = document.getElementById("activityAdded");
    anotherElement.innerHTML = "";
  }

}
