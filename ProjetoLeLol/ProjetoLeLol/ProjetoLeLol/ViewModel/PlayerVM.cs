using ProjetoLeLol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.ViewModel
{
    public class PlayerVM
    {
        public List<Champions> Champions;
        public List<Roles> Roles;
        public List<States> States;
        public List<Schedule> Schedules;
        public ErrorView errorView;

        public PlayerVM(ErrorView err)
        {
            Array _champions = System.Enum.GetValues(typeof(Champions));
            Array _roles = System.Enum.GetValues(typeof(Roles));
            Array _states = System.Enum.GetValues(typeof(States));
            Array _schedule = System.Enum.GetValues(typeof(Schedule));

            Champions = _champions.OfType<Champions>().ToList();
            Roles = _roles.OfType<Roles>().ToList();
            States = _states.OfType<States>().ToList();
            Schedules = _schedule.OfType<Schedule>().ToList();

            errorView = err;
        }
    }
}