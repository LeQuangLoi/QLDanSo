using HTTTQLDanSo.Constants;
using System;
using System.Data.Common;

namespace HTTTQLDanSo.DataManagerment
{
    public abstract class SqlRepository
    {
        private readonly IDatabaseFactory _dataFactory;

        /// <summary>
        /// Instantiate an object of <see cref="SqlRepository"/>
        /// </summary>
        /// <param name="dataFactory">The data factory</param>
        protected SqlRepository(IDatabaseFactory dataFactory)
        {
            _dataFactory = dataFactory ?? throw new ArgumentNullException(nameof(dataFactory));
        }

        public DbConnection CreateConnection()
        {
            return _dataFactory.GetDbConnection(AppSettings.PrimaryConnection);
        }

        protected string LikeInput(string input)
        {
            return string.IsNullOrEmpty(input) ? "%" : $"%{input}%";
        }
    }
}