using System.Threading.Tasks;
using ApplicationCore.Interfaces.Services;
using ApplicationService.TransactionReaders;
using Xunit;

namespace ApplicationService.UnitTests.TransactionReaders
{
    public class XmlTransactionReaderTest
    {
        private readonly ITransactionFileReader _transactionFileReader;
        public XmlTransactionReaderTest()
        {
            _transactionFileReader = new XmlTransactionReader();
        }

        [Fact]
        public async Task Read_data_from_valid_file()
        {
            // arrange and act
            var transactions = await _transactionFileReader.Read("./Setup/2c2p.xml");

            //assert
            Assert.Equal(2, transactions.Count);
        }
    }
}