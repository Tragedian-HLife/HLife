using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HLife_2
{
    public class DialogController
        : Controller
    {
        public Dictionary<string, DialogGroup> DialogGroups { get; set; }

        private Bitmap Regular { get; set; }

        private Bitmap Blurred { get; set; }

        public DialogController()
        {
            this.DialogGroups = new Dictionary<string, DialogGroup>();
        }

        public override void Initialize()
        {
            List<DialogGroup> groups = XmlUtilities.CreateInstances<DialogGroup>(Game.Instance.ResourceController.BuildPath(@"Resources\Language\en-us\Actions.xml"));
            groups.ForEach(e => this.DialogGroups.Add(e.Id, e));
        }

        public override void Update()
        { }

        public void DrawDialog(string message, bool blur)
        {
            WindowController.Get<Hlife2>().SuspendLayout();

            Control container = ((SplitContainer)WindowController.Get<Hlife2>().Controls.Find("split", false)[0]).Panel2.Controls.Find("panel_View", false)[0];

            this.Regular = new Bitmap(Game.Instance.ResourceController.GetBackgroundImage(Game.Instance.Player.Location.BackgroundImage));
            this.Blurred = ImageUtilities.FastBlur(this.Regular, 2);
            container.BackgroundImage = this.Blurred;

            container.Controls.Clear();

            Label lbl = new Label();
            lbl.Text = message;
            lbl.BackColor = Color.Transparent;
            lbl.BackColor = Color.FromArgb(175, 0, 0, 0);
            lbl.ForeColor = Color.LightGray;
            lbl.Font = new Font(lbl.Font.FontFamily, 12f);
            lbl.Parent = container;
            container.Controls.Add(lbl);

            PictureBox dialogBG = new PictureBox();
            dialogBG.Width = container.Width;
            dialogBG.Height = 200;
            dialogBG.Location = new Point(0, container.Height - dialogBG.Height);
            dialogBG.BackColor = Color.FromArgb(175, 0, 0, 0);
            dialogBG.SizeMode = PictureBoxSizeMode.StretchImage;
            dialogBG.Parent = container;
            container.Controls.Add(dialogBG);


            lbl.MouseClick += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    container.BackgroundImage = this.Regular;
                    container.Controls.Clear();
                }
                else if (e.Button == MouseButtons.Right)
                {
                    this.ToggleDialog();
                }
            };

            dialogBG.MouseClick += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    container.BackgroundImage = this.Regular;
                    container.Controls.Clear();
                }
                else if (e.Button == MouseButtons.Right)
                {
                    this.ToggleDialog();
                }
            };

            container.MouseClick += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    container.BackgroundImage = this.Regular;
                    container.Controls.Clear();
                }
                else if (e.Button == MouseButtons.Right)
                {
                    this.ToggleDialog();
                }
            };


            lbl.Width = dialogBG.Width - 10;
            lbl.Height = dialogBG.Height - 10;
            lbl.Location = new Point(dialogBG.Location.X + 20, dialogBG.Location.Y + 20);

            WindowController.Get<Hlife2>().ResumeLayout(true);
        }

        public void ToggleDialog()
        {
            WindowController.Get<Hlife2>().SuspendLayout();

            Control container = ((SplitContainer)WindowController.Get<Hlife2>().Controls.Find("split", false)[0]).Panel2.Controls.Find("panel_View", false)[0];

            bool blurred = false;

            foreach (Control control in container.Controls)
            {
                control.Visible = !control.Visible;

                blurred = control.Visible;
            }

            if (blurred)
            {
                container.BackgroundImage = this.Blurred;
            }
            else
            {
                container.BackgroundImage = this.Regular;
            }

            WindowController.Get<Hlife2>().ResumeLayout(true);
        }
    }
}
