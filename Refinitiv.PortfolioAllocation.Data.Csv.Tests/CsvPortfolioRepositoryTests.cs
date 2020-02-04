using CsvHelper;
using NUnit.Framework;
using Refinitiv.PortfolioAllocation.Domain.Data;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Refinitiv.PortfolioAllocation.Data.Csv.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var csvRepository = new CsvPortfolioRepository("./portfolio1.csv");

            var results = csvRepository.GetItems();

            Assert.AreEqual(results, null);
        }
    }
}