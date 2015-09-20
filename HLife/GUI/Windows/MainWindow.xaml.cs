using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            
            HLife.MainWindow.Instance = this;
            Game.Run("Fairmont");
        }

        private void img_LocationBackground_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        private void GameWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            // Exit the entire game.
            Application.Current.Shutdown();
        }

        private void GameWindow_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.M:
                    this.ToggleWindow<MapWindow>();
                    break;

                case Key.P:
                    this.ToggleWindow<PersonInformationWindow>(Game.Instance.Player);
                    break;

                case Key.I:
                    this.tab_Inventory.Focus();
                    break;

                case Key.O:
                    this.tab_Occupants.Focus();
                    break;

                case Key.S:
                    if (Keyboard.IsKeyDown(Key.LeftCtrl))
                    {
                        SaveLoadController.Save();
                    }
                    break;

                case Key.L:
                    if (Keyboard.IsKeyDown(Key.LeftCtrl))
                    {
                        SaveLoadController.Load();
                    }
                    break;
            }
        }

        private void ToggleWindow<T>(object args = null) where T : Window
        {
            if (WindowController.Get<T>(args) == null)
            {
                Game.Instance.WindowController.Add<T>(args);

                if(typeof(T) == typeof(PersonInformationWindow))
                {
                    WindowController.Get<PersonInformationWindow>(args).UpdateWindow();
                }
            }


            if (    WindowController.Get<T>(args).Visibility == Visibility.Hidden
                ||  WindowController.Get<T>(args).WindowState == WindowState.Minimized)
            {
                WindowController.Get<T>(args).WindowState = WindowState.Normal;
                WindowController.Get<T>(args).Show();
                WindowController.Get<T>(args).Focus();
            }
            else
            {
                WindowController.Get<T>(args).Hide();
            }
        }
    }
}
