namespace PopNomerSem
{
    /// <summary>
    /// Note that user create.
    /// </summary>
    public class Note
    {
        /// <summary>
        /// Title of note.
        /// </summary>
        private string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        /// <summary>
        /// Text of note.
        /// </summary>
        private string _text = string.Empty;
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
        
        /// <summary>
        /// Date of note.
        /// </summary>
        private DateTime _date = DateTime.Now;
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
    }
}
