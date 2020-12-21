import { Patient } from "./patient";
import { vitals } from "./vitals";

export interface PatientReports {
    id: number;
    Patient: Patient | null;
    time: Date;
    info: string;
    vitals: vitals;
}