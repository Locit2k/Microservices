using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.Enities
{
    public class BaseEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
