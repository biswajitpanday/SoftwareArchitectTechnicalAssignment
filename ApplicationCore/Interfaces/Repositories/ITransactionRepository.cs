using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.Models.ViewModels;

namespace ApplicationCore.Interfaces.Repositories
{
    public interface ITransactionRepository
    {
        Task ImportTransaction(List<Transaction> transactions);
        Task<List<Transaction>> GetAllTransactions(SearchFilter filter);
    }
}