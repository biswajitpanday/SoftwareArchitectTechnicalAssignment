using System.Threading.Tasks;
using ApplicationCore.Interfaces.Services;
using ApplicationService.TransactionReaders;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ApplicationService.UnitTests.TransactionReaders
{
    public class CsvTransactionReaderTest
    {
        private readonly ITransactionFileReader _transactionFileReader;
        public CsvTransactionReaderTest()
        {
            var logger = new Mock<ILogger<CsvTransactionReader>>();
            _transactionFileReader = new CsvTransactionReader(logger.Object);
        }

        [Fact]
        public async Task Read_data_from_valid_file()
        {
            // arrange and act
            var transactions = await _transactionFileReader.Read("./Setup/2c2p.csv");

            //assert
            Assert.Equal(2, transactions.Count);
        }

    }
}