import { TimeslotsComponent } from "../timeslots/timeslots.component";
import { Patient } from "./patient";
import { Timeslot } from "./timeslot";

export class Doctor {

    constructor(id: number, name:string, title:string ){
        this.id = id;
        this.name = name;
        this.title = title;
        this.timeslots = [];
        this.patients = [];
    }

    
    id: number;
    name: string;
    title: string;

    timeslots: Timeslot[];
    patients: Patient[];
}