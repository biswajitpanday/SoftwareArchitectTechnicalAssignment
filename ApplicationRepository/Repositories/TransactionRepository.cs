using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Models;
using ApplicationCore.Models.ViewModels;

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

        public Task<List<Transaction>> GetAllTransactions(SearchFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}