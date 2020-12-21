import { Doctor } from "./doctor";
import { PatientReports } from "./patientreport";
import { prescription } from "./prescription";

export interface Patient{
    insuranceProvider: string;
    reports: PatientReports[];
    prescriptions: prescription[];
    doctor: Doctor;
    doctorid: number | null;
    birthday: Date;
    ssn: string;
}