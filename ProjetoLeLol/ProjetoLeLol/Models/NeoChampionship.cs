using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.Models
{
    public class NeoChampionship
    {
        private int _id;
        private string _name;
        private string _game;
        private string _type;
        private string _link;
        private string _details;

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

        public string Game
        {
            get { return _game; }
            set { _game = value; }
        }

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string Link
        {
            get { return _link; }
            set { _link = value; }
        }

        public NeoChampionship() { }
    }
}