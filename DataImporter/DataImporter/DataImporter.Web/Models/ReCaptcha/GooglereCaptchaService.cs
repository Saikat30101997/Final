using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DataImporter.Web.Models.ReCaptcha
{
    public class GooglereCaptchaService  :IGooglereCaptchaService
    {
        private ReCaptchaSettings _reCaptchSettings;
        public GooglereCaptchaService(IOptions<ReCaptchaSettings> reCaptchSettings)
        {
            _reCaptchSettings = reCaptchSettings.Value;
        }

        public virtual async Task<GoogleResponse> RecaptchaVer(string token)
        {
            var googlerreCaptchaData = new GooglereCaptchaData
            {
                Response = token,
                Secret = _reCaptchSettings.ReCAPTCHA_Secret_Key
            };

            var client = new HttpClient();
            var response = await client.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret={googlerreCaptchaData.Secret}&response={googlerreCaptchaData.Response}");
            var capResponse = JsonConvert.DeserializeObject<GoogleResponse>(response);
            return capResponse;
        }
    }
}
