using System.Collections.ObjectModel;

namespace PopNomerSem
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<Note> _notes = [
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

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {            
            var search = e.NewTextValue?.ToLower() ?? string.Empty;

            if(search.Equals(string.Empty))
            {
                ShownNotes.Clear();
                foreach (var note in _notes)
                {
                    ShownNotes.Add(note);
                }
            }
            else
            {
                var filteredNotes = _notes
                    .Where(n => n.Title.ToLower().Contains(search))
                    .ToList();

                ShownNotes.Clear();
                foreach (var note in filteredNotes)
                {
                    ShownNotes.Add(note);
                }
            }

        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}
