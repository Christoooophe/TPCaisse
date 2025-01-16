using System.ComponentModel.DataAnnotations;

namespace TPCaisse.Models
{
    public class Categorie
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Le nom de la catégorie est trop long")]
        public string Nom { get; set; }
        public ICollection<Produit> ?ListeProduits { get; } = new List<Produit>();
    }
}
