using Autofac;
using DataImporter.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Common
{
    public class CommonModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConfirmationEmailSettings>().AsSelf();
            builder.RegisterType<EmailService>().As<IEmailService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<FileSearching>().As<IFileSearching>()
                .InstancePerLifetimeScope();
            builder.RegisterType<DateTimeUtility>().As<IDateTimeUtility>()
                .InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
