import { Patient } from './patient';

export interface Doctor {
    id: number;
    name: string;
    title: string;
    patients: Patient[];
}