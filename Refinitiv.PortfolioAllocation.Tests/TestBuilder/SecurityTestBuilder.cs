using System.Collections.Generic;

namespace Refinitiv.PortfolioAllocation.Domain.Tests.TestBuilder
{
    internal class SecurityTestBuilder
    {
        public Dictionary<string, Security> Securities { get; private set; } = new Dictionary<string, Security>();
        private PortfoliosTestBuilder _parentBuilder;
        private int count = 0;

        public SecurityTestBuilder(PortfoliosTestBuilder parentBuilder)
        {
            _parentBuilder = parentBuilder;
        }

        public SecurityTestBuilder WithSecurity(Security security)
        {
            Securities.Add(security.Name, security);
            return this;
        }

        public SecurityTestBuilder WithDummySecurity()
        {
            count++;
            string name = $"DummyStock_{count}";
            Securities.Add(name, new Security { Name = name, Price = 1, QuantityHeld = 1 });

            return this;
        }

        public PortfoliosTestBuilder Done()
        {
            return _parentBuilder;
        }
    }
}
