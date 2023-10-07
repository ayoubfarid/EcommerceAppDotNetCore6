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
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<Product> productsQuery = _context.Products.Include(p => p.Category);

            if (!string.IsNullOrEmpty(SearchTerm))
            {
              /*  productsQuery = productsQuery.Where(p =>
                    p.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)
                );*/
              
                Products = await productsQuery.Where(p =>

                EF.Functions.Like(p.Name, $"%{SearchTerm}%"))
                .ToListAsync();

            }else

            Products = await productsQuery.ToListAsync();
            
            // Print the products to the console
            foreach (var product in Products)
            {
                Console.WriteLine($"Product ID: {product.ProductID}, Name: {product.Name}, Description: {product.Description}");
            }
        }
    }
}
