using Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreAPI.Core
{
    public class AnonActor : IApplicatioActor
    {
        public int Id => 0;

        public string Identity => "Anon";

        public IEnumerable<int> UseCases => new List<int> { 12 };
    }
}
