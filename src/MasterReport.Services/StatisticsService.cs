using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterReport.Data;
using MasterReport.Models;
using MasterReport.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace MasterReport.Services
{
    public interface IStatisticsService : IService
    {
        public Task<HomePageDTO> GetHomeStatistics();
    }
    public class StatisticsService : Service, IStatisticsService
    {
        public StatisticsService(DataContext db) : base(db)
        {
        }


        public async Task<HomePageDTO> GetHomeStatistics()
        {
            return new HomePageDTO
            {
                Users = await _db.Users.CountAsync(),
                Reports = await _db.Reports.CountAsync(),
                DataSources = await _db.DataSources.CountAsync(),
                EmailAccounts = await _db.EmailAccounts.CountAsync()
            };
        }
    }
}
