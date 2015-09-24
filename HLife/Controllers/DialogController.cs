using HLife.GUI.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HLife
{
    public class DialogController
        : Controller
    {
        public SerializableDictionary<string, DialogGroup> DialogGroups { get; set; }

        private DialogGroup CurrentDialog { get; set; }

        private Timer TypingTimer { get; set; }

        private Timer ImageEffectTimer { get; set; }

        private int TypingTicks { get; set; }

        private int ImageEffectTicks { get; set; }

        public DialogController()
        {
            this.DialogGroups = new SerializableDictionary<string, DialogGroup>();
            this.TypingTimer = new Timer();
            this.ImageEffectTimer = new Timer();
        }

        public override void Initialize()
        {
            //List<DialogGroup> groups = XmlUtilities.CreateInstances<DialogGroup>(Game.Instance.ResourceController.BuildPath(@"Resources\Language\en-us\Actions.xml"));
            //groups.ForEach(e => this.DialogGroups.Add(e.Id, e));
        }

        public override void Update()
        { }

        public void DrawDialog(DialogGroup dialogControl)
        {
            this.CurrentDialog = dialogControl;

            Grid container = (Grid)LogicalTreeHelper.FindLogicalNode(WindowController.Get<MainWindow>(), "grid_View");

            Game.Instance.Player.Location.BlurBackground();

            container.Children.RemoveRange(1, container.Children.Count);

            MediaElement pb = new MediaElement();
            if (this.CurrentDialog.Current().Image != null)
            {
                pb.Stretch = System.Windows.Media.Stretch.Uniform;
                pb.Name = "pb_Action";
                pb.StretchDirection = StretchDirection.Both;
                pb.Source = new Uri(this.CurrentDialog.Current().Image);
                pb.VerticalAlignment = VerticalAlignment.Center;
                pb.HorizontalAlignment = HorizontalAlignment.Center;
                pb.Opacity = 0;
                container.Children.Add(pb);
                
                this.ImageEffectTicks = 0;
                this.ImageEffectTimer.Elapsed += ImageEffectTick;
                this.ImageEffectTimer.Interval = 1;
                this.ImageEffectTimer.Start();
            }


            this.CurrentDialog.Current().StartBeginEffects(container);

            this.DrawDialog();

            container.PreviewMouseLeftButtonUp += NextDialog;
            container.PreviewMouseRightButtonUp += ToggleDialog;
        }

        private void DrawDialog()
        {
            Grid container = (Grid)LogicalTreeHelper.FindLogicalNode(WindowController.Get<MainWindow>(), "grid_View");

            HLife.GUI.Controls.Dialog dialog = new GUI.Controls.Dialog();
            dialog.Width = container.Width;
            dialog.Height = 150;
            dialog.Margin = new Thickness(10);
            dialog.VerticalAlignment = VerticalAlignment.Bottom;
            dialog.HorizontalAlignment = HorizontalAlignment.Stretch;
            container.Children.Add(dialog);
            
            TextBlock lbl = (TextBlock)LogicalTreeHelper.FindLogicalNode(container, "text_Message");
            lbl.Text = "";
            lbl.TextWrapping = TextWrapping.Wrap;
            lbl.Foreground = Brushes.LightGray;
            lbl.Width = container.Width;
            lbl.FontSize = 18;
            lbl.Padding = new Thickness(20);
            lbl.Background = new SolidColorBrush(Brushes.Black.Color);
            lbl.Background.Opacity = 0.8;
            lbl.FontFamily = ResourceController.GetFont("Montserrat");

            
            Label pageCounter = (Label)LogicalTreeHelper.FindLogicalNode(container, "lbl_Page");
            pageCounter.Content = (this.CurrentDialog.Index + 1) + " / " + this.CurrentDialog.Entries.Count;
            pageCounter.Foreground = Brushes.LightGray;
            pageCounter.FontSize = 12;
            pageCounter.Background = new SolidColorBrush(Brushes.Black.Color);
            pageCounter.Background.Opacity = 0.8;
            pageCounter.FontFamily = ResourceController.GetFont("Montserrat");
            pageCounter.HorizontalContentAlignment = HorizontalAlignment.Center;


            this.TypingTicks = 0;
            this.TypingTimer.Elapsed += TypeIncremental;
            this.TypingTimer.Interval = 30;
            this.TypingTimer.Start();
        }

        private void ImageEffectTick(object sender, ElapsedEventArgs e)
        {
            WindowController.Get<MainWindow>().Dispatcher.BeginInvoke(new System.Action(() =>
            {
                MediaElement pb = (MediaElement)LogicalTreeHelper.FindLogicalNode(WindowController.Get<MainWindow>(), "pb_Action");
                pb.Opacity += 0.01;

                if (this.ImageEffectTicks < 100)
                {
                    this.ImageEffectTicks++;
                }
                else
                {
                    this.ImageEffectTimer.Stop();
                    this.ImageEffectTimer.Elapsed -= ImageEffectTick;
                }
            }), null);
        }

        private void TypeIncremental(object sender, ElapsedEventArgs e)
        {
            WindowController.Get<MainWindow>().Dispatcher.BeginInvoke(new System.Action(() =>
            {
                TextBlock lbl = (TextBlock)LogicalTreeHelper.FindLogicalNode(WindowController.Get<MainWindow>(), "text_Message");
                InlineExpression.SetInlineExpression(lbl, this.CurrentDialog.Current().RawText.Substring(0, this.TypingTicks));

                if (this.TypingTicks < this.CurrentDialog.Current().RawText.Length)
                {
                    this.TypingTicks++;
                }
                else
                {
                    this.TypingTimer.Stop();
                    this.TypingTimer.Elapsed -= TypeIncremental;
                }
            }), null);
        }
        
        private void NextDialog(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Grid container = (Grid)LogicalTreeHelper.FindLogicalNode(WindowController.Get<MainWindow>(), "grid_View");
            
            this.CurrentDialog.Current().StartEndEffects(container);
            this.CurrentDialog.Current().EndEffectsFinished += (sender2, e2) =>
            {
                container.Dispatcher.Invoke(new System.Action(() =>
                {
                    if (!this.CurrentDialog.IsLastDialog())
                    {
                        this.CurrentDialog.Next();

                        this.DrawDialog(this.CurrentDialog);
                    }
                    else
                    {
                        this.RemoveDialog(sender, e);
                    }
                }));
            };
        }

        private void RemoveDialog(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Grid container = (Grid)LogicalTreeHelper.FindLogicalNode(WindowController.Get<MainWindow>(), "grid_View");

            Game.Instance.Player.Location.UnblurBackground();

            container.Children.RemoveRange(1, container.Children.Count);

            container.PreviewMouseLeftButtonUp -= NextDialog;
            container.PreviewMouseRightButtonUp -= ToggleDialog;
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
        }
    }
}
