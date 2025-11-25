using Microsoft.EntityFrameworkCore;
using webparaalumnos.Models;
namespace webparaalumnos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Alumno> Alumnos { get; set; }
    }
}