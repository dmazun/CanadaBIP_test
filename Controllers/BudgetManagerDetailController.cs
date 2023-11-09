using CanadaBIP_test.Data;
using CanadaBIP_test.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var data = _context.BudgetManagerDetail;
            return _context.BudgetManagerDetail.Where(x => x.Budget_Manager_ID == id).ToList();
        }
    }
}
