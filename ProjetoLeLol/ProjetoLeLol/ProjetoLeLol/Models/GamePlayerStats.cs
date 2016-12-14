using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.Models
{
    public class GamePlayerStats
    {
        private int _id;
        private int _id_gameStats;
        private int _id_player;
        private string _link;
        private JObject _json;

        #region Attributes
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int Id_gameStats
        {
            get { return _id_gameStats; }
            set { _id_gameStats = value; }
        }

        public int Id_player
        {
            get { return _id_player; }
            set { _id_player = value; }
        }

        public string Link
        {
            get { return _link; }
            set { _link = value; }
        }

        public JObject Json
        {
            get { return _json; }
            set { _json = value; }
        }
        #endregion

        public GamePlayerStats() { }
    }
}