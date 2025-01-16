using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TPCaisse.Models;

namespace TPCaisse.Data
{
    public class TPCaisseContext : DbContext
    {
        public TPCaisseContext (DbContextOptions<TPCaisseContext> options)
            : base(options)
        {
        }

        public DbSet<TPCaisse.Models.Produit> Produit { get; set; } = default!;
        public DbSet<TPCaisse.Models.Categorie> Categorie { get; set; } = default!;
    }
}
