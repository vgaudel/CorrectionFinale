using CorrectionJ2.Models;

namespace CorrectionFinale.Models
{
    public class VisionnageService : IVisionnageService
    {
        private BddContext _bddContext;

        public VisionnageService(BddContext _bddContext)
        {
            this._bddContext = _bddContext;
        }
        public int CreerVisionnage(Film film, Utilisateur utilisateur, DateTime dateVisionnage)
        {
            Visionnage visionnage = new Visionnage() { 
                Film = film, 
                Utilisateur = utilisateur, 
                DateVisionnage = dateVisionnage 
            };
            _bddContext.Visionnages.Add(visionnage);
            _bddContext.SaveChanges();
            return visionnage.Id;

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void ModifierVisionnage(int Id, Film film, Utilisateur utilisateur, DateTime dateVisionnage)
        {
            throw new NotImplementedException();
        }

        public void SupprimerVisionnage(int id)
        {
            throw new NotImplementedException();
        }
    }
}
