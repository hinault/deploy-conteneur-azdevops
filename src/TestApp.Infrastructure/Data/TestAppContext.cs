using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.ApplicationCore.Entites;

namespace TestApp.Infrastructure.Data
{
    public class TestAppContext : DbContext
    {
        public TestAppContext(DbContextOptions<TestAppContext> options) : base(options)
        {
        }

        public DbSet<Etudiant> Etudiants { get; set; }
    }
}
