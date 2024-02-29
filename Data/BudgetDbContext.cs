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
        public DbSet<BudgetRepNameSelectModel> BRepNameSelect { get; set; }
        public DbSet<BudgetRepProductModel> BRepProductSelect { get; set; }
        public DbSet<BudgetRepSummaryModel> BRepSummary { get; set; }
        public DbSet<BudgetRepresentativeModel> BRepresentative { get; set; }
        public DbSet<BudgetRepInitiativeModel> BRepInitiativeSelect { get; set; }
        public DbSet<BudgetRepStatusModel> BRepStatusSelect { get; set; }
        public DbSet<BudgetRepEventTypeModel> BRepEventTypeSelect { get; set; }
        public DbSet<BudgetCustomerModel> BRepCustomerSelect { get; set; }
        public DbSet<BudgetAccountModel> BRepAccountSelect { get; set; }
        public DbSet<BudgetCustTypeModel> BRepCustTypes { get; set; }
        public DbSet<BudgetRepresentativeDetailModel> BRepDetails { get; set; }
    }
}
