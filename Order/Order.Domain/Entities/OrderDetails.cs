using Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Entities
{
    public class OrderDetails : BaseEntity
    {
        public Guid RecID { get; set; } = Guid.NewGuid();
        public string OrderID { get; set; } = string.Empty;
        public string ProductID { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; } = 0;
        public decimal Price { get; set; } = 0;
        public decimal Total { get; set; } = 0;
    }
}
