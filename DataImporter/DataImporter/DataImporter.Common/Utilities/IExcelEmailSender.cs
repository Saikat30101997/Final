using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Common.Utilities
{
    public interface IExcelEmailSender
    {
        void SendEmail(string receiver, string subject, string body,string file);
    }
}
