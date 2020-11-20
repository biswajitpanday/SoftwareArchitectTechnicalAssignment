using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.Interfaces.Repositories
{
    public interface ITransactionRepository
    {
        Task ImportTransaction(List<Transaction> transactions);
        Task<List<Transaction>> GetAllByCurrency(string currency);
        Task<List<Transaction>> GetAllByDateRange(DateTime startDate, DateTime endDate);
        Task<List<Transaction>> GetAllByStatus(string status);
    }
}