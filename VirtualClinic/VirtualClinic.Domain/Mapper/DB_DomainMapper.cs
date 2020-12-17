using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualClinic.Domain.Models;
using VirtualClinic.DataModel;

namespace VirtualClinic.Domain.Mapper
{
    static class DB_DomainMapper
    {
        /// <summary>
        /// Convert a report from a db object to a domain model object.
        /// </summary>
        /// <remarks>
        /// Will not get patient or vitals.
        /// </remarks>
        /// <param name="report">The report from the db</param>
        /// <returns></returns>
        public static Models.PatientReport MapReport(DataModel.PatientReport report)
        {
            if(report is null)
            {
                throw new ArgumentNullException("MapReport Report is null.");
            }

            Models.PatientReport modelreport = new Models.PatientReport(report.Id, report.Information);

            modelreport.Time = report.ReportTime;

            return modelreport;
        }

        /// <summary>
        /// Maps a DB vitals to a Domain Vitals exluding blood pressure and pain level.
        /// </summary>
        /// <param name="vital">A DB vitals.</param>
        /// <returns>A Domain Vitals.</returns>
        public static Models.Vitals MapVitals(DataModel.Vital vital)
        {
            Models.Vitals modelvitals = new Models.Vitals(vital.Id);
            modelvitals.Id = vital.Id;

            if(vital.Temperature is not null)
            {
                decimal d = (decimal) vital.Temperature;
                modelvitals.Temperature = decimal.ToDouble(d);
            } else
            {
                modelvitals.Temperature = null;
            }

            //todo: make sure this is correct.
            modelvitals.HeartRate = vital.Diastolic;

            //todo: get blood pressure or remove from model
            modelvitals.BloodPressure = null;

            return modelvitals;
        }

        /// <summary>
        /// Converts a timeslot, excluding apointment information to a Domain Model version of it.
        /// </summary>
        /// <param name="DbTimeSlot">A DB timeslot to be converted</param>
        /// <returns>The domain model version of a DB timeslot</returns>
        internal static Models.Timeslot MapTimeslot(DataModel.Timeslot DbTimeSlot)
        {

            Models.Timeslot timeslot = new Models.Timeslot();
            //TODO: get timeslot from db when added to db

            //TODO: get id from timeslot

            return timeslot;
        }

        /// <summary>
        /// Map a DB apointment to an equivilent model apointment. Will not fill in doctor, or patient reference.
        /// </summary>
        /// <param name="Dbappointment">A DB version of the apointment to be converted.</param>
        /// <returns>A model version of the given apointment.</returns>
        internal static Models.Appointment MapApointment(DataModel.Appointment Dbappointment, Models.Doctor doctor = null)
        {
            return new Models.Appointment(Dbappointment.Id, Dbappointment.Notes, doctor);
        }

        /// <summary>
        /// Converts a DB prescription into a model prescription.
        /// </summary>
        /// <param name="script">The DB representation of the prescription.</param>
        /// <returns>A model representation of the presctiption</returns>
        internal static Models.Prescription MapPerscription(DataModel.Prescription script)
        {
            return new Models.Prescription(script.Id, script.Information, script.Drug);
        }
    }
}
