## LocknShop - Hackathon 2025 (CRIF)

**Team**: CRIF Cyberguard (Team 1)  
**Event**: Hackathon - CRIF Bologna

### Overview
**LocknShop** is a web e-commerce application built with **ASP.NET Core Razor Pages**. The application follows a classic three-tier architecture:

- **Frontend**: Razor Pages (.cshtml) rendered server-side  
- **Backend**: ASP.NET Core (C#) handling business logic and data access  
- **Database**: SQLite (`locknshop-dev.db`) managed via Entity Framework Core

### Architecture

**Frontend**:
- **Technology**: ASP.NET Core Razor Pages, responsive layout with Bootstrap and FontAwesome  
- **Features**: product/category display, cart, orders, registration/login, checkout  
- **Communication**: fgrontend communicates with database only through backend  

**Backend**:
- **Technology**: ASP.NET Core (C#), Razor Pages with code-behind (.cshtml.cs)  
- **Features**: authentication/authorization (ASP.NET Identity), business logic, data access via Entity Framework Core  

**Database**:
- **Technology**: SQLite (local file), managed with Entity Framework Core  
- **Main tables**: Users (Identity), Products, Categories, Cart, Orders

### Key features

1. **User authentication & management**
   - Managed internally with **ASP.NET Core Identity**  
   - Roles: User, Admin  
   - Session via authentication cookies  

2. **Products & categories**
   - CRUD operations for products and categories (Admin only)  
   - Viewable by all users  

3. **User cart**
   - Add/remove products, quantity management, price summary, discounts  

4. **Checkout & orders**
   - Form for personal details, address, and simulated payment  
   - Orders saved in local database; cart cleared after checkout  
   - Users can view their order history  

5. **Administration**
   - Manage users, roles, products, and categories (Admin only)  

### Security
- Authenticated users only can access cart, checkout, and orders  
- Admin-only management for products/categories  
- Passwords and sensitive data managed securely via Identity  

### Extensibility & scalability
- Can integrate payment gateways in the future (Stripe, PayPal)  
- Scalable to more robust databases (SQL Server, PostgreSQL) via EF Core  
- Architecture allows evolution towards cloud/enterprise-ready solutions  

### Technological choices

- **Razor Pages**: clear separation of frontend/backend, fast and secure development  
- **Entity Framework + SQLite**: simple database management, easy migration to robust DB  
- **ASP.NET Core Identity**: secure authentication, role management, easily extendable  
- **Internal cart/order management**: full control of business logic and user data  
- **Bootstrap/FontAwesome**: modern, responsive UI  

### Differences from advanced/cloud solution

| Aspect                | Current solution (local)            | Advanced/cloud solution        |
|-----------------------|------------------------------------|-------------------------------|
| Frontend              | Razor Pages server-side            | SPA                           |
| Backend               | Razor Pages                        | Web API RESTful               |
| Authentication        | Cookie-based Identity              | JWT, MFA, Microsoft Entra ID  |
| Database              | Local SQLite                       | Azure SQL Database             |
| Security              | Basic server-side                  | Rate limiting, CSP, Key Vault, logging |
| Deployment            | Local/simple                       | Azure App Service / Static Web App |
| Scalability           | Limited                            | Horizontal and vertical       |


### Suggested test Suite
- **Unit tests**: verify critical functions (cart totals, discounts, business logic)  
- **Integration tests**: ensure backend, database, and external services work together  
- **End-to-End (E2E) tests**: simulate real user flows (registration, login, checkout)  
- **Security tests**: authentication/roles, rate limiting, XSS, secure uploads, CSP  

**Tools**: MSTest/xUnit, Visual Studio Test Explorer, Playwright for .NET, Azure DevOps Test Plans, Application Insights

**Note**: this solution was developed under **time and resource constraints** during the Hackathon at CRIF Bologna. It represents a functional, demonstrable product while remaining prepared for cloud.
