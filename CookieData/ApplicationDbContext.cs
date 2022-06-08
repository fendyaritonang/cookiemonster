using Microsoft.EntityFrameworkCore;

namespace CookieMonster.CookieData
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : base(opt)
        {

        }
    }
}