using System.Threading.Tasks;
using ApplicationCore.Interfaces.Services;
using ApplicationService.TransactionReaders;
using Xunit;

namespace ApplicationService.UnitTests.TransactionReaders
{
    public class CsvTransactionReaderTest
    {
        private readonly ITransactionFileReader _transactionFileReader;
        public CsvTransactionReaderTest()
        {
            _transactionFileReader = new CsvTransactionReader();
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