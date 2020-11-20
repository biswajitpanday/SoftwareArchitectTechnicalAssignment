using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Extensions;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Models.ViewModels;

namespace ApplicationService.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<List<TransactionViewModel>> GetAllByCurrency(string currency)
        {
            var transactions = await _transactionRepository.GetAllByCurrency(currency);
            return transactions.ToTransactionViewModel();
        }

        public async Task<List<TransactionViewModel>> GetAllByDateRange(DateTime startDate, DateTime endDate)
        {
            var transactions = await _transactionRepository.GetAllByDateRange(startDate, endDate);
            return transactions.ToTransactionViewModel();
        }

        public async Task<List<TransactionViewModel>> GetAllByStatus(string status)
        {
            var transactions = await _transactionRepository.GetAllByStatus(status);
            return transactions.ToTransactionViewModel();
        }
    }
}