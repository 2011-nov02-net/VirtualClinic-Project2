import { TimeslotsComponent } from "../timeslots/timeslots.component";
import { Patient } from "./patient";
import { Timeslot } from "./timeslot";

export interface Doctor {
    id: number;
    name: string;
    patients: Patient[];
    title: string;
    timeslots: Timeslot[];
}