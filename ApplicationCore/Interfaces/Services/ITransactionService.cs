using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Models.ViewModels;

namespace ApplicationCore.Interfaces.Services
{
    public interface ITransactionService
    {
        Task<List<TransactionViewModel>> GetAllByCurrency(string currency);
        Task<List<TransactionViewModel>> GetAllByDateRange(DateTime startDate, DateTime endDate);
        Task<List<TransactionViewModel>> GetAllByStatus(string status);
    }
}