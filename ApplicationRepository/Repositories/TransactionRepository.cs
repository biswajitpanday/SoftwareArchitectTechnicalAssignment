using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicationRepository.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TransactionRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task ImportTransaction(List<Transaction> transactions)
        {
            await _applicationDbContext.Transactions.AddRangeAsync(transactions);
            await _applicationDbContext.SaveChangesAsync();
        }

        public Task<List<Transaction>> GetAllByCurrency(string currency)
        {
            return _applicationDbContext.Transactions.Where(x => x.CurrencyCode == currency).ToListAsync();
        }

        public Task<List<Transaction>> GetAllByDateRange(DateTime startDate, DateTime endDate)
        {
            return _applicationDbContext.Transactions
                .Where(x => x.TransactionDate >= startDate && x.TransactionDate <= endDate).ToListAsync();
        }

        public Task<List<Transaction>> GetAllByStatus(string status)
        {
            return _applicationDbContext.Transactions.Where(x => x.Status == status).ToListAsync();
        }

    }
}