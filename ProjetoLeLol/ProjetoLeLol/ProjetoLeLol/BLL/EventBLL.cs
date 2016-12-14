using ProjetoLeLol.DAL;
using ProjetoLeLol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.BLL
{
    public class EventBLL
    {
        private EventDAL _eventDAL = new EventDAL();

        public Event EventSelect(int id_event)
        {
            return _eventDAL.EventSelect(id_event);
        }

        public bool EventCreate(Event e)
        {
            return _eventDAL.EventCreate(e);
        }

        public List<Event> EventList()
        {
            return _eventDAL.EventList();
        }
    }
}