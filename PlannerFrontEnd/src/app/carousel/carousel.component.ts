import { Component, OnInit } from '@angular/core';
@Component({
  selector: 'app-carousel',
  templateUrl: './carousel.component.html',
  styleUrls: ['./carousel.component.css']
})
export class CarouselComponent implements OnInit {

  event_list = [
    {
      event:' Event 1',
      eventLocation:'FOR FAMILY EVENTS',
      eventDescription:"",
      img: '/assets/FamilyMovie.jpg',
      eventStartDate: new Date('2019/05/20'),
      eventEndingDate: new Date('2019/05/24')
    },
     {
      event:' Event 2',
      eventLocation:'FOR YOU & YOUR FRIENDS',
      eventDescription:"",
      img: '/assets/FriendsReunion.jpg',
      eventStartDate: new Date('2019/07/28'),
      eventEndingDate: new Date('2019/07/30')
    },
     {
      event:' Event 3',
      eventLocation:'FOR THE WORKPLACE',
      eventDescription:"",
      img: '/assets/OfficeStuff.png',
      eventStartDate: new Date('2020/05/20'),
      eventEndingDate: new Date('2020/05/24')
    }
  ]

  upcoming_events =  this.event_list.filter( event => event.eventStartDate > new Date());
  past_events = this.event_list.filter(event => event.eventEndingDate < new Date());
  current_events =  this.event_list.filter( event => (event.eventStartDate >= new Date() && (event.eventEndingDate <= new Date())))
  constructor() {
  }

  ngOnInit() {
  }

}