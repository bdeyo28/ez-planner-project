import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {tap, catchError} from 'rxjs/operators';
import {of} from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Activity } from '../interfaces/Activity';

@Injectable({
  providedIn: 'root'
})
export class ActivityService {

  baseURL : String = "http://localhost:20292/api";

  httpOptions = {headers: new HttpHeaders({"Content-Type" : "application/json"})};

  constructor(private http : HttpClient) { }

  getAllActivities() : Observable<Activity[]> {
    return this.http.get<Activity[]>(this.baseURL + "/Activity")
    .pipe(
      tap(x => console.log(x)),
      catchError(err => {
        console.log(err);
        return of(null);
      })
    );
  }

  getActivityById(id : number) : Observable<Activity> {
    return this.http.get<Activity>(this.baseURL + "/Activity/" + id)
    .pipe(
      tap(x => console.log(x)),
      catchError(err => {
        console.log(err);
        return of(null);
      })
    );
  }

  deleteActivity(id : number) : Observable<Activity> {
    return this.http.delete<Activity>(this.baseURL + "/Activity/" + id)
    .pipe(
      tap(x => console.log(x)),
      catchError(err => {
        console.log(err);
        return of(null);
      })
    );
  }

  editActivity(edited : Activity) : Observable<Activity> {
    return this.http.put<Activity>(this.baseURL + "/Activity", edited, this.httpOptions)
    .pipe(
      tap(x => console.log(x)),
      catchError(err => {
        console.log(err);
        return of(null);
      })
    );
  }

  addActivity(toAdd : Activity) : Observable<Activity> {
    return this.http.post<Activity>(this.baseURL + "/Activity", toAdd, this.httpOptions)
    .pipe(
      tap(x => console.log(x)),
      catchError(err => {
        console.log(err);
        return of(null);
      })
    );
  }

}
