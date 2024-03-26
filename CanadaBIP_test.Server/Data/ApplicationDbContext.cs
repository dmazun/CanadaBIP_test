using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CanadaBIP_test.Server.Models;
using CanadaBIP_test.Server.Migrations;

namespace CanadaBIP_test.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) 
        : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<ProjectUser> ProjectUser { get; set; }
        public DbSet<BudgetManagerViewModel> BudgetManager { get; set; }
        public DbSet<BMProductModel> BMProduct { get; set; }
        public DbSet<BudgetManagerEditModel> BudgetManagerEdit { get; set; }
        public DbSet<BudgetManagerDetailViewModel> BudgetManagerDetail { get; set; }
        public DbSet<BudgetManagerDetailEditModel> BudgetManagerDetailEdit { get; set; }
        public DbSet<Result> Result { get; set; }
        public DbSet<BMRepresentativeViewModel> BMRepresentative { get; set; }
        public DbSet<BMRepProductModel> BMRepProduct { get; set; }
        public DbSet<BMRepNameModel> BMRepName { get; set; }
        public DbSet<BudgetRepresentativeModel> BRepresentative { get; set; }
        public DbSet<BudgetRepresentativeDetailModel> BRepDetails { get; set; }
        public DbSet<BudgetRepNameModel> BRepName { get; set; }
        public DbSet<BudgetRepNameSelectModel> BRepNameSelect { get; set; }
        public DbSet<BudgetRepProductModel> BRepProductSelect { get; set; }
        public DbSet<BudgetRepSummaryModel> BRepSummary { get; set; }
        public DbSet<BudgetRepInitiativeModel> BRepInitiativeSelect { get; set; }
        public DbSet<BudgetRepStatusModel> BRepStatusSelect { get; set; }
        public DbSet<BudgetRepEventTypeModel> BRepEventTypeSelect { get; set; }
        public DbSet<BudgetCustomerModel> BRepCustomerSelect { get; set; }
        public DbSet<BudgetAccountModel> BRepAccountSelect { get; set; }
        public DbSet<BudgetCustTypeModel> BRepCustTypes { get; set; }
    }
}
