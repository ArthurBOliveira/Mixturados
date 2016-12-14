using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.Models
{
    public class League
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _prize;

        public string Prize
        {
            get { return _prize; }
            set { _prize = value; }
        }
        private string _linkRules;

        public string LinkRules
        {
            get { return _linkRules; }
            set { _linkRules = value; }
        }
    }
}