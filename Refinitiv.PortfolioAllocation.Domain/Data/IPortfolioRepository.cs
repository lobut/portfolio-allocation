using Refinitiv.PortfolioAllocation.Console.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Refinitiv.PortfolioAllocation.Domain.Data
{
    public interface IPortfolioRepository
    {
        IList<Portfolio> GetItems();
    }
}
