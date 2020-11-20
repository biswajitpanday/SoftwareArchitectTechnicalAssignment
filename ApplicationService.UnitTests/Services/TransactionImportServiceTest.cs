using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Models;
using ApplicationService.Services;
using Moq;
using Xunit;

namespace ApplicationService.UnitTests.Services
{
    public class TransactionImportServiceTest
    {
        public TransactionImportServiceTest()
        {
            InitMockTransactionFileReaders();
            InitMockTransactionRepository();

            _transactionImportService = new TransactionImportService(
                new List<ITransactionFileReader> {_mockCsvFileReader.Object, _mockXmlFileReader.Object},
                _mockTransactionRepository.Object);
        }

        private readonly ITransactionImportService _transactionImportService;
        private Mock<ITransactionFileReader> _mockCsvFileReader;
        private Mock<ITransactionFileReader> _mockXmlFileReader;
        private Mock<ITransactionRepository> _mockTransactionRepository;

        private void InitMockTransactionFileReaders()
        {
            _mockCsvFileReader = new Mock<ITransactionFileReader>();
            _mockCsvFileReader.Setup(x => x.FileType).Returns("csv");
            _mockCsvFileReader.Setup(x => x.Read(It.IsAny<string>())).Returns(Task.Run(() => new List<Transaction>()));

            _mockXmlFileReader = new Mock<ITransactionFileReader>();
            _mockXmlFileReader.Setup(x => x.FileType).Returns("xml");
            _mockXmlFileReader.Setup(x => x.Read(It.IsAny<string>())).Returns(Task.Run(() => new List<Transaction>()));
        }

        private void InitMockTransactionRepository()
        {
            _mockTransactionRepository = new Mock<ITransactionRepository>();
            _mockTransactionRepository.Setup(x => x.ImportTransaction(It.IsAny<List<Transaction>>()));
        }

        [Fact]
        public async Task Read_CsvData_from_valid_file()
        {
            // arrange and act
            await _transactionImportService.Import("./Setup/2c2p.csv");

            //assert
            _mockCsvFileReader.Verify(m => m.Read(It.IsAny<string>()), Times.Once);
            _mockXmlFileReader.Verify(m => m.Read(It.IsAny<string>()), Times.Never);
            _mockTransactionRepository.Verify(m => m.ImportTransaction(It.IsAny<List<Transaction>>()), Times.Once);

        }

        [Fact]
        public async Task Read_XmlData_from_valid_file()
        {
            // arrange and act
            await _transactionImportService.Import("./Setup/2c2p.xml");

            //assert
            _mockXmlFileReader.Verify(m => m.Read(It.IsAny<string>()), Times.Once);
            _mockCsvFileReader.Verify(m => m.Read(It.IsAny<string>()), Times.Never);
            _mockTransactionRepository.Verify(m => m.ImportTransaction(It.IsAny<List<Transaction>>()), Times.Once);
        }

        [Fact]
        public async Task Do_not_allow_wrong_file_format()
        {
            //arrange and act
            Task Action() => _transactionImportService.Import("./Setup/2c2p.docx");

            //Assert
            await Assert.ThrowsAsync<Exception>(Action);
        }

        //exception

    }
}