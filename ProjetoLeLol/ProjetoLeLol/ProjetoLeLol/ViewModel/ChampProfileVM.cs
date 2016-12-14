using ProjetoLeLol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.ViewModel
{
    public class ChampProfileVM
    {
        public Player CurrentPlayer;
        public Championship Championship;
        public ErrorView Err;

        public ChampProfileVM(Player p, Championship c)
        {
            CurrentPlayer = p;
            Championship = c;
            Err = new ErrorView();
        }
    }
}