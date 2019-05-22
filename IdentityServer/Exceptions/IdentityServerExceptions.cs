using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Exceptions
{
    public class IdentityServerExceptions : Exception
    {
        public string Code { get; }
        public string test{get;set;}

        public IdentityServerExceptions()
        {
            test ="test4";
        }

        public IdentityServerExceptions(string code)
        {
            Code = code;
        }

        public IdentityServerExceptions(string message, params object[] args) : this(string.Empty, message, args)
        {
        }

        public IdentityServerExceptions(string code, string message, params object[] args) : this(null, code, message, args)
        {
        }

        public IdentityServerExceptions(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public IdentityServerExceptions(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}
