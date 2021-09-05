using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterReport.Data;
using MasterReport.Data.Entites;
using MasterReport.Models;
using MasterReport.Services.Base;
using MasterReport.Services.Enums;
using MasterReport.Services.Exceptions;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Data.SqlClient;
using System.Threading;
using MasterReport.Models.Paging;
using MasterReport.Services.Helpers;
using Microsoft.EntityFrameworkCore;

namespace MasterReport.Services
{
    public interface IDataSourceService : IService
    {
        public Task<PaginatedFilter<DataSourceDTO>> Get(string filter, int page ,int itens);
        public Task AddOrUpdate(DataSourceDTO dto);

        public Task Test(DataSourceDTO dto, CancellationToken cancel);

        public Task<List<DataSourceTypeDTO>> GetDataSourceTypes();

        public Task Remove(Guid id);
    }
    public class DataSourceService :Service, IDataSourceService
    {
        public DataSourceService(DataContext db) : base(db)
        {

        }

        public async Task<PaginatedFilter<DataSourceDTO>> Get(string filter, int page, int itens)
        {
            var qry = _db.DataSources
                .Include(x=>x.DataSourceType)
                .AsQueryable()
                .AsNoTracking();

            if (!string.IsNullOrEmpty(filter))
                qry = qry.Where(x => x.Name.Contains(filter));

            return await qry.OrderBy(x => x.Name)
                .Select(x=> new DataSourceDTO(x))
                .Paginate(page, itens);
        }

        public async Task AddOrUpdate(DataSourceDTO dto)
        {
            if (dto.DataSourceId.HasValue)
            {
                var ds = await _db.DataSources.FindAsync(dto.DataSourceId);

                if (ds == null)
                    throw new DataSourceNotFoundException();

                ds.Name = dto.Name;
                ds.ConnectionString = dto.ConnectionString;
                ds.DataSourceTypeId = dto.DataSourceTypeId;

                _db.Update(ds);
            }
            else
            {
                var dataSource = new DataSource
                {
                    DataSourceId = Guid.NewGuid(),
                    Name = dto.Name,
                    ConnectionString = dto.ConnectionString,
                    DataSourceTypeId = dto.DataSourceTypeId
                };

                await _db.DataSources.AddAsync(dataSource);
            }
        }

        public async Task Test(DataSourceDTO dto, CancellationToken cancel)
        {
            var dsType = (DataSourceTypeEnum) dto.DataSourceTypeId;

            switch (dsType)
            {
                case DataSourceTypeEnum.MSSQL:

                    await using (var sqlConn = new SqlConnection(dto.ConnectionString))
                    {
                        await sqlConn.OpenAsync(cancel);

                        var cmd = new SqlCommand("Select GetDate()", sqlConn);

                        await cmd.ExecuteNonQueryAsync(cancel);

                        await sqlConn.CloseAsync();
                    }
                    
                    break;
                case DataSourceTypeEnum.MySQL:
                    throw new NotImplementedException();

                case DataSourceTypeEnum.PostgreSQL:
                    throw new NotImplementedException();

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public async Task<List<DataSourceTypeDTO>> GetDataSourceTypes()
        {
            return await _db.DataSourceTypes.Select(x => new DataSourceTypeDTO(x)).ToListAsync();
        }

        public async Task Remove(Guid id)
        {
            if (await _db.Documents.AnyAsync(x => x.DataSourceId == id))
                throw new Exception("Data Source in use");


        }
    }
}
