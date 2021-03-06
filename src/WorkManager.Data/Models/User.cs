﻿using System;
using Microsoft.AspNetCore.Identity;
using WorkManager.Data.Models.Interfaces;

namespace WorkManager.Data.Models
{
    public class User : IdentityUser<int>, IAudit
    {
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
