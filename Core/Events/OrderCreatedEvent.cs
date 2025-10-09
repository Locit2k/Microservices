using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events
{
    public class OrderCreatedEvent
    {
        public string OrderId { get; set; } = default!;
        public string ProductId { get; set; } = default!;
        public int Quantity { get; set; }
    }
    public class StockReservedEvent()
    {
        public string OrderId { get; set; } = default!;
        public string ProductId { get; set; } = default!;
        public int Quantity { get; set; }
    }
    public class StockRejectedEvent
    {
        public string OrderId { get; set; } = default!;
        public string Reason { get; set; } = default!;
    }
    public class PaymentCompletedEvent
    {
        public string OrderId { get; set; } = default!;
        public decimal Amount { get; set; }
    }

    public class PaymentFailedEvent
    {
        public string OrderId { get; set; } = default!;
        public bool Success { get; set; }
    }
}
