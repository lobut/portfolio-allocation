using CsvHelper;
using Refinitiv.PortfolioAllocation.Domain;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Refinitiv.PortfolioAllocation.Data.Csv
{
    public class CsvPortfolioRepository : IPortfolioRepository
    {
        private string _filename;

        public CsvPortfolioRepository(string filename)
        {
            _filename = filename;
        }
        
        public IList<Portfolio> GetItems()
        {
            var portfolios = new List<Portfolio>();
            var portfolioItem = new List<CsvPortfolioItem>();

            using (var reader = new StreamReader(_filename))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = false;
                portfolioItem = csv.GetRecords<CsvPortfolioItem>().ToList();
            }

            var securityByPortfolioName = portfolioItem.GroupBy(x => x.Portfolio);

            foreach(var securitiesByPortfolioName in securityByPortfolioName)
            {
                var portfolio = new Portfolio(securitiesByPortfolioName.Key);

                foreach(var security in securitiesByPortfolioName)
                {
                    portfolio.Securities[security.Security] = new Security
                    {
                        Price = security.Price,
                        Name = security.Security,
                        QuantityHeld = security.QuantityHeld
                    };
                }

                portfolios.Add(portfolio);
            }

            return portfolios;
        }
    }
}
