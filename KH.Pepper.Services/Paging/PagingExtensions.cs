using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KH.Pepper.Core.AppServices
{
    public static class PagingExtensions
    {
        public static async Task<PagedResult<TResult>> GetPaged<TEntity, TResult>(
            this IQueryable<TEntity> query,
            int page,
            int pageSize,
            string sortBy,
            bool sortDesc,
            CancellationToken cancellationToken,
            Func<IEnumerable<TEntity>, IEnumerable<TResult>> transform
            )
        {
            var result = new PagedResult<TResult>
            {
                CurrentPage = page,
                PageSize = pageSize,
                SortBy = sortBy,
                SortDesc = sortDesc,
                RowCount = await query.CountAsync(cancellationToken)
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;

            var results = query
                           .OrderByDynamic(sortBy, sortDesc)
                           .Skip(skip)
                           .Take(pageSize);

            result.Results = transform(results).ToList();

            return result;
        }

        /// <summary>
        /// Method needed to allow dynamic sorting by passing a string so entity framework can translate it to the correct SQL query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="sortBy"></param>
        /// <param name="isSortDescending"></param>
        /// <returns></returns>
        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> queryable, string sortBy, bool isSortDescending)
        {
            if (!string.IsNullOrEmpty(sortBy))
            {
                var param = Expression.Parameter(typeof(T), "p");
                var prop = Expression.Property(param, MapSortingColumnToSql(sortBy));
                var exp = Expression.Lambda(prop, param);
                string method = isSortDescending ? "OrderByDescending" : "OrderBy";
                Type[] types = new Type[] { queryable.ElementType, exp.Body.Type };
                var mce = Expression.Call(typeof(Queryable), method, types,queryable.Expression,exp);
                return queryable.Provider.CreateQuery<T>(mce);
            }
            return queryable;
        }

        /// <summary>
        /// To map DTO columns to sql columns for sorting
        /// </summary>
        /// <param name="sortByColumn"></param>
        /// <returns></returns>
        private static string MapSortingColumnToSql(string sortByColumn)
        {
            return DtoSqlColumnMapping.Any() && DtoSqlColumnMapping.ContainsKey(sortByColumn) ? DtoSqlColumnMapping[sortByColumn] : sortByColumn;
        }

        /// <summary>
        /// to store dto to sql column mapping for sorting purpose
        /// </summary>
        public static Dictionary<string, string> DtoSqlColumnMapping = new Dictionary<string, string>
        {
        };
    }

}
