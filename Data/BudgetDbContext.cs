using CanadaBIP_test.Models;
using Microsoft.EntityFrameworkCore;

namespace CanadaBIP_test.Data
{
    public class BudgetDbContext : DbContext
    {
        public BudgetDbContext(DbContextOptions<BudgetDbContext> options) : base(options) { }
        public DbSet<BudgetManagerViewModel> BudgetManager { get; set; }
        public DbSet<BudgetManagerDetailViewModel> BudgetManagerDetail { get; set; }
    }
}
