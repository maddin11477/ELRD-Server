﻿using ELRDDataAccessLibrary.Models;
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
        public DbSet<Role> Roles { get; set; }
        public DbSet<BaseUnit> BaseUnits { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
