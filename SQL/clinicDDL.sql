CREATE SCHEMA Clinic;
GO


DROP TABLE IF EXISTS Clinic.Patients;
DROP TABLE IF EXISTS Clinic.Doctors;
DROP TABLE IF EXISTS Clinic.PatientReports;
DROP TABLE IF EXISTS Clinic.Vitals;
DROP TABLE IF EXISTS Clinic.Prescriptions;
DROP TABLE IF EXISTS Clinic.Timeslots;
DROP TABLE IF EXISTS Clinic.Appointments;


CREATE TABLE Clinic.Patients
(
    Id          INT NOT NULL,
    Name        NVARCHAR(200),
    DoctorId    INT NOT NULL,
    DOB         DATE NOT NULL,
    SSN         NVARCHAR(11),
    Insurance   NVARCHAR(200),
    CONSTRAINT Patient_PK PRIMARY KEY (Id),
    CONSTRAINT PatientsDoctor_FK FOREIGN KEY (DoctorId) REFERENCES Clinic.Doctors (Id)
);

CREATE TABLE Clinic.Doctors
(
    Id          INT NOT NULL,
    Name        NVARCHAR(200),
    Title       NVARCHAR(100),
    CONSTRAINT Doctor_PK PRIMARY KEY (Id)
);

CREATE TABLE Clinic.PatientReports
(
    Id          INT NOT NULL,
    PatientId   INT NOT NULL, 
    Information NVARCHAR(2000),
    ReportTime  DateTime,
    VitalsId    INT,
    CONSTRAINT PatientReport_PK PRIMARY KEY (Id),
    CONSTRAINT Patient_FK FOREIGN KEY (PatientId) REFERENCES Clinic.Patients (Id),
    CONSTRAINT Vitals_FK FOREIGN KEY (VitalsId) REFERENCES Clinic.Vitals (Id)
);

CREATE TABLE Clinic.Vitals
(
    Id          INT NOT NULL,
    Systolic    INT NOT NULL CHECK ( Systolic > 80 AND Systolic < 250 ),
    Diastolic   INT NOT NULL CHECK  ( Diastolic > 40 AND Diastolic < 150),
    Temperature DECIMAL  CHECK ( Temperature > 95.0 AND Temperature < 105.0),
    Pain        INT CHECK   ( Pain > 0 And  Pain <= 10),
    CONSTRAINT Vitals_PK PRIMARY KEY (Id),
);

CREATE TABLE Clinic.Prescriptions
(
    Id           INT NOT NULL,
    PatientId    INT NOT NULL,
    DoctorId     INT NOT NULL,
    Drug         NVARCHAR(300),
    Information  NVARCHAR(2000),
    Date       DateTime,

CONSTRAINT Prescription_PK PRIMARY KEY (Id),
CONSTRAINT Patient_FK FOREIGN KEY (PatientId) REFERENCES Clinic.Patients (Id),
CONSTRAINT Doctor_FK FOREIGN KEY (DoctorId) REFERENCES Clinic.Doctors (Id)

);

CREATE TABLE Clinic.Timeslots
(
    Id            INT NOT NULL,
    DoctorId      INT NOT NULL,
    AppointmentId INT,
    CONSTRAINT Doctor_FK FOREIGN KEY (DoctorId) REFERENCES Clinic.Doctors (Id),
    CONSTRAINT Appointment_FK FOREIGN KEY (AppointmentId) REFERENCES Clinic.Appointments (Id)
);

CREATE TABLE Clinic.Appointments
(
    Id         INT NOT NULL,
    Notes      NVARCHAR(2000),
    VitalsId   INT,
    PatientId  INT NOT NULL,
 
    CONSTRAINT Appointment_PK PRIMARY KEY (Id),
    CONSTRAINT Patient_FK FOREIGN KEY (PatientId) REFERENCES Clinic.Patients (Id),
    CONSTRAINT Vitals_FK FOREIGN KEY (VitalsId) REFERENCES Clinic.Vitals (Id)

);