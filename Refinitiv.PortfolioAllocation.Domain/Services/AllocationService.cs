using System;
using System.Collections.Generic;
using System.Text;

namespace Refinitiv.PortfolioAllocation.Domain.Services
{
    public class AllocationService : IAllocationService
    {
        private IPortfolioRepository _portfolioRepository;

        public AllocationService(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public IList<ShareResult> Calculate(string targetStock, decimal targetWeight)
        {
            var results = new List<ShareResult>();
            bool adequateCash = true;
            // validate inputs

            var items = _portfolioRepository.GetItems();
            decimal targetPercentage = targetWeight / 100;

            foreach (var portfolio in items)
            {
                Security targetSecurity = portfolio.Securities[targetStock];

                decimal totalValue = portfolio.GetTotalValue() * targetPercentage;

                decimal numberOfShares = Math.Floor(totalValue / targetSecurity.Price) - targetSecurity.TotalPrice();

                if (numberOfShares * targetSecurity.Price > portfolio.Securities["Cash"].TotalPrice())
                {
                    adequateCash = true;
                }

                results.Add(new ShareResult
                {
                    Quantity = numberOfShares,
                    PortfolioName = portfolio.Name,
                    AdequateFunds = adequateCash
                });
            }

            return results;
        }
    }
}
