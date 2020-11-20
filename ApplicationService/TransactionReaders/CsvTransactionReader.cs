using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Models;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace ApplicationService.TransactionReaders
{
    public class CsvTransactionReader : ITransactionFileReader
    {
        public string FileType => "csv";

        public Task<List<Transaction>> Read(string path)
        {
            return Task.Run(() =>
            {
                using var reader = new StreamReader(path);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                csv.Configuration.RegisterClassMap<TransactionCsvMap>();
                csv.Configuration.BadDataFound = null;
                csv.Configuration.HasHeaderRecord = false;
                var records = csv.GetRecords<Transaction>().ToList();
                return records;
            });
        }

        #region Supported Class

        private class TransactionCsvMap : ClassMap<Transaction>
        {
            public TransactionCsvMap()
            {
                Map(m => m.TransactionId).Index(0);
                Map(m => m.Amount).Index(1).TypeConverter<DecimalConverter>();
                Map(m => m.CurrencyCode).Index(2);
                Map(m => m.TransactionDate).Index(3).TypeConverterOption.Format("dd/MM/yyyy hh:mm:ss");
                Map(m => m.Status).Index(4);
            }
        }

        #endregion
    }
}