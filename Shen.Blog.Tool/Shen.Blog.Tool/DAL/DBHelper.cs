using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;

namespace Shen.Blog.Tool.DAL
{
    class DBHelper
    {
        private static SQLiteConnection CreateConnection()
        {
            var connStr = ConfigurationManager.ConnectionStrings["sqlite"].ConnectionString;

            return new SQLiteConnection(connStr);
        }

        public static IDataReader ExecuteReader(string sql, params SQLiteParameter[] sqlParams)
        {
            var connection = CreateConnection();

            try
            {
                SQLiteCommand cmd = new SQLiteCommand(sql, connection);

                cmd.Parameters.AddRange(sqlParams);
                connection.Open();

                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                connection.Close();
                connection.Dispose();

                throw ex;
            }
        }

        public static int ExecuteNoneQuery(string sql, params SQLiteParameter[] sqlParams)
        {
            using (var connection = CreateConnection())
            {
                SQLiteCommand cmd = new SQLiteCommand(sql, connection);

                cmd.Parameters.AddRange(sqlParams);
                connection.Open();

                return cmd.ExecuteNonQuery();
            }
        }

        public static object ExecuteScalar(string sql, params SQLiteParameter[] sqlParams)
        {
            using (var connection = CreateConnection())
            {
                SQLiteCommand cmd = new SQLiteCommand(sql, connection);

                cmd.Parameters.AddRange(sqlParams);
                connection.Open();

                return cmd.ExecuteScalar();
            }
        }
    }
}
