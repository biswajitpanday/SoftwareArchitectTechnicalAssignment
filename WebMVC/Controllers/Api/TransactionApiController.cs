using System;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionApiController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionApiController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }


        [HttpGet("GetAllByCurrency/{currency}")]
        public async Task<IActionResult> GetAllByCurrency(string currency)
        {
            try
            {
                var transactions = await _transactionService.GetAllByCurrency(currency);
                return Ok(transactions);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetAllByDateRange/{startDate}/{endDate}")]
        public async Task<IActionResult> GetAllByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                var transactions = await _transactionService.GetAllByDateRange(startDate, endDate);
                return Ok(transactions);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetAllByStatus/{status}")]
        public async Task<IActionResult> GetAllByStatus(string status)
        {
            try
            {
                var transactions = await _transactionService.GetAllByStatus(status);
                return Ok(transactions);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}