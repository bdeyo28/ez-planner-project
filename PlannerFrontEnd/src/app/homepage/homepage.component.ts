import { getSourceFileOrError } from '@angular/compiler-cli/src/ngtsc/file_system';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { Organizer } from '../interfaces/Organizer';
import { EventService } from '../services/event.service';
import { OrganizerService } from '../services/organizer.service';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomepageComponent implements OnInit {

  eventName : string = "Event";

  organizer : Organizer;

  username : string = '';

  constructor(private eventService : EventService,
    private authService : AuthService,
    private orgService : OrganizerService) { }

  ngOnInit(): void 
  {

    if(this.authService.isSignedIn())
    {
      this.username = this.authService.user.username;
    }
    
    this.authService.loginChangedEvent.subscribe((signedIn) => this.username = this.authService.user.username);
  }

  getUser() {
    this.orgService.getUserAsOrganizer().subscribe(org => this.organizer = org);
  }

  public slides = [
    "./assets/WaterBalloonFight.jpg",
    "./assets/TugOfWar.jpg",
    "./assets/TheOffice.png"
  ];

}
