using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Logging;

namespace ApplicationService.TransactionReaders
{
    public class CsvTransactionReader : TransactionBaseReader
    {
        public CsvTransactionReader(ILogger<CsvTransactionReader> logger) : base(logger)
        {
        }

        internal override Dictionary<string, string> StatusMap => new Dictionary<string, string>
            {{"Approved", "A"}, {"Failed", "R"}, {"Finished", "D"}};

        public override string FileType => "csv";

        public override Task<List<TransactionRawData>> ReadTransactions(string path)
        {
            return Task.Run(() =>
            {
                using var reader = new StreamReader(path);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                csv.Configuration.RegisterClassMap<TransactionCsvMap>();
                csv.Configuration.BadDataFound = null;
                csv.Configuration.HasHeaderRecord = false;
                var records = csv.GetRecords<TransactionRawData>().ToList();
                return records;
            });
        }

        #region Supported Class

        private class TransactionCsvMap : ClassMap<TransactionRawData>
        {
            public TransactionCsvMap()
            {
                Map(m => m.TransactionId).Index(0);
                Map(m => m.CurrencyCode).Index(2);
                Map(m => m.Status).Index(4);
                Map(m => m.TransactionDate).Index(3).TypeConverterOption.Format("dd/MM/yyyy hh:mm:ss");
                Map(m => m.OriginalAmount).Index(1);
            }
        }

        #endregion
    }
}