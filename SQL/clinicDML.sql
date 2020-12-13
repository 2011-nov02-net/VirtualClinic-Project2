-- Select * from 
-- Clinic.Appointments as A LEFT JOIN Clinic.Timeslots as T on A.Id = T.AppointmentId
-- Where T.DoctorId = 1;

INSERT INTO Clinic.Doctors (Id,Name, Title) VALUES 
     (1,'Gregory House', 'Diagnostician' ),
     (2,'Critina Yang', 'Cardiologist'),
     (3,'Perry Cox', 'Oncologist'),
     (4,'Dana Scully', 'Radiologist');

INSERT INTO Clinic.Patients (Id, Name, DoctorId, DOB, SSN, Insurance) VALUES 
      (1,'Walter White',1,'2015-12-17','626-70-4664', 'Aetna'),
      (2,'Tyrion Lannister',2,'1997-06-21','004-36-9199', 'BlueCross BlueShield'),
      (3,'Sherlock Holmes',3,'1951-08-30','039-60-1334', 'National General'),
      (4,'Chandler Bing',4,'1994-10-10','516-49-5594', 'Aetna'),
      (5,'Michael Scott',1,'2009-09-21','681-24-2024', 'Cigna'),
      (6,'Dean Winchester',2,'1979-03-19','042-60-4547', 'Kaiser Permanente');

INSERT INTO Clinic.Vitals ( Id, Systolic, Diastolic, Temperature, Pain) VALUES 
       (1,120, 80, 97.2, 2),
       (2,143, 91, 96.3, 1),
       (3,123, 84, 97.2, 1),
       (4,111, 75, 99.0, 2),
       (5,170, 95, 96.8, 3),
       (6,123, 65, 98.1, 4),
       (7,134, 64, 96.7, 8),
       (8,138, 90, 97.5, 10);
INSERT INTO Clinic.PatientReports ( Id,PatientId, Information, ReportTime, VitalsId) VALUES
       (1,1, 'Lorem Ipsum1', GetDate(), 1),
       (2,2, 'Lorem Ipsum2', GetDate(), 2),
       (3,3, 'Lorem Ipsum3', GetDate(), 3),
       (4,4, 'Lorem Ipsum4', GetDate(), 4),
       (5,5, 'Lorem Ipsum5', GetDate(), 5),
       (6,6, 'Lorem Ipsum1', GetDate(), 6),
       (7,1, 'Lorem Ipsum1', GetDate(), 7),
       (8,2, 'Lorem Ipsum6', GetDate(), 8);

INSERT INTO Clinic.Prescriptions ( Id,PatientId, DoctorId, Drug, Information, [Date]) VALUES
       ( 1,1, 1,'Levothyroxine','Lorem Ipsum1' , GETDATE()),
       ( 2,2, 2,'Lisinopril','Lorem Ipsum2' , GETDATE()),
       ( 3,3, 3,'Omeprazole','Lorem Ipsum3' , GETDATE()),
       ( 4,1, 1,'Albuterol','Lorem Ipsum4' , GETDATE()),
       (5, 2, 2,'Simvastatin','Lorem Ipsum5' , GETDATE());

INSERT INTO Clinic.Appointments (Id, Notes, VitalsId, PatientId)   VALUES
       (1,'Lorem Ipsum1',1,1),   
       (2,'Lorem Ipsum2',2,2), 
       (3,'Lorem Ipsum3',3,3), 
       (4,'Lorem Ipsum4',4,4), 
       (5,'Lorem Ipsum5',5,5), 
       (6,'Lorem Ipsum6',6,6);

INSERT INTO Clinic.Timeslots ( Id,DoctorId, AppointmentId)  VALUES
       (1,1,1),
       (2,2,2),
       (3,3,3),
       (4,4,4),
       (5,1,5),
       (6,2,6);