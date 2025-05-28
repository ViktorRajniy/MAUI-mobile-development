namespace PopNomerSem;

public partial class NotePage : ContentPage
{
    private Note _note;
    private MainPage _mainPage;

    public NotePage(MainPage mainPage)
    {
        InitializeComponent();
        _mainPage = mainPage;
    }

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

    private void OnCancelClicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}
