import { Doctor } from "./doctor";
import { Patient } from "./patient";

export interface prescription {
    id: number;
    patient: Patient | null;
    doctor: Doctor | null;
    info: string;
    drugName: string;
}