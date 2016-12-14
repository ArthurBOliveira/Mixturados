using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.Models
{
    public class News
    {
        private int _id;
        private string _title;
        private DateTime _date;
        private string _content;
        private string _author;
        private string _link;
        private string _img;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }

        public string Link
        {
            get { return _link; }
            set { _link = value; }
        }

        public string Img
        {
            get { return _img; }
            set { _img = value; }
        }

        public News() { }

        public News(string title, DateTime date, string content, string link, string img)
        {
            _title = title;
            _date = date;
            _content = content;
            _link = link;
            _img = img;
        }
    }
}