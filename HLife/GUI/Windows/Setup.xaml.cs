using HLife.GameEntities.People;
using HLife.Mods;
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

        private void OpenModsFolder(object sender, RoutedEventArgs e)
        {
            Process.Start(ResourceController.BuildRootPath(@"Mods\"));
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            this.RetrieveModList(true);
            this.PopulateModList();


            this.img_Avatar.Source = Game.Instance.ResourceController.GetImage(
                ResourceController.BuildRootPath(@"Cities\Fairmont\Assets\Images\Characters\" + combo_Sex.Text + @"\" + combo_Sex.Text + "_0.png"));
        }

        private void RetrieveModList(bool AutoDefault = true)
        {
            ModController.ModsAvailable.Clear();
            ModController.ModsEnabled.Clear();

            foreach (string dir in Directory.EnumerateDirectories(ResourceController.BuildRootPath(@"Mods\")))
            {
                Mod mod = XmlUtilities.CreateInstance<Mod>(dir + @"\ModInfo.xml");
                mod.Directory = dir;

                if (    AutoDefault
                        && (
                               mod.Name == "Default Actions"
                            || mod.Name == "Default Props"
                            || mod.Name == "Default Name Pack"
                            || mod.Name == "Default Perks"
                            || mod.Name == "Default Person Stats"
                            || mod.Name == "Default Prop Stats"
                        )
                    )
                {
                    mod.Enabled = true;
                }
            }

            this.PopulateModList();
        }

        private void PopulateModList()
        {
            this.stack_ModsEnabled.Children.Clear();
            this.stack_ModsAvailable.Children.Clear();
            
            this.gb_ModsEnabled.Header = "Mods Enabled (" + ModController.ModsEnabled.Count + ")";
            this.gb_ModsAvailable.Header = "Mods Available (" + (ModController.ModsAvailable.Count - ModController.ModsEnabled.Count) + ")";

            foreach(Mod mod in ModController.ModsAvailable)
            {
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

                ToolTip tt = new System.Windows.Controls.ToolTip();
                tt.Content = mod.Description;

                lbl.ToolTip = tt;

                if(mod.Enabled)
                {
                    this.stack_ModsEnabled.Children.Add(lbl);
                }
                else
                {
                    this.stack_ModsAvailable.Children.Add(lbl);
                }
            }

            this.PopulatePerkList();
        }

        private void PopulatePerkList()
        {
            List<Perk> perks = new List<Perk>();

            foreach (Mod mod in ModController.GetModsByType("Perks"))
            {
                perks.AddRange(XmlUtilities.CreateInstances<Perk>(mod.Directory + @"\Perks\Perks.xml"));
            }

            this.stack_PerksAvailable.Children.Clear();
            this.stack_PerksEnabled.Children.Clear();

            foreach (Perk perk in perks)
            {
                Label item = new Label();
                item.Content = perk.Name;
                item.Tag = perk;
                item.MouseLeftButtonDown += (leftSender, leftArgs) =>
                {
                    var dragData = new DataObject(typeof(Perk), perk);

                    DragDrop.DoDragDrop(item,
                                        dragData,
                                        DragDropEffects.Move);

                    ((StackPanel)((Label)leftSender).Parent).Children.Remove((Label)leftSender);
                };

                ToolTip tt = new System.Windows.Controls.ToolTip();
                tt.Content = perk.Description;

                item.ToolTip = tt;

                this.stack_PerksAvailable.Children.Add(item);
            }
        }

        private void PopulateSexualitiesList()
        {
            List<string> sexualities = new List<string>()
            {
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
            };

            foreach (string perk in sexualities)
            {
                Label item = new Label();
                item.Content = perk;
                item.MouseLeftButtonDown += (leftSender, leftArgs) =>
                {
                    var dragData = new DataObject(typeof(string), item.Content);

                    DragDrop.DoDragDrop(item,
                                        dragData,
                                        DragDropEffects.Move);

                    ((StackPanel)((Label)leftSender).Parent).Children.Remove((Label)leftSender);
                };

                this.stack_SexualitiesAvailable.Children.Add(item);
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

            ToolTip tt = new System.Windows.Controls.ToolTip();
            tt.Content = mod.Description;

            lbl.ToolTip = tt;

            panel.Children.Add(lbl);


            this.PopulateModList();
        }

        private void DropPerk(object sender, DragEventArgs e)
        {
            StackPanel panel = sender as StackPanel;

            var dataObj = e.Data as DataObject;
            var perk = (Perk)dataObj.GetData(typeof(Perk));


            Label lbl = new Label();
            lbl.Content = perk.Name;
            lbl.Tag = perk;
            lbl.MouseLeftButtonDown += (leftSender, leftArgs) =>
            {
                var dragData = new DataObject(typeof(Perk), perk);

                DragDrop.DoDragDrop(lbl,
                                    perk,
                                    DragDropEffects.Move);

                ((StackPanel)((Label)leftSender).Parent).Children.Remove((Label)leftSender);
            };

            ToolTip tt = new System.Windows.Controls.ToolTip();
            tt.Content = perk.Description;

            lbl.ToolTip = tt;

            panel.Children.Add(lbl);
        }

        private void RefreshModList(object sender, RoutedEventArgs e)
        {
            this.RetrieveModList(true);
        }

        private void UnloadAllMods(object sender, RoutedEventArgs e)
        {
            ModController.DisabledAllMods();

            this.PopulateModList();
        }

        private void LoadAllMods(object sender, RoutedEventArgs e)
        {
            ModController.EnableAllMods();

            this.PopulateModList();
        }

        private void combo_Sex_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = (e.AddedItems[0] as ComboBoxItem).Content as string;

            this.img_Avatar.Source = Game.Instance.ResourceController.GetImage(
                ResourceController.BuildRootPath(@"Cities\Fairmont\Assets\Images\Characters\" + text + @"\" + text + "_0.png"));
        }
    }
}
