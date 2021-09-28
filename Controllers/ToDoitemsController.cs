using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoCoreAPP.Models;

namespace ToDoCoreAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoitemsController : ControllerBase
    {
        private readonly ToDoContext _context;

        public ToDoitemsController(ToDoContext context)
        {
            _context = context;
        }

        // GET: api/ToDoitems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoitem>>> GetToDoitems()
        {
            return await _context.ToDoitems.ToListAsync();
        }

        // GET: api/ToDoitems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoitem>> GetToDoitem(uint id)
        {
            var toDoitem = await _context.ToDoitems.FindAsync(id);

            if (toDoitem == null)
            {
                return NotFound();
            }

            return toDoitem;
        }

        // PUT: api/ToDoitems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDoitem(uint id, ToDoitem toDoitem)
        {
            if (id != toDoitem.id)
            {
                return BadRequest();
            }

            _context.Entry(toDoitem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoitemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ToDoitems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ToDoitem>> PostToDoitem(ToDoitem toDoitem)
        {
            _context.ToDoitems.Add(toDoitem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetToDoitem), new { id = toDoitem.id }, toDoitem);
        }

        // DELETE: api/ToDoitems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoitem(uint id)
        {
            var toDoitem = await _context.ToDoitems.FindAsync(id);
            if (toDoitem == null)
            {
                return NotFound();
            }

            _context.ToDoitems.Remove(toDoitem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ToDoitemExists(uint id)
        {
            return _context.ToDoitems.Any(e => e.id == id);
        }
    }
}
