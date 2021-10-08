using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Models.ReCaptcha
{
    public interface IReCaptchaSettings
    {
        string ReCAPTCHA_Site_Key { get; set; }
        string ReCAPTCHA_Secret_Key { get; set; }
    }
}
