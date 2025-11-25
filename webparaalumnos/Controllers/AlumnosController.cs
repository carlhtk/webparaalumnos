using Microsoft.AspNetCore.Mvc;
using webparaalumnos.Models; 
using System.Collections.Generic;
using System.Linq;

namespace webparaalumnos.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class AlumnosController : ControllerBase
    {
        
        private static List<Alumno> listaAlumnos = new List<Alumno>
        {
            new Alumno { Id = 1, Nombre = "Juan", Apellido = "Perez", Edad = 20 },
            new Alumno { Id = 2, Nombre = "Maria", Apellido = "Gomez", Edad = 22 }
        };

       
        [HttpGet]
        public ActionResult<IEnumerable<Alumno>> GetAlumnos()
        {
            return Ok(listaAlumnos);
        }

       
        [HttpGet("{id}")]
        public ActionResult<Alumno> GetAlumno(int id)
        {
            var alumno = listaAlumnos.FirstOrDefault(x => x.Id == id);
            if (alumno == null) return NotFound("Alumno no encontrado");
            return Ok(alumno);
        }

        
        [HttpPost]
        public ActionResult PostAlumno([FromBody] Alumno nuevoAlumno)
        {
        
            nuevoAlumno.Id = listaAlumnos.Max(x => x.Id) + 1;
            listaAlumnos.Add(nuevoAlumno);
            return Ok("Alumno creado con éxito");
        }

        
        [HttpPut("{id}")]
        public ActionResult PutAlumno(int id, [FromBody] Alumno alumnoActualizado)
        {
            var alumno = listaAlumnos.FirstOrDefault(x => x.Id == id);
            if (alumno == null) return NotFound();

            alumno.Nombre = alumnoActualizado.Nombre;
            alumno.Apellido = alumnoActualizado.Apellido;
            alumno.Edad = alumnoActualizado.Edad;

            return Ok("Alumno actualizado");
        }

       
        [HttpDelete("{id}")]
        public ActionResult DeleteAlumno(int id)
        {
            var alumno = listaAlumnos.FirstOrDefault(x => x.Id == id);
            if (alumno == null) return NotFound();

            listaAlumnos.Remove(alumno);
            return Ok("Alumno eliminado");
        }
    }
}