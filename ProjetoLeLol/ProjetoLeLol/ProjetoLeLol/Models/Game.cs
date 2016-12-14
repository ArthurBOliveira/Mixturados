using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.Models
{
    public class Game
    {
        private int _id;
        private Team _team1;
        private Team _team2;
        private DateTime _date;
        private Team _winner;
        private Team _loser;
        private string _obs;
        private int _round;
        private bool _isFinal;
        private bool _isThird;
        private int _idMatch;
        private string _tCode;

        #region Attributes
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Team Team1
        {
            get { return _team1; }
            set { _team1 = value; }
        }
        public Team Team2
        {
            get { return _team2; }
            set { _team2 = value; }
        }
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        public Team Winner
        {
            get { return _winner; }
            set { _winner = value; }
        }
        public Team Loser
        {
            get { return _loser; }
            set { _loser = value; }
        }
        public string Obs
        {
            get { return _obs; }
            set { _obs = value; }
        }

        public int Round
        {
            get { return _round; }
            set { _round = value; }
        }

        public bool IsFinal
        {
            get { return _isFinal; }
            set { _isFinal = value; }
        }

        public bool IsThird
        {
            get { return _isThird; }
            set { _isThird = value; }
        }

        public int IdMatch
        {
            get { return _idMatch; }
            set { _idMatch = value; }
        }
        public string TCode
        {
            get { return _tCode; }
            set { _tCode = value; }
        }
        #endregion

        #region Builders
        public Game() { }

        public Game(Team team1, Team team2, int round, bool isFinal, bool isThird, string tCode) 
        {
            _team1 = team1;
            _team2 = team2;
            _round = round;
            _isFinal = isFinal;
            _isThird = isThird;
            _tCode = tCode;
        }
        #endregion
    }
}