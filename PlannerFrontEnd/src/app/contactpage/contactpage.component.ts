import { Component, Input, OnInit, NgModule } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators, NgForm } from '@angular/forms';
import { HttpClientModule, HttpClient, HttpHeaders } from "@angular/common/http";
import { formatCurrency } from '@angular/common';

@Component({
  selector: 'app-contactpage',
  templateUrl: './contactpage.component.html',
  styleUrls: ['./contactpage.component.css']
})
export class ContactpageComponent implements OnInit {

  constructor(private http : HttpClient) { }

  ngOnInit() {

  }

  onSubmit(contactForm: NgForm) {
    if (contactForm.valid) {
      const email = contactForm.value;
      const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
      this.http.post('https://formspree.io/f/xqkgvvgy',
        { name: email.name, replyto: email.email, message: email.messages },
        { 'headers': headers }).subscribe(
          response => {
            console.log(response);
          }
        );
    }

    var verifySent = <HTMLDivElement>document.getElementById("messageSent");
    verifySent.innerHTML = "Thank You! Your Message Has Been Sent.";
    this.clearForm();

  }

  clearForm()
  {
    (<HTMLFormElement>document.getElementById("contactForm")).reset();
  }
  
}