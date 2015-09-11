using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HLife_2
{
    public struct DialogControl
    {
        public Image Image { get; set; }

        public string Message { get; set; }

        public bool BlurBackground { get; set; }

        public DialogControl(string message, Image image, bool blurBackground)
        {
            this.Message = message;

            this.Image = image;

            this.BlurBackground = blurBackground;
        }

        public DialogControl(string message, string image, bool blurBackground)
        {
            this.Message = message;

            this.Image = Game.Instance.ResourceController.GetActionImage(image, true);

            this.BlurBackground = blurBackground;
        }

        public DialogControl(string message, bool blurBackground)
        {
            this.Message = message;

            this.Image = null;

            this.BlurBackground = blurBackground;
        }
    }

    public class DialogController
        : Controller
    {
        public Dictionary<string, DialogGroup> DialogGroups { get; set; }

        private DialogControl CurrentDialog { get; set; }

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

        public void DrawDialog(DialogControl dialogControl)
        {
            this.CurrentDialog = dialogControl;

            WindowController.Get<Hlife2>().SuspendLayout();

            Control container = ((SplitContainer)WindowController.Get<Hlife2>().Controls.Find("split", false)[0]).Panel2.Controls.Find("panel_View", false)[0];

            if (this.CurrentDialog.BlurBackground)
            {
                Game.Instance.Player.Location.BlurBackground();
            }

            container.Controls.Clear();

            PictureBox pb = new PictureBox();
            if (this.CurrentDialog.Image != null)
            {
                pb.Dock = DockStyle.Fill;
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Image = this.CurrentDialog.Image;
                pb.BackColor = Color.Transparent;
                pb.Enabled = true;
                pb.Parent = container;
                container.Controls.Add(pb);
            }

            Label lbl = new Label();
            lbl.Text = this.CurrentDialog.Message;
            lbl.BackColor = Color.Transparent;
            lbl.BackColor = Color.FromArgb(175, 0, 0, 0);
            lbl.ForeColor = Color.LightGray;
            lbl.Font = new Font(lbl.Font.FontFamily, 12f);
            //lbl.Parent = container;
            //container.Controls.Add(lbl);

            PictureBox dialogBG = new PictureBox();
            dialogBG.Width = container.Width;
            dialogBG.Height = 200;
            dialogBG.Location = new Point(0, container.Height - dialogBG.Height);
            dialogBG.BackColor = Color.FromArgb(175, 0, 0, 0);
            dialogBG.BackColor = Color.Transparent;
            dialogBG.Image = Image.FromFile(Game.Instance.ResourceController.BuildPath(@"..\..\Global Resources\Assets\Images\black_50alpha.png"));
            dialogBG.SizeMode = PictureBoxSizeMode.StretchImage;
            dialogBG.Parent = container;
            container.Controls.Add(dialogBG);


            lbl.MouseClick += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    Game.Instance.Player.Location.UnblurBackground();
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
                    Game.Instance.Player.Location.UnblurBackground();
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
                    Game.Instance.Player.Location.UnblurBackground();
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

            if (this.CurrentDialog.Image != null)
            {
                container.Controls.Add(pb);

                pb.MouseClick += (sender, e) =>
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        Game.Instance.Player.Location.UnblurBackground();
                        container.Controls.Clear();
                    }
                    else if (e.Button == MouseButtons.Right)
                    {
                        this.ToggleDialog();
                    }
                };
            }

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

            if (blurred && this.CurrentDialog.BlurBackground)
            {
                Game.Instance.Player.Location.BlurBackground();
            }
            else
            {
                Game.Instance.Player.Location.UnblurBackground();
            }

            WindowController.Get<Hlife2>().ResumeLayout(true);
        }
    }
}
