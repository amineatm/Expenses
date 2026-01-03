using Expenses.API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Expenses.API.Data.Services
{
    public class TransactionsService(ExpensesDbContext context) : ITransactionsService
    {
        public async Task<Transaction> Create(TransactionRequestDto transactionDto)
        {
            var transaction = new Transaction()
            {
                Amount = transactionDto.Amount,
                Category = transactionDto.Category,
                Type = transactionDto.Type,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            await context.Transactions.AddAsync(transaction);
            await context.SaveChangesAsync();

            return transaction;
        }

        public async Task Delete(int TransactionId)
        {
            Transaction transaction = await context.Transactions.FindAsync(TransactionId);
            if (transaction is not null)
            {
                context.Transactions.Remove(transaction);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Transaction>> GetAll()
        {
            return await context.Transactions.ToListAsync();
        }

        public async Task<Transaction> GetById(int TransactionId)
        {
            return await context.Transactions.FirstAsync(x => x.Id == TransactionId);
        }

        public async Task<Transaction> Update(int transactionId, TransactionRequestDto dto)
        {
            var transaction = await context.Transactions.FindAsync(transactionId);
            if (transaction == null)
                return null;

            transaction.Category = dto.Category;
            transaction.Amount = dto.Amount;
            transaction.Type = dto.Type;
            transaction.UpdatedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();

            return transaction;
        }
    }
}
