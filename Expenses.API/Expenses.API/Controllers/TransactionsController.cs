using Expenses.API.Data;
using Expenses.API.Data.Services;
using Expenses.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Expenses.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController(ITransactionsService transactionsService) : ControllerBase
    {
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllTransations()
        {
            return Ok(await transactionsService.GetAll());
        }

        [HttpGet("Details/{TransactionId}")]
        public async Task<IActionResult> GetTransationById([FromRoute] int TransactionId)
        {
            var transaction = await transactionsService.GetById(TransactionId);
            if (transaction is null) return NotFound();
            return Ok(transaction);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionRequestDto payload)
        {
            return Ok(await transactionsService.Create(payload));
        }

        [HttpPut("Update/{TransactionId:int}")]
        public async Task<IActionResult> UpdateTransation(int TransactionId, [FromBody] TransactionRequestDto payload)
        {
            return Ok(await transactionsService.Update(TransactionId, payload));
        }

        [HttpDelete("Delete/{TransactionId:int}")]
        public async Task<IActionResult> DeleteTransation(int TransactionId)
        {
            await transactionsService.Delete(TransactionId);
            return NoContent();
        }
    }
}
