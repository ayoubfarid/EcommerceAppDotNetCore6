using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EcommerceApp.Models;

namespace EcommerceApp.Pages.Cart
{
    public class IndexModel : PageModel
    {


        public IList<Product> Products { get; set; } = default!;
        [BindProperty]
        public int ProductId { get; set; }

        public async Task OnGetAsync()
        {

            var retrievedCartService = HttpContext.Session.GetCartService();
            Products = retrievedCartService._cartItems;
            //_cartService.AddToCart(product);

        }
        public async Task OnPostAsync()
        {

            // Retrieve the product based on the productId (fetch it asynchronously)
            var retrievedCartService = HttpContext.Session.GetCartService();
            var selectedProduct = retrievedCartService._cartItems.Find(p =>
            {
                return p.ProductID == ProductId;
            });
            if(selectedProduct != null)
            {
                  retrievedCartService._cartItems
                   .Remove(selectedProduct);
                HttpContext.Session.SetCartService(retrievedCartService);

                Products = retrievedCartService._cartItems;
            }

            


          //  return RedirectToPage("./Index");

        }



    }
}
