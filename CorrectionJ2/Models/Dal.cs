
using CorrectionFinale.Models;

namespace CorrectionJ2.Models
{
    public class Dal : IDal
    {
        private BddContext _bddContext;
        private UserService _userService;
        private VisionnageService _visionnageService;

        public Dal()
        {
            _bddContext = new BddContext();
            _userService = new UserService(_bddContext);
            _visionnageService = new VisionnageService(_bddContext);
        }

        public void DeleteCreateDatabase()
        {
            _bddContext.Database.EnsureDeleted();
            _bddContext.Database.EnsureCreated();
        }

        public void initializeBdd()
        {
            CreerFilm("Titanic", 1999, "James Cameron", true);
            CreerFilm("Terminator", 1989, "James Cameron", false);
            CreerFilm("Oui oui au pays des voitures", 2004, "Mor Thiam", false);
            CreerFilm("Bisounours au pays des soviets", 2022, "Anthony", true);
            _userService.CreerUtilisateur("Vincent", "secret");
            _userService.CreerUtilisateur("John", "Tressecret");
            _userService.CreerUtilisateur("Bill", "A ne pas mettre en clair en BDD");
            _userService.CreerUtilisateur("François", "Un développeur ne devrait pas lire ça");
            ModifierFilm(3, "OuiOui au pays des voitures", 2014, "Mor Thiam", false);
            _visionnageService.CreerVisionnage(ChercherFilmParTitre("Titanic"),_bddContext.Utilisateurs.Find(1), DateTime.Now);
            _visionnageService.CreerVisionnage(ChercherFilmParTitre("Titanic"), _bddContext.Utilisateurs.Find(1), DateTime.Now);
            _visionnageService.CreerVisionnage(ChercherFilmParTitre("Titanic"), _bddContext.Utilisateurs.Find(1), DateTime.Now);
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

        public List<Film> ObtenirTousLesFilms()
        {
            return _bddContext.Films.ToList();
        }

        public int CreerFilm(string titre, int annee, string realisateur, bool visionne)
        {
            Film film = new Film() { Titre = titre, Annee = annee, Realisateur = realisateur, Visionne = visionne };
            _bddContext.Films.Add(film);
            _bddContext.SaveChanges();
            return film.Id;
        }

        public void ModifierFilm(int id, string titre, int annee, string realisateur, bool visionne)
        {
            Film film = _bddContext.Films.Find(id);

            if (film != null)
            {
                film.Titre=titre;
                film.Annee=annee;
                film.Realisateur=realisateur;
                film.Visionne=visionne;
                _bddContext.SaveChanges();
            }

        }
        public void SupprimerFilm(int id)
        {
            Film film = _bddContext.Films.Find(id);

            if (film != null)
            {
                _bddContext.Films.Remove(film);
                _bddContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("Film non trouvé");
            }
        }

        public Film ChercherFilmParTitre(string titre)
        {
            return ObtenirTousLesFilms().FirstOrDefault(monFilm => (monFilm.Titre == titre));
        }                                   //maFonctionAnonym(Film monFilm){return (monFilm.Titre == titre);}
    }
}
