using Moq;
using NUnit.Framework;
using Refinitiv.PortfolioAllocation.Domain;
using Refinitiv.PortfolioAllocation.Domain.Tests.TestBuilder;
using System;

namespace Refinitiv.PortfolioAllocation.Services.Tests
{
    public class AllocationServiceTests
    {
        IAllocationService _sut;
        Mock<IPortfolioRepository> mockRepo = new Mock<IPortfolioRepository>();

        [SetUp]
        public void Setup()
        {
            _sut = new AllocationService(mockRepo.Object);
        }

        [Test]
        public void Calculate_BasicStock_ReturnsCorrectResult()
        {
            // arrange
            var results = new PortfoliosTestBuilder()
                .WithPortfolio("A")
                    .WithSecurity(new Security { Name = "FB", Price = 100, QuantityHeld = 100 })
                    .WithSecurity(new Security { Name = "AAPL", Price = 100, QuantityHeld = 100 })
                    .WithSecurity(new Security { Name = "Cash", Price = 80000, QuantityHeld = 1 }).Done()
                .Build();

            mockRepo
                .Setup(x => x.GetItems())
                .Returns(results);

            // act
            var result = _sut.Calculate("FB", 50);

            // assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].PortfolioName, Is.EqualTo("A"));
            Assert.That(result[0].Quantity, Is.EqualTo(400));
        }

        [Test]
        public void Calculate_SellStockToMakePercentage_ReturnsCorrectResult()
        {
            // arrange
            var results = new PortfoliosTestBuilder()
                .WithPortfolio("A")
                    .WithSecurity(new Security { Name = "FB", Price = 100, QuantityHeld = 100 })
                    .WithSecurity(new Security { Name = "Cash", Price = 100, QuantityHeld = 1 }).Done()
                .Build();

            mockRepo
                .Setup(x => x.GetItems())
                .Returns(results);

            // act
            var result = _sut.Calculate("FB", 0);

            // assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].PortfolioName, Is.EqualTo("A"));
            Assert.That(result[0].Quantity, Is.EqualTo(-100));
        }

        [Test]
        public void Calculate_StockThatDoesntExist_ThrowsError()
        {
            // arrange
            var results = new PortfoliosTestBuilder()
                .WithPortfolio("A")
                    .WithDummySecurity().Done()
                .Build();

            mockRepo
                .Setup(x => x.GetItems())
                .Returns(results);

            // act / assert
            Assert.Throws<ApplicationException>(() => _sut.Calculate("THIS_STOCK_DOES_NOT_EXIST", 50));
        }

        [Test]
        public void Calculate_SpendAllCash_ThrowsError()
        {
            // arrange
            var results = new PortfoliosTestBuilder()
                .WithPortfolio("A")
                    .WithSecurity(new Security { Name = "AAPL", Price = 100, QuantityHeld = 100 })
                    .WithSecurity(new Security { Name = "Cash", Price = 1, QuantityHeld = 10000 }).Done()
                .Build();

            mockRepo
                .Setup(x => x.GetItems())
                .Returns(results);

            // act / assert
            var result = _sut.Calculate("AAPL", 100);

            // assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].PortfolioName, Is.EqualTo("A"));
            Assert.That(result[0].Quantity, Is.EqualTo(100));
        }

        [Test]
        public void Calculate_SpendNoCash_ReturnsZeroForQuantity()
        {
            // arrange
            var results = new PortfoliosTestBuilder()
                .WithPortfolio("A")
                    .WithSecurity(new Security { Name = "AAPL", Price = 100, QuantityHeld = 100 }).Done()
                .Build();

            mockRepo
                .Setup(x => x.GetItems())
                .Returns(results);

            // act / assert
            var result = _sut.Calculate("AAPL", 100);

            // assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].PortfolioName, Is.EqualTo("A"));
            Assert.That(result[0].Quantity, Is.EqualTo(0));
        }

