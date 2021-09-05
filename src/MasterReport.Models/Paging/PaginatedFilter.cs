using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MasterReport.Models.Paging
{
    public class PaginatedFilter<T> : List<T>
    {
        public PaginatedFilter()
        {
            
        }
        public PaginatedFilter(List<T> itens, int count, int pagina, int pageSize)
        {
            TotalResults = count;
            Page = pagina;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(itens);
        }

        public string Filter { get; set; } = "";
        public int Page { get; private set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalResults { get; }
        public int TotalPages { get; }
        public bool HasPreviousPage => (Page > 1);
        public bool HasNextPage => (Page > TotalPages);
        public bool HasItems => this.Any();
        public static async Task<PaginatedFilter<T>> PaginarAsync(IQueryable<T> qry, int page, int pageSize)
        {
            var count = await qry.CountAsync();

            var registos = await qry.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedFilter<T>(registos, count, page, pageSize);
        }

    }
}