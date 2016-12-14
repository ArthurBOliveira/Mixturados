using MySql.Data.MySqlClient;
using ProjetoLeLol.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.DAL
{
    public class NewsDAL : BaseDAL
    {
        public List<News> NewsList()
        {
            MySqlCommand cmd = new MySqlCommand("proc_news_list", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;

            List<News> news = new List<News>();
            News _news = new News();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    _news.Id = int.Parse(reader.GetString(0));
                    _news.Title = reader.GetValue(1).ToString();
                    _news.Date = DateTime.Parse(reader.GetValue(2).ToString());
                    _news.Content = reader.GetValue(3).ToString();
                    _news.Author = reader.GetValue(5).ToString();
                    _news.Link = reader.GetValue(6).ToString();
                    _news.Img = reader.GetValue(7).ToString();

                    news.Add(_news);
                    _news = new News();
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            return news;
        }

        public bool NewsCreate(News n, string nick)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_news_create", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_title", n.Title));
            cmd.Parameters.Add(new MySqlParameter("p_date", n.Date));
            cmd.Parameters.Add(new MySqlParameter("p_content", n.Content));
            cmd.Parameters.Add(new MySqlParameter("p_author", nick));
            cmd.Parameters.Add(new MySqlParameter("p_link", n.Link));
            cmd.Parameters.Add(new MySqlParameter("p_img", n.Img));

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                result = true;
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public News NewsSelect(int id_news)
        {
            MySqlCommand cmd = new MySqlCommand("proc_news_select", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_news", id_news));

            News news = new News();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    news.Id = int.Parse(reader.GetString(0));
                    news.Title = reader.GetValue(1).ToString();
                    news.Date = DateTime.Parse(reader.GetValue(2).ToString());
                    news.Content = reader.GetValue(3).ToString();
                    news.Author = reader.GetValue(5).ToString();
                    news.Link = reader.GetValue(6).ToString();
                    news.Img = reader.GetValue(7).ToString();
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            return news;
        }
    }
}