using ProjetoLeLol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.ViewModel
{
    public class HomeVM
    {
        public Championship CurrentChampionship = new Championship();
        public Championship CurrentChampionshipx1 = new Championship();
        public List<Team> TeamsMatcher = new List<Team>();
        public List<Player> PlayersMatcher = new List<Player>();

        public HomeVM() { }

        public HomeVM(Championship c, Championship cx1, List<Team> tm, List<Player> pm)
        {
            CurrentChampionship = c;
            CurrentChampionshipx1 = cx1;
            TeamsMatcher = tm;
            PlayersMatcher = pm;
        }
    }
}