using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace BlazorApp.context
{
    public class UsuarioDbContext:DbContext
    {
        public UsuarioDbContext(DbContextOptions<UsuarioDbContext> options):base(options)
        {


        }
        public DbSet<Contato> contatos {get;set;} = null;
        
    }
}