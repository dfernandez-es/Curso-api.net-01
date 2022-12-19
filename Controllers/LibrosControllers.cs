using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using primer.Entidades;

namespace primer.Controllers
{
    [ApiController]
    [Route("api/libros")]
    public class LibrosControllers: ControllerBase
    {
        private readonly AplicationDbContext context;

        public LibrosControllers(AplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Libro>> Get(int id)
        {
            return await context.Libros.Include(x=>x.Autor).FirstOrDefaultAsync(libro=> libro.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Libro libro){
            var existeAutor = await context.Autores.AnyAsync(autor=>autor.Id == libro.AutorId);
            if(!existeAutor){
                return BadRequest($"No existe el autorId indicado {libro.AutorId}");
            }

            context.Add(libro);
            await context.SaveChangesAsync();
            return Ok();

        }
    }
}