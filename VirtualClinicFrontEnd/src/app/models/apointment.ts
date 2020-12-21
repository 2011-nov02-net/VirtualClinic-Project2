import { Doctor } from "./doctor";
import { patient } from "./patient";

export class apointment{
    id: number;
    doctor: Doctor;
    patient: patient;
    notes: string | null;
}