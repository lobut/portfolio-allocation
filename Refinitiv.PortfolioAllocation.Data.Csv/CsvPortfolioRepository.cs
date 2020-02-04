using CsvHelper;
using Refinitiv.PortfolioAllocation.Console.Domain;
using Refinitiv.PortfolioAllocation.Domain.Data;
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
            var records = new List<CsvPortfolioItem>();
            var portfolios = new List<Portfolio>();

            using (var reader = new StreamReader(_filename))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = false;
                records = csv.GetRecords<CsvPortfolioItem>().ToList();
            }

            var grouped = records.GroupBy(x => x.Portfolio);

            foreach(var group in grouped)
            {
                var portfolio = new Portfolio(group.Key);

                foreach(var u in group)
                {
                    portfolio.Securities[u.Security] = new Security
                    {
                        Price = u.Price,
                        Name = u.Security,
                        QuantityHeld = u.QuantityHeld
                    };
                }
            }

            return portfolios;
        }
    }
}
