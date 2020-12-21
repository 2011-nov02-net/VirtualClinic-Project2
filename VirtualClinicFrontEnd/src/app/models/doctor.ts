import { Patient } from "./patient";
import { PatientReports } from "./patientreport";

export interface Doctor {
    doctorid: number;
    patients: Patient[];
    title: string;
}