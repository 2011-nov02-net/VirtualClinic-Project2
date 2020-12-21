import { Patient } from "./patient";
import { Vitals } from "./vitals";

export interface PatientReports {
    id: number;
    patient: Patient | null;
    time: Date;
    info: string;
    vitals: Vitals | null;
}