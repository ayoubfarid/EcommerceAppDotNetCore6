using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EcommerceApp.Models;

namespace EcommerceApp.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly EcommerceApp.Models.EcomDbContext _context;

        public IndexModel(EcommerceApp.Models.EcomDbContext context)
        {
            _context = context;
        }

        public IList<Product> Products { get; set; } = default!;
        public IList<Category> Category { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchedProduct { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; }

        public async Task OnGetAsync()
        {
            
            IQueryable<Product> productsQuery = _context.Products.Include(p => p.Category);
            ViewData["SelectedCategory"] = SelectedCategory;
            int categoryId;
            if (SelectedCategory != null)
                categoryId = int.Parse(SelectedCategory);
            else
                categoryId = 0;



            if (_context.Categories != null)
            {
                Category = await _context.Categories.ToListAsync();
            }
          

           if (string.IsNullOrEmpty(SearchedProduct) &&  categoryId > 0)
            {
                if (categoryId == 0)
                {
                    Products = await productsQuery.Where(p =>

                    EF.Functions.Like(p.Name, $"%{SearchedProduct}%"))
                    .ToListAsync();
                }
                else
                {
                    // Filter products by category ID
                    Products = await productsQuery
                                        .Where(p => p.CategoryID == categoryId)
                                        .ToListAsync();

                }
                
            }
            else if (!string.IsNullOrEmpty(SearchedProduct) )
            {
                 if (categoryId == 0)
                {
                    Products = await productsQuery.Where(p =>

                    EF.Functions.Like(p.Name, $"%{SearchedProduct}%"))
                    .ToListAsync();
                }
                else
                {
                // Filtrer les produits par ID de catégorie et par nom de produit
                Products = await productsQuery
                    .Where(p => p.CategoryID == categoryId && EF.Functions.Like(p.Name, $"%{SearchedProduct}%"))
                    .ToListAsync();
                }
                
            }
            else
            Products = await productsQuery.ToListAsync();

            Console.WriteLine(SelectedCategory);
            Console.WriteLine(SearchedProduct);
            
        }
    }
}
