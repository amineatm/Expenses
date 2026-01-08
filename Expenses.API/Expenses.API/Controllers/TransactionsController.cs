using Expenses.API.Data.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class TransactionsController(ITransactionsService transactionsService) : ControllerBase
    {
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllTransations()
        {
            return Ok(await transactionsService.GetAll());
        }

        [HttpGet("Details/{transactionId:int}")]
        public async Task<IActionResult> GetTransationById([FromRoute] int transactionId)
        {
            var transaction = await transactionsService.GetById(transactionId);
            if (transaction is null) return NotFound();
            return Ok(transaction);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionRequestDto payload)
        {
            return Ok(await transactionsService.Create(payload));
        }

        [HttpPut("Update/{transactionId:int}")]
        public async Task<IActionResult> UpdateTransation(int transactionId, [FromBody] TransactionRequestDto payload)
        {
            var updated = await transactionsService.Update(transactionId, payload);
            if (updated == null)
                return NotFound();

            return Ok(updated);

        }

        [HttpDelete("Delete/{transactionId:int}")]
        public async Task<IActionResult> DeleteTransation(int transactionId)
        {
            await transactionsService.Delete(transactionId);
            return NoContent();
        }

        //[HttpDelete("DeleteAll")]
        //public async Task<IActionResult> DeleteAll()
        //{
        //    //var transactions = await context.Transactions.ToListAsync();

        //    //context.Transactions.RemoveRange(transactions);
        //    //await context.SaveChangesAsync();

        //    await context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE Transactions");

        //    return NoContent();
        //}
    }
}
