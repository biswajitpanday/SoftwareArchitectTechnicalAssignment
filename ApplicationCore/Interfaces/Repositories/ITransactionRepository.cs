using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.Models.ViewModels;

namespace ApplicationCore.Interfaces.Repositories
{
    public interface ITransactionRepository
    {
        Task SaveTransaction();
        Task<List<Transaction>> GetAllTransactions(SearchFilter filter);
    }
}