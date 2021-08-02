import { ChangeDetectorRef, OnDestroy, Component, ViewChild } from '@angular/core';
import {MatMenuModule, MatMenuTrigger, MatMenu} from '@angular/material/menu';
import {MatIconModule} from '@angular/material/icon';
import { MatSidenavModule } from '@angular/material/sidenav'
import { MediaMatcher } from '@angular/cdk/layout';
import { HttpClient } from '@angular/common/http';
import { Input } from '@angular/core';
import { AuthService } from './auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'PlannerFrontEnd';

  signedIn : boolean = false;

  username : string;

  constructor(private authService : AuthService,
    private router: Router) { 

  }

  ngOnInit() {
    this.signedIn = this.authService.isSignedIn();

    this.authService.loginChangedEvent.subscribe((signedIn) => this.signedIn = signedIn);

  }

  signOut() {
    this.authService.signOut();
  }

}
