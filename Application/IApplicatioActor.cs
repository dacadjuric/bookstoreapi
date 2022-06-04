using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public interface IApplicatioActor
    {
        int Id { get; }
        string Identity { get; }
        IEnumerable<int> UseCases { get; }
    }
}
