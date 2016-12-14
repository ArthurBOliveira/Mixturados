using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.Models
{
    public class NoLag
    {
        private int _id_no_lag;
        private int _id_player;
        private string _skype;
        private string _name;
        private string _email;
        private DateTime _date;

        public int Id_no_lag
        {
            get { return _id_no_lag; }
            set { _id_no_lag = value; }
        }

        public int Id_player
        {
            get { return _id_player; }
            set { _id_player = value; }
        }

        public string Skype
        {
            get { return _skype; }
            set { _skype = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public NoLag() { }

        public NoLag(int id_player, string skype, string name, string email, DateTime date)
        {
            _id_player = id_player;
            _skype = skype;
            _name = name;
            _email = email;
            _date = date;
        }
    }
}