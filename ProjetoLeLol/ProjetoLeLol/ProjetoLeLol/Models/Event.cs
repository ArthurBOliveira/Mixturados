using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.Models
{
    public class Event
    {
        private int _id;
        private string _name;
        private string _adress;
        private DateTime _date;
        private string _link;

        #region Atributtes
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Adress
        {
            get { return _adress; }
            set { _adress = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public string Link
        {
            get { return _link; }
            set { _link = value; }
        }
        #endregion

        public Event() { }

        public Event(string name, string adress, string date, string link)
        {
            Name = name;
            Adress = adress;
            Date = DateTime.Parse(date);
            Link = link;
        }
    }
}