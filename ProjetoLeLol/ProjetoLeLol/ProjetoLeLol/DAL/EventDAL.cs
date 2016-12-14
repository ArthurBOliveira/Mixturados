using MySql.Data.MySqlClient;
using ProjetoLeLol.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.DAL
{
    public class EventDAL : BaseDAL
    {
        public Event EventSelect(int id_event)
        {
            MySqlCommand cmd = new MySqlCommand("proc_event_select", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_event", id_event));

            Event _event = new Event();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    _event.Id = id_event;
                    _event.Name = reader.GetValue(1).ToString();
                    _event.Adress = reader.GetValue(2).ToString();
                    _event.Date = DateTime.Parse(reader.GetValue(3).ToString());
                    _event.Link = reader.GetValue(4).ToString();
                }
            }
            catch (Exception e)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }

            return _event;
        }

        public bool EventCreate(Event e)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_event_create", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_name", e.Name));
            cmd.Parameters.Add(new MySqlParameter("p_adress", e.Adress));
            cmd.Parameters.Add(new MySqlParameter("p_date", e.Date));
            cmd.Parameters.Add(new MySqlParameter("p_link", e.Link));

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                result = true;

            }
            catch (Exception ev)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public List<Event> EventList()
        {
            MySqlCommand cmd = new MySqlCommand("proc_event_list", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;

            List<Event> _events = new List<Event>();
            Event _event = new Event();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    _event.Id = (int)reader.GetValue(0);
                    _event.Name = reader.GetValue(1).ToString();
                    _event.Adress = reader.GetValue(2).ToString();
                    _event.Date = DateTime.Parse(reader.GetValue(3).ToString());
                    _event.Link = reader.GetValue(4).ToString();

                    _events.Add(_event);
                    _event = new Event();
                }
            }
            catch (Exception e)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }

            return _events;
        }
    }
}