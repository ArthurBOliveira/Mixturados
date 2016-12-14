using MySql.Data.MySqlClient;
using ProjetoLeLol.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.DAL
{
    public class NotificationDAL : BaseDAL
    {
        private PlayerDAL _playerDAL = new PlayerDAL();

        public bool NotificationCreate(Notification n)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_notification_create", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_sender", n.Sender.Id));
            cmd.Parameters.Add(new MySqlParameter("p_receiver", n.Receiver.Id));
            cmd.Parameters.Add(new MySqlParameter("p_text", n.Text));
            cmd.Parameters.Add(new MySqlParameter("p_type", n.Type));
            cmd.Parameters.Add(new MySqlParameter("p_date", n.Date));
            
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                result = true;

            }
            catch (Exception e)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public List<Notification> NotificationList(Player CurrentPlayer)
        {
            MySqlCommand cmd = new MySqlCommand("proc_notification_list", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_receiver", CurrentPlayer.Id));

            List<Notification> notifications = new List<Notification>();
            Notification notification = new Notification();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    notification.Id = (int)reader.GetValue(0);
                    notification.Sender = _playerDAL.PlayerSelect((int)reader.GetValue(1));
                    notification.Receiver = CurrentPlayer;
                    notification.Text = reader.GetValue(3).ToString();
                    notification.Type = (NotificationType)Enum.Parse(typeof(NotificationType), reader.GetValue(4).ToString(), true);
                    notification.Status = reader.GetBoolean(5);
                    notification.Date = DateTime.Parse(reader.GetValue(6).ToString());

                    notifications.Add(notification);
                    notification = new Notification();
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

            return notifications;
        }

        public bool NotificationDelete(int id_notification)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_notification_delete", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_notification", id_notification));

            Player player = new Player();

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (Exception e)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public Notification NotificationSelect(int id_notif)
        {
            MySqlCommand cmd = new MySqlCommand("proc_notification_select", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_notification", id_notif));

            Notification notification = new Notification();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    notification.Id = id_notif;
                    notification.Sender = _playerDAL.PlayerSelect((int)reader.GetValue(1));
                    notification.Receiver = _playerDAL.PlayerSelect((int)reader.GetValue(2));
                    notification.Text = reader.GetValue(3).ToString();
                    notification.Type = (NotificationType)Enum.Parse(typeof(NotificationType), reader.GetValue(4).ToString(), true);
                    notification.Status = reader.GetBoolean(5);
                    notification.Date = DateTime.Parse(reader.GetValue(6).ToString());
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

            return notification;
        }

        public bool NotificationHasNew(int id_player)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_notification_new", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_player", id_player));

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                if(reader.HasRows)
                    result = true;

            }
            catch (Exception e)
            {
                conn.Close();
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
    }
}