using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Models.ReCaptcha
{
    public class GooglereCaptchaData
    {
        public string Response { get; set; }
        public string Secret { get; set; }
    }
}
