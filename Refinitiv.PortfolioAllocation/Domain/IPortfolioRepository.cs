using System.Collections.Generic;

namespace Refinitiv.PortfolioAllocation.Domain
{
    public interface IPortfolioRepository
    {
        IList<Portfolio> GetItems();
    }
}
