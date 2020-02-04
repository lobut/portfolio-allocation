using Refinitiv.PortfolioAllocation.Domain;
using System.Collections.Generic;

namespace Refinitiv.PortfolioAllocation.Services
{
    public interface IAllocationService
    {
        IList<ShareResult> Calculate(string targetStock, decimal targetWeight);
    }
}
