using System.Net.Mail;


namespace CustomeModule.Interfaces.MailHelper.Interfaces
{
    public interface IMailService
    {
        void SendMail(string sender, string recipient, string subject, string body, bool isBodyHtml = false);
        void SendMail(MailMessage message);
    }
}
