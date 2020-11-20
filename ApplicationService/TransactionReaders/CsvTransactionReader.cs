using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace ApplicationService.TransactionReaders
{
    public class CsvTransactionReader : TransactionBaseReader
    {
        internal override string DateFormat => "dd/MM/yyyy hh:mm:ss";

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
                Map(m => m.OriginalDateTime).Index(3);
                Map(m => m.OriginalAmount).Index(1);
            }
        }

        #endregion
    }
}