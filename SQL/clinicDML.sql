-- Select * from 
-- Clinic.Appointments as A LEFT JOIN Clinic.Timeslots as T on A.Id = T.AppointmentId
-- Where T.DoctorId = 1;

INSERT INTO Clinic.Doctors (Name, Title) VALUES 
     ('Gregory House', 'Diagnostician' ),
     ('Critina Yang', 'Cardiologist'),
     ('Perry Cox', 'Oncologist'),
     ('Dana Scully', 'Radiologist');

INSERT INTO Clinic.Patients (Name, DoctorId, DOB, SSN, Insurance) VALUES 
      ('Walter White',1,'2015-12-17','626-70-4664', 'Aetna'),
      ('Tyrion Lannister',2,'1997-06-21','004-36-9199', 'BlueCross BlueShield'),
      ('Sherlock Holmes',3,'1951-08-30','039-60-1334', 'National General'),
      ('Chandler Bing',4,'1994-10-10','516-49-5594', 'Aetna'),
      ('Michael Scott',1,'2009-09-21','681-24-2024', 'Cigna'),
      ('Dean Winchester',2,'1979-03-19','042-60-4547', 'Kaiser Permanente');

INSERT INTO Clinic.Vitals (Systolic, Diastolic, HeartRate,Temperature, Pain) VALUES 
       (120, 80, 95,97.2, 2),
       (143, 91, 93,96.3, 1),
       (123, 84, 88,97.2, 7),
       (111, 72, 115,99.0, 2),
       (170, 95, 75,96.8, 3),
       (123, 65, 83,98.1, 4);

INSERT INTO Clinic.PatientReports (PatientId, Information, ReportTime, VitalsId) VALUES
       (13, 'Lorem Ipsum1', GetDate(), 1),
       (14, 'Lorem Ipsum2', GetDate(), 2),
       (15, 'Lorem Ipsum3', GetDate(), 3),
       (16, 'Lorem Ipsum4', GetDate(), 4),
       (17, 'Lorem Ipsum5', GetDate(), 5),
       (18, 'Lorem Ipsum1', GetDate(), 6),
       (13, 'Lorem Ipsum1', GetDate(), NULL),
       (14, 'Lorem Ipsum6', GetDate(), NULL);

INSERT INTO Clinic.Prescriptions (PatientId, DoctorId, Drug, Information, [Date]) VALUES
       (13, 1,'Levothyroxine','Lorem Ipsum1' , GETDATE()),
       (14, 2,'Lisinopril','Lorem Ipsum2' , GETDATE()),
       (15, 3,'Omeprazole','Lorem Ipsum3' , GETDATE()),
       (13, 1,'Albuterol','Lorem Ipsum4' , GETDATE()),
       (14, 2,'Simvastatin','Lorem Ipsum5' , GETDATE());

INSERT INTO Clinic.Appointments (Notes, DoctorId, PatientId, VitalsId, [Start], [End])   VALUES
       ('Lorem Ipsum1',1, 13, 1, '2020-12-20 12:30:00', '2020-12-20 13:00:00'),
       ('Lorem Ipsum2',2, 14, 2, '2020-12-20 9:30:00', '2020-12-20 11:00:00'), 
       ('Lorem Ipsum3',3, 15, 3, '2020-12-20 12:30:00', '2020-12-20 13:00:00'), 
       ('Lorem Ipsum4',4, 16, 4, '2020-12-20 12:00:00', '2020-12-20 13:00:00'), 
       ('Lorem Ipsum5',1, 17, 5, '2020-12-26 12:30:00', '2020-12-26 13:00:00'), 
       ('Lorem Ipsum6',2, 18, 6, '2020-12-30 12:30:00', '2020-12-30 13:00:00');

INSERT INTO Clinic.Timeslots (DoctorId, AppointmentId, [Start], [End])  VALUES
       (1,8, '2020-12-20 12:30:00', '2020-12-20 13:00:00'),
       (2,9, '2020-12-20 9:30:00', '2020-12-20 11:00:00'),
       (3,10, '2020-12-20 12:30:00', '2020-12-20 13:00:00'),
       (4,11, '2020-12-20 12:00:00', '2020-12-20 13:00:00'),
       (1,12, '2020-12-26 12:30:00', '2020-12-26 13:00:00'),
       (2,13, '2020-12-30 12:30:00', '2020-12-30 13:00:00'),
	   (1, NULL, '2020-12-25 1:30:00', '2020-12-25 2:30:00'),
	   (1, NULL, '2020-12-26 15:00:00', '2020-12-26 15:30:00');

select * from Clinic.Doctors
select * from Clinic.Appointments
select * from Clinic.PatientReports
select * from Clinic.Patients
select * from Clinic.Timeslots
select * from Clinic.Vitals
select * from Clinic.Prescriptions
select * from Clinic.Users

delete from Clinic.Appointments
delete from Clinic.Patients
delete from Clinic.Prescriptions