using ProjetoLeLol.DAL;
using ProjetoLeLol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.BLL
{
    public class NotificationBLL
    {
        private NotificationDAL _notifDAL = new NotificationDAL();
        private TeamBLL _teamBLL = new TeamBLL();

        public bool TeamCallPlayer(int id_player_receiver, int id_player_sender, string sender_name, string team_name)
        {
            Notification n = new Notification();

            n.Receiver = new Player();
            n.Receiver.Id = id_player_receiver;

            n.Sender = new Player();
            n.Sender.Id = id_player_sender;

            n.Date = DateTime.Now;
            n.Type = NotificationType.teamCalling;
            n.Text = "Você foi convidado por " + sender_name + " a participar do time " + team_name;

            return _notifDAL.NotificationCreate(n);
        }

        public bool PlayerCallTeam(int id_player_receiver, int id_player_sender, string sender_name)
        {
            Notification n = new Notification();

            n.Receiver = new Player();
            n.Receiver.Id = id_player_receiver;

            n.Sender = new Player();
            n.Sender.Id = id_player_sender;

            n.Date = DateTime.Now;
            n.Type = NotificationType.playerCalling;
            n.Text = "O jogador " + sender_name + " possui interesse em participar do seu time.";

            return _notifDAL.NotificationCreate(n);
        }

        public List<Notification> NotificationList(Player currentPlayer)
        {
            return _notifDAL.NotificationList(currentPlayer);
        }

        public Notification NotificationSelect(int id_notif)
        {
            return _notifDAL.NotificationSelect(id_notif);
        }

        public bool NotificationAnswer(Notification n, bool answer)
        {
            bool result = false;
            Notification New = new Notification();

            #region Se o jogador aceitar
            if (answer)
            {
                //Se for um pedido do jogador ao time
                if (n.Type == NotificationType.playerCalling)
                {
                    Team t = _teamBLL.TeamSelectByPlayerCap(n.Receiver.Id);
                    bool aux = t.SubCaptain.Id == 0 ? false : true;
                    if (!_teamBLL.TeamIsFull(t.Id, aux) && !_teamBLL.TeamPlayerHasTeam(n.Sender.Id))
                    {
                        result = _teamBLL.TeamAddPlayer(t.Id, n.Sender.Id);
                        New = new Notification(n.Sender,
                                                n.Receiver,
                                                "Parabéns! O jogador " + n.Receiver.Name + " aceitou seu pedido e agora você faz parte do time " + t.Name + ".",
                                                NotificationType.alert,
                                                true);
                        result = _notifDAL.NotificationCreate(New);
                    }
                }
                //Se for um pedido do time ao jogador
                if (n.Type == NotificationType.teamCalling)
                {
                    Team t = _teamBLL.TeamSelectByPlayerCap(n.Sender.Id);
                    bool aux = t.SubCaptain.Id == 0 ? false : true;
                    if (!_teamBLL.TeamIsFull(t.Id, aux) && !_teamBLL.TeamPlayerHasTeam(n.Receiver.Id))
                    {
                        result = _teamBLL.TeamAddPlayer(t.Id, n.Receiver.Id);
                        New = new Notification(n.Sender,
                                                n.Receiver,
                                                "Parabéns! O jogador " + n.Receiver.Name + " aceitou seu pedido e agora ele faz parte do seu time.",
                                                NotificationType.alert,
                                                true);
                        result = _notifDAL.NotificationCreate(New);
                    }
                }
            }
            #endregion

            #region Se o jogador recusar
            else
            {
                //Se for um pedido do jogador ao time
                if (n.Type == NotificationType.playerCalling)
                {
                    Team t = _teamBLL.TeamSelectByPlayerCap(n.Sender.Id);
                    New = new Notification(n.Sender,
                                       n.Receiver,
                                       "Infelizmente o time " + t.Name + " recusou seu pedido para participar.",
                                       NotificationType.alert,
                                       true);
                    result = _notifDAL.NotificationCreate(New);
                }
                //Se for um pedido do time ao jogador
                if (n.Type == NotificationType.teamCalling)
                {
                    New = new Notification(n.Sender,
                                       n.Receiver,
                                       "Infelizmente o jogador " + n.Receiver.Name + " recusou seu pedido para participar de seu time.",
                                       NotificationType.alert,
                                       true);
                    result = _notifDAL.NotificationCreate(New);
                }
            }
            #endregion
            
            result = _notifDAL.NotificationDelete(n.Id);

            return result;
        }

        public bool NotificationDelete(int id_notif)
        {
            return _notifDAL.NotificationDelete(id_notif);
        }

        public bool NotificationCreate(int id_receiver, Player sender, string text, NotificationType notificationType)
        {
            bool result = false;

            Player aux = new Player();
            aux.Id = id_receiver;

            Notification n = new Notification(aux, sender, text, NotificationType.alert, true);
            result = _notifDAL.NotificationCreate(n);

            return result;
        }

        public bool NotificationHasNew(int id_player)
        {
            return _notifDAL.NotificationHasNew(id_player);
        }
    }
}