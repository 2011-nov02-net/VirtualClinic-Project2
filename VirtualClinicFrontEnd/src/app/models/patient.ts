import { Doctor } from "./doctor";
import { PatientReports } from "./patientreport";
import { Prescription } from "./prescription";

export interface Patient{
    id : number;
    insuranceProvider: string;
    reports: PatientReports[];
    prescriptions: Prescription[];
    doctor: Doctor;
    birthday: Date;
    ssn: string;
}