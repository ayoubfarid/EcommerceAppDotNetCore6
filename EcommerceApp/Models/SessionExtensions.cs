using Microsoft.AspNetCore.Cors.Infrastructure;
using Newtonsoft.Json;

namespace EcommerceApp.Models
{
    public static class SessionExtensions
    {
        public static void SetCartService(this ISession session, CartProvider cartService)
        {
            string cartServiceJson = JsonConvert.SerializeObject(cartService);
            session.SetString("CartService", cartServiceJson);
        }

        public static CartProvider GetCartService(this ISession session)
        {
            string cartServiceJson = session.GetString("CartService");
            if (cartServiceJson != null)
            {
                return JsonConvert.DeserializeObject<CartProvider>(cartServiceJson);
            }
            return new CartProvider(); // Return  if the object is not found in session
        }


    }
}
