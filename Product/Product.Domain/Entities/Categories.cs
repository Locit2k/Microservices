using Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Entities
{
    public class Categories : BaseEntity
    {
        public Guid RecID { get; set; } = Guid.NewGuid();
        public string CategoryID { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
    }
}
