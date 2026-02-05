using System.Windows;
using System.Windows.Shell;

namespace MapperStudio;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
   

    private void imgAppLogo_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        DragMove();
    }

    private void btnClose_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void btnMaximize_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState ==WindowState.Maximized? WindowState.Normal :WindowState.Maximized;
    }

    private void btnClose_Click_1(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }
}
