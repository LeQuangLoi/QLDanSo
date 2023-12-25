using HTTTQLDanSo.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HTTTQLDanSo.DataManagerment.EntityFramework
{
    public class HTTTQLDanSoDbContext : IdentityDbContext<ApplicationUser>
    {
        public HTTTQLDanSoDbContext() : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public static HTTTQLDanSoDbContext Create()
        {
            return new HTTTQLDanSoDbContext();
        }
    }
}