using System.Collections.Generic;

namespace Refinitiv.PortfolioAllocation.Domain.Tests.TestBuilder
{
    internal class PortfoliosTestBuilder
    {
        private List<Portfolio> _portfolios = new List<Portfolio>();
        private Dictionary<string, SecurityTestBuilder> _securityBuilders = new Dictionary<string, SecurityTestBuilder>();

        public SecurityTestBuilder WithPortfolio(string portfolioName)
        {
            var securitiesTestBuilder = new SecurityTestBuilder(this);
            var portfolio = new Portfolio(portfolioName);

            portfolio.Securities = securitiesTestBuilder.Securities;

            _portfolios.Add(portfolio);

            return securitiesTestBuilder;
        }

        public List<Portfolio> Build()
        {
            return _portfolios;
        }
    }
}
