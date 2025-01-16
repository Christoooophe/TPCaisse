using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TPCaisse.Models;

namespace TPCaisse.Controllers
{
    public class PanierController : Controller
    {
        public IActionResult Index()
        {
            var panier = HttpContext.Session.GetString("Panier");

            if (string.IsNullOrEmpty(panier))
            {
                return View(new List<Produit>());
            }

            var produits = JsonConvert.DeserializeObject<List<Produit>>(panier);
            return View(produits);
        }
    }
}
