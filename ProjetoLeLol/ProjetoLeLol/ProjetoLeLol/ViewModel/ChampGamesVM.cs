using ProjetoLeLol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.ViewModel
{
    public class ChampGamesVM
    {
        public List<List<Game>> Games = new List<List<Game>>();
        public Championship Champ = new Championship();
        public List<Team> Teams = new List<Team>();
        public List<Team> TempTeams = new List<Team>();
        public Player CurrentPlayer = new Player();
    }
}