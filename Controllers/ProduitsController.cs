using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using TPCaisse.Data;
using TPCaisse.Models;
using TPCaisse.Models.ViewModel;

namespace TPCaisse.Controllers
{
    public class ProduitsController : Controller
    {
        private readonly TPCaisseContext _context;

        public ProduitsController(TPCaisseContext context)
        {
            _context = context;
        }

        // GET: Produits
        public async Task<IActionResult> Index()
        {
            var produits = await _context.Produit
                       .Include(p => p.Categorie)
                       .ToListAsync();
            return View(produits);
        }

        // GET: Produits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _context.Produit
                .Include(p => p.Categorie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produit == null)
            {
                return NotFound();
            }

            return View(produit);
        }

        // GET: Produits/Create
        public IActionResult Create()
        {
            var viewModel = new ProduitViewModel
            {
                Categories = _context.Categorie.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.Nom
                }).ToList()
            };
            return View(viewModel);
        }

        // POST: Produits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Description,Prix,Quantite,CategorieId,Image")] ProduitViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var produit = new Produit
                {
                    Nom = viewModel.Nom,
                    Description = viewModel.Description,
                    Prix = viewModel.Prix,
                    Quantite = viewModel.Quantite,
                    CategorieId = viewModel.CategorieId,
                    Image = viewModel.Image
                };
                _context.Produit.Add(produit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Produits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _context.Produit.Include(p => p.Categorie).FirstOrDefaultAsync(p => p.Id == id);
            if (produit == null)
            {
                return NotFound();
            }
            var viewModel = new ProduitViewModel
            {
                Nom = produit.Nom,
                Description = produit.Description,
                Prix = produit.Prix,
                Quantite = produit.Quantite,
                CategorieId = produit.CategorieId,
                Image = produit.Image,
                Categories = _context.Categorie
                             .Select(c => new SelectListItem
                             {
                                 Value = c.ID.ToString(),
                                 Text = c.Nom
                             }).ToList()
            };
            return View(viewModel);
        }

        // POST: Produits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Description,Prix,Quantite,CategorieId,Image")] ProduitViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var produit = _context.Produit.Find(id);
                    produit.Nom = viewModel.Nom;
                    produit.Description = viewModel.Description;
                    produit.Prix = viewModel.Prix;
                    produit.Quantite = viewModel.Quantite;
                    produit.CategorieId = viewModel.CategorieId;
                    produit.Image = viewModel.Image;

                    _context.Update(produit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduitExists(viewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Produits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produit = await _context.Produit
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produit == null)
            {
                return NotFound();
            }

            return View(produit);
        }

        // POST: Produits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produit = await _context.Produit.FindAsync(id);
            if (produit != null)
            {
                _context.Produit.Remove(produit);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProduitExists(int id)
        {
            return _context.Produit.Any(e => e.Id == id);
        }

        [HttpPost]
        private IActionResult AddToCart(Produit produit)
        {
            List<Produit> produits;
            var panier = HttpContext.Session.GetString("Panier");

            if (!string.IsNullOrEmpty(panier))
            {
                produits = JsonConvert.DeserializeObject<List<Produit>>(panier);
            }
            else
            {
                produits = new List<Produit>();
            }

            produits.Add(produit);

            HttpContext.Session.SetString("Panier", JsonConvert.SerializeObject(produits));

            return RedirectToAction("Index", "Panier");
        }
    }
}
