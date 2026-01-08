using Expenses.API.Models;

namespace Expenses.API.Data.Services.Transactions
{
    public interface ITransactionsService
    {
        Task<List<Transaction>> GetAll();
        Task<Transaction> GetById(int TransactionId);
        Task<Transaction> Create(TransactionRequestDto payload);
        Task<Transaction> Update(int TransactionId, TransactionRequestDto payload);
        Task Delete(int TransactionId);

        Task DeleteAll();

    }
}