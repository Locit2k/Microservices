using Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Domain.Entities
{
    public class Roles : BaseEntity
    {
        public Guid RecID { get; set; } = Guid.NewGuid();
        public string RoleID { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
    }
}
