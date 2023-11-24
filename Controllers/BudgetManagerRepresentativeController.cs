using CanadaBIP_test.Data;
using CanadaBIP_test.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CanadaBIP_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetManagerRepresentativeController : ControllerBase
    {
        private readonly BudgetDbContext _context;
        private readonly User _user;
        public BudgetManagerRepresentativeController(BudgetDbContext context) 
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
            List<BMRepresentativeViewModel> result = _context.BMRepresentative
                .Where(x => x.Manager_Sales_Area_Code == _user.Sales_Area_Code)
                .OrderByDescending(x => x.Date_Entry)
                .ToList();

            return Ok(result);
        }
    }
}
