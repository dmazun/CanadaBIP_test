using CanadaBIP_test.Models;
using Microsoft.EntityFrameworkCore;
// using System.Data.Objects;

namespace CanadaBIP_test.Data
{
    public class BudgetDbContext : DbContext
    {
        public BudgetDbContext(DbContextOptions<BudgetDbContext> options) : base(options) { }
        public DbSet<BudgetManagerViewModel> BudgetManager { get; set; }
        public DbSet<BudgetManagerDetailViewModel> BudgetManagerDetail { get; set; }
        public DbSet<BudgetManagerDetailEditModel> BudgetManagerDetailEdit { get; set; }
        public DbSet<BudgetManagerDetailEditOut> BudgetManagerDetailEditOut { get; set; }
    }
}
