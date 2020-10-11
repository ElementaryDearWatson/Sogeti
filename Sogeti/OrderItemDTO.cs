namespace Sogeti
{
    public class OrderItemDTO
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public bool IsComplete { get; set; }
    }
}
