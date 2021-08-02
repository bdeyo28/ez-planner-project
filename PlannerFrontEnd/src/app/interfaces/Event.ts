import { Activity } from "./Activity";
import { Attendee } from "./Attendee";

export interface Event{
    id? : number,
    activities?: Activity[],
    organizerId? : number,
    date : Date,
    eventName : string,
    duration? : number,
    attendees? : Attendee[],
    category : string,
    time: string,
    location: string

}