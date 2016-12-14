using ProjetoLeLol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.ViewModel
{
	public class TeamEditVM
	{
        public Player CurrentPlayer;
        public Team Team;
        public List<Schedule> Schedules;
        public ErrorView Err;

        public TeamEditVM()
        {
            Array _schedule = System.Enum.GetValues(typeof(Schedule));
            Schedules = _schedule.OfType<Schedule>().ToList();
            Err = new ErrorView();
        }

        public TeamEditVM(Player currentPlayer, Team team)
        {
            Err = new ErrorView();
            CurrentPlayer = currentPlayer;
            Team = team;
            Array _schedule = System.Enum.GetValues(typeof(Schedule));
            Schedules = _schedule.OfType<Schedule>().ToList();
        }
	}
}