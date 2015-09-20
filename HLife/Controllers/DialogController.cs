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

        public DialogControl(string message, Action action, string image, bool blurBackground)
        {
            this.Message = message;

            this.Image = Game.Instance.ResourceController.GetActionImage(action, image, true);

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
        public SerializableDictionary<string, DialogGroup> DialogGroups { get; set; }

        private DialogControl CurrentDialog { get; set; }

        public DialogController()
        {
            this.DialogGroups = new SerializableDictionary<string, DialogGroup>();
        }

        public override void Initialize()
        {
            //List<DialogGroup> groups = XmlUtilities.CreateInstances<DialogGroup>(Game.Instance.ResourceController.BuildPath(@"Resources\Language\en-us\Actions.xml"));
            //groups.ForEach(e => this.DialogGroups.Add(e.Id, e));
        }

        public override void Update()
        { }

        public void DrawDialog(DialogControl dialogControl)
        {
            this.CurrentDialog = dialogControl;

            Grid container = (Grid)LogicalTreeHelper.FindLogicalNode(WindowController.Get<MainWindow>(), "grid_View");

            if (this.CurrentDialog.BlurBackground)
            {
                Game.Instance.Player.Location.BlurBackground();
            }

            container.Children.RemoveRange(1, container.Children.Count);

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
            lbl.Background = new SolidColorBrush(Brushes.Black.Color);
            lbl.Background.Opacity = 0.75;
            lbl.Foreground = Brushes.LightGray;
            lbl.Width = container.Width;
            lbl.Height = (container.ActualHeight / 3);
            lbl.Padding = new Thickness(20);
            lbl.Margin = new Thickness(0, (container.ActualHeight / 3) * 2, 0, 0);
            lbl.FontSize = 16;
            container.Children.Add(lbl);
            

            lbl.MouseLeftButtonUp += RemoveDialog;

            container.MouseLeftButtonUp += RemoveDialog;
            container.MouseRightButtonUp += ToggleDialog;

            if (this.CurrentDialog.Image != null)
            {
                pb.MouseLeftButtonUp += RemoveDialog;
            }
        }

        private void RemoveDialog(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Grid container = (Grid)LogicalTreeHelper.FindLogicalNode(WindowController.Get<MainWindow>(), "grid_View");

            Game.Instance.Player.Location.UnblurBackground();

            container.Children.RemoveRange(1, container.Children.Count);

            ((UIElement)sender).MouseLeftButtonUp -= RemoveDialog;
            container.MouseRightButtonUp -= ToggleDialog;
        }

        public void ToggleDialog(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Grid container = (Grid)LogicalTreeHelper.FindLogicalNode(WindowController.Get<MainWindow>(), "grid_View");

            bool blurred = false;

            foreach (FrameworkElement control in container.Children)
            {
                if(     control.GetType() == typeof(Image)
                    ||  control.GetType() == typeof(MediaElement))
                {
                    continue;
                }

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
                //Game.Instance.Player.Location.BlurBackground();
            }
            else
            {
                //Game.Instance.Player.Location.UnblurBackground();
            }
        }
    }
}
