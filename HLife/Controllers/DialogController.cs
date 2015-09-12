using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HLife
{
    public struct DialogControl
    {
        public Uri Image { get; set; }

        public string Message { get; set; }

        public bool BlurBackground { get; set; }

        public DialogControl(string message, Uri image, bool blurBackground)
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

            Grid container = (Grid)LogicalTreeHelper.FindLogicalNode(WindowController.Get<MainWindow>(), "grid_View");// ((SplitContainer)WindowController.Get<MainWindow>().Controls.Find("split", false)[0]).Panel2.Controls.Find("panel_View", false)[0];

            if (this.CurrentDialog.BlurBackground)
            {
                Game.Instance.Player.Location.BlurBackground();
            }

            container.Children.Clear();

            MediaElement pb = new MediaElement();
            if (this.CurrentDialog.Image != null)
            {
                pb.Stretch = System.Windows.Media.Stretch.Uniform;
                pb.StretchDirection = StretchDirection.Both;
                pb.Source = this.CurrentDialog.Image;
                container.Children.Add(pb);
            }

            Label lbl = new Label();
            lbl.Content = this.CurrentDialog.Message;
            lbl.Background = Brushes.Transparent;
            lbl.Foreground = Brushes.LightGray;
            lbl.Width = container.Width;
            lbl.Height = 200;
            lbl.Margin = new Thickness(0, 0, 0, 200);
            lbl.Padding = new Thickness(10);
            //lbl.Font = new Font(lbl.Font.FontFamily, 12f);
            //lbl.Parent = container;
            container.Children.Add(lbl);

            /*
            Image dialogBG = new PictureBox();
            dialogBG.Width = container.Width;
            dialogBG.Height = 200;
            dialogBG.Location = new Point(0, container.Height - dialogBG.Height);
            dialogBG.BackColor = Color.FromArgb(175, 0, 0, 0);
            dialogBG.BackColor = Color.Transparent;
            dialogBG.Image = Image.FromFile(Game.Instance.ResourceController.BuildPath(@"..\..\Global Resources\Assets\Images\black_50alpha.png"));
            dialogBG.SizeMode = PictureBoxSizeMode.StretchImage;
            dialogBG.Parent = container;
            container.Controls.Add(dialogBG);
            */


            lbl.MouseUp += (sender, e) =>
            {
                if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                {
                    Game.Instance.Player.Location.UnblurBackground();
                    container.Children.Clear();
                }
                else if (e.RightButton == System.Windows.Input.MouseButtonState.Pressed)
                {
                    this.ToggleDialog();
                }
            };

            /*
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
            */

            container.MouseUp += (sender, e) =>
            {
                if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                {
                    Game.Instance.Player.Location.UnblurBackground();
                    container.Children.Clear();
                }
                else if (e.RightButton == System.Windows.Input.MouseButtonState.Pressed)
                {
                    this.ToggleDialog();
                }
            };


            //lbl.Width = dialogBG.Width - 10;
            //lbl.Height = dialogBG.Height - 10;
            //lbl.Location = new Point(dialogBG.Location.X + 20, dialogBG.Location.Y + 20);

            if (this.CurrentDialog.Image != null)
            {
                pb.MouseUp += (sender, e) =>
                {
                    if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                    {
                        Game.Instance.Player.Location.UnblurBackground();
                        container.Children.Clear();
                    }
                    else if (e.RightButton == System.Windows.Input.MouseButtonState.Pressed)
                    {
                        this.ToggleDialog();
                    }
                };
            }

            container.UpdateLayout();
        }

        public void ToggleDialog()
        {
            Grid container = (Grid)LogicalTreeHelper.FindLogicalNode(WindowController.Get<MainWindow>(), "grid_View");//.Controls.Find("split", false)[0]).Panel2.Controls.Find("panel_View", false)[0];

            bool blurred = false;

            foreach (Control control in container.Children)
            {
                if(control.Visibility != Visibility.Visible)
                {
                    control.Visibility = Visibility.Visible;
                }
                else
                {
                    control.Visibility = Visibility.Collapsed;
                }

                blurred = control.Visibility == Visibility.Visible;
            }

            if (blurred && this.CurrentDialog.BlurBackground)
            {
                Game.Instance.Player.Location.BlurBackground();
            }
            else
            {
                Game.Instance.Player.Location.UnblurBackground();
            }

            WindowController.Get<MainWindow>().UpdateLayout();
        }
    }
}
