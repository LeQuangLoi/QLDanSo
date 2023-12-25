using System.Data.Common;

namespace HTTTQLDanSo.DataManagerment
{
    public interface IDatabaseFactory
    {
        DbConnection GetDbConnection (string connectionName);

    }
}
