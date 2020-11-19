
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.Services;

namespace ApplicationService.Services
{
    public class TransactionImportService : ITransactionImportService
    {
        private readonly IEnumerable<ITransactionFileReader> _transactionFileReaders;

        public TransactionImportService(IEnumerable<ITransactionFileReader> transactionFileReaders)
        {
            _transactionFileReaders = transactionFileReaders;
        }

        public async Task Import(string filePath)
        {
            var fileExtension = GetFileExtension(filePath);
            var transactionFileReader = GetTransactionFileReader(fileExtension);
            if (transactionFileReader == null)
                throw new Exception("Unknown format");
            var transactions = await transactionFileReader.Read(filePath);
        }


        #region Supported Methods

        private ITransactionFileReader GetTransactionFileReader(string fileExtension)
        {
            return _transactionFileReaders.FirstOrDefault(x => x.FileType == fileExtension);
        }

        private string GetFileExtension(string filePath)
        {
            var extension = Path.GetExtension(filePath);
            return extension.Replace(".", "");
        }

        #endregion
    }
}