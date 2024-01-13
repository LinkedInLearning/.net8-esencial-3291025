using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Wpm.Web.Identity;

public class WpmIdentityDbContext(DbContextOptions<WpmIdentityDbContext> options) : IdentityDbContext<WpmIdentityUser>(options)
{
}

public class WpmIdentityUser : IdentityUser
{
}