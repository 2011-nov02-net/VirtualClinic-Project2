﻿using System;
using System.Collections.Generic;

#nullable disable

namespace VirtualClinic.DataModel
{
    public partial class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
    }
}
