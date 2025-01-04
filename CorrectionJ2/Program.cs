using CorrectionJ2.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CorrectionJ2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /***** Configuration du builder de l'application *****/
            //On commence par récupérer le builder de notre application
            var builder = WebApplication.CreateBuilder(args);
            //On configure le builder (type d'app)

            //On indique au builder que la manière dont on gère l'utilisateur non connecté
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Login/Connexion";//On indique la route relative pour s'authentifier

            });
            //On indique au builder que l'on fait une application MVC
            builder.Services.AddControllersWithViews();

            /***** Fin de la configuration du builder de l'application *****/
            
            var app = builder.Build(); //On construit l'application
            
            
            
            app.UseStaticFiles();// On prévient l'app qu'on utilise le repertoire wwwroot
            app.UseRouting();//On configure l'app pour lui faire utiliser les endpoints ci dessous

            app.UseAuthentication();//ce sont les options choisies pour la sécurité et la connexion
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {       //On configure le modèle de nos route : /Controller/action/paramètres
                    endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });

            //On fait ce qu'il faut pour avoir une base de données qui nous permet de tester notre code
            using (IDal dal = new Dal())
            {
                dal.DeleteCreateDatabase(); //On supprime et on recrée la BDD
                dal.initializeBdd();//On remplit la BDD toute neuve avec des valeurs par défaut qui
                                    //nous permettent d'utiliser notre application
            }

            app.Run(); // On lance l'application (notre serveur)
        }
    }
}
