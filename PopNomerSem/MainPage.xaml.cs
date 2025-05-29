namespace PopNomerSem
{
    using System;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Main page of application.
    /// </summary>
    public partial class MainPage : ContentPage
    {
        /// <summary>
        /// Data context of data base.
        /// </summary>
        private readonly DataBaseService _dataBaseService;

        /// <summary>
        /// Flag that shows that data is refreshed.
        /// </summary>
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { _isRefreshing = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// All user notes in application.
        /// </summary>
        private List<Note> _notes = [];
        public List<Note> Notes
        {
            get { return _notes; }
            set { _notes = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Notes that user see in main page.
        /// </summary>
        public ObservableCollection<Note> ShownNotes = [];

        /// <summary>
        /// Initialise main page.
        /// </summary>
        public MainPage(DataBaseService dataBaseService)
        {
            InitializeComponent();
            _dataBaseService = dataBaseService;
            LoadNotes();
            UpdateNoteList();
            NotesList.ItemsSource = ShownNotes;
        }

        /// <summary>
        /// Overrided method.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadNotes();
        }
        
        /// <summary>
        /// Load notes from data base.
        /// </summary>
        /// <returns>Task complite.</returns>
        public async Task LoadNotes()
        {
            try
            {
                IsRefreshing = true;
                Notes = await _dataBaseService.GetNotesAsync();
                UpdateNoteList();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Fail to load notes", "OK");
            }
            finally
            {
                IsRefreshing = false;
            }
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
        /// Update list of notes that user see.
        /// </summary>
        /// <param name="newNotes">New list of notes.</param>
        private void UpdateShowsNotes(List<Note> newNotes)
        {
            ShownNotes.Clear();
            if (newNotes != null)
            {
                foreach (var note in newNotes)
                {
                    ShownNotes.Add(note);
                }
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
            if (search.Equals(string.Empty))
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
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotePage(_dataBaseService));
        }

        /// <summary>
        /// Action when item in notes list is selected.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event args.</param>
        private async void NotesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Note selectedNote)
            {
                await Navigation.PushAsync(new NotePage(_dataBaseService, selectedNote));
                NotesList.SelectedItem = null;
            }
        }

        /// <summary>
        /// Action when Settings button clicked.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event args.</param>
        private async void SettingsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }
    }
}
