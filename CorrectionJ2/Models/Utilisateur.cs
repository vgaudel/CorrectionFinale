using System.ComponentModel.DataAnnotations;

namespace CorrectionJ2.Models
{
    public class Utilisateur
    {
        public int Id { get; set; }
        [Required]
        public string Nom {  get; set; }
        [Required]
        public string Mdp { get; set; }
        public string? Role { get; set; }
    }
}
