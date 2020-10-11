using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sogeti.Models;

namespace Sogeti.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly OrderProcess _context;

        public OrderItemsController(OrderProcess context)
        {
            _context = context;
        }

        // GET: api/OrderItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemDTO>>> GetOrderItems()
        {
            return await _context.OrderItems.Select(x => OrderToDto(x)).ToListAsync();
        }

        // GET: api/OrderItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItemDTO>> GetOrderItem(long id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);

            if (orderItem == null)
            {
                return NotFound();
            }

            return OrderToDto(orderItem);
        }

        // PUT: api/OrderItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderItem(long id, OrderItemDTO orderItemDTO)
        {
            if (id != orderItemDTO.Id)
            {
                return BadRequest();
            }

            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }
            orderItem.CustomerId = orderItemDTO.CustomerId;
            orderItem.CustomerName = orderItemDTO.CustomerName;
            orderItem.IsComplete = orderItemDTO.IsComplete;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OrderItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<OrderItemDTO>> PostOrderItem(OrderItemDTO orderItemDto)
        {
            var orderItemDtoToAdd = new OrderItem
            {
                Id = orderItemDto.CustomerId,
                CustomerId = orderItemDto.CustomerId,
                CustomerName = orderItemDto.CustomerName,
                IsComplete = orderItemDto.IsComplete
            };
            _context.OrderItems.Add(orderItemDtoToAdd);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrderItem), new { id = orderItemDtoToAdd.Id }, OrderToDto(orderItemDtoToAdd));
        }

        // DELETE: api/OrderItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderItem>> DeleteOrderItem(long id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }

            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderItemExists(long id)
        {
            return _context.OrderItems.Any(e => e.Id == id);
        }

        private static OrderItemDTO OrderToDto(OrderItem orderItem) => new OrderItemDTO
        {
            Id = orderItem.Id,
            CustomerId = orderItem.Id,
            CustomerName = orderItem.CustomerName,
            IsComplete = orderItem.IsComplete
        };
    }
}
