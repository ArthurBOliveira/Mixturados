using ProjetoLeLol.DAL;
using ProjetoLeLol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.BLL
{
    public class NewsBLL
    {
        private NewsDAL _newsDAL = new NewsDAL();

        public List<News> NewsList()
        {
            return _newsDAL.NewsList();
        }

        public bool NewsCreate(News n, string nick)
        {
            return _newsDAL.NewsCreate(n, nick);
        }

        public News NewSelect(int id_news)
        {
            return _newsDAL.NewsSelect(id_news);
        }
    }
}