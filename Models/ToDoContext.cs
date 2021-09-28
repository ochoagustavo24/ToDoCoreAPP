using Microsoft.EntityFrameworkCore;

namespace ToDoCoreAPP.Models
{
    public class ToDoContext: DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options): base(options) { }

        public DbSet<ToDoitem> ToDoitems { get; set; }
    }
}
