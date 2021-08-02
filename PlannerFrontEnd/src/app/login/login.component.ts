import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { LoginUserRequest } from '../interfaces/LoginUserRequest';
import { RegisterUserRequest } from '../interfaces/RegisterUserRequest';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  username: string;
  password: string;

  constructor(private authService: AuthService,
    private router: Router) { }

  ngOnInit(): void {
  }

  submit() {

    var passwordElement = document.getElementById("passwordInvalid");
    var usernameElement = document.getElementById("usernameInvalid");
    var errorBox = document.getElementById("errorBox");

    errorBox.style.display = "none";

    passwordElement.innerHTML = "";
    usernameElement.innerHTML = "";
    
    let checkValid : boolean = true;

    if (this.password == null || this.password == undefined) 
    {
      passwordElement.innerHTML += "<b><i>Username or password is invalid!</i></b>";
      errorBox.style.display = "block";
      checkValid = false;
      this.username = "";
      this.password = "";
    }
    if(this.username == null || this.username == undefined)
    {
      usernameElement.innerHTML += "<b><i>Username is invalid!</i></b>";
      errorBox.style.display = "block";
      checkValid = false;
      this.password = "";
      this.username = "";
    }

    if(checkValid)
    {
      let toSend: LoginUserRequest = {
        Username: this.username, Password: this.password
      }

      this.authService.loginUser(toSend);
    }
  }

  navigate() {
    this.router.navigate(["register"]);
  }

}
