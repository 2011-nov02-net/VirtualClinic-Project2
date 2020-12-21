import { Doctor } from "./doctor";
import { Patient } from "./patient";

export class appointment{
    id: number;
    doctor: Doctor;
    patient: Patient;
    notes: string | null;
}