CREATE SCHEMA Clinic;
GO


/* Added the Go's because of complaints 
 * about FK restraintrs 
 */
DROP TABLE IF EXISTS Clinic.Patients;
GO
DROP TABLE IF EXISTS Clinic.Doctors;
GO
DROP TABLE IF EXISTS Clinic.Vitals;
GO
DROP TABLE IF EXISTS Clinic.PatientReports;
DROP TABLE IF EXISTS Clinic.Prescriptions;
DROP TABLE IF EXISTS Clinic.Timeslots;
DROP TABLE IF EXISTS Clinic.Appointments;


CREATE TABLE Clinic.Doctors
(
    Id          INT NOT NULL,
    Name        NVARCHAR(200) NOT NULL,
    Title       NVARCHAR(100) NULL,

    CONSTRAINT Doctor_PK PRIMARY KEY (Id)
);

CREATE TABLE Clinic.Patients
(
    Id          INT NOT NULL,
    Name        NVARCHAR(200) NOT NULL,
    DoctorId    INT NOT NULL,
    DOB         DATE NOT NULL,
    SSN         NVARCHAR(11) NULL,
    Insurance   NVARCHAR(200) NULL,

    CONSTRAINT Patient_PK PRIMARY KEY (Id),
    CONSTRAINT PatientsDoctor_FK FOREIGN KEY (DoctorId) REFERENCES Clinic.Doctors (Id)
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


CREATE TABLE Clinic.PatientReports
(
    Id          INT NOT NULL,
    PatientId   INT NOT NULL, 
    Information NVARCHAR(2000) NULL,
    ReportTime  DateTime NOT NULL,
    VitalsId    INT NULL,

    CONSTRAINT PatientReport_PK PRIMARY KEY (Id),
    CONSTRAINT ReportPatient_FK FOREIGN KEY (PatientId) REFERENCES Clinic.Patients (Id),
    CONSTRAINT ReportVitals_FK FOREIGN KEY (VitalsId) REFERENCES Clinic.Vitals (Id)
);


CREATE TABLE Clinic.Prescriptions
(
    Id           INT NOT NULL,
    PatientId    INT NOT NULL,
    DoctorId     INT NOT NULL,
    Drug         NVARCHAR(300) NULL,
    Information  NVARCHAR(2000) NULL,
    Date         DateTime NOT NULL,

	CONSTRAINT Prescription_PK PRIMARY KEY (Id),
	CONSTRAINT PerscriptionPatient_FK FOREIGN KEY (PatientId) REFERENCES Clinic.Patients (Id),
	CONSTRAINT PerscriptionDoctor_FK FOREIGN KEY (DoctorId) REFERENCES Clinic.Doctors (Id)

);

CREATE TABLE Clinic.Appointments
(
    Id         INT NOT NULL,
    Notes      NVARCHAR(2000),
    VitalsId   INT NULL,
    PatientId  INT NOT NULL,
 
    CONSTRAINT Appointment_PK PRIMARY KEY (Id),
    CONSTRAINT ApointmentPatient_FK FOREIGN KEY (PatientId) REFERENCES Clinic.Patients (Id),
    CONSTRAINT ApointmentVitals_FK FOREIGN KEY (VitalsId) REFERENCES Clinic.Vitals (Id)

);

CREATE TABLE Clinic.Timeslots
(
    Id            INT NOT NULL,
    DoctorId      INT NOT NULL,
    AppointmentId INT NULL,

    CONSTRAINT Doctor_FK FOREIGN KEY (DoctorId) REFERENCES Clinic.Doctors (Id),
    CONSTRAINT TimeslotAppointment_FK FOREIGN KEY (AppointmentId) REFERENCES Clinic.Appointments (Id)
);


