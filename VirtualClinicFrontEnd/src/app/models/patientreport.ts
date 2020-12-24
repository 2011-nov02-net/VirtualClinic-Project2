import { Doctor } from "./doctor";
import { Patient } from "./patient";
import { Vitals } from "./vitals";

export interface PatientReports {
    id: number;
    patient: Patient | null;
    doctor: Doctor | null;
    patientId: number;
    doctorId: number;
    time: Date;
    info: string;
    vitals: Vitals | null;
}