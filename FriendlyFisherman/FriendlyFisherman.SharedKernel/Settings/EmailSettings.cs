namespace FriendlyFisherman.SharedKernel.Settings
{
    public class EmailSettings
    {
        public string EmailAccount { get; set; }
        public string EmailAccountName { get; set; }
        public string EmailPassword { get; set; }
        public string SiteRedirectUrl { get; set; }

        public bool UseDefaultCredentials { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }

        public string EmailTo { get; set; }

        public string EmailsTemplatesFolder { get; set; }

        public string AccountConfirmationSubject { get; set; }
        public string AccountConfirmationEmailTemplate { get; set; }

        public string ResetPasswordSubject { get; set; }
        public string ResetPasswordEmailTemplate { get; set; }
    }

}
