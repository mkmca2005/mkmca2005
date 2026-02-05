using System.Windows;

namespace MapperStudio;

public partial class MainWindow : Window
{
    private bool _isDarkTheme = true;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void TitleBar_OnMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (e.ButtonState == System.Windows.Input.MouseButtonState.Pressed)
        {
            DragMove();
        }
    }

    private void ThemeToggle_OnClick(object sender, RoutedEventArgs e)
    {
        var appResources = Application.Current.Resources.MergedDictionaries;
        if (appResources.Count == 0)
        {
            return;
        }

        var themePath = _isDarkTheme ? "Themes/LightTheme.xaml" : "Themes/DarkTheme.xaml";
        appResources[0] = new ResourceDictionary { Source = new Uri(themePath, UriKind.Relative) };
        _isDarkTheme = !_isDarkTheme;
        ThemeToggle.Content = _isDarkTheme ? "Light" : "Dark";
    }
}
