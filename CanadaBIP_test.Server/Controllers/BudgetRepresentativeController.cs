﻿using CanadaBIP_test.Server.Data;
using CanadaBIP_test.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

    }
}
