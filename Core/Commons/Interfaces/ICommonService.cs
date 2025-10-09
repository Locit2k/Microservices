using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commons.Interfaces
{
    public interface ICommonService
    {
        string HashPassword(object data, string password);
        bool VerifyPassword(object user, string hashedPassword, string inputPassword);
    }
}
