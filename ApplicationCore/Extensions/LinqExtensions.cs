using System.Collections.Generic;
using System.Linq;
using ApplicationCore.Models;
using ApplicationCore.Models.ViewModels;

namespace ApplicationCore.Extensions
{
    public static class LinqExtensions
    {
        public static List<TransactionViewModel> ToTransactionViewModel(this List<Transaction> transactions)
        {
            return transactions.Select(x => new TransactionViewModel
            {
                Id = x.TransactionId,
                Payment = $"{x.Amount} {x.CurrencyCode}",
                Status = x.OutputStatus
            }).ToList();
        }
    }
}