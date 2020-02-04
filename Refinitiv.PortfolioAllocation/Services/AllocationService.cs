using Refinitiv.PortfolioAllocation.Domain;
using System;
using System.Collections.Generic;

namespace Refinitiv.PortfolioAllocation.Services
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
            var portfolios = _portfolioRepository.GetItems();

            if (targetWeight < 0 || targetWeight > 100)
            {
                throw new ApplicationException($"Invalid Target Weight has been specified: {targetWeight}. Please use a value between 0 and 100.");
            }

            decimal targetPercentage = targetWeight / 100;

            foreach (var portfolio in portfolios)
            {
                if (portfolio.Securities.ContainsKey(targetStock) == false)
                {
                    throw new ApplicationException($"Portfolio {portfolio} does not contain the stock {targetStock}.");
                }

                Security targetSecurity = portfolio.Securities[targetStock];

                decimal totalPortfolioValue = portfolio.GetTotalValue() * targetPercentage;
                decimal numberOfSharesNeededForTarget = Math.Floor(totalPortfolioValue / targetSecurity.Price) - targetSecurity.QuantityHeld;
                decimal maxSharesPurchaseable = GetMaximumPossibleSharesInCash(targetSecurity, portfolio);

                results.Add(new ShareResult
                {
                    Quantity = Math.Min(maxSharesPurchaseable, numberOfSharesNeededForTarget),
                    PortfolioName = portfolio.Name
                });
            }

            return results;
        }

        private decimal GetMaximumPossibleSharesInCash(Security targetSecurity, Portfolio portfolio)
        {
            if (portfolio.Securities.ContainsKey("Cash") == false)
            {
                return 0;
            }

            return (portfolio.Securities["Cash"].TotalPrice() / targetSecurity.Price);
        }
    }
}
