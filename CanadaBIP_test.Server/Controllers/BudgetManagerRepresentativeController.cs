using CanadaBIP_test.Server.Data;
using CanadaBIP_test.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CanadaBIP_test.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetManagerRepresentativeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public BudgetManagerRepresentativeController(
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

            List<BMRepresentativeViewModel> result = _context.BMRepresentative
                .Where(x => x.Manager_Sales_Area_Code == user.Sales_Area_Code)
                .OrderByDescending(x => x.Date_Entry)
                .ToList();

            return Ok(result);
        }

        [HttpGet("Products")]
        [Authorize]
        public async Task<IActionResult> GetProductsByAreaCode()
        {
            var appUser = await _userManager.GetUserAsync(User);
            var user = await _context.ProjectUser.FirstOrDefaultAsync(user => user.UserName == appUser.Email);

            List<BMRepProductModel> result = _context.BMRepProduct
                .Where(x => x.Sales_Area_Code == user.Sales_Area_Code)
                .ToList();

            return Ok(result);
        }

        [HttpGet("RepNames")]
        [Authorize]
        public async Task<IActionResult> GetRepNames()
        {
            var appUser = await _userManager.GetUserAsync(User);
            var user = await _context.ProjectUser.FirstOrDefaultAsync(user => user.UserName == appUser.Email);

            List<BMRepNameModel> result = _context.BMRepName
                .Where(x => x.Parent_Sales_Area_Code == user.Sales_Area_Code)
                .ToList();

            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(BMRepresentativeEditModel model)
        {
            var appUser = await _userManager.GetUserAsync(User);
            var user = await _context.ProjectUser.FirstOrDefaultAsync(user => user.UserName == appUser.Email);

            using var cmd = _context.Result.CreateDbCommand();
            cmd.CommandText = "[budget].[sp_Update_Budget_Manager_Representative]";
            cmd.CommandType = CommandType.StoredProcedure;

            if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

            cmd.Parameters.Add(new SqlParameter("@Int_Usr_ID", SqlDbType.NVarChar) { Value = user.ID });
            cmd.Parameters.Add(new SqlParameter("@step", SqlDbType.NVarChar) { Value = "INSERT" });
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = _context.BMRepresentative.Count() });
            cmd.Parameters.Add(new SqlParameter("@BU", SqlDbType.NVarChar) { Value = user.BU });
            cmd.Parameters.Add(new SqlParameter("@Manager_Sales_Area_Code", SqlDbType.NVarChar) { Value = user.Sales_Area_Code });
            cmd.Parameters.Add(new SqlParameter("@Sales_Area_Code", SqlDbType.NVarChar) { Value = model.Sales_Area_Code });
            cmd.Parameters.Add(new SqlParameter("@Date_Entry", SqlDbType.NVarChar) { Value = model.Date_Entry });
            cmd.Parameters.Add(new SqlParameter("@Product", SqlDbType.NVarChar) { Value = model.Product });
            cmd.Parameters.Add(new SqlParameter("@Amount_Allocated", SqlDbType.Decimal) { Value = model.Amount_Allocated });

            SqlParameter outputParameter = new SqlParameter
            {
                ParameterName = "@Result",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(outputParameter);

            await cmd.ExecuteNonQueryAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, BMRepresentativeEditModel model)
        {
            var appUser = await _userManager.GetUserAsync(User);
            var user = await _context.ProjectUser.FirstOrDefaultAsync(user => user.UserName == appUser.Email);

            using var cmd = _context.Result.CreateDbCommand();
            cmd.CommandText = "[budget].[sp_Update_Budget_Manager_Representative]";
            cmd.CommandType = CommandType.StoredProcedure;

            if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

            cmd.Parameters.Add(new SqlParameter("@Int_Usr_ID", SqlDbType.NVarChar) { Value = user.ID });
            cmd.Parameters.Add(new SqlParameter("@step", SqlDbType.NVarChar) { Value = "UPDATE" });
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = id });
            cmd.Parameters.Add(new SqlParameter("@BU", SqlDbType.NVarChar) { Value = user.BU });
            cmd.Parameters.Add(new SqlParameter("@Manager_Sales_Area_Code", SqlDbType.NVarChar) { Value = user.Sales_Area_Code });
            cmd.Parameters.Add(new SqlParameter("@Sales_Area_Code", SqlDbType.NVarChar) { Value = model.Sales_Area_Code });
            cmd.Parameters.Add(new SqlParameter("@Date_Entry", SqlDbType.NVarChar) { Value = model.Date_Entry });
            cmd.Parameters.Add(new SqlParameter("@Product", SqlDbType.NVarChar) { Value = model.Product });
            cmd.Parameters.Add(new SqlParameter("@Amount_Allocated", SqlDbType.Decimal) { Value = model.Amount_Allocated });

            SqlParameter outputParameter = new SqlParameter
            {
                ParameterName = "@Result",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(outputParameter);

            await cmd.ExecuteNonQueryAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = _userManager.GetUserId(User);
            using var cmd = _context.Result.CreateDbCommand();
            cmd.CommandText = "[budget].[sp_Update_Budget_Manager_Representative]";
            cmd.CommandType = CommandType.StoredProcedure;

            if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

            cmd.Parameters.Add(new SqlParameter("@Int_Usr_ID", SqlDbType.NVarChar) { Value = userId });
            cmd.Parameters.Add(new SqlParameter("@step", SqlDbType.NVarChar) { Value = "DELETE" });
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = id });
            cmd.Parameters.Add(new SqlParameter("@BU", SqlDbType.NVarChar) { Value = DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Manager_Sales_Area_Code", SqlDbType.NVarChar) { Value = DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Sales_Area_Code", SqlDbType.NVarChar) { Value = DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Date_Entry", SqlDbType.NVarChar) { Value = DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Product", SqlDbType.NVarChar) { Value = DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Amount_Allocated", SqlDbType.Decimal) { Value = DBNull.Value });

            SqlParameter outputParameter = new SqlParameter
            {
                ParameterName = "@Result",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(outputParameter);

            await cmd.ExecuteNonQueryAsync();

            return Ok();
        }
    }
}
