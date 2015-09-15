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
            Game.Instance.WindowController.Add<Setup>();

            WindowController.Get<Setup>().Show();
            WindowController.Get<Setup>().Focus();

            this.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            foreach (string dir in Directory.EnumerateDirectories(Game.Instance.ResourceController.BuildRootPath(@"Mods\")))
            {
                Mod mod = XmlUtilities.CreateInstance<Mod>(dir + @"\ModInfo.xml");
                mod.Directory = dir;

                if(mod.Name == "Default Actions")
                {
                    mod.Enabled = true;
                }
            }

            MainWindow window = new MainWindow();
            window.Show();

            this.Close();
        }
    }
}
