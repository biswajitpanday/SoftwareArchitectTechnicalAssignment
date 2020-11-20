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
        public Task SaveTransaction()
        {
            throw new NotImplementedException();
        }

        public Task<List<Transaction>> GetAllTransactions(SearchFilter filter)
        {
            throw new NotImplementedException();
        }
    }
}