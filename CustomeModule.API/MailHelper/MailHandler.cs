using System;
using System.Net.Mail;
using System.IO;
using CustomeModule.Interfaces.MailHelper.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using CustomeModule.Model.Model;

namespace CustomeModule.MailHelper
{
    public class MailHandler : IMailHandler
    {
        private readonly IMailService _mailService;
        private readonly IHostingEnvironment _hosting;
        private readonly IOptions<EmailTemplate> _template;


        public MailHandler(IMailService mailService, IHostingEnvironment hosting, IOptions<EmailTemplate> template)
        {
            _mailService = mailService;
            _hosting = hosting;
            _template = template;
        
        }

        public void SendMail(string from, string to, string subject, string sender, string receiver, Object model, int templateId, string cc = "")
        {
            string path = "";
            string title = "";
            if (templateId == 1)
            {
                path = _template.Value.EmailConfirmation;
                title = "Allbank: Email Confirmation.";
            }

            else if (templateId == 2)
            {
                path = _template.Value.EmailCheckStatus;
                title = "Allbank: Checkwriter Facility";
            }

            else if (templateId == 3)
            {
                path = _template.Value.EmailApproveStatus;
                title = "Allbank: Checkwriter Facility";
            }


            else if (templateId == 4)
            {
                path = _template.Value.EmailRejectStatus;
                title = "Allbank: Checkwriter Facility";
            }



            var mailMessage = CreateMailMessage(string.Format(title), from);

            string[] contactEmails = to.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string emailAdd in contactEmails)
                mailMessage.To.Add(new MailAddress(emailAdd));

            if (cc != null && cc != "")
            {
                string[] ccEmails = cc.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string emailAdd in ccEmails)
                    mailMessage.CC.Add(new MailAddress(emailAdd));
            }

            mailMessage = RenderMessage(path, mailMessage, sender, receiver, model, templateId);
            _mailService.SendMail(mailMessage);
        }

        public MailMessage CreateMailMessage(string subject, string from)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(from, "no-reply"),
                IsBodyHtml = true,
                Subject = subject
            };

            return mailMessage;
        }

        // Need  to add Mapping for User and Approver
        // TODO:       
        public MailMessage RenderMessage(string templateFilePath, MailMessage message, string sender, string receiver, Object model, int templateId)
        {
            string body, path;
            path = _hosting.WebRootPath.ToString() + "\\UploadImage\\Logo\\logo.png";
            templateFilePath = _hosting.WebRootPath.ToString() + templateFilePath;
    
            using (var reader = File.OpenText(templateFilePath))
            {
                body = reader.ReadToEnd();
            }

             body = body.Replace("[USER]", receiver);
            if (templateId == 1)
            {
                var user = (User)model;
                body = body.Replace("[ACCESSCODE]", user.password);
            }




            message.Body = body;

            var inlineLogo = new LinkedResource(path) { ContentId = "company-logo" };
            var view = AlternateView.CreateAlternateViewFromString(message.Body, null, "text/html");


            view.LinkedResources.Add(inlineLogo);
            message.AlternateViews.Add(view);

            return message;
        }
    }
}