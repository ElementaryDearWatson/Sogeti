namespace Sogeti.Models
{
    public class OrderItem
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public bool IsComplete { get; set; }
    }
}