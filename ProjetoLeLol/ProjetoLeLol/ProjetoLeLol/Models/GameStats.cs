using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.Models
{
    public class GameStats
    {
        private int _id;
        private int _id_match;
        private int _id_game;
        private string _link;
        private JObject _json;
        private List<GamePlayerStats> _gamePlayerStats;

        #region Attributes
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int Id_match
        {
            get { return _id_match; }
            set { _id_match = value; }
        }

        public string Link
        {
            get { return _link; }
            set { _link = value; }
        }

        public int Id_game
        {
            get { return _id_game; }
            set { _id_game = value; }
        }

        public JObject Json
        {
            get { return _json; }
            set { _json = value; }
        }

        public List<GamePlayerStats> GamePlayerStats
        {
            get { return _gamePlayerStats; }
            set { _gamePlayerStats = value; }
        }
        #endregion

        public GameStats() { }
    }
}