using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.Models
{
    public class League_team
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private int _id_league;

        public int Id_league
        {
            get { return _id_league; }
            set { _id_league = value; }
        }
        private Team team;

        public Team Team
        {
            get { return team; }
            set { team = value; }
        }
        private int _points;

        public int Points
        {
            get { return _points; }
            set { _points = value; }
        }
    }
}