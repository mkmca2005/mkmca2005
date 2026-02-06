using System;
using System.Windows;
using System.Windows.Controls;

namespace MapperStudio;

public partial class MainWindow : Window
{
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

    private void ThemeButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button || button.Tag is not string themeName)
        {
            return;
        }

        var themePath = themeName switch
        {
            "Dark" => "Themes/DarkTheme.xaml",
            "Light" => "Themes/LightTheme.xaml",
            "Blue" => "Themes/BlueTheme.xaml",
            _ => "Themes/DarkTheme.xaml"
        };

        var appResources = Application.Current.Resources.MergedDictionaries;
        if (appResources.Count == 0)
        {
            return;
        }

        var index = -1;
        for (var i = 0; i < appResources.Count; i++)
        {
            var source = appResources[i].Source?.OriginalString ?? string.Empty;
            if (source.Contains("Themes/DarkTheme.xaml", StringComparison.OrdinalIgnoreCase) ||
                source.Contains("Themes/LightTheme.xaml", StringComparison.OrdinalIgnoreCase) ||
                source.Contains("Themes/BlueTheme.xaml", StringComparison.OrdinalIgnoreCase))
            {
                index = i;
                break;
            }
        }

        if (index == -1)
        {
            appResources.Add(new ResourceDictionary { Source = new Uri(themePath, UriKind.Relative) });
            return;
        }

        appResources[index] = new ResourceDictionary { Source = new Uri(themePath, UriKind.Relative) };
    }
}
