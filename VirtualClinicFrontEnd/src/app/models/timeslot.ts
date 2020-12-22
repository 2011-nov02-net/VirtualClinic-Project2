import { Appointment } from "./appointment";

export interface Timeslot {
    id: number;
    start: Date;
    end: Date;
    appointment: Appointment | null
}