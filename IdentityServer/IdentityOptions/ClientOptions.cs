using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.IdentityOptions
{
    public class ClientOptions
    {
        public  string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public List<string> AllowedGrantTypes { get; set; }
        public  List<string> AllowedScopes { get; set; }
    }
}
