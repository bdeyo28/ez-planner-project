import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {tap, catchError} from 'rxjs/operators';
import {of} from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Attendee } from '../interfaces/Attendee';

@Injectable({
  providedIn: 'root'
})
export class AttendeeService {

  baseURL : String = "http://localhost:20292/api";

  httpOptions = {headers: new HttpHeaders({"Content-Type" : "application/json"})};

  constructor(private http : HttpClient) { }

  getAllAttendees() : Observable<Attendee[]> {
    return this.http.get<Attendee[]>(this.baseURL + "/Attendee/")
    .pipe(
      tap(x => console.log(x)),
      catchError(err => {
        console.log(err);
        return of(null);
      })
    );
  }

  getAttendeeById(id : number) : Observable<Attendee> {
    return this.http.get<Attendee>(this.baseURL + "/Attendee/" + id)
    .pipe(
      tap(x => console.log(x)),
      catchError(err => {
        console.log(err);
        return of(null);
      })
    );
  }

  deleteAttendee(id : number) : Observable<Attendee> {
    return this.http.delete<Attendee>(this.baseURL + "/Attendee/" + id)
    .pipe(
      tap(x => console.log(x)),
      catchError(err => {
        console.log(err);
        return of(null);
      })
    );
  }

  editAttendee(edited : Attendee) : Observable<Attendee> {
    return this.http.put<Attendee>(this.baseURL + "/Attendee", edited, this.httpOptions)
    .pipe(
      tap(x => console.log(x)),
      catchError(err => {
        console.log(err);
        return of(null);
      })
    );
  }

  addAttendee(toAdd : Attendee) : Observable<Attendee> {
    return this.http.post<Attendee>(this.baseURL + "/Attendee", toAdd, this.httpOptions)
    .pipe(
      tap(x => console.log(x)),
      catchError(err => {
        console.log(err);
        return of(null);
      })
    );
  }

}
