using CsvHelper;
using Refinitiv.PortfolioAllocation.Domain.Data;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Refinitiv.PortfolioAllocation.Data.Csv
{
    public class CsvPortfolioRepository : IPortfolioRepository
    {
        private string _filename;

        public CsvPortfolioRepository(string filename)
        {
            _filename = filename;
        }

        public IEnumerable<PortfolioItem> GetItems()
        {
            using (var reader = new StreamReader(_filename))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = false;
                var records = csv.GetRecords<PortfolioItem>();
                
                return records;
            }
        }
    }
}
