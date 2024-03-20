using CanadaBIP_test.Server.Data;
using CanadaBIP_test.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CanadaBIP_test.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetManagerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public BudgetManagerController(
            ApplicationDbContext context,
            UserManager<AppUser> userManager
        )
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var appUser = await _userManager.GetUserAsync(User);
            var user = await _context.ProjectUser.FirstOrDefaultAsync(user => user.UserName == appUser.Email);

            List<BudgetManagerViewModel> result = _context.BudgetManager
                .Take(100)
                .Where(x => x.Sales_Area_Code == user.Sales_Area_Code)
                .ToList();

            return Ok(result);
        }

        [HttpGet("Products")]
        [Authorize]
        public async Task<IActionResult> GetProductsByAreaCode()
        {
            var appUser = await _userManager.GetUserAsync(User);
            //_userManager.GetClaimsAsync()
            var user = await _context.ProjectUser.FirstOrDefaultAsync(user => user.UserName == appUser.Email);

            List<BMProductModel> result = _context.BMProduct
                .Where(x => x.Sales_Area_Code == user.Sales_Area_Code)
                .ToList();

            return Ok(result);
        }

        [HttpPost]
        public async Task Create(BudgetManagerEditModel model)
        {
            var appUser = await _userManager.GetUserAsync(User);
            var user = await _context.ProjectUser.FirstOrDefaultAsync(user => user.UserName == appUser.Email);

            using var cmd = _context.Result.CreateDbCommand();
            cmd.CommandText = "[budget].[sp_Update_Budget_Manager]";
            cmd.CommandType = CommandType.StoredProcedure;

            if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

            cmd.Parameters.Add(new SqlParameter("@Int_Usr_ID", SqlDbType.NVarChar) { Value = user.ID });
            cmd.Parameters.Add(new SqlParameter("@step", SqlDbType.NVarChar) { Value = "INSERT" });
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = _context.BudgetManager.Count() });
            cmd.Parameters.Add(new SqlParameter("@BU", SqlDbType.NVarChar) { Value = user.BU });
            cmd.Parameters.Add(new SqlParameter("@Sales_Area_Code", SqlDbType.NVarChar) { Value = user.Sales_Area_Code });
            cmd.Parameters.Add(new SqlParameter("@Product", SqlDbType.NVarChar) { Value = model.Product });
            cmd.Parameters.Add(new SqlParameter("@Amount_Budget", SqlDbType.Decimal) { Value = model.Amount_Budget });

            SqlParameter outputParameter = new SqlParameter
            {
                ParameterName = "@Result",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(outputParameter);

            await cmd.ExecuteNonQueryAsync();
        }

        [HttpPut("{id}")]
        public async Task Update(int id, BudgetManagerEditModel model)
        {
            var appUser = await _userManager.GetUserAsync(User);
            var user = await _context.ProjectUser.FirstOrDefaultAsync(user => user.UserName == appUser.Email);

            using var cmd = _context.Result.CreateDbCommand();
            cmd.CommandText = "[budget].[sp_Update_Budget_Manager]";
            cmd.CommandType = CommandType.StoredProcedure;

            if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

            cmd.Parameters.Add(new SqlParameter("@Int_Usr_ID", SqlDbType.NVarChar) { Value = user.ID });
            cmd.Parameters.Add(new SqlParameter("@step", SqlDbType.NVarChar) { Value = "UPDATE" });
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = id });
            cmd.Parameters.Add(new SqlParameter("@BU", SqlDbType.NVarChar) { Value = model.BU });
            cmd.Parameters.Add(new SqlParameter("@Sales_Area_Code", SqlDbType.NVarChar) { Value = model.Sales_Area_Code });
            cmd.Parameters.Add(new SqlParameter("@Product", SqlDbType.NVarChar) { Value = model.Product });
            cmd.Parameters.Add(new SqlParameter("@Amount_Budget", SqlDbType.Decimal) { Value = model.Amount_Budget });

            SqlParameter outputParameter = new SqlParameter
            {
                ParameterName = "@Result",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(outputParameter);

            await cmd.ExecuteNonQueryAsync();
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var appUser = await _userManager.GetUserAsync(User);
            var user = await _context.ProjectUser.FirstOrDefaultAsync(user => user.UserName == appUser.Email);

            using var cmd = _context.Result.CreateDbCommand();
            cmd.CommandText = "[budget].[sp_Update_Budget_Manager]";
            cmd.CommandType = CommandType.StoredProcedure;

            if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

            cmd.Parameters.Add(new SqlParameter("@Int_Usr_ID", SqlDbType.NVarChar) { Value = user.ID });
            cmd.Parameters.Add(new SqlParameter("@step", SqlDbType.NVarChar) { Value = "DELETE" });
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = id });
            cmd.Parameters.Add(new SqlParameter("@BU", SqlDbType.NVarChar) { Value = "" });
            cmd.Parameters.Add(new SqlParameter("@Sales_Area_Code", SqlDbType.NVarChar) { Value = "" });
            cmd.Parameters.Add(new SqlParameter("@Product", SqlDbType.NVarChar) { Value = "" });
            cmd.Parameters.Add(new SqlParameter("@Amount_Budget", SqlDbType.Decimal) { Value = 0 });

            SqlParameter outputParameter = new SqlParameter
            {
                ParameterName = "@Result",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(outputParameter);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}
