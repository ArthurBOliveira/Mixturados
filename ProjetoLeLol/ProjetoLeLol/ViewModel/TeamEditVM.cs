using ProjetoLeLol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.ViewModel
{
    public class TeamEditVM
    {
        public Team Team;
        public Player CurrentPlayer;

        public TeamEditVM(Team t, Player cp)
        {
            Team = t;
            CurrentPlayer = cp;
        }
    }
}