using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;

namespace ApplicationService.Services
{
    public class TransactionImportService : ITransactionImportService
    {
        private readonly IEnumerable<ITransactionFileReader> _transactionFileReaders;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionImportService(IEnumerable<ITransactionFileReader> transactionFileReaders,
            ITransactionRepository transactionRepository)
        {
            _transactionFileReaders = transactionFileReaders;
            _transactionRepository = transactionRepository;
        }

        public async Task Import(string filePath)
        {
            var fileExtension = GetFileExtension(filePath);
            var transactionFileReader = GetTransactionFileReader(fileExtension);
            if (transactionFileReader == null)
                throw new Exception("Unknown format");
            var transactions = await transactionFileReader.Read(filePath);
            transactions = transactions.Select(x =>
            {
                x.OutputStatus = MapStatus(x.Status);
                return x;
            }).ToList();
            await _transactionRepository.ImportTransaction(transactions);
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

        private string MapStatus(string status)
        {
            var statusMaps = new Dictionary<string, string> { { "Approved", "A" }, { "Failed", "R" }, { "Finished", "D" }, { "Rejected", "R" }, { "Done", "D" } };
            if (statusMaps.TryGetValue(status, out var outputStatus))
                return outputStatus;
            return string.Empty;
        }

        #endregion
    }
}