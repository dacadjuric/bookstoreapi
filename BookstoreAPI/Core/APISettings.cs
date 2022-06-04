using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreAPI.Core
{
    public class APISettings
    {
        public string DbConnectionString { get; set; }
        public string JWTSecretKey { get; set; }
        public string JWTIssuer { get; set; }
        public string EmailFrom { get; set; }
        public string EmailPassword { get; set; }
    }
}
