using ProjetoLeLol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.ViewModel
{
    public class TeamCreateVM
    {
        public ErrorView Err;
        public List<Schedule> Schedules;

        public TeamCreateVM(ErrorView err)
        {
            Err = err;
            Array _schedule = System.Enum.GetValues(typeof(Schedule));
            Schedules = _schedule.OfType<Schedule>().ToList();
        }
    }
}