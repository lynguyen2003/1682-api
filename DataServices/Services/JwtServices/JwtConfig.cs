using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Services.JwtServices
{
    public class JwtConfig
    {
        public string Secret => Environment.GetEnvironmentVariable("JwtSecret");
    }
}
