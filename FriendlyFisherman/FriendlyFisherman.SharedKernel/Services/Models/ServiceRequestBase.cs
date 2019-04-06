namespace FriendlyFisherman.SharedKernel
{
    public class ServiceRequestBase<T>
    {
        public string ID { get; set; }
        public T Item { get; set; }
    }
}
