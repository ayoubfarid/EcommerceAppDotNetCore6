using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EcommerceApp.Models;

namespace EcommerceApp.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly EcommerceApp.Models.EcomDbContext _context;

        public CreateModel(EcommerceApp.Models.EcomDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        //ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryID");
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryID");

            // ViewData["CategoryName"] = new SelectList(_context.Categories, "Name", "Name");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Products == null || Product == null)
            {
                return Page();
            }
            var directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Handle the uploaded image
            if (Product.ImageFile != null && Product.ImageFile.Length > 0)
            {
                // Generate a unique file name (you can use GUIDs, timestamps, etc.)
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Product.ImageFile.FileName;

                // Combine the unique file name with the path to the "images" folder
                var imagePath = Path.Combine("images", uniqueFileName);

                // Save the image to the wwwroot/images folder
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Product.ImageFile.CopyToAsync(stream);
                }

                Product.Image = "/" + imagePath; // Update the ImagePath property with the relative path
            }

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
