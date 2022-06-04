using Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreAPI.Core
{
    public class JWTActor : IApplicatioActor
    {
        public int Id { get; set; }
        public string Identity { get; set; }
        public IEnumerable<int> UseCases { get; set; }
    }
}
