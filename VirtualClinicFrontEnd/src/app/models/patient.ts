import { Doctor } from "./doctor";
import { PatientReports } from "./patientreport";
import { Prescription } from "./prescription";

export class Patient{

    constructor(id:number, name:string, insurance:string, ssn: string, birhtday:Date){
        this.id = id;
        this.name = name;
        this.insuranceProvider = insurance;
        this.ssn = ssn;
        this.dateOfBirth = new Date();

        this.reports = [];
        this.prescriptions = [];
        this.doctor = new Doctor(-1, "unknown", "dr" );
    }

    id: number;
    name: string;
    ssn: string;
    insuranceProvider: string;
    dateOfBirth: Date;

    reports: PatientReports[];
    prescriptions: Prescription[];

    doctor: Doctor;
    
    
}