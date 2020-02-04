using NUnit.Framework;
using Refinitiv.PortfolioAllocation.Domain.Data;
using Refinitiv.PortfolioAllocation.Domain.Services;

namespace Refinitiv.PortfolioAllocation.Services.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            IPortfolioRepository portfolioRepository = new MockPortfolioRepository();
            var allocationService = new AllocationService(portfolioRepository);

            var result = allocationService.Calculate("FB", 50);

            Assert.IsTrue(false);
        }
    }
}