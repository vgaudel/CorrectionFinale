using System.ComponentModel.DataAnnotations;

namespace CorrectionJ2.Models
{
    public class Utilisateur
    {
        public int Id { get; set; }// on a toujours 1 Id qui est utilisé par l'entityFramework
                                   // on ne le modifie pas nous même, c'est le gestionnaire de
                                   // la base de donnée qui s'en occupe (BddContext)
        [Required]
        public string Nom {  get; set; }
        [Required] //On peut ajouter des annotation pour vérifier les données plus tard
        public string Mdp { get; set; }
        public string? Role { get; set; }//Si il y a des endroits/moments dans le code
                                         //où un attribut peut être null, il faut le préciser en ajoutant
                                         // un ? après sont type (sinon pb avec le ModelState.isValid)
    }
}
