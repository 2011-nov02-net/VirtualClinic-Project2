﻿using System;
using System.Collections.Generic;

#nullable disable

namespace VirtualClinic.DataModel
{
    public partial class PatientReport
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Information { get; set; }
        public DateTime ReportTime { get; set; }
        public int? VitalsId { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual Vital Vitals { get; set; }
    }
}
