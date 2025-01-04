using CorrectionJ2.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CorrectionJ2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //On commence par récupérer le builder de notre application
            var builder = WebApplication.CreateBuilder(args);
            //On configure le builder (type d'app)
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Login/Connexion";

            });
            builder.Services.AddControllersWithViews();


            var app = builder.Build();
            app.UseStaticFiles();// On prévient l'app qu'on utilise le repertoire wwwroot
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "ajout",
                    pattern: "Add/{valeur1}/{valeur2}",
                    defaults: new { controller = "Calculateur", action = "Ajouter" });


                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });

            using (IDal dal = new Dal())
            {
                dal.DeleteCreateDatabase();
                dal.initializeBdd();
                dal.SupprimerFilm(4);
                dal.SupprimerFilm(4);
            }

            app.Run();
        }
    }
}
