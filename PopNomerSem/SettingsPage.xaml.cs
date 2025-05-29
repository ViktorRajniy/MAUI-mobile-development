namespace PopNomerSem
{
    using System.Collections.ObjectModel;

    public partial class SettingsPage : ContentPage
    {
        public ObservableCollection<ColorOption> AvailableColors { get; } = new()
        {
            new ColorOption("Синий", "#512BD4"),
            new ColorOption("Красный", "#FF0000"),
            new ColorOption("Зеленый", "#00FF00")
        };

        public ObservableCollection<string> AvailableFonts { get; } = new()
        {
            "OpenSans",
            "Roboto",
            "Arial"
        };

        private double _fontSize = 14;
        public double FontSize
        {
            get => _fontSize;
            set
            {
                _fontSize = value;
                OnPropertyChanged();
                UpdateStyles();
            }
        }

        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = this;

            // Загрузка текущих настроек
            if (Preferences.ContainsKey("PrimaryColor"))
            {
                var colorName = Preferences.Get("PrimaryColor", "Синий");
                ColorPicker.SelectedItem = AvailableColors.FirstOrDefault(c => c.Name == colorName);
            }

            if (Preferences.ContainsKey("FontFamily"))
                FontPicker.SelectedItem = Preferences.Get("FontFamily", "OpenSans");

            if (Preferences.ContainsKey("FontSize"))
                FontSizeSlider.Value = Preferences.Get("FontSize", 14.0);
        }

        private void UpdateStyles()
        {
            // Обновляем ресурсы приложения
            if (ColorPicker.SelectedItem is ColorOption selectedColor)
            {
                Application.Current.Resources["PrimaryColor"] = Color.FromArgb(selectedColor.Value);
            }

            if (FontPicker.SelectedItem is string selectedFont)
            {
                Application.Current.Resources["FontFamily"] = selectedFont;
            }

            Application.Current.Resources["FontSize"] = FontSize;
        }

        private void OnSaveSettingsClicked(object sender, EventArgs e)
        {
            if (ColorPicker.SelectedItem is ColorOption color)
                Preferences.Set("PrimaryColor", color.Name);

            if (FontPicker.SelectedItem is string font)
                Preferences.Set("FontFamily", font);

            Preferences.Set("FontSize", FontSize);

            DisplayAlert("Сохранено", "Настройки успешно сохранены", "OK");
        }

        private void OnResetSettingsClicked(object sender, EventArgs e)
        {
            Preferences.Clear();
            ColorPicker.SelectedItem = AvailableColors[0];
            FontPicker.SelectedItem = "OpenSans";
            FontSizeSlider.Value = 14;
            UpdateStyles();
        }
    }

    public class ColorOption
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public ColorOption(string name, string value)
        {
            Name = name;
            Value = value;
        }

    }
}