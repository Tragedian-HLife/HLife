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
using System.Windows.Shapes;

namespace HLife
{
    /// <summary>
    /// Interaction logic for PersonInformation.xaml
    /// </summary>
    public partial class PersonInformationWindow : Window
    {
        public PersonInformationWindow()
        {
            InitializeComponent();

            this.CreateStatus();

            this.CreateAttributes();

            // Release the form on close.
            //this.FormClosed += (sender, e) => { Game.Instance.WindowController.Windows.Remove(this); this.Dispose(true); };
        }

        protected void CreateStatus()
        {
            this.wrap_Status.Height = 0;

            foreach (Stat item in Stats.DefaultPersonStatusItems.Where(e => e.Type == "status"))
            {
                GroupBox box = new GroupBox();
                box.Width = this.wrap_Status.Width - 20;
                box.VerticalAlignment = VerticalAlignment.Top;
                box.Header = item.Name;
                this.wrap_Status.Children.Add(box);


                Grid grid = new Grid();
                grid.VerticalAlignment = VerticalAlignment.Top;
                box.Content = grid;


                if (item.ControlType == typeof(ProgressBar))
                {
                    ProgressBar value = (ProgressBar)item.GetControlInstance();
                    value.Margin = new Thickness(0, 2, 0, 0);
                    value.Name = "pgb_" + item.Name;
                    value.Height = 14;
                    grid.Children.Add(value);
                }
                else if (item.ControlType == typeof(CheckBox))
                {
                    CheckBox value = (CheckBox)item.GetControlInstance();
                    value.Margin = new Thickness(0, 2, 0, 0);
                    value.Name = "chk_" + item.Name;
                    value.Height = 14;
                    value.IsEnabled = false;
                    grid.Children.Add(value);
                }

                this.wrap_Status.Height += box.Height;
            }
        }

        protected void CreateAttributes()
        {
            this.stack_Attributes.Height = 0;

            foreach (Stat item in Stats.DefaultPersonStatusItems.Where(e => e.Type == "attribute"))
            {
                GroupBox box = new GroupBox();
                box.Width = this.wrap_Status.Width - 20;
                box.VerticalAlignment = VerticalAlignment.Top;
                box.Header = item.Name;
                this.stack_Attributes.Children.Add(box);


                Grid grid = new Grid();
                grid.VerticalAlignment = VerticalAlignment.Top;
                box.Content = grid;


                if (item.ControlType == typeof(ProgressBar))
                {
                    ProgressBar value = (ProgressBar)item.GetControlInstance();
                    value.Margin = new Thickness(0, 2, 0, 0);
                    value.Name = "pgb_" + item.Name;
                    value.Height = 14;
                    grid.Children.Add(value);
                }
                else if (item.ControlType == typeof(CheckBox))
                {
                    CheckBox value = (CheckBox)item.GetControlInstance();
                    value.Margin = new Thickness(0, 2, 0, 0);
                    value.Name = "chk_" + item.Name;
                    value.Height = 14;
                    value.IsEnabled = false;
                    grid.Children.Add(value);
                }

                this.stack_Attributes.Height += box.Height;
            }
        }

        public void UpdateWindow()
        {
            if (this.Tag != null)
            {
                Person myPerson = (Person)this.Tag;

                this.Title = myPerson.Name;

                ((Image)this.FindName("img_Avatar")).Source = Game.Instance.ResourceController.GetImage(Game.Instance.ResourceController.BuildPath(@"\Assets\Images\Characters\" + myPerson.Image));

                this.UpdateAttributes(myPerson);

                this.UpdateStatus(myPerson);
            }
        }

        protected void UpdateAttributes(Person myPerson)
        {
            int count = 0;

            foreach (Stat item in myPerson.Stats.StatEntries.Where(e => e.Type == "attribute"))
            {
                if (item.ControlType == typeof(ProgressBar))
                {
                    ProgressBar value = (ProgressBar)LogicalTreeHelper.FindLogicalNode(this.tab_Attributes, "pgb_" + item.Name);
                    value.Value = Math.Max(Math.Min((int)(double)item.Value, value.Maximum), value.Minimum);
                }
                else if (item.ControlType == typeof(CheckBox))
                {
                    CheckBox value = (CheckBox)LogicalTreeHelper.FindLogicalNode(this.tab_Attributes, "chk_" + item.Name);
                    value.IsChecked = (bool)item.Value;
                }

                count++;
            }
        }

        protected void UpdateStatus(Person myPerson)
        {
            int count = 0;

            foreach (Stat item in myPerson.Stats.StatEntries.Where(e => e.Type == "status"))
            {
                if (item.ControlType == typeof(ProgressBar))
                {
                    ProgressBar value = (ProgressBar)LogicalTreeHelper.FindLogicalNode(this.tab_Status, "pgb_" + item.Name);

                    if(item.Maximum != null)
                    {
                        value.Maximum = myPerson.Stats.GetValue<double>(item.Maximum);
                    }

                    value.Value = Math.Max(Math.Min((int)(double)item.Value, value.Maximum), value.Minimum);
                }
                else if (item.ControlType == typeof(CheckBox))
                {
                    CheckBox value = (CheckBox)LogicalTreeHelper.FindLogicalNode(this.tab_Status, "chk_" + item.Name);
                    value.IsChecked = (bool)item.Value;
                }

                count++;
            }
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            IconHelper.RemoveIcon(this);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Game.Instance.WindowController.Windows.Remove(this);
        }

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.S:
                    this.tab_Status.Focus();
                    break;

                case Key.A:
                    this.tab_Attributes.Focus();
                    break;

                case Key.I:
                    this.tab_Inventory.Focus();
                    break;
            }
        }
    }
}
