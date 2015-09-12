﻿using System;
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
            int count = 0;

            foreach (PersonStatusItem item in XmlUtilities.CreateInstances<PersonStatusItem>(Game.Instance.ResourceController.BuildPath(@"Resources\PersonStatus.xml")))
            {
                Label lbl = new Label();
                lbl.Margin = new Thickness(6, 3 + (33 * count), 0, 0);
                lbl.Name = "lbl_" + item.Name;
                lbl.Width = 66;
                lbl.Height = 13;
                lbl.Content = item.Name;


                ((Grid)this.grid_Status).Children.Add(lbl);

                if (item.ControlType == typeof(ProgressBar))
                {
                    ProgressBar value = (ProgressBar)item.GetControlInstance();
                    value.Margin = new Thickness(6, 19 + (33 * count), 0, 0);
                    value.Name = "pgb_" + item.Name;
                    value.Width = 100;
                    value.Height = 14;
                    //value.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
                    ((Grid)this.grid_Status).Children.Add(value);
                }
                else if (item.ControlType == typeof(CheckBox))
                {
                    CheckBox value = (CheckBox)item.GetControlInstance();
                    value.Margin = new Thickness(6, 19 + (33 * count), 0, 0);
                    value.Name = "chk_" + item.Name;
                    value.Width = 100;
                    value.Height = 14;
                    ((Grid)this.grid_Status).Children.Add(value);
                }

                count++;
            }
        }

        protected void CreateAttributes()
        {
            int count = 0;

            foreach (System.Reflection.PropertyInfo item in typeof(PersonAttributes).GetProperties())
            {
                Label lbl = new Label();
                lbl.Margin = new Thickness(6, 3 + (33 * count), 0, 0);
                lbl.Name = "lbl_" + item.Name;
                lbl.Width = 66;
                lbl.Height = 13;
                lbl.Content = item.Name + ": -";


                ((Grid)this.grid_Attributes).Children.Add(lbl);

                count++;
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

            foreach (System.Reflection.PropertyInfo item in typeof(PersonAttributes).GetProperties())
            {
                Label lbl = (Label)LogicalTreeHelper.FindLogicalNode(this.tab_Attributes, "lbl_" + item.Name);
                lbl.Content = item.Name + ": " + item.GetValue(myPerson.Attributes, null);

                count++;
            }
        }

        protected void UpdateStatus(Person myPerson)
        {
            int count = 0;

            foreach (PersonStatusItem item in myPerson.Status.StatusItems)
            {
                if (item.ControlType == typeof(ProgressBar))
                {
                    ProgressBar value = (ProgressBar)LogicalTreeHelper.FindLogicalNode(this.tab_Status, "pgb_" + item.Name);
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
    }
}
