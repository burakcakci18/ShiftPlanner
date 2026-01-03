using Common.Infrastructure.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Utils
{
    public static class ExecuteQuery
    {
        public static async Task<IList<T>> QueryWithCaseInsensitiveAsync<T>(AppDbContext context, string baseSql, object? parameters = null) where T : class
        {
            parameters ??= new { };

            var sqlParams = new List<SqlParameter>();

            foreach (var prop in parameters.GetType().GetProperties())
            {
                var name = prop.Name;
                var value = prop.GetValue(parameters) ?? DBNull.Value;

                sqlParams.Add(new SqlParameter($"@{name}", value));
            }

            return await context.Set<T>().FromSqlRaw(baseSql, sqlParams.ToArray()).ToListAsync();
        }

        public static async Task<T?> QuerySingleWithCaseInsensitiveAsync<T>(AppDbContext context, string baseSql, object? parameters = null) where T : class
        {
            parameters ??= new { };

            var sqlParams = new List<SqlParameter>();

            foreach (var prop in parameters.GetType().GetProperties())
            {
                var name = prop.Name;
                var value = prop.GetValue(parameters) ?? DBNull.Value;

                sqlParams.Add(new SqlParameter($"@{name}", value));
            }

            return await context.Set<T>().FromSqlRaw(baseSql, sqlParams.ToArray()).FirstOrDefaultAsync();
        }
    }
}
