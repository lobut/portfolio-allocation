using System;
using System.Collections.Generic;
using System.Text;

namespace Refinitiv.PortfolioAllocation.Data.Csv
{
    public class CsvPortfolioItem
    {
        public string Portfolio { get; set; }
        public string Security { get; set; }
        public int Price { get; set; }
        public int QuantityHeld { get; set; }
    }
}
