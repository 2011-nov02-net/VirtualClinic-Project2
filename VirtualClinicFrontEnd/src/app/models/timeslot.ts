import { Appointment } from "./appointment";

export interface timeslot {
    id: number;
    start: Date;
    end: Date;
    appointment: Appointment;
}