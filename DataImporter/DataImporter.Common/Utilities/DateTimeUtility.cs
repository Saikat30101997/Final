﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Common.Utilities
{
    public class DateTimeUtility : IDateTimeUtility
    {
        public DateTime Now() =>
            DateTime.Today;

        public DateTime NowWithTime
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}
