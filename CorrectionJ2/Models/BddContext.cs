using CorrectionFinale.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace CorrectionJ2.Models
{
    public class BddContext : DbContext
    {
        //dans le BddContext on a 1 DbSet par table dans la bdd
        public DbSet<Film> Films { get; set; }// La table prend des objets de type Film et s'appelle Films
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Visionnage> Visionnages { get; set; }

        //On configure notre BddContext pour lui expliquer comment se connecter à notre base de données
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user id=root;password=rrrrr;database=filmsCDA28");
        }// On donne la connexionString spécifique à notre sqlServer (à modifier pour chaque dev)

    }
}
