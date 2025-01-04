
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

            //Après avoir créé mon modèle et ma table, j'ajoute des valeurs par défaut à la bdd.
            _userService.CreerUtilisateur("Vincent", "secret");
            _userService.CreerUtilisateur("John", "Tressecret");
            _userService.CreerUtilisateur("Bill", "A ne pas mettre en clair en BDD");
            _userService.CreerUtilisateur("François", "Un développeur ne devrait pas lire ça");
            ModifierFilm(3, "OuiOui au pays des voitures", 2014, "Mor Thiam", false);

            //Après avoir créé mon modèle et ma table, j'ajoute des valeurs par défaut à la bdd.
            _visionnageService.CreerVisionnage(ChercherFilmParTitre("Titanic"),_bddContext.Utilisateurs.Find(1), DateTime.Now);
            _visionnageService.CreerVisionnage(ChercherFilmParTitre("Titanic"), _bddContext.Utilisateurs.Find(1), DateTime.Now);
            _visionnageService.CreerVisionnage(ChercherFilmParTitre("Titanic"), _bddContext.Utilisateurs.Find(1), DateTime.Now);
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

        //dans le projet, ce qu'il y a en dessous doit être dans un service dédié
        public List<Film> ObtenirTousLesFilms()
        {
            return _bddContext.Films.ToList();
        }

        public int CreerFilm(string titre, int annee, string realisateur, bool visionne)
        {
            //On créé l'objet à ajouter en bbd
            Film film = new Film() { Titre = titre, Annee = annee, Realisateur = realisateur, Visionne = visionne };
            //On ajoute l'objet au DbSet
            _bddContext.Films.Add(film);
            //On enregistre les changements
            _bddContext.SaveChanges();
            return film.Id;
        }

        public void ModifierFilm(int id, string titre, int annee, string realisateur, bool visionne)
        {
            //On récupère l'objet à modifier en bbd
            Film film = _bddContext.Films.Find(id);

            // Si on a trouvé l'objet, on écrase les valeurs de ses attributs
            if (film != null)
            {
                film.Titre=titre;
                film.Annee=annee;
                film.Realisateur=realisateur;
                film.Visionne=visionne;
                // On enregistre les changements
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
            //Quand on cherche un objet en particulier, il faut fournir une fonction
            // qui prend en paramètre un objet du type souhaité et qui retourne un
            // booléen qui nous dit si c'est lui ou non 
            // ((nomDeLaVariableQuiContientLObjet => (condition qui vaut true ou false))
            //                   monFilm          => (monFilm.Titre == titreRecherché)
            // c'est le framework qui s'occupe d'appliquer la méthode
            return ObtenirTousLesFilms().FirstOrDefault(monFilm => (monFilm.Titre == titre));
        }                            //maFonctionAnonym(Film monFilm){return (monFilm.Titre == titre);}
    }
}
