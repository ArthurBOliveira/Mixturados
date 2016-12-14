using ProjetoLeLol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.Utility
{
    public class SessionHelper
    {
        private const string SessionCurrentPlayer = "CurrentPlayer";
        private const string SessionTemp = "Temp";

        private System.Web.SessionState.HttpSessionState _context;

        public SessionHelper(System.Web.SessionState.HttpSessionState stateBase)
        {
            _context = stateBase;
        }

        public string SessionId
        {
            get
            {
                return _context.SessionID;
            }
        }

        public Player CurrentPlayer
        {
            get
            {
                if (_context[SessionCurrentPlayer] == null)
                    _context[SessionCurrentPlayer] = new Player();
                return (Player)_context[SessionCurrentPlayer];
            }
            set
            {
                _context[SessionCurrentPlayer] = value;
            }
        }

        public bool Temp
        {
            get
            {
                if (_context[SessionTemp] == null)
                    _context[SessionTemp] = false;
                return (bool)_context[SessionTemp];
            }
            set
            {
                _context[SessionTemp] = value;
            }
        }
    }
}