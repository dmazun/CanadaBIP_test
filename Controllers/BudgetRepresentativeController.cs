using CanadaBIP_test.Data;
using CanadaBIP_test.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Dynamic;

namespace CanadaBIP_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetRepresentativeController : ControllerBase
    {
        private readonly BudgetDbContext _context;
        private readonly User _user;        
        public BudgetRepresentativeController(BudgetDbContext context)
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

        [HttpGet("RepNames")]
        public IActionResult GetRepNames()
        {
            List<BudgetRepNameModel> result = _context.BRepName
                .Where(x => x.Parent_Sales_Area_Code == _user.Sales_Area_Code)
                .ToList();

            return Ok(result);
        }

        [HttpGet("RepNamesSelect")]
        public IActionResult GetRepNamesSelect()
        {
            List<BudgetRepNameSelectModel> result = _context.BRepNameSelect
                .Where(x => x.Parent_Sales_Area_Code == _user.Sales_Area_Code)
                .ToList();

            return Ok(result);
        }

        [HttpGet("RepProducts")]
        public IActionResult GetRepProductsSelect()
        {
            List<BudgetRepProductModel> result = _context.BRepProductSelect.ToList();

            return Ok(result);
        }

        [HttpGet("RepInitiatives")]
        public IActionResult GetRepInitiativeSelect()
        {
            List<BudgetRepInitiativeModel> result = _context.BRepInitiativeSelect.ToList();

            return Ok(result);
        }


        [HttpGet("RepStatuses")]
        public IActionResult GetRepStatusSelect()
        {
            List<BudgetRepStatusModel> result = _context.BRepStatusSelect.ToList();

            return Ok(result);
        }

        [HttpGet("RepEventTypes")]
        public IActionResult GetRepEventTypeSelect()
        {
            List<BudgetRepEventTypeModel> result = _context.BRepEventTypeSelect.ToList();

            return Ok(result);
        }

        [HttpGet("RepCustomers")]
        public IActionResult GetRepCustomerSelect
            (   
                [FromQuery] int skip = 0, 
                [FromQuery] int take = 100,
                [FromQuery] string? filter = ""
            )
        {
            var items = _context.BRepCustomerSelect.AsQueryable();
            string value = "";
            
            if (filter?.Length > 0)
            {
                string innerArray = filter.Trim('[', ']');
                string[] elements = innerArray.Split(',');

                if (elements.Length == 2)
                {
                    value = elements[1].Trim('"');
                }

                if (elements.Length == 3)
                {
                    value = elements[2].Trim('"');
                }
            }

            var result = items
                .OrderBy(item => item.ID)
                .Where(item => item.Name.Contains(value))
                .Skip(skip)
                .Take(take)
                .ToList(); 

            return Ok(result);
        }
        
        [HttpGet("Summary")]
        public IActionResult GetRepSummary()
        {
            List<BudgetRepSummaryModel> result = _context.BRepSummary
                .Where(x => x.Manager_Sales_Area_Code == _user.Sales_Area_Code)
                .ToList();

            return Ok(result);
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<BudgetRepresentativeModel> result = _context.BRepresentative
                .Where(x => x.Manager_Sales_Area_Code == _user.Sales_Area_Code)
                .ToList();

            return Ok(result);
        }

        [HttpPost]
        public async Task Create(BudgetRepresentativeEditModel model)
        {
            using var cmd = _context.BudgetResult.CreateDbCommand();
            cmd.CommandText = "[budget].[sp_Update_Budget_Representative]";
            cmd.CommandType = CommandType.StoredProcedure;

            if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

            cmd.Parameters.Add(new SqlParameter("@Int_Usr_ID", SqlDbType.NVarChar) { Value = _user.ID });
            cmd.Parameters.Add(new SqlParameter("@step", SqlDbType.NVarChar) { Value = "INSERT" });
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = _context.BRepresentative.Count() });
            cmd.Parameters.Add(new SqlParameter("@BU", SqlDbType.NVarChar) { Value = _user.BU });
            cmd.Parameters.Add(new SqlParameter("@Sales_Area_Code", SqlDbType.NVarChar) { Value = _user.Sales_Area_Code });
            cmd.Parameters.Add(new SqlParameter("@Date_Entry", SqlDbType.Date) { Value = model.Date_Entry });
            cmd.Parameters.Add(new SqlParameter("@Event_Name", SqlDbType.NVarChar) { Value = model.Event_Name });
            cmd.Parameters.Add(new SqlParameter("@Product", SqlDbType.NVarChar) { Value = model.Product });
            cmd.Parameters.Add(new SqlParameter("@Initiative_ID", SqlDbType.Int) { Value = model.Initiative_ID });
            cmd.Parameters.Add(new SqlParameter("@Note", SqlDbType.NVarChar) { Value = model.Note });
            cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.NVarChar) { Value = model.Type });
            cmd.Parameters.Add(new SqlParameter("@Event_Type", SqlDbType.NVarChar) { Value = model.Event_Type });
            cmd.Parameters.Add(new SqlParameter("@Attendance", SqlDbType.NVarChar) { Value = model.Attendance });
            cmd.Parameters.Add(new SqlParameter("@Shared_Individual", SqlDbType.NVarChar) { Value = model.Shared_Individual });
            cmd.Parameters.Add(new SqlParameter("@Amount_Allocated", SqlDbType.Decimal) { Value = model.Amount_Allocated });
            cmd.Parameters.Add(new SqlParameter("@Customer_ID", SqlDbType.NVarChar) { Value = model.Customer_ID });
            cmd.Parameters.Add(new SqlParameter("@Customer_Count", SqlDbType.Int) { Value = model.Customer_Count });
            cmd.Parameters.Add(new SqlParameter("@Customer_Type", SqlDbType.NVarChar) { Value = model.Customer_Type });
            cmd.Parameters.Add(new SqlParameter("@FCPA_Veeva_ID", SqlDbType.NVarChar) { Value = model.FCPA_Veeva_ID });
            cmd.Parameters.Add(new SqlParameter("@Account_ID", SqlDbType.NVarChar) { Value = model.Account_ID });
            cmd.Parameters.Add(new SqlParameter("@Tier", SqlDbType.Int) { Value = model.Tier });

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
        public async Task Update(int id, BudgetRepresentativeEditModel model)
        {
            using var cmd = _context.BudgetResult.CreateDbCommand();
            cmd.CommandText = "[budget].[sp_Update_Budget_Representative]";
            cmd.CommandType = CommandType.StoredProcedure;

            if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

            cmd.Parameters.Add(new SqlParameter("@Int_Usr_ID", SqlDbType.NVarChar) { Value = _user.ID });
            cmd.Parameters.Add(new SqlParameter("@step", SqlDbType.NVarChar) { Value = "UPDATE" });
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = id });
            cmd.Parameters.Add(new SqlParameter("@BU", SqlDbType.NVarChar) { Value = _user.BU });
            cmd.Parameters.Add(new SqlParameter("@Sales_Area_Code", SqlDbType.NVarChar) { Value = _user.Sales_Area_Code });
            cmd.Parameters.Add(new SqlParameter("@Date_Entry", SqlDbType.Date) { Value = model.Date_Entry });
            cmd.Parameters.Add(new SqlParameter("@Event_Name", SqlDbType.NVarChar) { Value = model.Event_Name });
            cmd.Parameters.Add(new SqlParameter("@Product", SqlDbType.NVarChar) { Value = model.Product });
            cmd.Parameters.Add(new SqlParameter("@Initiative_ID", SqlDbType.Int) { Value = model.Initiative_ID });
            cmd.Parameters.Add(new SqlParameter("@Note", SqlDbType.NVarChar) { Value = model.Note });
            cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.NVarChar) { Value = model.Type });
            cmd.Parameters.Add(new SqlParameter("@Event_Type", SqlDbType.NVarChar) { Value = model.Event_Type });
            cmd.Parameters.Add(new SqlParameter("@Attendance", SqlDbType.NVarChar) { Value = model.Attendance });
            cmd.Parameters.Add(new SqlParameter("@Shared_Individual", SqlDbType.NVarChar) { Value = model.Shared_Individual });
            cmd.Parameters.Add(new SqlParameter("@Amount_Allocated", SqlDbType.Decimal) { Value = model.Amount_Allocated });
            cmd.Parameters.Add(new SqlParameter("@Customer_ID", SqlDbType.NVarChar) { Value = model.Customer_ID });
            cmd.Parameters.Add(new SqlParameter("@Customer_Count", SqlDbType.Int) { Value = model.Customer_Count });
            cmd.Parameters.Add(new SqlParameter("@Customer_Type", SqlDbType.NVarChar) { Value = model.Customer_Type });
            cmd.Parameters.Add(new SqlParameter("@FCPA_Veeva_ID", SqlDbType.NVarChar) { Value = model.FCPA_Veeva_ID });
            cmd.Parameters.Add(new SqlParameter("@Account_ID", SqlDbType.NVarChar) { Value = model.Account_ID });
            cmd.Parameters.Add(new SqlParameter("@Tier", SqlDbType.Int) { Value = model.Tier });

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
            cmd.CommandText = "[budget].[sp_Update_Budget_Representative]";
            cmd.CommandType = CommandType.StoredProcedure;

            if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

            cmd.Parameters.Add(new SqlParameter("@Int_Usr_ID", SqlDbType.NVarChar) { Value = _user.ID });
            cmd.Parameters.Add(new SqlParameter("@step", SqlDbType.NVarChar) { Value = "DELETE" });
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = id });
            cmd.Parameters.Add(new SqlParameter("@BU", SqlDbType.NVarChar) { Value = "" });
            cmd.Parameters.Add(new SqlParameter("@Sales_Area_Code", SqlDbType.NVarChar) { Value = "" });
            cmd.Parameters.Add(new SqlParameter("@Date_Entry", SqlDbType.Date) { Value = "" });
            cmd.Parameters.Add(new SqlParameter("@Event_Name", SqlDbType.NVarChar) { Value = "" });
            cmd.Parameters.Add(new SqlParameter("@Product", SqlDbType.NVarChar) { Value = "" });
            cmd.Parameters.Add(new SqlParameter("@Initiative_ID", SqlDbType.Int) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@Note", SqlDbType.NVarChar) { Value = "" });
            cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.NVarChar) { Value = "" });
            cmd.Parameters.Add(new SqlParameter("@Event_Type", SqlDbType.NVarChar) { Value = "" });
            cmd.Parameters.Add(new SqlParameter("@Attendance", SqlDbType.NVarChar) { Value = "" });
            cmd.Parameters.Add(new SqlParameter("@Shared_Individual", SqlDbType.NVarChar) { Value = "" });
            cmd.Parameters.Add(new SqlParameter("@Amount_Allocated", SqlDbType.Decimal) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@Customer_ID", SqlDbType.NVarChar) { Value = "" });
            cmd.Parameters.Add(new SqlParameter("@Customer_Count", SqlDbType.Int) { Value = 0 });
            cmd.Parameters.Add(new SqlParameter("@Customer_Type", SqlDbType.NVarChar) { Value = "" });
            cmd.Parameters.Add(new SqlParameter("@FCPA_Veeva_ID", SqlDbType.NVarChar) { Value = "" });
            cmd.Parameters.Add(new SqlParameter("@Account_ID", SqlDbType.NVarChar) { Value = "" });
            cmd.Parameters.Add(new SqlParameter("@Tier", SqlDbType.Int) { Value = 0 });

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
