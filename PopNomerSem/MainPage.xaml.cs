using System.Collections.ObjectModel;

namespace PopNomerSem
{
    public partial class MainPage : ContentPage
    {
        private List<Note> _notes = [
            new Note {Title = "qwe"},
            new Note {Title = "wer"},
            new Note {Title = "ert"},
            ];

        public ObservableCollection<Note> ShownNotes = [];

        public MainPage()
        {
            InitializeComponent();
            foreach (var note in _notes)
            {
                ShownNotes.Add(note);
            }
            NotesList.ItemsSource = ShownNotes;
        }

        public void UpdateNoteList()
        {
            ShownNotes.Clear();
            foreach (var note in _notes)
            {
                ShownNotes.Add(note);
            }
        }

        public void UpdateNote(Note oldNote, Note newNote)
        {
            var index = _notes.IndexOf(oldNote);
            if (index != -1)
            {
                _notes[index] = newNote;
            }
            UpdateNoteList();
        }
        public void AddNote(Note note)
        {
            _notes.Add(note);
            UpdateNoteList();
        }

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

        private void UpdateShowsNotes(List<Note> newNotes)
        {
            ShownNotes.Clear();
            foreach (var note in newNotes)
            {
                ShownNotes.Add(note);
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NotePage(this));
        }
    }
}
