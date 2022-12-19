using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using primer.Entidades;

namespace primer.Controllers
{
    [ApiController]
    [Route("api/autores")]
    public class AutoresControllers: ControllerBase
    {
        private readonly AplicationDbContext context;
        public AutoresControllers(AplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Autor>>> Get()
        {
            return await context.Autores.Include(x=>x.Libros).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Autor autor){
            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Autor autor, int id){
            if(autor.Id != id){
                return BadRequest("El id del usuario no coincide");
            }
            context.Update(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id){
            var existe = await context.Autores.AnyAsync(autor => autor.Id == id);
            if(!existe){
                return NotFound();
            }
            context.Remove(new Autor { Id = id});
            await context.SaveChangesAsync();
            return Ok();
        }

    }
}