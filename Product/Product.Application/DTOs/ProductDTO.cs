using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.DTOs
{
    public class ProductDTO
    {
        public Guid RecID { get; set; } = Guid.NewGuid();
        public string ProductID { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string CategoryID { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
        public int Stock { get; set; } = 0;
        public string ImageUrl { get; set; } = string.Empty;
        public int Status { get; set; } = 0;
    }
}
