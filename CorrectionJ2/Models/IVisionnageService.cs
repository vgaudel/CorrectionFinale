using CorrectionJ2.Models;

namespace CorrectionFinale.Models
{
    public interface IVisionnageService : IDisposable
    {
        public int CreerVisionnage(Film film, Utilisateur utilisateur, DateTime dateVisionnage);
        public void ModifierVisionnage(int Id, Film film, Utilisateur utilisateur, DateTime dateVisionnage);
        public void SupprimerVisionnage(int id);
    }
}
