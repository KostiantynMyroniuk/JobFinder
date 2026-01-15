using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Interfaces
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool Verify(string password, string hashPassword);
    }
}
