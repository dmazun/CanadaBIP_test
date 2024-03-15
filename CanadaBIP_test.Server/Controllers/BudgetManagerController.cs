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
        private readonly SignInManager<AppUser> _signInManager;

        public BudgetManagerController(
            ApplicationDbContext context,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager
        )
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;

           // _user = User.Identity.GetUserId() ?? throw new Exception();

            //_user = new User()
            //{
            //    ID = 3,
            //    UserName = "Jacinthe.Lamarche@pfizer.com",
            //    RoleName = "District",
            //    BU = "PBGO",
            //    BU_NAME = "PBG Oncology",
            //    Sales_Area_Type = "District",
            //    Sales_Area_Code = "CA_40005",
            //    Sales_Area_Name = "O_RBM_QUEBEC",
            //};
        }

      /*  [HttpGet("Test")]
        [Authorize]
        public async Task<IActionResult> GetTest()
        {            
            var appUser = await _userManager.GetUserAsync(User);
            var user = await _context.CurrentUser.FirstOrDefaultAsync(user => user.Email_Address == appUser.Email);
            
            return Ok(user.Sales_Area_Code);
        }*/

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var appUser = await _userManager.GetUserAsync(User);
            var user = await _context.CurrentUser.FirstOrDefaultAsync(user => user.Email_Address == appUser.Email);

            List<BudgetManagerViewModel> result = _context.BudgetManager
                .Take(100)
                .Where(x => x.Sales_Area_Code == user.Sales_Area_Code)
                .ToList();

            return Ok(result);
        }
    }
}
