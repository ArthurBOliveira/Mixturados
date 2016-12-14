using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.Models
{
    public class GameX1
    {
        private int _id;
        private Player _player1;
        private Player _player2;
        private DateTime _date;
        private Player _winner;
        private Player _loser;
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

        public Player Player1
        {
            get { return _player1; }
            set { _player1 = value; }
        }
        public Player Player2
        {
            get { return _player2; }
            set { _player2 = value; }
        }
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        public Player Winner
        {
            get { return _winner; }
            set { _winner = value; }
        }
        public Player Loser
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
        public GameX1() { }

        public GameX1(Player player1, Player player2, int round, bool isFinal, bool isThird, string tCode) 
        {
            _player1 = player1;
            _player2 = player2;
            _round = round;
            _isFinal = isFinal;
            _isThird = isThird;
            _tCode = tCode;
        }
        #endregion
    }
}