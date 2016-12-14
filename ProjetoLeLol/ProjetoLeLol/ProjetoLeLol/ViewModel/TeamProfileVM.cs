using ProjetoLeLol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.ViewModel
{
    public class TeamProfileVM
    {
        public Team Team;
        public Player CurrentPlayer;

        public TeamProfileVM(Team t, Player p)
        {
            Team = t;
            CurrentPlayer = p;
        }
    }
}