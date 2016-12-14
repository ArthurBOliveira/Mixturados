using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLeLol.Models
{
    public class ErrorView
    {
        private bool _hasError;
        private string _msgError;

        public bool HasError
        {
            get { return _hasError; }
            set { _hasError = value; }
        }
        public string MsgError
        {
            get { return _msgError; }
            set { _msgError = value; }
        }

        public ErrorView() { }

        public ErrorView(bool hasError, string msgError)
        {
            _hasError = hasError;
            _msgError = msgError;
        }
    }
}