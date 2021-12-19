﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Data
{
    public class AplicacionDbContext : IdentityDbContext<AplicacionUser>
    {
        public AplicacionDbContext(DbContextOptions<AplicacionDbContext> options) : base(options)
        {

        }

        public DbSet<Libro> Libro { get; set; }
        public DbSet<LibroImagen> LibroImagen { get; set; }
    }
}
