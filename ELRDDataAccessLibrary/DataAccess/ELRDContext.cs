using ELRDDataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELRDDataAccessLibrary.DataAccess
{
    public class ELRDContext : DbContext
    {
        //Konstruktor wenn notwendig
        public ELRDContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }

    }
}
