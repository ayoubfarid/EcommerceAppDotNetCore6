using EcommerceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly EcommerceApp.Models.EcomDbContext _context;

        public IndexModel(EcommerceApp.Models.EcomDbContext context)
        {
            _context = context;
        }

        private readonly ILogger<IndexModel> _logger;
        public IList<Product> Products { get; set; } = default!;
        public IList<Category> Category { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchedProduct { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; }
        [BindProperty]
        public int ProductId { get; set; }
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


            if (string.IsNullOrEmpty(SearchedProduct))
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
            else if (!string.IsNullOrEmpty(SearchedProduct))
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
            /* else
             Products = await productsQuery.ToListAsync();
            */
            Console.WriteLine(SelectedCategory);
            Console.WriteLine(SearchedProduct);

        }



        public async Task<IActionResult> OnPostAsync()
        {

            // Retrieve the product based on the productId (fetch it asynchronously)
            var product = await _context.Products.FindAsync(ProductId);
            await Console.Out.WriteLineAsync("bla bla: " + product.Description);
            

            if (product != null)
            {

                var retrievedCartService = HttpContext.Session.GetCartService();

                retrievedCartService._cartItems.Add(product);
                HttpContext.Session.SetCartService(retrievedCartService);
                //_cartService.AddToCart(product);
            }



            return RedirectToPage("./Index");

        }
    }


}