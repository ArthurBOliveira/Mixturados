using ProjetoLeLol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.ViewModel
{
    public class PlayerProfileVM
    {
        public Player CurrentPlayer;
        public string Age;
        public Player ProfilePlayer;
        public Team PlayerTeam;

        public PlayerProfileVM(Player cp, Player pp, Team pt)
        {
            CurrentPlayer = cp;
            ProfilePlayer = pp;
            PlayerTeam = pt;
        }
    }
}