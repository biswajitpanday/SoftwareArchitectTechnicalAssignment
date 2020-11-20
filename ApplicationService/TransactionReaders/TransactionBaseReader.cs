using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Models;

namespace ApplicationService.TransactionReaders
{
    public abstract class TransactionBaseReader : ITransactionFileReader
    {
        internal abstract string DateFormat { get; }

        internal abstract Dictionary<string, string> StatusMap { get; }
        public abstract string FileType { get; }

        public async Task<List<Transaction>> Read(string path)
        {
            var transactions = await ReadTransactions(path);
            var invalidTransactions = GetInvalidTransactions(transactions);
            if (invalidTransactions.Any())
                throw new Exception("Invalid file");

            return transactions.Select(x => new Transaction
            {
                TransactionId = x.TransactionId,
                TransactionDate = System.Convert.ToDateTime(x.OriginalDateTime),
                Amount = System.Convert.ToDecimal(x.OriginalAmount),
                CurrencyCode = x.CurrencyCode,
                Status = x.Status,
                OutputStatus = GetOutputStatus(x.Status)
            }).ToList();
        }

        public abstract Task<List<TransactionRawData>> ReadTransactions(string path);

        #region Supported Methods

        private List<TransactionRawData> GetInvalidTransactions(List<TransactionRawData> transactions)
        {
            var invalidTransactions = new List<TransactionRawData>();
            foreach (var transaction in transactions)
                if (!IsValidTransactionId(transaction) || !IsValidAmount(transaction) ||
                    !IsValidCurrencyCode(transaction) || !IsValidTransactionDate(transaction) ||
                    !IsValidStatus(transaction) || !CheckMandatoryFields(transaction))
                    invalidTransactions.Add(transaction);
            return invalidTransactions;
        }


        private bool CheckMandatoryFields(TransactionRawData transaction)
        {
            if (string.IsNullOrEmpty(transaction.TransactionId) || transaction.Amount <= -1 ||
                string.IsNullOrEmpty(transaction.CurrencyCode) || string.IsNullOrEmpty(transaction.OriginalDateTime))
                return false;
            return true;
        }

        private bool IsValidTransactionId(TransactionRawData transaction)
        {
            var isValid = transaction.TransactionId.Length <= 50;
            return isValid;
        }

        private bool IsValidAmount(TransactionRawData transaction)
        {
            var isValid = decimal.TryParse(transaction.OriginalAmount, out _);
            return isValid;
        }

        private bool IsValidCurrencyCode(TransactionRawData transaction)
        {
            IEnumerable<string> currencySymbols = CultureInfo
                .GetCultures(CultureTypes.SpecificCultures)
                .Select(x => new RegionInfo(x.LCID).ISOCurrencySymbol)
                .Distinct()
                .OrderBy(x => x);
            var isValid = currencySymbols.Contains(transaction.CurrencyCode);
            return isValid;
        }

        private bool IsValidTransactionDate(TransactionRawData transaction)
        {
            var isValid = DateTime.TryParseExact(transaction.OriginalDateTime, new[] {DateFormat},
                CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
            return isValid;
        }

        private bool IsValidStatus(TransactionRawData transaction)
        {
            var outputStatus = GetOutputStatus(transaction.Status);
            return !string.IsNullOrEmpty(outputStatus);
        }


        private string GetOutputStatus(string status)
        {
            if (StatusMap.TryGetValue(status, out var outputStatus))
                return outputStatus;
            return string.Empty;
        }

        #endregion
    }


    public class TransactionRawData : Transaction
    {
        public string OriginalDateTime { get; set; }
        public string OriginalAmount { get; set; }
    }
}