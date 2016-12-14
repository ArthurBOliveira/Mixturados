using Newtonsoft.Json.Linq;
using ProjetoLeLol.DAL;
using ProjetoLeLol.Models;
using ProjetoLeLol.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace ProjetoLeLol.BLL
{
    public class PlayerBLL
    {
        private PlayerDAL _playerDAL = new PlayerDAL();

        public Player PlayerLogin(string email, string password)
        {
            //PlayerRequestIdRiot("SirAyrus");

            Player playerLogado = _playerDAL.PlayerLogin(email);

            if (playerLogado != null)
            {
                if (Hash.ValidatePassword(password, playerLogado.Password))
                {
                    playerLogado = PlayerSelect(playerLogado.Id);
                }
                else
                {
                    playerLogado = null;
                }
            }

            return playerLogado;
        }

        public Player PlayerSelect(int id_player)
        {
            Player player = _playerDAL.PlayerSelect(id_player);

            player.PlayerStatistics = _playerDAL.PlayerStatisticsSelect(player.Id);

            return player;
        }

        public bool PlayerCreate(Player p)
        {
            bool result = false;

            p.Password = Hash.CreateHash(p.Password);

            p.IdRiot = PlayerRequestIdRiot(p.Nick);

            p.PlayerStatistics = PlayerStatisticsCreate(p.IdRiot);

            if (!_playerDAL.PlayerVerifyNick(p.Nick))
            {
                result = _playerDAL.PlayerCreate(p);
                result = _playerDAL.PlayerStatisticsCreate(p.Id, p.PlayerStatistics);
            }

            return result;
        }

        public bool PlayerDelete(int id_player)
        {
            bool result = _playerDAL.PlayerDelete(id_player);

            return result;
        }

        public List<Player> PlayerAvailableSelect()
        {
            List<Player> players = _playerDAL.PlayerAvailabeSelect();

            return players;
        }

        public bool PlayerEdit(Player p)
        {
            return _playerDAL.PlayerEdit(p);
        }     

        public List<Player> PlayerAvailableSelectAdmin()
        {
            List<Player> players = _playerDAL.PlayerAvailableSelectAdmin();

            return players;
        }

        public bool PlayerChangePassword(int id_player, string oldPass, string newPass)
        {
            bool result = false;

            Player p = _playerDAL.PlayerSelect(id_player);

            if (Hash.ValidatePassword(oldPass, p.Password))
            {
                result = _playerDAL.PlayerChangePassword(p.Id, Hash.CreateHash(newPass));
            }

            return result;
        }

        public bool PlayerVerifyEmail(string email)
        {
            return _playerDAL.PlayerVerifyEmail(email);
        }

        public bool PlayerStatisticsUploadAll()
        {
            bool result = false;

            List<Player> players = _playerDAL.PlayerAvailabeSelect();

            foreach (Player p in players)
            {
                PlayerStatistics ps = _playerDAL.PlayerStatisticsSelect(p.Id);

                if (ps.Wins == 0)
                {
                    ps = PlayerStatisticsCreate(p.IdRiot);

                    result = _playerDAL.PlayerStatisticsCreate(p.Id, ps);
                }
                else
                {
                    ps = PlayerStatisticsCreate(p.IdRiot);

                    result = _playerDAL.PlayerStatisticsUpdate(p.Id, ps);
                }
            }

            return result;
        }

        public bool PlayerNickUpdateAll()
        {
            bool result = true;

            List<Player> players = _playerDAL.PlayerAvailabeSelect();

            try
            {
                foreach (Player p in players)
                {
                    string aux = PlayerUpdateNickApi(p.IdRiot);

                    p.Nick = aux;

                    _playerDAL.PlayerEditNick(p);
                }
            }
            catch (Exception e)
            {
                result = false;
            }

            return result;
        }

        public bool PlayerForgetPassword(string email)
        {
            bool result = false;
            EmailHelper eh = new EmailHelper();

            Player p = _playerDAL.PlayerSelectByEmail(email);

            string pass = System.Web.Security.Membership.GeneratePassword(8, 0);

            pass.Replace(':', 'a');

            string hashpass = Hash.CreateHash(pass);

            result = _playerDAL.PlayerChangePassword(p.Id, hashpass);

            result = eh.SendEmail(p.Email, "Troca de Senha", "Sua senha foi alterada para: " + pass + " recomendamos que você altere esta senha assim que efetuar o login.");

            return result;
        }

        public List<Player> PlayerSearch(Roles role, Schedule schedule, States state, string division)
        {
            return _playerDAL.PlayerSearch(role, schedule, state, division);
        }

        private long PlayerRequestIdRiot(string nick)
        {
            string result;
            long idRiot = 0;

            WebRequest request = WebRequest.Create("https://br.api.pvp.net/api/lol/br/v1.4/summoner/by-name/" + nick + "?api_key=a7a1b04f-5788-4caa-87ea-0e691d0a6547");
            request.ContentType = "application/json; charset=utf-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                result = sr.ReadToEnd();
            }

            string[] aux = result.Split(':');
            aux = aux[2].Split(',');
            idRiot = long.Parse(aux[0]);

            return idRiot;
        }

        private PlayerStatistics PlayerStatisticsCreate(long idRiot)
        {
            string result;
            PlayerStatistics ps = new PlayerStatistics();

            WebRequest request = WebRequest.Create("https://br.api.pvp.net/api/lol/br/v2.5/league/by-summoner/" + idRiot + "/entry?api_key=a7a1b04f-5788-4caa-87ea-0e691d0a6547");
            request.ContentType = "application/json; charset=utf-8";

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                }

                string[] aux = result.Split(':');

                string[] tier = aux[3].Split(',');
                ps.Tier = tier[0].Split(',')[0];
                ps.Tier = ps.Tier.Split('\"')[1];

                string[] div = aux[8].Split(',');
                ps.Division = div[0].Split(',')[0];
                ps.Division = ps.Division.Split('\"')[1];

                string[] win = aux[10].Split(',');
                ps.Wins = int.Parse(win[0].Split(',')[0]);

                string[] los = aux[11].Split(',');
                ps.Losses = int.Parse(los[0].Split(',')[0]);
            }
            catch (Exception e)
            {
                ps = new PlayerStatistics("UNRANKED", "UNRANKED", 0, 0);
            }           

            return ps;
        }

        private string PlayerUpdateNickApi(long idRiot)
        {
            string result;

            WebRequest request = WebRequest.Create("https://br.api.pvp.net/api/lol/br/v1.4/summoner/" + idRiot + "?api_key=a7a1b04f-5788-4caa-87ea-0e691d0a6547");
            request.ContentType = "application/json; charset=utf-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                result = sr.ReadToEnd();
            }

            JObject json = JObject.Parse(result);

            result = json[idRiot.ToString()]["name"].ToString();

            return result;
        }

        public List<Player> PlayerMatcherList()
        {
            return _playerDAL.PlayerMatcherList();
        }

        public bool PlayerMatcherUpdate(int id_player, bool matcher)
        {
            return _playerDAL.PlayerMatcherUpdate(id_player, matcher);
        }
    }
}