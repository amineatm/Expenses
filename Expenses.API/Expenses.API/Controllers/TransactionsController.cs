using Expenses.API.Data.Services.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    [Authorize]
    public class TransactionsController(ITransactionsService _transactionsService) : ControllerBase
    {
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllTransations()
        {
            return Ok(await _transactionsService.GetAll());
        }

        [HttpGet("Details/{transactionId:int}")]
        public async Task<IActionResult> GetTransationById([FromRoute] int transactionId)
        {
            var transaction = await _transactionsService.GetById(transactionId);
            if (transaction is null) return NotFound();
            return Ok(transaction);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionRequestDto payload)
        {
            return Ok(await _transactionsService.Create(payload));
        }

        [HttpPut("Update/{transactionId:int}")]
        public async Task<IActionResult> UpdateTransation(int transactionId, [FromBody] TransactionRequestDto payload)
        {
            var updated = await _transactionsService.Update(transactionId, payload);
            if (updated == null)
            {
                return NotFound();
            }
            return Ok(updated);
        }

        [HttpDelete("Delete/{transactionId:int}")]
        public async Task<IActionResult> DeleteTransation(int transactionId)
        {
            await _transactionsService.Delete(transactionId);
            return NoContent();
        }

        [AllowAnonymous]
        [HttpDelete("DeleteAllTransactions")]
        public async Task<IActionResult> DeleteAll()
        {
            await _transactionsService.DeleteAll();
            return NoContent();
        }
    }
}
