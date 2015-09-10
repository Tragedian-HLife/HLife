using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HLife_2
{
    public partial class PlayerInfoWindow : Form
    {
        public TabControl tabs;

        public PlayerInfoWindow()
        {
            InitializeComponent();

            this.tabs = this.tabs_Container;

            this.CreateStatus();
            
            this.CreateAttributes();
            
            // Release the form on close.
            this.FormClosed += (sender, e) => { Game.Instance.WindowController.Windows.Remove(this); this.Dispose(true); };
        }

        private void PlayerInfoWindow_Load(object sender, EventArgs e)
        { }

        protected void CreateStatus()
        {
            int count = 0;

            foreach (PersonStatusItem item in XmlUtilities.CreateInstances<PersonStatusItem>(Game.Instance.ResourceController.BuildPath(@"Resources\PersonStatus.xml")))
            {
                Label lbl = new Label();
                lbl.AutoSize = true;
                lbl.Location = new System.Drawing.Point(6, 3 + (33 * count));
                lbl.Name = "lbl_" + item.Name;
                lbl.Size = new System.Drawing.Size(66, 13);
                lbl.Text = item.Name;


                this.tab_playerStats.Controls.Add(lbl);

                if (item.ControlType == typeof(ProgressBar))
                {
                    ProgressBar value = (ProgressBar)item.GetControlInstance();
                    value.Location = new System.Drawing.Point(6, 19 + (33 * count));
                    value.Name = "pgb_" + item.Name;
                    value.Size = new System.Drawing.Size(100, 14);
                    value.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
                    this.tab_playerStats.Controls.Add(value);
                }
                else if (item.ControlType == typeof(CheckBox))
                {
                    CheckBox value = (CheckBox)item.GetControlInstance();
                    value.Location = new System.Drawing.Point(6, 19 + (33 * count));
                    value.Name = "chk_" + item.Name;
                    value.Size = new System.Drawing.Size(100, 14);
                    value.AutoCheck = false;
                    this.tab_playerStats.Controls.Add(value);
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
                lbl.AutoSize = true;
                lbl.Location = new System.Drawing.Point(6, 3 + (33 * count));
                lbl.Name = "lbl_" + item.Name;
                lbl.Size = new System.Drawing.Size(66, 13);
                lbl.Text = item.Name + ": -";


                this.tab_attributes.Controls.Add(lbl);

                count++;
            }
        }

        public void UpdateWindow()
        {
            if (this.Tag != null)
            {
                Person myPerson = (Person)this.Tag;

                this.Text = myPerson.Name;

                this.pic_avatar.Image = Game.Instance.ResourceController.GetImage(Game.Instance.ResourceController.BuildPath(@"\Assets\Images\Characters\" + myPerson.Image));
                
                this.UpdateAttributes(myPerson);

                this.UpdateStatus(myPerson);
            }
        }

        protected void UpdateAttributes(Person myPerson)
        {
            int count = 0;

            foreach (System.Reflection.PropertyInfo item in typeof(PersonAttributes).GetProperties())
            {
                Label lbl = (Label)this.tab_attributes.Controls.Find("lbl_" + item.Name, false)[0];
                lbl.Text = item.Name + ": " + item.GetValue(myPerson.Attributes, null);

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
                    ProgressBar value = (ProgressBar)this.tab_playerStats.Controls.Find("pgb_" + item.Name, false)[0];
                    value.Value = Math.Max(Math.Min((int)(double)item.Value, value.Maximum), value.Minimum);
                }
                else if (item.ControlType == typeof(CheckBox))
                {
                    CheckBox value = (CheckBox)this.tab_playerStats.Controls.Find("chk_" + item.Name, false)[0];
                    value.Checked = (bool)item.Value;
                }

                count++;
            }
        }
    }
}
