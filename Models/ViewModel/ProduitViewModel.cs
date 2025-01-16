using Microsoft.AspNetCore.Mvc.Rendering;

namespace TPCaisse.Models.ViewModel
{
    public class ProduitViewModel
    {
        public int Id { get; set; }
        public string Nom {  get; set; }
        public string Description { get; set; }
        public float Prix { get; set; }
        public int Quantite { get; set; }
        public int CategorieId { get; set; }
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        public string Image {  get; set; }
    }
}
