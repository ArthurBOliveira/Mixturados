using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.Models
{
    public class Notification
    {
        private int _id;
        private Player _receiver;
        private Player _sender;
        private string _text;
        private NotificationType _type;
        private bool _status;
        private DateTime _date;

        #region Attributes
        public Player Receiver
        {
            get { return _receiver; }
            set { _receiver = value; }
        }

        public Player Sender
        {
            get { return _sender; }
            set { _sender = value; }
        }

        public String Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public NotificationType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public bool Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        #endregion

        #region Builders
        public Notification() { }

        public Notification(Player receiver, Player sender, string text, NotificationType type, bool status) 
        {
            Receiver = receiver;
            Sender = sender;
            Text = text;
            Type = type;
            Status = status;
            Date = DateTime.Now;
        }
        #endregion
    }
}