using System.ComponentModel.DataAnnotations;

namespace TPCaisse.Models
{
    public class Produit
    {
        public int Id { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "Le titre est trop long")]
        public string Nom { get; set; }

        [Required]
        [StringLength(512, ErrorMessage = "Le titre est trop long")]
        public string Description { get; set; }

        [Required]
        [Range(0.10, 10000, ErrorMessage = "Le prix doit être compris entre 0,10€ et 10000€")]
        public float Prix { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "La quantité doit être comprise entre 1 et 1000")]
        public int Quantite { get; set; }

        public int CategorieId { get; set; }
        public Categorie ?Categorie { get; set; } = null!;
        public string ?Image { get; set; }
    }
}
