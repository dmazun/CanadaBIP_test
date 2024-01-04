using CanadaBIP_test.Models;
using Microsoft.EntityFrameworkCore;

namespace CanadaBIP_test.Data
{
    public class BudgetDbContext : DbContext
    {
        public BudgetDbContext(DbContextOptions<BudgetDbContext> options) : base(options) { }
        public DbSet<BudgetManagerViewModel> BudgetManager { get; set; }
        public DbSet<BMProductModel> BMProduct { get; set; }
        public DbSet<BudgetManagerEditModel> BudgetManagerEdit { get; set; }
        public DbSet<BudgetManagerDetailViewModel> BudgetManagerDetail { get; set; }
        public DbSet<BudgetManagerDetailEditModel> BudgetManagerDetailEdit { get; set; }
        public DbSet<BudgetResult> BudgetResult { get; set; }
        public DbSet<BMRepresentativeViewModel> BMRepresentative { get; set; }
        public DbSet<BMRepProductModel> BMRepProduct { get; set; }
        public DbSet<BMRepNameModel> BMRepName { get; set; }
        public DbSet<BudgetRepNameModel> BRepName { get; set; }
    }
}
