using System.Windows;
using System.Windows.Input;

namespace Convert3DObject.Views
{
    /// <summary>
    /// Interaction logic for WelcomeView.xaml
    /// </summary>
    public partial class WelcomeView : Window
    {
        public WelcomeView()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            MainMenuView mainMenu = new MainMenuView();
            mainMenu.Show();
            mainMenu.Owner = this;
            this.Hide();
        }
    }
}
