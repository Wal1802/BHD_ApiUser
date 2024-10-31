using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHD.Application.Security.Authentication
{
    public interface IJwtFactory
    {
        string GenerateEncodedToken(string username);
    }
}
