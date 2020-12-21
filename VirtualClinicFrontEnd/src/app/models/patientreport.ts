import { patient } from "./patient";
import { vitals } from "./vitals";

export class PatientReports {
    id: number;
    patinet: patient | null;
    time: Date;
    info: string;
    vitals: vitals;
}