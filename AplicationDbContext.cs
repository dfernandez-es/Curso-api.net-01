using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using primer.Entidades;

namespace primer
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Autor> Autores {get; set; }
        public DbSet<Libro> Libros {get; set; }
    }
}