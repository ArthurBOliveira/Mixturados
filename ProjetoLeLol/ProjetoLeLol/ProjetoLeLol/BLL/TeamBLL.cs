using ProjetoLeLol.DAL;
using ProjetoLeLol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.BLL
{
    public class TeamBLL
    {
        private TeamDAL _teamDAL;

        public TeamBLL()
        {
            _teamDAL = new TeamDAL();
        }

        public bool TeamCreate(Team t, int id_player)
        {
            int id_team = _teamDAL.TeamCreate(t);

            //bool result = _teamDAL.teamAddPlayer(id_team, id_player);

            bool result = _teamDAL.TeamUpdateToCaptain(id_player);

            return result;
        }

        public bool TeamDelete(int id_team, int id_player)
        {
            bool result = _teamDAL.teamDelete(id_team);

            result = _teamDAL.playerTeamDeleteByTeam(id_team);

            result = _teamDAL.TeamDemoteToCaptain(id_player);

            return result;
        }

        public bool TeamEdit(Team t)
        {
            return _teamDAL.TeamEdit(t);
        }

        public bool TeamAddPlayer(int id_team, int id_player)
        {
            bool result = _teamDAL.teamAddPlayer(id_team, id_player);

            return result;
        }

        public bool TeamRemPlayer(int id_player)
        {
            bool result = _teamDAL.teamRemPlayer(id_player);

            return result;
        }

        public List<Team> TeamAvailableSelect()
        {
            List<Team> players = _teamDAL.TeamAvailabeSelect();

            return players;
        }

        public Team TeamSelect(int id_team)
        {
            return _teamDAL.TeamSelect(id_team);
        }

        public Team TeamSelectByPlayerCap(int p)
        {
            Team t = _teamDAL.TeamSelectByPlayerCap(p);

            t.Players = _teamDAL.TeamListPlayer(t.Id);

            return t;
        }

        public Team TeamSelectByPlayerMember(int p)
        {
            Team t = _teamDAL.TeamSelectByPlayerMember(p);

            t.Players = _teamDAL.TeamListPlayer(t.Id);

            return t;
        }

        public List<Player> TeamListPlayer(int id_team)
        {
            return _teamDAL.TeamListPlayer(id_team);
        }

        public bool TeamPromoteSub(int id_player, int id_team)
        {
            bool result = _teamDAL.TeamPromoteSub(id_player, id_team);

            result = _teamDAL.PlayerPromoteSub(id_player);

            result = _teamDAL.teamRemPlayer(id_player);

            return result;
        }

        public bool TeamDemoteSub(int id_player, int id_team)
        {
            bool result = _teamDAL.TeamDemoteSub(id_team);

            result = _teamDAL.PlayerDemoteSub(id_player);

            result = _teamDAL.teamAddPlayer(id_team, id_player);

            return result;
        }

        public bool TeamSelfDemote(int id_player, int id_team)
        {
            bool result = _teamDAL.TeamDemoteSub(id_team);

            result = _teamDAL.PlayerDemoteSub(id_player);

            return result;
        }

        public bool TeamPlayerHasTeam(int id_player)
        {
            Team t = _teamDAL.TeamSelectByPlayerMember(id_player);
            if (t.Id != 0)
            {
                return true;
            }
            else
            {
                t = _teamDAL.TeamSelectByPlayerCap(id_player);
                if (t.Id != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //Verifica sem tem ja tem 7
        public bool TeamIsFull(int id_team, bool hasSub)
        {
            return _teamDAL.TeamIsFull(id_team, hasSub);
        }

        //Verifica sem tem no minimo 5
        public bool TeamCompleted(int id_team, bool hasSub)
        {
            return _teamDAL.TeamCompleted(id_team, hasSub);
        }

        public bool TeamNameVerify(string name)
        {
            return _teamDAL.TeamNameVerify(name);
        }

        public bool TeamTagVerify(string tag)
        {
            return _teamDAL.TeamTagVerify(tag);
        }

        public List<Team> TeamMatcherList()
        {
            return _teamDAL.TeamMatcherList();
        }

        public bool TeamMatcherUpdate(int id_team, bool matcher)
        {
            return _teamDAL.TeamMatcherUpdate(id_team, matcher);
        }
    }
}