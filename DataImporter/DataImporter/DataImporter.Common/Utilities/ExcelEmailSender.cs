using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Common.Utilities
{
    public class ExcelEmailSender : IExcelEmailSender
    {
        private SmtpConfiguration _smtp;
        public ExcelEmailSender(IOptions<SmtpConfiguration> smtp)
        {
            _smtp = smtp.Value;
        }

        public void SendEmail(string receiver, string subject, string body,string file)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_smtp.from, _smtp.from));
            message.To.Add(new MailboxAddress(receiver, receiver));
            message.Subject = subject;
            var builder = new BodyBuilder();
            builder.TextBody = @"Hello This is Your Excel File";
            builder.Attachments.Add(file);
            message.Body = builder.ToMessageBody();

            using var client = new SmtpClient();
            client.Timeout = 60000;
            client.Connect(_smtp.host, _smtp.port, _smtp.useSSL);


            // Note: only needed if the SMTP server requires authentication
            client.Authenticate(_smtp.username, _smtp.password);

            client.Send(message);
            client.Disconnect(true);

        }
    }
}
