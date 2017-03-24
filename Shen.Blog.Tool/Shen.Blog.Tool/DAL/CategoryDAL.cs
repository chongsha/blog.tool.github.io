using Shen.Blog.Tool.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shen.Blog.Tool.DAL
{
    class CategoryDAL
    {
        public static List<Category> GetCategories()
        {
            string sql = "SELECT Id, Name, SortNum FROM Categories ORDER BY SortNum";

            using (var reader = DBHelper.ExecuteReader(sql))
            {
                List<Category> categories = new List<Category>();

                while (reader.Read())
                {
                    Category item = new Category();
                    item.Id = (long)reader["Id"];
                    item.Name = reader["Name"].ToString();
                    item.SortNum = (long)reader["SortNum"];

                    categories.Add(item);
                }

                return categories;
            }
        }

        public static void Update(Category category)
        {
            string sql = "UPDATE Categories SET Name = @Name, SortNum = @SortNum WHERE Id = @Id";

            DBHelper.ExecuteNoneQuery(sql, new SQLiteParameter("@Name", category.Name)
                , new SQLiteParameter("@SortNum", category.SortNum), new SQLiteParameter("@Id", category.Id));
        }

        public static void Insert(Category category)
        {
            string sql = @"INSERT INTO Categories(Name, SortNum) VALUES(@Name, @SortNum);
SELECT last_insert_rowid();";

            var id = DBHelper.ExecuteScalar(sql, new SQLiteParameter("@Name", category.Name)
                          , new SQLiteParameter("@SortNum", category.SortNum));

            category.Id = (long)id;

        }
    }
}
