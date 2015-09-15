using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for Setup.xaml
    /// </summary>
    public partial class Setup : Window
    {
        public Setup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(Game.Instance.ResourceController.BuildRootPath(@"Mods\"));
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            foreach (string dir in Directory.EnumerateDirectories(Game.Instance.ResourceController.BuildRootPath(@"Mods\")))
            {
                Mod mod = XmlUtilities.CreateInstance<Mod>(dir + @"\ModInfo.xml");
                mod.Directory = dir;

                Label lbl = new Label();
                lbl.Content = mod.Name;
                lbl.MouseLeftButtonDown += (leftSender, leftArgs) =>
                {
                    var dragData = new DataObject(typeof(Mod), mod);

                    DragDrop.DoDragDrop(lbl,
                                        dragData,
                                        DragDropEffects.Move);

                    ((StackPanel)((Label)leftSender).Parent).Children.Remove((Label)leftSender);
                };

                if (mod.Name == "Default Actions")
                {
                    mod.Enabled = true;

                    this.stack_ModsEnabled.Children.Add(lbl);
                }
                else
                {
                    this.stack_ModsAvailable.Children.Add(lbl);
                }
            }
        }

        private void DropOnPanel(object sender, DragEventArgs e)
        {
            StackPanel panel = sender as StackPanel;

            var dataObj = e.Data as DataObject;
            Mod mod = dataObj.GetData(typeof(Mod)) as Mod;

            if (panel == this.stack_ModsAvailable)
            {
                mod.Enabled = false;
            }
            else if (panel == this.stack_ModsEnabled)
            {
                mod.Enabled = true;
            }

            Label lbl = new Label();
            lbl.Content = mod.Name;
            lbl.MouseLeftButtonDown += (leftSender, leftArgs) =>
            {
                var dragData = new DataObject(typeof(Mod), mod);

                DragDrop.DoDragDrop(lbl,
                                    mod,
                                    DragDropEffects.Move);

                ((StackPanel)((Label)leftSender).Parent).Children.Remove((Label)leftSender);
            };

            panel.Children.Add(lbl);
        }
    }
}
