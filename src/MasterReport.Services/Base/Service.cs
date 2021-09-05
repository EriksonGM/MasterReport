using System.Threading.Tasks;
using MasterReport.Data;

namespace MasterReport.Services.Base
{
    public interface IService
    {
        public int Save();

        public Task<int> SaveAsync();
    }
    public abstract class Service : IService
    {
        public DataContext _db;

        protected Service(DataContext db)
        {
            _db = db;
        }

        public int Save()
        {
            return _db.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }

}