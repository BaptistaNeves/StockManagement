using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Shared.Pagination
{
    public class PagedList<TEntidade> : List<TEntidade>
    {
        public PagedList(IEnumerable<TEntidade> items, int count, int pageNumber, 
                int pageSize)
        {
            CurrentPage = pageNumber;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);
            PageSize = pageNumber;
            TotalCount = count;
            AddRange(items);
        }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public static async Task<PagedList<TEntidade>> CreateAsync(IQueryable<TEntidade> source, 
            int pageNumer, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumer - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<TEntidade>(items, count, pageNumer, pageSize);
        }
    }
}
