import { Doctor } from "./doctor";
import { patient } from "./patient";

export class prescription {
    id: number;
    patient: patient | null;
    doctor: Doctor | null;
    info: string;
    drugName: string;
}