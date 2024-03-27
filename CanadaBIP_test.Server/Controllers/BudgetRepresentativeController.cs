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
    public class BudgetRepresentativeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public BudgetRepresentativeController(
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

            List<BudgetRepresentativeModel> result = _context.BRepresentative
                .Where(x => x.Manager_Sales_Area_Code == user.Sales_Area_Code)
                .OrderByDescending(x => x.ID)
                .ToList();

            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task Create(BudgetRepresentativeEditModel model)
        {
            var userId = _userManager.GetUserId(User);
            using var cmd = _context.Result.CreateDbCommand();
            cmd.CommandText = "[budget].[sp_Update_Budget_Representative]";
            cmd.CommandType = CommandType.StoredProcedure;

            if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

            cmd.Parameters.Add(new SqlParameter("@Int_Usr_ID", SqlDbType.NVarChar) { Value = userId });
            cmd.Parameters.Add(new SqlParameter("@step", SqlDbType.NVarChar) { Value = "INSERT" });
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = _context.BRepresentative.Count() });
            cmd.Parameters.Add(new SqlParameter("@Sales_Area_Code", SqlDbType.NVarChar) { Value = model.Sales_Area_Code });
            cmd.Parameters.Add(new SqlParameter("@Date_Entry", SqlDbType.Date) { Value = model.Date_Entry });
            cmd.Parameters.Add(new SqlParameter("@Event_Name", SqlDbType.NVarChar) { Value = model.Event_Name });
            cmd.Parameters.Add(new SqlParameter("@Product", SqlDbType.NVarChar) { Value = model.Product });
            cmd.Parameters.Add(new SqlParameter("@Initiative_ID", SqlDbType.Int) { Value = model.Initiative_ID });
            cmd.Parameters.Add(new SqlParameter("@Amount_Allocated", SqlDbType.Decimal) { Value = model.Amount_Allocated });

            cmd.Parameters.Add(new SqlParameter("@Note", SqlDbType.NVarChar)
            {
                Value = string.IsNullOrEmpty(model.Note) ? (object)DBNull.Value : model.Note
            });
            cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.NVarChar)
            {
                Value = string.IsNullOrEmpty(model.Type) ? (object)DBNull.Value : model.Type
            });
            cmd.Parameters.Add(new SqlParameter("@Event_Type", SqlDbType.NVarChar)
            {
                Value = string.IsNullOrEmpty(model.Event_Type) ? (object)DBNull.Value : model.Event_Type
            });
            cmd.Parameters.Add(new SqlParameter("@Attendance", SqlDbType.NVarChar)
            {
                Value = string.IsNullOrEmpty(model.Attendance) ? (object)DBNull.Value : model.Attendance
            });
            cmd.Parameters.Add(new SqlParameter("@Shared_Individual", SqlDbType.NVarChar)
            {
                Value = string.IsNullOrEmpty(model.Shared_Individual) ? (object)DBNull.Value : model.Shared_Individual
            });
            cmd.Parameters.Add(new SqlParameter("@Customer_ID", SqlDbType.NVarChar)
            {
                Value = string.IsNullOrEmpty(model.Customer_ID) ? (object)DBNull.Value : model.Customer_ID
            });
            cmd.Parameters.Add(new SqlParameter("@Customer_Count", SqlDbType.Int)
            {
                Value = model.Customer_Count == null ? (object)DBNull.Value : model.Customer_Count
            });
            cmd.Parameters.Add(new SqlParameter("@Customer_Type", SqlDbType.NVarChar)
            {
                Value = string.IsNullOrEmpty(model.Customer_Type) ? (object)DBNull.Value : model.Customer_Type
            });
            cmd.Parameters.Add(new SqlParameter("@FCPA_Veeva_ID", SqlDbType.NVarChar)
            {
                Value = string.IsNullOrEmpty(model.FCPA_Veeva_ID) ? (object)DBNull.Value : model.FCPA_Veeva_ID
            });
            cmd.Parameters.Add(new SqlParameter("@Account_ID", SqlDbType.NVarChar)
            {
                Value = string.IsNullOrEmpty(model.Account_ID) ? (object)DBNull.Value : model.Account_ID
            });
            cmd.Parameters.Add(new SqlParameter("@Tier", SqlDbType.Int)
            {
                Value = model.Tier == null ? (object)DBNull.Value : model.Tier
            });

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
        public async Task Update(int id, BudgetRepresentativeEditModel model)
        {
            var userId = _userManager.GetUserId(User);
            using var cmd = _context.Result.CreateDbCommand();
            cmd.CommandText = "[budget].[sp_Update_Budget_Representative]";
            cmd.CommandType = CommandType.StoredProcedure;

            if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

            cmd.Parameters.Add(new SqlParameter("@Int_Usr_ID", SqlDbType.NVarChar) { Value = userId });
            cmd.Parameters.Add(new SqlParameter("@step", SqlDbType.NVarChar) { Value = "UPDATE" });
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = id });
            cmd.Parameters.Add(new SqlParameter("@Sales_Area_Code", SqlDbType.NVarChar) { Value = model.Sales_Area_Code });
            cmd.Parameters.Add(new SqlParameter("@Date_Entry", SqlDbType.Date) { Value = model.Date_Entry });
            cmd.Parameters.Add(new SqlParameter("@Event_Name", SqlDbType.NVarChar) { Value = model.Event_Name });
            cmd.Parameters.Add(new SqlParameter("@Product", SqlDbType.NVarChar) { Value = model.Product });
            cmd.Parameters.Add(new SqlParameter("@Initiative_ID", SqlDbType.Int) { Value = model.Initiative_ID });
            cmd.Parameters.Add(new SqlParameter("@Amount_Allocated", SqlDbType.Decimal) { Value = model.Amount_Allocated });

            cmd.Parameters.Add(new SqlParameter("@Note", SqlDbType.NVarChar)
            {
                Value = string.IsNullOrEmpty(model.Note) ? (object)DBNull.Value : model.Note
            });
            cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.NVarChar)
            {
                Value = string.IsNullOrEmpty(model.Type) ? (object)DBNull.Value : model.Type
            });
            cmd.Parameters.Add(new SqlParameter("@Event_Type", SqlDbType.NVarChar)
            {
                Value = string.IsNullOrEmpty(model.Event_Type) ? (object)DBNull.Value : model.Event_Type
            });
            cmd.Parameters.Add(new SqlParameter("@Attendance", SqlDbType.NVarChar)
            {
                Value = string.IsNullOrEmpty(model.Attendance) ? (object)DBNull.Value : model.Attendance
            });
            cmd.Parameters.Add(new SqlParameter("@Shared_Individual", SqlDbType.NVarChar)
            {
                Value = string.IsNullOrEmpty(model.Shared_Individual) ? (object)DBNull.Value : model.Shared_Individual
            });
            cmd.Parameters.Add(new SqlParameter("@Customer_ID", SqlDbType.NVarChar)
            {
                Value = string.IsNullOrEmpty(model.Customer_ID) ? (object)DBNull.Value : model.Customer_ID
            });
            cmd.Parameters.Add(new SqlParameter("@Customer_Count", SqlDbType.Int)
            {
                Value = model.Customer_Count == null ? (object)DBNull.Value : model.Customer_Count
            });
            cmd.Parameters.Add(new SqlParameter("@Customer_Type", SqlDbType.NVarChar)
            {
                Value = string.IsNullOrEmpty(model.Customer_Type) ? (object)DBNull.Value : model.Customer_Type
            });
            cmd.Parameters.Add(new SqlParameter("@FCPA_Veeva_ID", SqlDbType.NVarChar)
            {
                Value = string.IsNullOrEmpty(model.FCPA_Veeva_ID) ? (object)DBNull.Value : model.FCPA_Veeva_ID
            });
            cmd.Parameters.Add(new SqlParameter("@Account_ID", SqlDbType.NVarChar)
            {
                Value = string.IsNullOrEmpty(model.Account_ID) ? (object)DBNull.Value : model.Account_ID
            });
            cmd.Parameters.Add(new SqlParameter("@Tier", SqlDbType.Int)
            {
                Value = model.Tier == null ? (object)DBNull.Value : model.Tier
            });

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
            cmd.CommandText = "[budget].[sp_Update_Budget_Representative]";
            cmd.CommandType = CommandType.StoredProcedure;

            if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

            cmd.Parameters.Add(new SqlParameter("@Int_Usr_ID", SqlDbType.NVarChar) { Value = userId });
            cmd.Parameters.Add(new SqlParameter("@step", SqlDbType.NVarChar) { Value = "DELETE" });
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = id });
            cmd.Parameters.Add(new SqlParameter("@Sales_Area_Code", SqlDbType.NVarChar) { Value = "" });
            cmd.Parameters.Add(new SqlParameter("@Date_Entry", SqlDbType.NVarChar) { Value = "" });
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
        
        [HttpGet("RepDetails/{repId}")]
        [Authorize]
        public IActionResult GetRepDetails(int repId)
        {
            List<BudgetRepresentativeDetailModel> result = _context.BRepDetails
                .Where(x => x.Budget_Representative_ID == repId)
                .ToList();

            return Ok(result);
        }

        [HttpPost("RepDetails")]
        [Authorize]
        public async Task Create(BudgetRepresentativeDetailEditModel model)
        {
            var userId = _userManager.GetUserId(User);
            using var cmd = _context.Result.CreateDbCommand();
            cmd.CommandText = "[budget].[sp_Update_Budget_Representative_Detail]";
            cmd.CommandType = CommandType.StoredProcedure;

            if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

            cmd.Parameters.Add(new SqlParameter("@Int_Usr_ID", SqlDbType.NVarChar) { Value = userId });
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@step", SqlDbType.NVarChar) { Value = "INSERT" });
            cmd.Parameters.Add(new SqlParameter("@Budget_Representative_ID", SqlDbType.NVarChar) { Value = model.Budget_Representative_ID });
            cmd.Parameters.Add(new SqlParameter("@Amount_Allocated", SqlDbType.NVarChar) { Value = model.Amount_Allocated });
            cmd.Parameters.Add(new SqlParameter("@Date_Entry", SqlDbType.DateTime) { Value = DateTime.Now });
            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar)
            {
                Value = string.IsNullOrEmpty(model.Name) ? DBNull.Value : model.Name
            });
            cmd.Parameters.Add(new SqlParameter("@Comment", SqlDbType.NVarChar)
            {
                Value = string.IsNullOrEmpty(model.Comment) ? DBNull.Value : model.Comment
            });

            SqlParameter outputParameter = new SqlParameter
            {
                ParameterName = "@Result",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(outputParameter);

            await cmd.ExecuteNonQueryAsync();
        }

        [HttpPut("RepDetails/{id}")]
        [Authorize]
        public async Task Update(int id, BudgetRepresentativeDetailEditModel model)
        {
            var userId = _userManager.GetUserId(User);
            using var cmd = _context.Result.CreateDbCommand();
            cmd.CommandText = "[budget].[sp_Update_Budget_Representative_Detail]";
            cmd.CommandType = CommandType.StoredProcedure;

            if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

            cmd.Parameters.Add(new SqlParameter("@Int_Usr_ID", SqlDbType.NVarChar) { Value = userId });
            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = id });
            cmd.Parameters.Add(new SqlParameter("@step", SqlDbType.NVarChar) { Value = "UPDATE" });
            cmd.Parameters.Add(new SqlParameter("@Budget_Representative_ID", SqlDbType.NVarChar) { Value = model.Budget_Representative_ID });
            cmd.Parameters.Add(new SqlParameter("@Amount_Allocated", SqlDbType.NVarChar) { Value = model.Amount_Allocated });
            cmd.Parameters.Add(new SqlParameter("@Date_Entry", SqlDbType.DateTime) { Value = model.Date_Entry });
            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar)
            {
                Value = string.IsNullOrEmpty(model.Name) ? DBNull.Value : model.Name
            });
            cmd.Parameters.Add(new SqlParameter("@Comment", SqlDbType.NVarChar)
            {
                Value = string.IsNullOrEmpty(model.Comment) ? DBNull.Value : model.Comment
            });

            SqlParameter outputParameter = new SqlParameter
            {
                ParameterName = "@Result",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(outputParameter);

            await cmd.ExecuteNonQueryAsync();
        }

        [HttpDelete("RepDetails/{id}")]
        [Authorize]
        public async Task DeleteDetail(int id)
        {
            var userId = _userManager.GetUserId(User);
            using var cmd = _context.Result.CreateDbCommand();
            cmd.CommandText = "[budget].[sp_Update_Budget_Representative_Detail]";
            cmd.CommandType = CommandType.StoredProcedure;

            if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();

            cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int) { Value = id });
            cmd.Parameters.Add(new SqlParameter("@Int_Usr_ID", SqlDbType.NVarChar) { Value = userId });
            cmd.Parameters.Add(new SqlParameter("@step", SqlDbType.NVarChar) { Value = "DELETE" });
            cmd.Parameters.Add(new SqlParameter("@Budget_Representative_ID", SqlDbType.Int) { Value = DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Date_Entry", SqlDbType.DateTime) { Value = DateTime.Now });
            cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar) { Value = DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Amount_Allocated", SqlDbType.Int) { Value = DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@Comment", SqlDbType.NVarChar) { Value = DBNull.Value });

            SqlParameter outputParameter = new SqlParameter
            {
                ParameterName = "@Result",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            cmd.Parameters.Add(outputParameter);

            await cmd.ExecuteNonQueryAsync();
        }

        [HttpGet("RepNames")]
        [Authorize]
        public async Task<IActionResult> GetRepNames()
        {
            var appUser = await _userManager.GetUserAsync(User);
            var user = await _context.ProjectUser.FirstOrDefaultAsync(user => user.UserName == appUser.Email);

            List<BudgetRepNameModel> result = _context.BRepName
                .Where(x => x.Parent_Sales_Area_Code == user.Sales_Area_Code)
                .ToList();

            return Ok(result);
        }

        [HttpGet("RepNamesSelect")]
        [Authorize]
        public async Task<IActionResult> GetRepNamesSelect()
        {
            var appUser = await _userManager.GetUserAsync(User);
            var user = await _context.ProjectUser.FirstOrDefaultAsync(user => user.UserName == appUser.Email);

            List<BudgetRepNameSelectModel> result = _context.BRepNameSelect
                .Where(x => x.Parent_Sales_Area_Code == user.Sales_Area_Code)
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

        [HttpGet("RepCustTypes")]
        public IActionResult GetRepCustTypes()
        {
            List<BudgetCustTypeModel> result = _context.BRepCustTypes
                .Where(item => item.Status == 1)
                .ToList();

            return Ok(result);
        }

        [HttpGet("RepCustomers")]
        public IActionResult GetRepCustomerSelect
            (
                [FromQuery] int skip = 0,
                [FromQuery] int take = 25,
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

        [HttpGet("RepAccounts")]
        public IActionResult GetRepAccountSelect
            (
                [FromQuery] int skip = 0,
                [FromQuery] int take = 25,
                [FromQuery] string? filter = ""
            )
        {
            var items = _context.BRepAccountSelect.AsQueryable();
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
        [Authorize]
        public async Task<IActionResult> GetRepSummary()
        {
            var appUser = await _userManager.GetUserAsync(User);
            var user = await _context.ProjectUser.FirstOrDefaultAsync(user => user.UserName == appUser.Email);

            List<BudgetRepSummaryModel> result = _context.BRepSummary
                .Where(x => x.Manager_Sales_Area_Code == user.Sales_Area_Code)
                .ToList();

            return Ok(result);
        }

    }
}
