using System.Text;
using System.Security.Cryptography;

namespace CorrectionJ2.Models
{
    public class UserService : IUserService
    {
       
        private BddContext _bddContext;
        public UserService(BddContext _bddContext)
        {
            this._bddContext = _bddContext;
        }
        public Utilisateur ChercherUtilisateurParNom(string nom)
        {
            return _bddContext.Utilisateurs.FirstOrDefault(u => u.Nom == nom);
        }

        public int CreerUtilisateur(string nom, string mdp)
        {
            Utilisateur utilisateur;
            if (nom == "aa")
            {
                 utilisateur = new Utilisateur() { Nom = nom, Mdp = EncodeMD5(mdp), Role = "Admin" };
            }
            else
            {
                utilisateur = new Utilisateur() { Nom = nom, Mdp = EncodeMD5(mdp), Role = "Lambda" };
            }

            _bddContext.Utilisateurs.Add(utilisateur);
            _bddContext.SaveChanges();
            return utilisateur.Id; 
        }
        //Exemple de méthode générée à partir de l'interface
        public void ModifierUtilisateur(int Id, string nom, string mdp)
        {
            throw new NotImplementedException();//Levée d'exception à remplacer par le vrai code
        }

        public void SupprimerUtilisateur(int id)
        {
            throw new NotImplementedException();
        }

        private string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "NetWish" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }

        public Utilisateur ObtenirUtilisateur(int id)
        {
            return this._bddContext.Utilisateurs.Find(id);
        }
        public Utilisateur ObtenirUtilisateur(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                return this.ObtenirUtilisateur(id);
            }
            return null;
        }
        public void Dispose()
        {
            this._bddContext.Dispose();

        }

        public Utilisateur Authentifier(string nom, string mdp)
        {
            string MDPCrypte = EncodeMD5(mdp);
            Utilisateur utilisateur = this._bddContext.Utilisateurs.FirstOrDefault(x => (x.Nom == nom) && (x.Mdp==MDPCrypte));
            return utilisateur;
        }
    }
}
