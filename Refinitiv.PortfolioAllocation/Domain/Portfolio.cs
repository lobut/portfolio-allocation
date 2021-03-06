﻿using System.Collections.Generic;

namespace Refinitiv.PortfolioAllocation.Domain
{
    public class Portfolio
    {
        public string Name { get; internal set; }
        public Dictionary<string, Security> Securities { get; set; } = new Dictionary<string, Security>();

        public Portfolio(string name)
        {
            Name = name;
        }

        public decimal GetTotalValueOf(string securityName)
        {
            return Securities[securityName].Price * Securities[securityName].QuantityHeld;
        }

        public decimal GetTotalValue()
        {
            decimal totalValue = 0;

            foreach(var security in Securities)
            {
                totalValue += (security.Value.Price * security.Value.QuantityHeld);
            }

            return totalValue;
        }
    }
}
