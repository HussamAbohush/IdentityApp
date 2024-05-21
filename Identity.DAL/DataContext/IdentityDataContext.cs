
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.DAL.DataContext;

public class IdentityDataContext : IdentityDbContext
{
    public IdentityDataContext(DbContextOptions<IdentityDataContext> options) : base(options)
    {
        
    }
}
