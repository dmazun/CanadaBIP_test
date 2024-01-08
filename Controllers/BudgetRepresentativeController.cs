using CanadaBIP_test.Data;
using CanadaBIP_test.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("Summary")]
        public IActionResult GetRepSummary()
        {
            List<BudgetRepSummaryModel> result = _context.BRepSummary
                .Where(x => x.Manager_Sales_Area_Code == _user.Sales_Area_Code)
                .ToList();

            return Ok(result);
        }
    }
}
