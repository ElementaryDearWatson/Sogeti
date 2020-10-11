using Microsoft.EntityFrameworkCore;

namespace Sogeti.Models
{
    public class OrderProcess : DbContext
    {
        public OrderProcess(DbContextOptions<OrderProcess> OrderDetails) : base(OrderDetails)
        {
        }

        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
