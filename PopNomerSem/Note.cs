namespace PopNomerSem
{
    using SQLite;

    /// <summary>
    /// Note that user create.
    /// </summary>
    [Table("Note")]
    public class Note
    {
        /// <summary>
        /// Id of node.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

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
        /// Date of note creation.
        /// </summary>
        public DateTime DateOfCreation { get; set; } = DateTime.Now;

        /// <summary>
        /// New date of note.
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Scheduled date.
        /// </summary>
        public DateTime ScheduledDate { get; set; }
    }
}
