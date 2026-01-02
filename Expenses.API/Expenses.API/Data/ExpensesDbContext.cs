using Expenses.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Expenses.API.Data
{
    public class ExpensesDbContext(DbContextOptions<ExpensesDbContext> options) : DbContext(options)
    {

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; } 
    }
}
