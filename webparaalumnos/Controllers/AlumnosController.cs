using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webparaalumnos.Data;
using webparaalumnos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace webparaalumnos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlumnosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alumno>>> GetAlumnos()
        {
            return await _context.Alumnos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Alumno>> GetAlumno(int id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);

            if (alumno == null)
            {
                return NotFound("Alumno no encontrado");
            }

            return alumno;
        }

        [HttpPost]
        public async Task<ActionResult<Alumno>> PostAlumno(Alumno nuevoAlumno)
        {
            _context.Alumnos.Add(nuevoAlumno);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlumno", new { id = nuevoAlumno.Id }, nuevoAlumno);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlumno(int id, Alumno alumnoActualizado)
        {
            if (id != alumnoActualizado.Id)
            {
                return BadRequest("El ID no coincide");
            }

            _context.Entry(alumnoActualizado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlumnoExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlumno(int id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null)
            {
                return NotFound();
            }

            _context.Alumnos.Remove(alumno);
            await _context.SaveChangesAsync();

            return Ok("Alumno eliminado");
        }

        private bool AlumnoExists(int id)
        {
            return _context.Alumnos.Any(e => e.Id == id);
        }
    }
}