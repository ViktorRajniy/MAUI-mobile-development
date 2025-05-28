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
    /// Instance of main page.
    /// </summary>
    private MainPage _mainPage;

    /// <summary>
    /// Initialise instance of page.
    /// </summary>
    /// <param name="mainPage">Instancec of main page.</param>
    public NotePage(MainPage mainPage)
    {
        InitializeComponent();
        _mainPage = mainPage;
    }

    /// <summary>
    /// Initialise instance of page.
    /// </summary>
    /// <param name="note">Existing data of note.</param>
    /// <param name="mainPage">Instancec of main page.</param>
    public NotePage(Note note, MainPage mainPage)
    {
        InitializeComponent();
        _note = note;
        _mainPage = mainPage;

        TitleEntry.Text = note.Title;
        TextEditor.Text = note.Text;
        DatePicker.Date = note.Date;
        TimePicker.Time = note.Date.TimeOfDay;
    }

    /// <summary>
    /// Action when Save-button clicked.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">Event args.</param>
    private void OnSaveClicked(object sender, EventArgs e)
    {
        var note = new Note
        {
            Title = TitleEntry.Text,
            Text = TextEditor.Text,
            Date = DatePicker.Date + TimePicker.Time
        };

        if (_note != null)
        {
            _mainPage.UpdateNote(_note, note);
        }
        else
        {
            _mainPage.AddNote(note);
        }

        Navigation.PopAsync();
    }

    /// <summary>
    /// Action when Cancel-button clicked.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">Event args.</param>
    private void OnCancelClicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}
