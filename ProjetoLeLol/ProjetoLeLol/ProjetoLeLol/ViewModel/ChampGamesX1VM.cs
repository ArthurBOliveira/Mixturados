using ProjetoLeLol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.ViewModel
{
    public class ChampGamesX1VM
    {
        public List<List<GameX1>> GamesX1 = new List<List<GameX1>>();
        public Championship Champ = new Championship();
        public List<Player> Players = new List<Player>();
        public List<Player> TempPlayers = new List<Player>();
        public Player CurrentPlayer = new Player();
    }
}