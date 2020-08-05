

namespace CustomeModule.MailHelper
{
    public class EmailSetting
    {
        public string SMTPServer { get; set; }
        public string SMTPUser { get; set; }
        public string SMTPPassword { get; set; }
        public int SMTPHost { get; set; }
        public bool EnableSSL { get; set; }
    }
}
