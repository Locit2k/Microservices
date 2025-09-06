using Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Domain.Entities
{
    public class UserLogins : BaseEntity
    {
        public Guid RecID { get; set; } = Guid.NewGuid();
        public string UserID { get; set; } = string.Empty;
        public string ProviderID { get; set; } = string.Empty;
        public string ProviderName { get; set; } = string.Empty;
        public int Status { get; set; } = 1;
    }
}
