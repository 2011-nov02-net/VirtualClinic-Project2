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
        /// <param name="report">The report from the db</param>
        /// <returns></returns>
        public static Models.PatientReport MapReport(DataModel.PatientReport report)
        {
            if(report is null)
            {
                throw new ArgumentNullException("MapReport Report is null.");
            }

            Models.PatientReport modelreport = new Models.PatientReport();

            modelreport.Id = report.Id;
            modelreport.Info = report.Information;
            modelreport.Time = report.ReportTime;

            //todo: get patient and vitals

            return modelreport;
        }

        /// <summary>
        /// Maps a DB vitals to a Domain Vitals exluding blood pressure and pain level.
        /// </summary>
        /// <param name="vital"></param>
        /// <returns></returns>
        public static Models.Vitals MapVitals(DataModel.Vital vital)
        {
            Models.Vitals modelvitals = new Models.Vitals();
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

        internal static Models.Appointment MapApointment(DataModel.Appointment appointment)
        {
            throw new NotImplementedException();
        }
    }
}
