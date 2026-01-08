using Expenses.API.Data.Services.Authentication;
using Expenses.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Expenses.API.Data.Services.Transactions
{
    public class TransactionsService(ExpensesDbContext context, ICurrentUserService currentUserService) : ITransactionsService
    {
        private int CurrentUserId => currentUserService.UserId;
        public async Task<Transaction> Create(TransactionRequestDto transactionDto)
        {
            var transaction = new Transaction()
            {
                UserId = CurrentUserId,
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
            var userId = currentUserService.UserId;
            return await context.Transactions
                .Where(t => t.UserId == userId)
                .ToListAsync();
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

        public async Task DeleteAll()
        {
            await context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE Transactions");
        }
    }
}
