using CorrectionJ2.Models;
using Microsoft.AspNetCore.Mvc;

namespace CorrectionJ2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View("Error");
            }
            else
            {
                ViewData["Titre"] = id;
                return View();
            }
        }

        public IActionResult ListeFilms()
        {
            using (IDal myDal = new Dal()) {
                //ViewData["Films"] = myDal.ObtenirTousLesFilms();
                return View(myDal.ObtenirTousLesFilms());
            }
        }

        public IActionResult ChercheFilm(string id)
        {
            using (IDal myDal = new Dal())
            {
                ViewData["Titre"] = id;
                Film film = myDal.ObtenirTousLesFilms().FirstOrDefault(c => c.Titre == id);
                if ((film != null) && (film.Visionne == true))
                {
                    return View("Visionne",film);
                }
                else if ((film != null) && (film.Visionne == false))
                {
                    return View("AVisionner",film);
                }
                return View("NonVisionne");
            }
        }
        public IActionResult ListeUtilisateurs()
        {
            using (UserService us = new UserService(new BddContext()))
            {
                //ViewData["Userlms"] = us.ObtenirUsers();
                return View();
            }
        }
    }
}
