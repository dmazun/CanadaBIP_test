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
    public class BudgetManagerController : ControllerBase
    {
        private readonly BudgetDbContext _context;
        private readonly User _user;

        public BudgetManagerController(BudgetDbContext context)
        {
            _context = context;

            _user = new User()
            {
                ID = 3,
                UserName = "Jacinthe.Lamarche@pfizer.com",
                RoleName = "District",
                BU = "PBGO",
                BU_NAME = "PBG Oncology",
                Sales_Area_Type = "District",
                Sales_Area_Code = "CA_40005",
                Sales_Area_Name = "O_RBM_QUEBEC",
            };
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<BudgetManagerViewModel> result = _context.BudgetManager
                .Where(x => x.Sales_Area_Code == _user.Sales_Area_Code)
                .ToList();

            return Ok(result);
        }

        [HttpGet("Products")]
        public IActionResult GetProductsByAreaCode()
        {
            List<BMProductModel> result = _context.BMProduct
                .Where(x => x.Sales_Area_Code == _user.Sales_Area_Code)
                .ToList();

            return Ok(result);
        }

        [HttpPost]
        public async Task Create(BudgetManagerEditModel model)
        {
            using var cmd = _context.BudgetResult.CreateDbCommand();
            cmd.CommandText = "[budget].[sp_Update_Budget_Manager]";
            cmd.CommandType = CommandType.StoredProcedure;

            if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

            cmd.Parameters.Add(new SqlParameter("@Int_Usr_ID", SqlDbType.NVarChar) { Value = _user.ID });
            cmd.Parameters.Add(new SqlParameter("@step", SqlDbType.NVarChar) { Value = "INSERT" });
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = _context.BudgetManager.Count() });
            cmd.Parameters.Add(new SqlParameter("@BU", SqlDbType.NVarChar) { Value = _user.BU });
            cmd.Parameters.Add(new SqlParameter("@Sales_Area_Code", SqlDbType.NVarChar) { Value = _user.Sales_Area_Code });
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
            using var cmd = _context.BudgetResult.CreateDbCommand();
            cmd.CommandText = "[budget].[sp_Update_Budget_Manager]";
            cmd.CommandType = CommandType.StoredProcedure;

            if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

            cmd.Parameters.Add(new SqlParameter("@Int_Usr_ID", SqlDbType.NVarChar) { Value = _user.ID });
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
            using var cmd = _context.BudgetResult.CreateDbCommand();
            cmd.CommandText = "[budget].[sp_Update_Budget_Manager]";
            cmd.CommandType = CommandType.StoredProcedure;

            if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

            cmd.Parameters.Add(new SqlParameter("@Int_Usr_ID", SqlDbType.NVarChar) { Value = _user.ID });
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
