using System;
using System.Collections.Generic;
using System.Text;

namespace Refinitiv.PortfolioAllocation.Domain.Data
{
    public class ShareResult
    {
        public string PortfolioName { get; set; }
        public decimal Quantity { get; set; }

        public bool AdequateFunds { get; set; } = true;

        public string Error { get; set; } 
    }
}
