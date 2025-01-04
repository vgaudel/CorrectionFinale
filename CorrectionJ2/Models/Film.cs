using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CorrectionJ2.Models
{
    public class Film
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Veuillez entrer un titre de film")]
        [MinLength(2, ErrorMessage = "Le titre doit comporter au moins 2 caractères")]
        public string Titre { get; set; }
        [DisplayName("Année")]
        public int Annee { get; set; }
        [DisplayName("Réalisateur du film")]
        public string Realisateur { get; set; }
        public bool Visionne { get; set; }

    }
}
 