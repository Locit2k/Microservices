using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.DTOs
{
    public class OrderDTO
    {
        public Guid RecID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DiliveryDate { get; set; }
        public string UserID { get; set; }
        public string PaymentID { get; set; }
        public string Notes { get; set; }
        public string ShippingAddress { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public List<OrderDetailDTO> Details { get; set; } = new List<OrderDetailDTO>();
    }
}
