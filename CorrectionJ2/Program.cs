using CorrectionJ2.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CorrectionJ2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /***** Configuration du builder de l'application *****/
            //On commence par r�cup�rer le builder de notre application
            var builder = WebApplication.CreateBuilder(args);
            //On configure le builder (type d'app)

            //On indique au builder que la mani�re dont on g�re l'utilisateur non connect�
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Login/Connexion";//On indique la route relative pour s'authentifier

            });
            //On indique au builder que l'on fait une application MVC
            builder.Services.AddControllersWithViews();

            /***** Fin de la configuration du builder de l'application *****/
            
            var app = builder.Build(); //On construit l'application
            
            
            
            app.UseStaticFiles();// On pr�vient l'app qu'on utilise le repertoire wwwroot
            app.UseRouting();//On configure l'app pour lui faire utiliser les endpoints ci dessous

            app.UseAuthentication();//ce sont les options choisies pour la s�curit� et la connexion
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {       //On configure le mod�le de nos route : /Controller/action/param�tres
                    endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });

            //On fait ce qu'il faut pour avoir une base de donn�es qui nous permet de tester notre code
            using (IDal dal = new Dal())
            {
                dal.DeleteCreateDatabase(); //On supprime et on recr�e la BDD
                dal.initializeBdd();//On remplit la BDD toute neuve avec des valeurs par d�faut qui
                                    //nous permettent d'utiliser notre application
            }

            app.Run(); // On lance l'application (notre serveur)
        }
    }
}
