using Microsoft.Extensions.Options;
using CustomeModule.Interfaces.MailHelper.Interfaces;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace CustomeModule.MailHelper
{
    public class MailService : IMailService
    {
        private readonly IOptions<EmailSetting> _settings;

        public MailService(IOptions<EmailSetting> setting)
        {
            _settings = setting;
        }

        public void SendMail(string sender, string recipient, string subject, string body, bool isBodyHtml = false)
        {
            var message = new MailMessage(sender, recipient, subject, body) { IsBodyHtml = isBodyHtml };
            SendMail(message);
        }

        public void SendMail(MailMessage message)
        {
            try
            {
                var mSmtp = _settings.Value.SMTPServer;
                var mPort = _settings.Value.SMTPHost;
                var uName = _settings.Value.SMTPUser;
                var password = _settings.Value.SMTPPassword;
                bool enableSSL = _settings.Value.EnableSSL;

                var mailClient = new SmtpClient
                {
                    Host = mSmtp,
                    Port = mPort,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true,
                    EnableSsl = enableSSL,

                    // Code below is use only if there's a specific user for email sending in the SMTP server
                    // Does not work for individual user credentials.
                    Credentials = new NetworkCredential(uName, password)
                };
                // Notes
                // Code below used only to buy pass the certificate of the Mcafee Anti Virus in order to work with SMTP
                // Do not use this approach in real life scenario. 
                //  ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                mailClient.Send(message);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}