        [Test]
        public void Calculate_TooLowPercentage_ThrowsError()
        {
            // arrange
            var results = new PortfoliosTestBuilder()
                .WithPortfolio("A")
                    .WithSecurity(new Security { Name = "AAPL", Price = 100, QuantityHeld = 100 })
                    .WithSecurity(new Security { Name = "Cash", Price = 80000, QuantityHeld = 1 }).Done()
                .Build();

            mockRepo
                .Setup(x => x.GetItems())
                .Returns(results);

            // act / assert
            Assert.Throws<ApplicationException>(() => _sut.Calculate("AAPL", -1));
        }

        [Test]
        public void Calculate_TooHighPercentage_ThrowsError()
        {
            // arrange
            var results = new PortfoliosTestBuilder()
                .WithPortfolio("A")
                    .WithSecurity(new Security { Name = "AAPL", Price = 100, QuantityHeld = 100 })
                    .WithSecurity(new Security { Name = "Cash", Price = 80000, QuantityHeld = 1 }).Done()
                .Build();

            mockRepo
                .Setup(x => x.GetItems())
                .Returns(results);

            // act / assert
            Assert.Throws<ApplicationException>(() => _sut.Calculate("AAPL", 101));
        }

        [Test]
        public void Calculate_BasicStocks_ReturnsCorrectResult()
        {
            // arrange
            var results = new PortfoliosTestBuilder()
                .WithPortfolio("A")
                    .WithSecurity(new Security { Name = "FB", Price = 100, QuantityHeld = 100 })
                    .WithSecurity(new Security { Name = "AAPL", Price = 100, QuantityHeld = 100 })
                    .WithSecurity(new Security { Name = "Cash", Price = 80000, QuantityHeld = 1 }).Done()
                .Build();

            mockRepo
                .Setup(x => x.GetItems())
                .Returns(results);

            // act
            var result = _sut.Calculate("FB", 50);

            // assert
            var expectedResult = new ShareResult[]
            {
                new ShareResult { PortfolioName = "A", Quantity = 400 }
            };

            Assert.That(expectedResult.Length, Is.EqualTo(1));
            Assert.That(expectedResult[0].PortfolioName, Is.EqualTo("A"));
            Assert.That(expectedResult[0].Quantity, Is.EqualTo(400));
        }

        [Test]
        public void Calculate_MultipleStocks_ReturnsCorrectResult()
        {
            // arrange
            var results = new PortfoliosTestBuilder()
                .WithPortfolio("A")
                    .WithSecurity(new Security { Name = "FB", Price = 200, QuantityHeld = 3500 })
                    .WithSecurity(new Security { Name = "AAPL", Price = 300, QuantityHeld = 3000 })
                    .WithSecurity(new Security { Name = "GOOG", Price = 1400, QuantityHeld = 1000 })
                    .WithSecurity(new Security { Name = "Cash", Price = 1, QuantityHeld = 2000000 }).Done()
                .WithPortfolio("B")
                    .WithSecurity(new Security { Name = "FB", Price = 200, QuantityHeld = 3000 })
                    .WithSecurity(new Security { Name = "AAPL", Price = 300, QuantityHeld = 3000 })
                    .WithSecurity(new Security { Name = "GOOG", Price = 1400, QuantityHeld = 1000 })
                    .WithSecurity(new Security { Name = "Cash", Price = 1, QuantityHeld = 2100000 }).Done()
                .WithPortfolio("C")
                    .WithSecurity(new Security { Name = "FB", Price = 200, QuantityHeld = 600 })
                    .WithSecurity(new Security { Name = "AAPL", Price = 300, QuantityHeld = 500 })
                    .WithSecurity(new Security { Name = "GOOG", Price = 1400, QuantityHeld = 300 })
                    .WithSecurity(new Security { Name = "Cash", Price = 1, QuantityHeld = 500000 }).Done()
                .Build();

            mockRepo
                .Setup(x => x.GetItems())
                .Returns(results);

            // act
            var result = _sut.Calculate("FB", 15);

            // assert
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result[0].PortfolioName, Is.EqualTo("A"));
            Assert.That(result[0].Quantity, Is.EqualTo(250));
            Assert.That(result[1].PortfolioName, Is.EqualTo("B"));
            Assert.That(result[1].Quantity, Is.EqualTo(750));
            Assert.That(result[2].PortfolioName, Is.EqualTo("C"));
            Assert.That(result[2].Quantity, Is.EqualTo(292));
        }
    }
}