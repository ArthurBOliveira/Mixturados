using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.ViewModel
{
    public class FullInfoPlayerVM
    {
        //Summoner's Info
        private int _id;
        private string _name;
        private string _profilePic;
        private string _level;

        //Summoner's Ranked
        private string _nameRanked;
        private int _leaguePoints;
        private string _tier;
        private string _division;
        private int _winsRanked;
        private int _lossRanked;
        private string _tierPic;
        private int _totalChampionKills;

        public int TotalChampionKills
        {
            get { return _totalChampionKills; }
            set { _totalChampionKills = value; }
        }
        private int _totalMinionKills;

        public int TotalMinionKills
        {
            get { return _totalMinionKills; }
            set { _totalMinionKills = value; }
        }
        private int _totalTurretsKilled;

        public int TotalTurretsKilled
        {
            get { return _totalTurretsKilled; }
            set { _totalTurretsKilled = value; }
        }
        private int _totalNeutralMinionsKilled;

        public int TotalNeutralMinionsKilled
        {
            get { return _totalNeutralMinionsKilled; }
            set { _totalNeutralMinionsKilled = value; }
        }
        private int _totalAssists;

        public int TotalAssists
        {
            get { return _totalAssists; }
            set { _totalAssists = value; }
        }

        //Summoner's Summary


        #region Attributes
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

        public string ProfilePic
        {
            get { return _profilePic; }
            set { _profilePic = value; }
        }

        public string Level
        {
            get { return _level; }
            set { _level = value; }
        }

        public string NameRanked
        {
            get { return _nameRanked; }
            set { _nameRanked = value; }
        }

        public int LeaguePoints
        {
            get { return _leaguePoints; }
            set { _leaguePoints = value; }
        }

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

        public int WinsRanked
        {
            get { return _winsRanked; }
            set { _winsRanked = value; }
        }

        public int LossRanked
        {
            get { return _lossRanked; }
            set { _lossRanked = value; }
        }

        public string TierPic
        {
            get { return _tierPic; }
            set { _tierPic = value; }
        }
        #endregion

        #region Methods

        #endregion
    }
}