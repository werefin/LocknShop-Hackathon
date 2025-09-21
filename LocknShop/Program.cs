using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LocknShop.Data;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

//var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));
//builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseLazyLoadingProxies().UseSqlite(connectionString));
}
else
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseLazyLoadingProxies().UseSqlServer(connectionString));
}

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddDefaultTokenProviders()
    .AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages().AddRazorRuntimeCompilation().AddRazorPagesOptions(options =>
{
    options.Conventions.AuthorizeFolder("/admin", "RequireAdmins");
});

builder.Services.AddAuthorization(options => options.AddPolicy("RequireAdmins", policy => policy.RequireRole("Admin")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

// SEED CATEGORIES AND PRODUCTS IN DEVELOPMENT
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<LocknShop.Data.ApplicationDbContext>();
        // Seed a default category if none exists
        if (!context.Categories.Any())
        {
            context.Categories.Add(new LocknShop.Models.Category { Name = "Default" });
            context.SaveChanges();
        }
        var category = context.Categories.First();
        // Seed products if none exist
        if (!context.Products.Any())
        {
            context.Products.AddRange(
                new LocknShop.Models.Product { Name = "Black Sunglasses", Category = category, Desc = "Classic black sunglasses for any occasion.", SKU = "SKU001", Quantity = 10, Price = 29.99M, Discount = 0, DateCreated = DateTime.Now, ImageLocation = "black_sunglasses.png" },
                new LocknShop.Models.Product { Name = "Black T-Shirt", Category = category, Desc = "Comfortable black t-shirt, perfect for everyday wear.", SKU = "SKU002", Quantity = 15, Price = 19.99M, Discount = 0, DateCreated = DateTime.Now, ImageLocation = "blacktshirt.png" },
                new LocknShop.Models.Product { Name = "Blue Blazer", Category = category, Desc = "Elegant blue blazer for formal and casual events.", SKU = "SKU003", Quantity = 5, Price = 49.99M, Discount = 10, DateCreated = DateTime.Now, ImageLocation = "blue_blazer.png" },
                new LocknShop.Models.Product { Name = "Cheetos Pants", Category = category, Desc = "Trendy Cheetos-themed pants for a bold look.", SKU = "SKU004", Quantity = 8, Price = 34.99M, Discount = 0, DateCreated = DateTime.Now, ImageLocation = "cheetos_pants.png" },
                new LocknShop.Models.Product { Name = "Eat Sleep Game Repeat T-Shirt", Category = category, Desc = "T-shirt for gamers: Eat, Sleep, Game, Repeat!", SKU = "SKU005", Quantity = 12, Price = 21.99M, Discount = 0, DateCreated = DateTime.Now, ImageLocation = "eat_sleep_game_repeat_tshirt.png" },
                new LocknShop.Models.Product { Name = "Gray Dress Pants", Category = category, Desc = "Stylish gray dress pants for business or pleasure.", SKU = "SKU006", Quantity = 7, Price = 39.99M, Discount = 0, DateCreated = DateTime.Now, ImageLocation = "gray_dress_pants.png" },
                new LocknShop.Models.Product { Name = "Half Zip Polo Shirt", Category = category, Desc = "Modern half-zip polo shirt for a smart look.", SKU = "SKU007", Quantity = 9, Price = 24.99M, Discount = 0, DateCreated = DateTime.Now, ImageLocation = "half_zip_polo_shirt.png" },
                new LocknShop.Models.Product { Name = "Hippo Shirt", Category = category, Desc = "Fun hippo print shirt for animal lovers.", SKU = "SKU008", Quantity = 6, Price = 22.99M, Discount = 0, DateCreated = DateTime.Now, ImageLocation = "hippo_shirt.png" },
                new LocknShop.Models.Product { Name = "Hoodie", Category = category, Desc = "Classic hoodie for warmth and comfort.", SKU = "SKU009", Quantity = 11, Price = 29.99M, Discount = 0, DateCreated = DateTime.Now, ImageLocation = "hoodie.png" },
                new LocknShop.Models.Product { Name = "Invisible Hoodie", Category = category, Desc = "Invisible hoodie: for those who want to stand out by blending in!", SKU = "SKU010", Quantity = 4, Price = 59.99M, Discount = 0, DateCreated = DateTime.Now, ImageLocation = "invisible_hoodie.png" },
                new LocknShop.Models.Product { Name = "Keep Calm and Fish On", Category = category, Desc = "Keep calm and fish on with this cool t-shirt.", SKU = "SKU011", Quantity = 10, Price = 18.99M, Discount = 0, DateCreated = DateTime.Now, ImageLocation = "keepcalmandfishon.png" },
                new LocknShop.Models.Product { Name = "Loafers", Category = category, Desc = "Comfortable loafers for everyday use.", SKU = "SKU012", Quantity = 7, Price = 44.99M, Discount = 0, DateCreated = DateTime.Now, ImageLocation = "loafers.png" },
                new LocknShop.Models.Product { Name = "Pink Sunglasses", Category = category, Desc = "Bright pink sunglasses for a fun summer look.", SKU = "SKU013", Quantity = 8, Price = 29.99M, Discount = 0, DateCreated = DateTime.Now, ImageLocation = "pink_sunglasses.png" },
                new LocknShop.Models.Product { Name = "Plaid Shirt", Category = category, Desc = "Classic plaid shirt, always in style.", SKU = "SKU014", Quantity = 6, Price = 27.99M, Discount = 0, DateCreated = DateTime.Now, ImageLocation = "plaid_shirt.png" },
                new LocknShop.Models.Product { Name = "Skinny Jeans", Category = category, Desc = "Trendy skinny jeans for a modern fit.", SKU = "SKU015", Quantity = 10, Price = 34.99M, Discount = 0, DateCreated = DateTime.Now, ImageLocation = "skinnyjeans.png" },
                new LocknShop.Models.Product { Name = "Trench Coat", Category = category, Desc = "Elegant trench coat for all seasons.", SKU = "SKU016", Quantity = 3, Price = 89.99M, Discount = 0, DateCreated = DateTime.Now, ImageLocation = "trench_coat.png" },
                new LocknShop.Models.Product { Name = "Women's Black Skinny Jeans", Category = category, Desc = "Women's black skinny jeans for a sleek look.", SKU = "SKU017", Quantity = 5, Price = 36.99M, Discount = 0, DateCreated = DateTime.Now, ImageLocation = "womens_black_skinny_jeans.png" }
            );
            context.SaveChanges();
        }

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // Create roles if they don't exist
        if (!await roleManager.RoleExistsAsync("Admin"))
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        if (!await roleManager.RoleExistsAsync("User"))
            await roleManager.CreateAsync(new IdentityRole("User"));

        // Create test admin
        var adminEmail = "admin@locknshop.dev";
        var adminPassword = "Admin123!";
        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

        // Create test user
        var userEmail = "user@locknshop.dev";
        var userPassword = "User123!";
        if (await userManager.FindByEmailAsync(userEmail) == null)
        {
            var normalUser = new IdentityUser { UserName = userEmail, Email = userEmail, EmailConfirmed = true };
            var result = await userManager.CreateAsync(normalUser, userPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(normalUser, "User");
            }
        }
    }
}

app.Run();
