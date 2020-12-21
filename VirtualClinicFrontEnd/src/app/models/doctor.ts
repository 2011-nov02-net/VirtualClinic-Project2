import { patient } from "./patient";
import { PatientReports } from "./patientreport";

export class Doctor {
    doctorid: number;
    patients: patient[];
    title: string;
}