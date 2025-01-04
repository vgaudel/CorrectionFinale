namespace CorrectionJ2.Models
{
    public interface IUserService : IDisposable
    {
        //dans le service, on doit retrouver à minima le CRUD
        //On commence par mettre la méthode dans l'interface, puis on l'implémente
        public int CreerUtilisateur(string nom, string mdp);
        public void ModifierUtilisateur(int Id, string nom, string mdp);
        public void SupprimerUtilisateur(int id);
        public Utilisateur ChercherUtilisateurParNom(string nom);
        public Utilisateur ObtenirUtilisateur(int id);
        public Utilisateur ObtenirUtilisateur(string idStr);// spécifique pour le Login
        public Utilisateur Authentifier(string nom, string mdp);// spécifique pour le Login

    }
}
