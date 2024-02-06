using HTTTQLDanSo.DataManagerment.DataModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTTTQLDanSo.DataManagerment.Repositorys
{
    public interface IUserWorkerRepository
    {
        Task<IEnumerable<UserWorker>> GetUserWorkerByUserIdAsync(string userId);

        Task<bool> AddUserWorkerAsync(IEnumerable<UserWorker> UserWorkers);

        Task<bool> UpdateUserWorkerAsync(IEnumerable<UserWorker> UserWorkers);
    }
}