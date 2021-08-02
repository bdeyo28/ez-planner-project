import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { RegisterUserRequest } from '../interfaces/RegisterUserRequest';

@Component({
  selector: 'app-userregistration',
  templateUrl: './userregistration.component.html',
  styleUrls: ['./userregistration.component.css']
})
export class UserregistrationComponent implements OnInit {

  userName: string;
  userPassword: string;
  userEmail: string;
  confirmPassword: string;

  constructor(private authService: AuthService,
    private router: Router) { }

  ngOnInit(): void {
  }

  submit() {

    let checkValidInput: boolean = true;

    var confirmPasswordElement = document.getElementById("notMatchingPassword");
    var passwordInvalidElement = document.getElementById("passwordInvalid");
    var emailInvalidElement = document.getElementById("emailInvalid");
    var usernameInvalidElement = document.getElementById("usernameInvalid");
    var errorBox = document.getElementById("errorBox");
    errorBox.style.display = "none";

    confirmPasswordElement.innerHTML = "";
    passwordInvalidElement.innerHTML = "";
    emailInvalidElement.innerHTML = "";
    usernameInvalidElement.innerHTML = "";

    if (this.userPassword != this.confirmPassword) {
      errorBox.style.display = "block";
      confirmPasswordElement.innerHTML += "<b><i>Passwords do not match.</i></b>";
      checkValidInput = false;
      this.userEmail = "";
      this.userPassword = "";
      this.confirmPassword = "";
      this.userName = "";
    }

    if (this.userPassword == null || this.userPassword == undefined) {
      errorBox.style.display = "block";
      passwordInvalidElement.innerHTML += "<b><i>Password is invalid. Please enter a new password.</i></b>";
      checkValidInput = false;
      this.userEmail = "";
      this.userPassword = "";
      this.confirmPassword = "";
      this.userName = "";
    }
    else if(this.userPassword.length < 8)
    {
      errorBox.style.display = "block";
      passwordInvalidElement.innerHTML += "<b><i>Password is not enough characters.</i></b>";
      checkValidInput = false;
      this.userEmail = "";
      this.userPassword = "";
      this.confirmPassword = "";
      this.userName = "";
    }

    let checkEmailInvalid: boolean = true;

    if(!this.userEmail.includes("@"))
    {
      checkEmailInvalid = false;
    }

    if (!checkEmailInvalid) {
      errorBox.style.display = "block";
      emailInvalidElement.innerHTML += "<b><i>Email is not invalid. Please use a valid email.</i></b>";
      checkValidInput = false;
      this.userEmail = "";
      this.userPassword = "";
      this.confirmPassword = "";
      this.userName = "";
    }

    if (checkValidInput) {
      let toSend: RegisterUserRequest = {
        Username: this.userName, Password: this.userPassword, Email: this.userEmail
      }

      this.authService.registerUser(toSend).subscribe((_) => this.router.navigate(["login"]));
    }
  }

}
