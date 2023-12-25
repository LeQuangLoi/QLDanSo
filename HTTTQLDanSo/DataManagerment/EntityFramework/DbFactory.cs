using Autofac.Util;

namespace HTTTQLDanSo.DataManagerment.EntityFramework
{
    public class DbFactory : Disposable, IDbFactory
    {
        private HTTTQLDanSoDbContext dbContext;

        public HTTTQLDanSoDbContext Init()
        {
            return dbContext ?? (dbContext = new HTTTQLDanSoDbContext());
        }
    }
}