using CanadaBIP_test.Data;
using CanadaBIP_test.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CanadaBIP_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetManagerDetailController : ControllerBase
    {
        private readonly BudgetDbContext _context;

        public BudgetManagerDetailController(BudgetDbContext context)
        {
            _context = context;
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
        public async Task Create(BudgetManagerDetailEditModel model)
        {
            string userId = "Dima";
            using var cmd = _context.BudgetManagerDetailEditOut.CreateDbCommand();
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

        [HttpPut("{id}")]
        public async Task Update(int id, BudgetManagerDetailEditModel model)
        {
            string userId = "Dima";
            using var cmd = _context.BudgetManagerDetailEditOut.CreateDbCommand();
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
        public async Task Delete(int id)
        {
            string userId = "Dima";
            using var cmd = _context.BudgetManagerDetailEditOut.CreateDbCommand();
            cmd.CommandText = "[budget].[sp_Update_Budget_Manager_Detail]";
            cmd.CommandType = CommandType.StoredProcedure;

            if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

            cmd.Parameters.Add(new SqlParameter("@Int_Usr_ID", SqlDbType.NVarChar) { Value = userId });
            cmd.Parameters.Add(new SqlParameter("@step", SqlDbType.NVarChar) { Value = "DELETE" });
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.NVarChar) { Value = id });
            cmd.Parameters.Add(new SqlParameter("@Budget_Manager_ID", SqlDbType.Int) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@Date_Entry", SqlDbType.Date) { Value = DateTime.Now });
            cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.NVarChar) { Value = "" });
            cmd.Parameters.Add(new SqlParameter("@Amount_Budget", SqlDbType.Decimal) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@Comment", SqlDbType.NVarChar) { Value = "" });

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
