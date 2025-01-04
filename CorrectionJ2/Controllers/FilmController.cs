using CorrectionFinale.Models;
using CorrectionJ2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CorrectionFinale.Controllers
{
    
    public class FilmController : Controller
    {
        [Authorize(Roles = "Lambda")]
        [HttpGet]
        public IActionResult AjoutFilm()
        {
            Film film = new Film(); 
            return View(film);
        }
        [Authorize(Roles = "Lambda")]
        [HttpPost]
        public IActionResult AjoutFilm(Film film)
        {
            if (!ModelState.IsValid)
            {
                return View(film);
            }
            using (IDal dal = new Dal())
            {
                dal.CreerFilm(film.Titre, film.Annee, film.Realisateur, false);
            }

            return RedirectToAction("ListeFilms","Home");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AjoutVisionnage()
        {
            using (IDal dal = new Dal())
            {
                ViewBag.listeFilms = dal.ObtenirTousLesFilms();
            }
            using (BddContext bdd = new BddContext())
            {
                ViewBag.listeUtilisateurs = bdd.Utilisateurs.ToList();
            }
            Visionnage visionnage = new Visionnage();
            return View(visionnage);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AjoutVisionnage(Visionnage visionnage)
        {
          

            Console.WriteLine(visionnage.Film.Id + " " + visionnage.Utilisateur.Id + " " + visionnage.DateVisionnage.ToString());
            
            using (BddContext bdd = new BddContext())
            {
                IVisionnageService visionnageService = new VisionnageService(bdd);
                visionnageService.CreerVisionnage(
                    bdd.Films.Find(visionnage.Film.Id), 
                    bdd.Utilisateurs.Find(visionnage.Utilisateur.Id), 
                    visionnage.DateVisionnage);
            }

            return RedirectToAction("ListeFilms", "Home");
        }

    }
}
