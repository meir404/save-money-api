using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Attributes;
using Dapper;

namespace DAL.Services
{
    public class DataResolver
    {
        private readonly string ConnectionString;

        public DataResolver(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public async Task<IEnumerable<T>> GetAll<T>(string where, object parms)
        {
            IEnumerable<T> res;
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                var query = GenerateQuerySelect<T>(null) + (where ?? "");
                res = await connection.QueryAsync<T>(query, parms);
            }
            return res;
        }
        public async Task<T> GetSingle<T>(string where, object parms)
        {
            IEnumerable<T> res;
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                var query = GenerateQuerySelect<T>(" top 1 ") + (where != null ?" where " + where : "");
                res = await connection.QueryAsync<T>(query, parms);
            }
            return res.FirstOrDefault();
        }

        public async Task<int> AddOne<T>(T obj)
        {
            int res;
            var query = GenerateQueryInsert(obj);
            if (query == null) return 0;
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                res = await connection.QuerySingleAsync<int>(query, obj);
            }
            return res;
        }
        private string GenerateQueryInsert<T>(T obj)
        {
            var propInsert = new List<string>();
            var table = typeof(T);
            var props = table.GetProperties();
            for (int i = 0; i < props.Length; i++)
            {
                if (props[i].GetValue(obj) != null && props[i].Name != "Id")
                {
                    propInsert.Add(props[i].Name);
                }
            }
            if (propInsert.Count == 0) return null;
            var tableInfo = table.GetCustomAttributes(false).FirstOrDefault(a => a.GetType() == typeof(TableInfo)) as TableInfo;
            var query = "insert " + (tableInfo == null ? table.Name : tableInfo.Name);
            query += "( " + String.Join(",", propInsert) + ")";
            query += "select  @" + String.Join(",@", propInsert) + " ";
            query += " select @@IDENTITY";
            return query;
        }
        private string GenerateQuerySelect<T>(string take)
        {
            string query = "select " + take ?? "";
            var table = typeof(T);
            var props = table.GetProperties();
            for (int i = 0; i < props.Length; i++)
            {
                query += props[i].Name;
                if (i != props.Length - 1)
                    query += " ,";
            }

            var tableInfo = table.GetCustomAttributes(false).FirstOrDefault(a => a.GetType() == typeof(TableInfo)) as TableInfo;

            query += " from " + (tableInfo != null ? tableInfo.Name : table.Name);
            return query;
        }
    }
}
