using System;
using System.Collections.Generic;
using System.Text;

namespace Refinitiv.PortfolioAllocation.Domain.Data
{
    public interface IPortfolioRepository
    {
        IEnumerable<PortfolioItem> GetItems();
    }
}
