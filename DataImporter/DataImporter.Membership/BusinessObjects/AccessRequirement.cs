﻿using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataImporter.Membership.BusinessObjects
{
    public class AccessRequirement : IAuthorizationRequirement
    {
        public AccessRequirement()
        {
        }
    }
}
