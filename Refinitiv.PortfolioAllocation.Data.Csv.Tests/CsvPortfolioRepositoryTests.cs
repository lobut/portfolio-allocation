using FluentAssertions;
using NUnit.Framework;
using Refinitiv.PortfolioAllocation.Domain;
using System.Collections.Generic;

namespace Refinitiv.PortfolioAllocation.Data.Csv.Tests
{
    public class CsvPortfolioRepositoryTests
    {
        [Test]
        public void GetItems_WithMultiplePortfolios_Succeeds()
        {
            var csvRepository = new CsvPortfolioRepository("./portfolio1.csv");

            var results = csvRepository.GetItems();

            var portfolio1 = new Portfolio("A");
            
            portfolio1.Securities.Add("FB", new Security { Name = "FB", Price = 200, QuantityHeld = 3500 });
            portfolio1.Securities.Add("AAPL", new Security { Name = "AAPL", Price = 300, QuantityHeld = 3000 });
            portfolio1.Securities.Add("GOOG", new Security { Name = "GOOG", Price = 1400, QuantityHeld = 1000 });
            portfolio1.Securities.Add("Cash", new Security { Name = "Cash", Price = 1, QuantityHeld = 2000000 });

            var portfolio2 = new Portfolio("B");

            portfolio2.Securities.Add("FB", new Security { Name = "FB", Price = 200, QuantityHeld = 3000 });
            portfolio2.Securities.Add("AAPL", new Security { Name = "AAPL", Price = 300, QuantityHeld = 3000 });
            portfolio2.Securities.Add("GOOG", new Security { Name = "GOOG", Price = 1400, QuantityHeld = 1000 });
            portfolio2.Securities.Add("Cash", new Security { Name = "Cash", Price = 1, QuantityHeld = 2100000 });

            var portfolio3 = new Portfolio("C");

            portfolio3.Securities.Add("FB", new Security { Name = "FB", Price = 200, QuantityHeld = 600 });
            portfolio3.Securities.Add("AAPL", new Security { Name = "AAPL", Price = 300, QuantityHeld = 500 });
            portfolio3.Securities.Add("GOOG", new Security { Name = "GOOG", Price = 1400, QuantityHeld = 300 });
            portfolio3.Securities.Add("Cash", new Security { Name = "Cash", Price = 1, QuantityHeld = 500000 });

            var portfolios = new List<Portfolio> { portfolio1, portfolio2, portfolio3 };

            results.Should().BeEquivalentTo(portfolios);
        }
    }
}