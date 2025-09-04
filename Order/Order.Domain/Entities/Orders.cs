using Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Entities
{
    public class Orders : BaseEntity
    {
        public Guid RecID { get; set; } = Guid.NewGuid();
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime? DiliveryDate { get; set; } = null;
        public string UserID { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; } = 0;
        public string PaymentID { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public string ShippingAddress { get; set; } = String.Empty;
        public int Status { get; set; } = 0;
    }
}
