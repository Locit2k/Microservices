using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.Enities
{
    public class Categories : BaseEntity
    {
        public string CategoryID { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
    }
}
