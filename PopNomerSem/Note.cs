using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopNomerSem
{
    public class Note
    {
        private string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _text = string.Empty;
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        private DateTime _date = DateTime.Now;
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
    }
}
