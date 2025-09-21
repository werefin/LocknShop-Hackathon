using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LocknShop.Data;
using LocknShop.Models;

namespace LocknShop.Pages.admin.categories
{
    public class CreateModel : PageModel
    {
        private readonly LocknShop.Data.ApplicationDbContext _context;
        public CreateModel(LocknShop.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            ViewData["ParentCategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return Page();
        }
        [BindProperty]
        public Category Category { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Categories.Add(Category);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}   
