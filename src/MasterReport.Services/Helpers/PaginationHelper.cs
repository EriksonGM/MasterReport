using System.Linq;
using System.Threading.Tasks;
using MasterReport.Models.Paging;
using Microsoft.EntityFrameworkCore;

namespace MasterReport.Services.Helpers
{
    public static class PaginationHelper
    {
        public static async Task<PaginatedFilter<T>> Paginate<T>(this IQueryable<T> qry, int page, int take)
        {
            return new PaginatedFilter<T>(
                await qry.Skip((page - 1) * take)
                    .Take(take).ToListAsync(),
                await qry.CountAsync(),
                page,
                take);

            //return res;
        }

    }
}