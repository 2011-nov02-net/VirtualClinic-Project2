import { Doctor } from "./doctor";
import { Patient } from "./patient";

export interface Appointment{
    id: number;
    doctor: Doctor;
    patient: Patient;
    notes: string | null;
}