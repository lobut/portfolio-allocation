using Refinitiv.PortfolioAllocation.Console.Domain;
using Refinitiv.PortfolioAllocation.Domain.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Refinitiv.PortfolioAllocation.Services.Tests
{
    public class MockPortfolioRepository : IPortfolioRepository
    {
        public IList<Portfolio> GetItems()
        {
            var portfolioA = new Portfolio("A");

            portfolioA.Securities.Add("FB", new Security { Name = "FB", Price = 100, QuantityHeld = 100 });
            portfolioA.Securities.Add("AAPL", new Security { Name = "AAPL", Price = 100, QuantityHeld = 100 });
            portfolioA.Securities.Add("Cash", new Security { Name = "Cash", Price = 80000, QuantityHeld = 1 });

            return new List<Portfolio> { portfolioA };
        }
    }
}
