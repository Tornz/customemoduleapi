using System;
using System.Net.Mail;


namespace CustomeModule.Interfaces.MailHelper.Interfaces
{
    public interface IMailHandler
    {
        void SendMail(string from, string to, string subject, string sender, string receiver, Object model, int templateId, string cc = "");
        MailMessage CreateMailMessage(string subject, string from);
        MailMessage RenderMessage(string templateFilePath, MailMessage message, string sender, string receiver, Object model, int templateId);
    }
}
