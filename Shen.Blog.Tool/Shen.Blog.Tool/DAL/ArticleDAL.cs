using Shen.Blog.Tool.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shen.Blog.Tool.DAL
{
    /// <summary>
    /// 文章数据访问
    /// </summary>
    class ArticleDAL
    {
        public static List<Article> GetAllNoContent()
        {
            string sql = "SELECT CategoryId,CreateTime,HasChange,Id,Title FROM Articles ORDER BY CreateTime DESC";

            using (var reader = DBHelper.ExecuteReader(sql))
            {
                List<Article> articles = new List<Article>();
                while (reader.Read())
                {
                    articles.Add(new Article()
                    {
                        CategoryId = (long)reader["CategoryId"],
                        CreateTime = (DateTime)reader["CreateTime"],
                        HasChange = (bool)reader["HasChange"],
                        Id = (long)reader["Id"],
                        Title = reader["Title"].ToString()
                    });
                }

                return articles;
            }
        }

        public static List<Article> GetAll()
        {
            string sql = "SELECT CategoryId,CreateTime,HasChange,Id,Title,Content FROM Articles ORDER BY CreateTime DESC";

            using (var reader = DBHelper.ExecuteReader(sql))
            {
                List<Article> articles = new List<Article>();
                while (reader.Read())
                {
                    articles.Add(new Article()
                    {
                        CategoryId = (long)reader["CategoryId"],
                        CreateTime = (DateTime)reader["CreateTime"],
                        HasChange = (bool)reader["HasChange"],
                        Id = (long)reader["Id"],
                        Title = reader["Title"].ToString(),
                        Content = reader["Content"].ToString()
                    });
                }

                return articles;
            }
        }

        public static Article Get(long id)
        {
            string sql = "SELECT CategoryId,CreateTime,HasChange" +
                ",Id,Title,Content,Summary FROM Articles WHERE Id= @Id ORDER BY CreateTime DESC";

            using (var reader = DBHelper.ExecuteReader(sql, new SQLiteParameter("@Id", id)))
            {
                if (reader.Read())
                {
                    return new Article()
                    {
                        CategoryId = (long)reader["CategoryId"],
                        CreateTime = (DateTime)reader["CreateTime"],
                        HasChange = (bool)reader["HasChange"],
                        Id = (long)reader["Id"],
                        Title = reader["Title"].ToString(),
                        Content = reader["Content"].ToString(),
                        Summary = reader["Summary"].ToString()
                    };
                }
                return null;
            }
        }

        public static void Delete(long id)
        {
            string sql = "DELETE FROM Articles WHERE Id = @Id";

            DBHelper.ExecuteNoneQuery(sql, new SQLiteParameter("@Id", id));
        }

        public static void Insert(Article article)
        {
            article.CreateTime = DateTime.Now;

            string sql = "INSERT INTO Articles(Title,Content,CategoryId,CreateTime,HasChange,Summary)"
                + " VALUES(@Title,@Content,@CategoryId,@CreateTime,1,@Summary);"
                + "SELECT last_insert_rowid();";

            SQLiteParameter[] sParams = new SQLiteParameter[] {
                new SQLiteParameter("@Title",article.Title),
                new SQLiteParameter("@Content",article.Content),
                new SQLiteParameter("@CategoryId",article.CategoryId),
                new SQLiteParameter("@CreateTime",article.CreateTime),
                new SQLiteParameter("@Summary",article.Summary),
            };

            object id = DBHelper.ExecuteScalar(sql, sParams);
            article.Id = (long)id;
        }

        public static void Update(Article article)
        {
            string sql = "UPDATE Articles SET Content=@Content, Title=@Title, Summary = @Summary"
                + ", CategoryId = @CategoryId, HasChange = 1 WHERE Id = @Id";

            SQLiteParameter[] sParams = new SQLiteParameter[] {
                new SQLiteParameter("@Title",article.Title),
                new SQLiteParameter("@Content",article.Content),
                new SQLiteParameter("@CategoryId",article.CategoryId),
                new SQLiteParameter("@Summary",article.Summary),
                new SQLiteParameter("@Id",article.Id)
            };

            DBHelper.ExecuteNoneQuery(sql, sParams);
        }

        public static void SetNoHasChange(long id)
        {
            string sql = "UPDATE Articles SET HasChange = 1 WHERE Id = @Id";

            DBHelper.ExecuteNoneQuery(sql, new SQLiteParameter("Id", id));
        }
    }
}
