using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.Models
{
    public class PlayerStatistics
    {
        private string _tier;
        private string _division;
        private int _wins;
        private int _losses;

        #region Attributes
        public string Tier
        {
            get { return _tier; }
            set { _tier = value; }
        }

        public string Division
        {
            get { return _division; }
            set { _division = value; }
        }

        public int Wins
        {
            get { return _wins; }
            set { _wins = value; }
        }

        public int Losses
        {
            get { return _losses; }
            set { _losses = value; }
        }
        #endregion

        public PlayerStatistics() { }

        public PlayerStatistics(string tier, string division, int wins, int losses)
        {
            _tier = tier;
            _division = division;
            _wins = wins;
            _losses = losses;
        }
    }
}