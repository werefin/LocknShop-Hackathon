using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LocknShop.Data;
using LocknShop.Helpers;
using LocknShop.Models;

namespace LocknShop.Pages.products
{
    public class IndexModel : PageModel
    {
        private readonly LocknShop.Data.ApplicationDbContext _context;

        public IndexModel(LocknShop.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string SearchTerm { get; set; }

        public IList<Product> Products { get; set; }


        public async Task OnGetAsync()
        {
            if (_context.Products != null)
            {
                Products = await _context.Products.ToListAsync();
            }
        }

        public IActionResult OnPostResetSearch()
        {
            return Redirect("/products");
        }

        public IActionResult OnPostSearch()
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
            {
                TempData["info"] = $"Please enter a valid search term.";
                return Redirect("/products");
            }

            Products = _context.Products.Where(x => x.Name.Contains(SearchTerm) || x.Category.Name.Contains(SearchTerm)).ToList();
            return Page();
        }

        public IActionResult OnPostAddToCart(int productId)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return Redirect("/Identity/Account/Login");

                Products = _context.Products.ToList();

                Product product = _context.Products.First(x => x.Id == productId);

                //handle product out of stock
                if (product.IsOutOfStock)
                {
                    TempData["error"] = $"{product.Name} is out of stock.";
                    return Redirect("/products");
                }

                var u = UsersHelper.GetUser(_context, this.User);
               
                CartHelper.AddToCartDb(product, _context, this.User);
                
                TempData["success"] = $"{product.Name} added to cart successfully!";
                return Redirect("/products");
            }
            catch 
            {
                TempData["error"] = $"Login to add items to your cart.";
                return Redirect("/products");
            }
            
        }


    }
}
