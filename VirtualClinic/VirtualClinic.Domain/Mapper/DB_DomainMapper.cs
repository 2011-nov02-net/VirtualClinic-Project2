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
        /// 
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public static Models.PatientReport MapReport(DataModel.PatientReport report)
        {
            Models.PatientReport modelreport = new Models.PatientReport();

            modelreport.Id = report.Id;
            modelreport.Info = report.Information;
            modelreport.Time = report.ReportTime;

            //todo: get patient and vitals

            return modelreport;
        }
    }
}
