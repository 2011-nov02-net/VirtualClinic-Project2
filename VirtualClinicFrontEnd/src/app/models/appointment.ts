import { Doctor } from "./doctor";
import { Patient } from "./patient";
import { Vitals } from "./vitals";

export interface Appointment{
    id: number;
    doctor: Doctor;
    patient: Patient;
    notes: string | null;
    start: Date;
    end: Date;
    vitals : Vitals | null;
}