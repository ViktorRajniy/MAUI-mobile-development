namespace PopNomerSem
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// Main page of application.
    /// </summary>
    public partial class MainPage : ContentPage
    {
        /// <summary>
        /// All user notes in application.
        /// </summary>
        private List<Note> _notes = [
            new Note {Title = "qwe"},
            new Note {Title = "wer"},
            new Note {Title = "ert"},
            ];

        /// <summary>
        /// Notes that user see in main page.
        /// </summary>
        public ObservableCollection<Note> ShownNotes = [];

        /// <summary>
        /// Initialise main page.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            foreach (var note in _notes)
            {
                ShownNotes.Add(note);
            }
            NotesList.ItemsSource = ShownNotes;
        }

        /// <summary>
        /// Update note list. Show all user's notes.
        /// </summary>
        public void UpdateNoteList()
        {
            UpdateShowsNotes(_notes);
        }

        /// <summary>
        /// Update one note.
        /// </summary>
        /// <param name="oldNote">Old note that have to edit.</param>
        /// <param name="newNote">New version of note.</param>
        public void UpdateNote(Note oldNote, Note newNote)
        {
            var index = _notes.IndexOf(oldNote);
            if (index != -1)
            {
                _notes[index] = newNote;
            }
            UpdateNoteList();
        }

        /// <summary>
        /// Add new note.
        /// </summary>
        /// <param name="note">New note.</param>
        public void AddNote(Note note)
        {
            _notes.Add(note);
            UpdateNoteList();
        }

        /// <summary>
        /// Update list of notes that user see.
        /// </summary>
        /// <param name="newNotes">New list of notes.</param>
        private void UpdateShowsNotes(List<Note> newNotes)
        {
            ShownNotes.Clear();
            foreach (var note in newNotes)
            {
                ShownNotes.Add(note);
            }
        }

        /// <summary>
        /// Action when search bar is edited.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event args.</param>
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {            
            var search = e.NewTextValue?.ToLower();
            if(search.Equals(string.Empty))
            {
                UpdateShowsNotes(_notes);
            }
            else
            {
                var filteredNotes = _notes
                    .Where(n => n.Title.ToLower().Contains(search))
                    .ToList();
                UpdateShowsNotes(filteredNotes);
            }
        }

        /// <summary>
        /// Action when New-note-button clicked.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event args.</param>
        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NotePage(this));
        }
    }
}
