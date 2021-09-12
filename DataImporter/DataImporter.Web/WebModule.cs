using Autofac;
using DataImporter.Web.Models;
using DataImporter.Web.Models.ReCaptcha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<ReCaptchaSettings>().AsSelf();
            builder.RegisterType<GooglereCaptchaService>()
                .As<IGooglereCaptchaService>()
                .InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}

