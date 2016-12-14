using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.Models
{
    public class NotificationText
    {
        private int _id;
        private string _text;

        #region Attributes
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
        #endregion

        #region Builders
        public NotificationText() { }
        #endregion
    }
}