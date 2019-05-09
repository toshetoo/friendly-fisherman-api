namespace FriendlyFisherman.SharedKernel.Models.EmailTemplates
{
    public class EmailTemplateModel
    {
        public string Name { get; set; }

        public string CallBackUrl { get; set; }

        public static EmailTemplateModel Create(string name, string callBackUrl)
        {
            return new EmailTemplateModel
            {
                Name = name,
                CallBackUrl = callBackUrl,
            };
        }
    }

}
