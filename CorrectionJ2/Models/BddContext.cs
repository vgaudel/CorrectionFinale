using CorrectionFinale.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace CorrectionJ2.Models
{
    public class BddContext : DbContext
    {
        public DbSet<Film> Films { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Visionnage> Visionnages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user id=root;password=rrrrr;database=filmsCDA28");
        }

    }
}
