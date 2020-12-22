import { Doctor } from "./doctor";
import { Patient } from "./patient";

export interface Prescription {
    id: number;
    patient: Patient;
    doctor: Doctor;
    info: string;
    drugName: string;
}