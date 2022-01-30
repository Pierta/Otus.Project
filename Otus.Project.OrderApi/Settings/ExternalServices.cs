namespace Otus.Project.OrderApi.Settings
{
    public class ExternalServices
    {
        public BillingApi BillingApi { get; set; }
    }

    public class BillingApi
    {
        public string Url { get; set; }
    }
}
