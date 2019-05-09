namespace FriendlyFisherman.SharedKernel.Services.Models
{
    public class ServiceRequestBase<T>
    {
        public string ID { get; set; }
        public T Item { get; set; }
    }
}
