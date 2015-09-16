using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace HLife.GUI.Windows
{
    /// <summary>
    /// Interaction logic for LauncherWindow.xaml
    /// </summary>
    public partial class LauncherWindow : Window
    {
        public LauncherWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            // Create and show the setup window.
            Setup window = new Setup();
            window.Show();

            // Get rid of this.
            this.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Remove this, once game loading is implimented.
            // Load in the default mods.
            foreach (string dir in Directory.EnumerateDirectories(ResourceController.BuildRootPath(@"Mods\")))
            {
                Mod mod = XmlUtilities.CreateInstance<Mod>(dir + @"\ModInfo.xml");
                mod.Directory = dir;

                if(mod.Name == "Default Actions")
                {
                    mod.Enabled = true;
                }
                else if(mod.Name == "Default Props")
                {
                    mod.Enabled = true;
                }
                else if (mod.Name == "Default Name Pack")
                {
                    mod.Enabled = true;
                }
            }

            // Create and show the game window.
            MainWindow window = new MainWindow();
            window.Show();

            // Get rid of this window.
            this.Close();
        }
    }
}
