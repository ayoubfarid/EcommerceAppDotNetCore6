using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public String? Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; } // Add this property for image upload

        public int CategoryID { get; set; }
        public virtual Category? Category { get; set; }

    }
}
