/* Test query for getting the DR's apointments */
Select * from 
Clinic.Appointments as A LEFT JOIN Clinic.Timeslots as T on A.Id = T.AppointmentId
Where T.DoctorId = 1;