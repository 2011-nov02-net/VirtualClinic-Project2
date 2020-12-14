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
    }
}
