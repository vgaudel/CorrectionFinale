using CorrectionFinale.ViewModels;
using CorrectionJ2.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CorrectionFinale.Controllers
{
    public class LoginController : Controller
    {
        //On définit une méthode par requète Http que l'on veut créer

        [HttpGet]
        // La route créée est /NomDuControler/NomDeLaMethode
        // ici ->  /Login/CreerCompte 
        public IActionResult CreerCompte() // le type de retour IActionResult definit que c'est une route
        {
            return View();// le type de retour IActionResult veut dire qu'on retourne une vue
                          // par défaut la vue doit avoir lemême nom que l'action : CreerCompte
                          // Sinon on précise le nom de la vue return View("nomDeLAutreVue");     
        }
        [HttpPost]
        public IActionResult CreerCompte(Utilisateur utilisateur)
        {//Sur la requète Get on envoie le formulaire
        //sur la requète post, on vérifie que l'utilisateur à bien rempli le formulaire
        //et on ajoute les données à la base de donnée
            using (IUserService ius = new UserService(new BddContext()))
            {
                if (ModelState.IsValid && (ius.ChercherUtilisateurParNom(utilisateur.Nom)==null))
                {
               
                    ius.CreerUtilisateur(utilisateur.Nom, utilisateur.Mdp);
                }
            }
            return View(utilisateur);
        }
        public IActionResult Connexion()
        {
            using (IUserService ius = new UserService(new BddContext()))
            {
                UtilisateurViewModel utilisateurViewModel = new UtilisateurViewModel
                {
                    Authentifie = HttpContext.User.Identity.IsAuthenticated
                };
                if (utilisateurViewModel.Authentifie)
                {
                    Console.WriteLine("Je suis déjà authentifié");
                    utilisateurViewModel.Utilisateur = ius.ObtenirUtilisateur(HttpContext.User.Identity.Name);
                    return RedirectToAction("ListeFilm","Home");
                }
                return View(utilisateurViewModel);
            }
        }
        [HttpPost]
        public IActionResult Connexion(UtilisateurViewModel utilisateurViewModel, string returnUrl)
        {
            using (IUserService ius = new UserService(new BddContext()))
            {
                    Utilisateur utilisateur = ius.Authentifier(utilisateurViewModel.Utilisateur.Nom, utilisateurViewModel.Utilisateur.Mdp);
                    if (utilisateur != null) // bon mot de passe
                    {
                        Console.WriteLine("Connexion réussie");

                        List<Claim> userClaims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Name, utilisateur.Id.ToString()),
                            new Claim(ClaimTypes.Role, utilisateur.Role)
                        };

                        var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

                        var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });

                        HttpContext.SignInAsync(userPrincipal);

                        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                            return Redirect(returnUrl);

                        return Redirect("/Home/Index/1");
                    }
                    Console.WriteLine("Connexion ratée Mdp Incorrect");
                    ModelState.AddModelError("Utilisateur.Nom", "Nom et/ou mot de passe incorrect(s)");
                
                
                return View(utilisateurViewModel);
            }
        }

        public IActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Connexion","Login");
        }

    }
}
