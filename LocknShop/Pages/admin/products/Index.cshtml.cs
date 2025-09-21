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

namespace LocknShop.Pages.admin.products
{
    public class IndexModel : PageModel
    {
        private readonly LocknShop.Data.ApplicationDbContext _context;

        public IndexModel(LocknShop.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Product> Products { get; set; }


        public async Task OnGetAsync()
        {
            if (_context.Products != null)
            {
                Products = await _context.Products.ToListAsync();
            }
        }  

    }
}
