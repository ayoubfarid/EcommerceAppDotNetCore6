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
       

        public IList<Product> Products { get;set; } = default!;

        public async Task OnGetAsync()
        {
            
            var retrievedCartService = HttpContext.Session.GetCartService();
            Products = retrievedCartService._cartItems;
            //_cartService.AddToCart(product);

        }
    }
}
