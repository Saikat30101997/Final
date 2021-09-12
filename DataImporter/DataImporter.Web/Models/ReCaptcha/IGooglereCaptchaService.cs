using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web.Models.ReCaptcha
{
    public interface IGooglereCaptchaService
    {
        Task<GoogleResponse> RecaptchaVer(string token);
    }
}
