using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LocknShop.Helpers;
using LocknShop.Models;

namespace LocknShop.Pages
{
    public class IndexModel : PageModel
    {
        private readonly LocknShop.Data.ApplicationDbContext _context;

        public IndexModel(LocknShop.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Product> Products { get; set; } = default!;


        public async Task OnGetAsync()
        {
            if (_context.Products != null)
            {
                Products = await _context.Products.ToListAsync(); // get from db
                Products = Products.OrderByDescending(x => x.DateCreated).Take(8).ToList(); // order by newest
            }

        }

        public IActionResult OnPostAddToCart(int productId)
        {
            if (!User.Identity.IsAuthenticated)
                return Redirect("/Identity/Account/Login");

            Products = _context.Products.ToList();

            Product product = _context.Products.First(x => x.Id == productId);

            //handle product out of stock
            if (product.IsOutOfStock)
            {
                TempData["error"] = $"{product.Name} is out of stock.";
                return Redirect("/");
            }

            var u = UsersHelper.GetUser(_context, this.User);

            CartHelper.AddToCartDb(product, _context, this.User);

            TempData["success"] = $"{product.Name} added to cart successfully!";
            return Redirect("/");
        }
    }
}
