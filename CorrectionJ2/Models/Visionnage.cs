using CorrectionJ2.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace CorrectionFinale.Models
{
    public class Visionnage
    { 
        public int Id { get; set; }
        public virtual Film Film { get; set; }
        public virtual Utilisateur Utilisateur { get; set; }
        public DateTime DateVisionnage { get; set; }

    }
}
