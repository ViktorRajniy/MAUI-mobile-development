namespace PopNomerSem;

/// <summary>
/// Note page.
/// </summary>
public partial class NotePage : ContentPage
{
    /// <summary>
    /// Data of note.
    /// </summary>
    private Note _note;

    /// <summary>
    /// Data context of data base.
    /// </summary>
    private readonly DataBaseService _databaseService;

    /// <summary>
    /// Initialise instance of page.
    /// </summary>
    /// <param name="databaseService">Instancec of data base.</param>
    public NotePage(DataBaseService databaseService)
    {
        InitializeComponent();
        _databaseService = databaseService;
        _note = new Note();
        DatePicker.Date = DateTime.Now;
        TimePicker.Time = DateTime.Now.TimeOfDay;
    }

    /// <summary>
    /// Initialise instance of page.
    /// </summary>
    /// <param name="note">Existing data of note.</param>
    /// <param name="mainPage">Instancec of main page.</param>
    public NotePage(DataBaseService databaseService, Note note)
    {
        InitializeComponent();
        _databaseService = databaseService;
        _note = note;

        TitleEntry.Text = note.Title;
        TextEditor.Text = note.Text;
        DatePicker.Date = note.DateOfCreation;
        TimePicker.Time = note.DateOfCreation.TimeOfDay;
    }

    /// <summary>
    /// Override method.
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (int.TryParse(Shell.Current.CurrentState.Location.OriginalString.Split('=').Last(), out var id))
        {
            _note = await _databaseService.GetNoteAsync(id); if (_note != null)
            {
                TitleEntry.Text = _note.Title; TextEditor.Text = _note.Text; DatePicker.Date = _note.ScheduledDate;
                TimePicker.Time = _note.ScheduledDate.TimeOfDay;
            }
        }
    }


    /// <summary>
    /// Action when Save-button clicked.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">Event args.</param>
    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TitleEntry.Text))
        {
            await DisplayAlert("Error", "Enter note title", "OK");
            return;
        }
        var note = new Note
        {
            Id = _note?.Id ?? 0,
            Title = TitleEntry.Text,
            Text = TextEditor.Text,
            ScheduledDate = DatePicker.Date + TimePicker.Time,
            DateOfCreation = _note?.DateOfCreation ?? DateTime.Now,
            ModifiedDate = DateTime.Now
        };
        await _databaseService.SaveNoteAsync(note);
        await Shell.Current.GoToAsync("..");
    }

    /// <summary>
    /// Action when Cancel-button clicked.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">Event args.</param>
    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
        if (Shell.Current.CurrentPage is MainPage mainPage)
        {
            await mainPage.LoadNotes();
        }
    }
}
