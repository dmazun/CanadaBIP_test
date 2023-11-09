using CanadaBIP_test.Data;
using CanadaBIP_test.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CanadaBIP_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetManagerController : ControllerBase
    {
        private readonly BudgetDbContext _context;

        public BudgetManagerController(BudgetDbContext context)            
        {
            _context = context;
        }

        [HttpGet]
        public List<BudgetManagerViewModel> Get()
        {
            return _context.BudgetManager.ToList();
        }

        [HttpGet("{id}")]
        public BudgetManagerViewModel Get(int id)
        {
            return _context.BudgetManager.FirstOrDefault(x => x.ID == id);
        }
    }
}
