using Microsoft.AspNetCore.Mvc;
using Models;
using System.Threading.Tasks;
using ToDo.Data;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentsController : ControllerBase
    {
        private readonly ToDoContext _context;
        public AssignmentsController(ToDoContext context) => _context = context;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Assignment assignment)
        {
            await _context.AddAsync(assignment);
            await _context.SaveChangesAsync();
            return Ok(assignment);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Assignment assignment)
        {
            Assignment dbAssignment = await _context.Assignments.FindAsync(assignment.Id);
            if (dbAssignment == default(Assignment)) return NotFound();


            _context.Entry(dbAssignment).CurrentValues.SetValues(assignment);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
