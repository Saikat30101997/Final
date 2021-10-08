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
            builder.RegisterType<GroupListModel>().AsSelf();
            builder.RegisterType<EditGroupModel>().AsSelf();
            builder.RegisterType<ContactModel>().AsSelf();
            builder.RegisterType<CreateImportModel>().AsSelf();
            builder.RegisterType<ImportContactModel>().AsSelf();
            builder.RegisterType<ExportJobModel>().AsSelf();
            builder.RegisterType<ImportJobModel>().AsSelf();
            builder.RegisterType<ListModel>().AsSelf();
            base.Load(builder);
        }
    }
}

