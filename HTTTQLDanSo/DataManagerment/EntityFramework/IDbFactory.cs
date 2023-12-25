using System;

namespace HTTTQLDanSo.DataManagerment.EntityFramework
{
    public interface IDbFactory : IDisposable
    {
        HTTTQLDanSoDbContext Init();
    }
}