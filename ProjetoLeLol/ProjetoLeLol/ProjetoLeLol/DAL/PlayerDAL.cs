using MySql.Data.MySqlClient;
using ProjetoLeLol.Models;
using ProjetoLeLol.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.DAL
{
    public class PlayerDAL : BaseDAL
    {
        public Player PlayerLogin(string email)
        {            
            MySqlCommand cmd = new MySqlCommand("proc_player_login", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_email", email));

            Player player = null;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    player = new Player();

                    player.Id = (int)reader.GetValue(0);
                    player.Password = reader.GetValue(1).ToString();
                }
            }
            catch (Exception e)
            {
                conn.Close();
                LogError.GenerateError("PlayerDAL", "PlayerLogin", e.InnerException.ToString());
            }
            finally
            {
                conn.Close();
            }

            return player;
        }

        public Player PlayerSelect(int id_player)
        {
            MySqlCommand cmd = new MySqlCommand("proc_player_select", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_player", id_player));

            Player player = new Player();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    player.Id = id_player;
                    player.Name = reader.GetValue(1).ToString();
                    player.Birthday = DateTime.Parse(reader.GetValue(2).ToString());
                    player.Password = reader.GetValue(3).ToString();
                    player.Nick = reader.GetValue(4).ToString();
                    player.DateCreated = DateTime.Parse(reader.GetValue(5).ToString());
                    player.Email = reader.GetValue(6).ToString();
                    player.Skype = reader.GetValue(7).ToString();
                    player.Champion = (Champions)Enum.Parse(typeof(Champions), reader.GetValue(8).ToString(), true);
                    player.Role1 = (Roles)Enum.Parse(typeof(Roles), reader.GetValue(9).ToString(), true);
                    player.Role2 = (Roles)Enum.Parse(typeof(Roles), reader.GetValue(10).ToString(), true);
                    player.IsCaptain = reader.GetBoolean(11);
                    player.IsSubCaptain = reader.GetBoolean(12);
                    player.IdRiot = long.Parse(reader.GetValue(14).ToString());
                    player.Admin = reader.GetBoolean(16);
                    player.State = (States)Enum.Parse(typeof(States), reader.GetValue(17).ToString(), true);
                    player.Schedule = (Schedule)Enum.Parse(typeof(Schedule), reader.GetValue(18).ToString(), true);
                    player.Img = reader.GetValue(19).ToString();
                    player.Schedule2 = (Schedule)Enum.Parse(typeof(Schedule), reader.GetValue(20).ToString(), true);
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

            return player;
        }

        public bool PlayerCreate(Player p)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_player_create", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_name", p.Name));
            cmd.Parameters.Add(new MySqlParameter("p_birthday", p.Birthday));
            cmd.Parameters.Add(new MySqlParameter("p_password", p.Password));
            cmd.Parameters.Add(new MySqlParameter("p_nick", p.Nick));
            cmd.Parameters.Add(new MySqlParameter("p_dateCreate", DateTime.Now));
            cmd.Parameters.Add(new MySqlParameter("p_email", p.Email));
            cmd.Parameters.Add(new MySqlParameter("p_skype", p.Skype));
            cmd.Parameters.Add(new MySqlParameter("p_champion", p.Champion.ToString()));
            cmd.Parameters.Add(new MySqlParameter("p_role1", p.Role1.ToString()));
            cmd.Parameters.Add(new MySqlParameter("p_role2", p.Role2.ToString()));
            cmd.Parameters.Add(new MySqlParameter("p_id_riot", p.IdRiot));
            cmd.Parameters.Add(new MySqlParameter("p_state", p.State.ToString()));
            cmd.Parameters.Add(new MySqlParameter("p_timeTable", p.Schedule.ToString()));
            cmd.Parameters.Add(new MySqlParameter("p_img", p.Img));
            cmd.Parameters.Add(new MySqlParameter("p_timeTable2", p.Schedule2.ToString()));

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

        public bool PlayerDelete(int id_player)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_player_delete", conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_player", id_player));

            Player player = new Player();

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                conn.Open();
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public List<Player> PlayerAvailabeSelect()
        {
            MySqlCommand cmd = new MySqlCommand("proc_player_list", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;

            List<Player> players = new List<Player>();
            Player player = new Player();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    player.Id = (int)reader.GetValue(0);
                    player.Name = reader.GetValue(1).ToString();
                    player.Birthday = DateTime.Parse(reader.GetValue(2).ToString());
                    player.Nick = reader.GetValue(4).ToString();
                    player.Email = reader.GetValue(6).ToString();
                    player.Skype = reader.GetValue(7).ToString();
                    player.Champion = (Champions)Enum.Parse(typeof(Champions), reader.GetValue(8).ToString(), true);
                    player.Role1 = (Roles)Enum.Parse(typeof(Roles), reader.GetValue(9).ToString(), true);
                    player.Role2 = (Roles)Enum.Parse(typeof(Roles), reader.GetValue(10).ToString(), true);
                    player.IdRiot = long.Parse(reader.GetValue(14).ToString());
                    player.State = (States)Enum.Parse(typeof(States), reader.GetValue(17).ToString(), true);
                    player.Schedule = (Schedule)Enum.Parse(typeof(Schedule), reader.GetValue(18).ToString(), true);
                    player.Schedule = (Schedule)Enum.Parse(typeof(Schedule), reader.GetValue(20).ToString(), true);

                    players.Add(player);
                    player = new Player();
                }
            }
            catch (Exception e)
            {
                LogError.GenerateError("PlayerDAL", "PlayerAvailabeSelect", e.InnerException.ToString());
                conn.Close();
            }
            finally
            {
                conn.Close();
            }

            return players;
        }

        public bool PlayerEdit(Player p)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_player_edit", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id", p.Id));
            cmd.Parameters.Add(new MySqlParameter("p_name", p.Name));
            cmd.Parameters.Add(new MySqlParameter("p_birthday", p.Birthday));
            cmd.Parameters.Add(new MySqlParameter("p_skype", p.Skype));
            cmd.Parameters.Add(new MySqlParameter("p_champion", p.Champion.ToString()));
            cmd.Parameters.Add(new MySqlParameter("p_role1", p.Role1.ToString()));
            cmd.Parameters.Add(new MySqlParameter("p_role2", p.Role2.ToString()));
            cmd.Parameters.Add(new MySqlParameter("p_state", p.State.ToString()));
            cmd.Parameters.Add(new MySqlParameter("p_timeTable", p.Schedule.ToString()));
            cmd.Parameters.Add(new MySqlParameter("p_img", p.Img));
            cmd.Parameters.Add(new MySqlParameter("p_timeTable2", p.Schedule2.ToString()));

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

        public bool PlayerVerifyNick(string nick)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_player_select_nick", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_nick", nick));

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

        public List<Player> PlayerAvailableSelectAdmin()
        {
            MySqlCommand cmd = new MySqlCommand("proc_player_list_admin", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;

            List<Player> players = new List<Player>();
            Player player = new Player();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    player.Id = (int)reader.GetValue(0);
                    player.Name = reader.GetValue(1).ToString();
                    player.Birthday = DateTime.Parse(reader.GetValue(2).ToString());
                    player.Nick = reader.GetValue(4).ToString();
                    player.Email = reader.GetValue(6).ToString();
                    player.Skype = reader.GetValue(7).ToString();
                    player.Champion = (Champions)Enum.Parse(typeof(Champions), reader.GetValue(8).ToString(), true);
                    player.Role1 = (Roles)Enum.Parse(typeof(Roles), reader.GetValue(9).ToString(), true);
                    player.Role2 = (Roles)Enum.Parse(typeof(Roles), reader.GetValue(10).ToString(), true);
                    player.State = (States)Enum.Parse(typeof(States), reader.GetValue(17).ToString(), true);
                    player.Schedule = (Schedule)Enum.Parse(typeof(Schedule), reader.GetValue(18).ToString(), true);
                    player.Schedule2 = (Schedule)Enum.Parse(typeof(Schedule), reader.GetValue(20).ToString(), true);

                    players.Add(player);
                    player = new Player();
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

            return players;
        }

        public bool PlayerChangePassword(int id_player, string newPass)
        {
            MySqlCommand cmd = new MySqlCommand("proc_player_change_password", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_player", id_player));
            cmd.Parameters.Add(new MySqlParameter("p_new_pass", newPass));

            bool result = true;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                result = true;
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public bool PlayerVerifyEmail(string email)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_player_select_email", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_email", email));

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    result = true;
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

            return result;
        }

        public Player PlayerSelectByEmail(string email)
        {
            Player player = new Player();

            MySqlCommand cmd = new MySqlCommand("proc_player_select_email", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_email", email));

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    player.Id = (int)reader.GetValue(0);
                    player.Name = reader.GetValue(1).ToString();
                    player.Birthday = DateTime.Parse(reader.GetValue(2).ToString());
                    player.Nick = reader.GetValue(4).ToString();
                    player.Email = reader.GetValue(6).ToString();
                    player.Skype = reader.GetValue(7).ToString();
                    player.Champion = (Champions)Enum.Parse(typeof(Champions), reader.GetValue(8).ToString(), true);
                    player.Role1 = (Roles)Enum.Parse(typeof(Roles), reader.GetValue(9).ToString(), true);
                    player.Role2 = (Roles)Enum.Parse(typeof(Roles), reader.GetValue(10).ToString(), true);
                    player.IdRiot = long.Parse(reader.GetValue(14).ToString());
                    player.State = (States)Enum.Parse(typeof(States), reader.GetValue(17).ToString(), true);
                    player.Schedule = (Schedule)Enum.Parse(typeof(Schedule), reader.GetValue(18).ToString(), true);
                    player.Schedule = (Schedule)Enum.Parse(typeof(Schedule), reader.GetValue(20).ToString(), true);
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

            return player;
        }

        public bool PlayerEditNick(Player p)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_player_edit_nick", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id", p.Id));
            cmd.Parameters.Add(new MySqlParameter("p_nick", p.Nick));

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

        public List<Player> PlayerSearch(Roles role, Schedule schedule, States state, string division)
        {
            MySqlCommand cmd = new MySqlCommand("proc_player_search", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_role", role));
            cmd.Parameters.Add(new MySqlParameter("p_timeTable", schedule));
            cmd.Parameters.Add(new MySqlParameter("p_state", state));
            cmd.Parameters.Add(new MySqlParameter("p_tier", division));

            List<Player> players = new List<Player>();
            Player player = new Player();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    player.PlayerStatistics = new PlayerStatistics();

                    player.Id = (int)reader.GetValue(0);
                    player.Nick = reader.GetString(1);
                    player.Role1 = (Roles)Enum.Parse(typeof(Roles), reader.GetValue(2).ToString(), true);
                    player.Role2 = (Roles)Enum.Parse(typeof(Roles), reader.GetValue(3).ToString(), true);
                    player.Schedule = (Schedule)Enum.Parse(typeof(Schedule), reader.GetValue(4).ToString(), true);
                    player.Schedule = (Schedule)Enum.Parse(typeof(Schedule), reader.GetValue(5).ToString(), true);
                    player.State = (States)Enum.Parse(typeof(States), reader.GetValue(6).ToString(), true);
                    player.PlayerStatistics.Tier = reader.GetString(7);
                    player.PlayerStatistics.Division = reader.GetString(8);

                    players.Add(player);
                    player = new Player();
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

            return players;
        }

        #region PlayerStatistics
        public bool PlayerStatisticsCreate(int id_player, PlayerStatistics ps)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_player_statistics_create", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_player", id_player));
            cmd.Parameters.Add(new MySqlParameter("p_tier", ps.Tier));
            cmd.Parameters.Add(new MySqlParameter("p_division", ps.Division));
            cmd.Parameters.Add(new MySqlParameter("p_wins", ps.Wins));
            cmd.Parameters.Add(new MySqlParameter("p_losses", ps.Losses));

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

        public PlayerStatistics PlayerStatisticsSelect(int id_player)
        {
            MySqlCommand cmd = new MySqlCommand("proc_player_statistics_select", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_player", id_player));

            PlayerStatistics ps = new PlayerStatistics();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    ps.Tier = reader.GetValue(2).ToString();
                    ps.Division = reader.GetValue(3).ToString();
                    ps.Wins = int.Parse(reader.GetValue(4).ToString());
                    ps.Losses = int.Parse(reader.GetValue(5).ToString());
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

            return ps;
        }

        public bool PlayerStatisticsUpdate(int id_player, PlayerStatistics ps)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_player_statistics_update", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_player", id_player));
            cmd.Parameters.Add(new MySqlParameter("p_tier", ps.Tier));
            cmd.Parameters.Add(new MySqlParameter("p_division", ps.Division));
            cmd.Parameters.Add(new MySqlParameter("p_wins", ps.Wins));
            cmd.Parameters.Add(new MySqlParameter("p_losses", ps.Losses));

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
        #endregion

        public List<Player> PlayerMatcherList()
        {
            MySqlCommand cmd = new MySqlCommand("proc_player_matcher_list", conn);
            MySqlDataReader reader;

            List<Player> players = new List<Player>();
            Player player = new Player();

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    player.PlayerStatistics = new PlayerStatistics();

                    player.Id = (int)reader.GetValue(0);
                    player.Nick = reader.GetString(1);
                    player.Role1 = (Roles)Enum.Parse(typeof(Roles), reader.GetValue(2).ToString(), true);
                    player.Role2 = (Roles)Enum.Parse(typeof(Roles), reader.GetValue(3).ToString(), true);
                    player.Schedule = (Schedule)Enum.Parse(typeof(Schedule), reader.GetValue(4).ToString(), true);
                    player.Schedule = (Schedule)Enum.Parse(typeof(Schedule), reader.GetValue(5).ToString(), true);
                    player.State = (States)Enum.Parse(typeof(States), reader.GetValue(6).ToString(), true);
                    player.PlayerStatistics.Tier = reader.GetString(7);
                    player.PlayerStatistics.Division = reader.GetString(8);

                    players.Add(player);
                    player = new Player();
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

            return players;
        }

        public bool PlayerMatcherUpdate(int id_player, bool matcher)
        {
            bool result = false;

            MySqlCommand cmd = new MySqlCommand("proc_player_matcher_update", conn);
            MySqlDataReader reader;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("p_id_player", id_player));
            cmd.Parameters.Add(new MySqlParameter("p_matcher", matcher));

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
    }
}