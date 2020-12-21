import { Doctor } from "./doctor";
import { PatientReports } from "./patientreport";
import { prescription } from "./prescription";

export class Patient{
    insuranceProvider: string;
    reports: PatientReports[];
    prescriptions: prescription[];
    doctor: Doctor;
    doctorid: number | null;
    birthday: Date;
    ssn: string;
}