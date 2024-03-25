using CanadaBIP_test.Server.Data;
using CanadaBIP_test.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace CanadaBIP_test.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetManagerDetailController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public BudgetManagerDetailController(
            ApplicationDbContext context,
            UserManager<AppUser> userManager
        )
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public List<BudgetManagerDetailViewModel> Get()
        {
            return _context.BudgetManagerDetail.ToList();
        }

        [HttpGet("ByManager/{id}")]
        public List<BudgetManagerDetailViewModel> Get(int id)
        {
            return _context.BudgetManagerDetail.Where(x => x.Budget_Manager_ID == id).ToList();
        }

        [HttpPost]
        [Authorize]
        public async Task Create(BudgetManagerDetailEditModel model)
        {
            var userId = _userManager.GetUserId(User);
            using var cmd = _context.Result.CreateDbCommand();
            cmd.CommandText = "[budget].[sp_Update_Budget_Manager_Detail]";
            cmd.CommandType = CommandType.StoredProcedure;

            if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

            cmd.Parameters.Add(new SqlParameter("@Int_Usr_ID", SqlDbType.NVarChar) { Value = userId });
            cmd.Parameters.Add(new SqlParameter("@step", SqlDbType.NVarChar) { Value = "INSERT" });
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.NVarChar) { Value = _context.BudgetManagerDetail.Count() });
            cmd.Parameters.Add(new SqlParameter("@Budget_Manager_ID", SqlDbType.Int) { Value = model.Budget_Manager_ID });
            cmd.Parameters.Add(new SqlParameter("@Date_Entry", SqlDbType.Date) { Value = model.Date_Entry });
            cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.NVarChar) { Value = model.Type });
            cmd.Parameters.Add(new SqlParameter("@Amount_Budget", SqlDbType.Decimal) { Value = model.Amount_Budget });
            cmd.Parameters.Add(new SqlParameter("@Comment", SqlDbType.NVarChar) { Value = model.Comment ?? "" });

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
        [Authorize]
        public async Task Update(int id, BudgetManagerDetailEditModel model)
        {
            var userId = _userManager.GetUserId(User);
            using var cmd = _context.Result.CreateDbCommand();
            cmd.CommandText = "[budget].[sp_Update_Budget_Manager_Detail]";
            cmd.CommandType = CommandType.StoredProcedure;

            if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

            cmd.Parameters.Add(new SqlParameter("@Int_Usr_ID", SqlDbType.NVarChar) { Value = userId });
            cmd.Parameters.Add(new SqlParameter("@step", SqlDbType.NVarChar) { Value = "UPDATE" });
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.NVarChar) { Value = id });
            cmd.Parameters.Add(new SqlParameter("@Budget_Manager_ID", SqlDbType.Int) { Value = model.Budget_Manager_ID });
            cmd.Parameters.Add(new SqlParameter("@Date_Entry", SqlDbType.Date) { Value = model.Date_Entry });
            cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.NVarChar) { Value = model.Type });
            cmd.Parameters.Add(new SqlParameter("@Amount_Budget", SqlDbType.Decimal) { Value = model.Amount_Budget });
            cmd.Parameters.Add(new SqlParameter("@Comment", SqlDbType.NVarChar) { Value = model.Comment });

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
        [Authorize]
        public async Task Delete(int id)
        {
            var userId = _userManager.GetUserId(User);
            using var cmd = _context.Result.CreateDbCommand();
            cmd.CommandText = "[budget].[sp_Update_Budget_Manager_Detail]";
            cmd.CommandType = CommandType.StoredProcedure;

            if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

            cmd.Parameters.Add(new SqlParameter("@Int_Usr_ID", SqlDbType.NVarChar) { Value = userId });
            cmd.Parameters.Add(new SqlParameter("@step", SqlDbType.NVarChar) { Value = "DELETE" });
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.NVarChar) { Value = id });
            cmd.Parameters.Add(new SqlParameter("@Budget_Manager_ID", SqlDbType.Int) { Value = (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Date_Entry", SqlDbType.Date) { Value = DateTime.Now });
            cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.NVarChar) { Value = (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Amount_Budget", SqlDbType.Decimal) { Value = (object)DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Comment", SqlDbType.NVarChar) { Value = (object)DBNull.Value });

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
