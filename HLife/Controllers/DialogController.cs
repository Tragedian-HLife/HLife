using HLife.Choices;
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

        private int TypingTicks { get; set; }

        private GUI.Controls.Dialog Dialog { get; set; }

        public DialogController()
        {
            this.DialogGroups = new SerializableDictionary<string, DialogGroup>();
            this.TypingTimer = new Timer();
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

            this.Dialog = null;
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
                container.Children.Add(pb);
            }
            
            container.PreviewMouseRightButtonUp += ToggleDialog;

            this.CurrentDialog.Current().PrepareBeginEffects(container);
            this.CurrentDialog.Current().PrepareEndEffects(container);

            this.AddDialogControl();

            this.CurrentDialog.Current().StartBeginEffects();
        }

        private void AddDialogControl()
        {
            Grid container = (Grid)LogicalTreeHelper.FindLogicalNode(WindowController.Get<MainWindow>(), "grid_View");

            this.Dialog = new GUI.Controls.Dialog();
            this.Dialog.Name = "dialog";
            this.Dialog.Width = container.Width;
            this.Dialog.Height = 150;
            this.Dialog.Margin = new Thickness(10);
            this.Dialog.Background = Brushes.Transparent;
            this.Dialog.VerticalAlignment = VerticalAlignment.Bottom;
            this.Dialog.HorizontalAlignment = HorizontalAlignment.Stretch;
            container.Children.Add(this.Dialog);

            ((Button)this.Dialog.FindName("btn_Next")).Click += NextDialog;
            ((Button)this.Dialog.FindName("btn_Prev")).Click += PrevDialog;

            TextBlock lbl = (TextBlock)LogicalTreeHelper.FindLogicalNode(container, "text_Message");
            lbl.Text = "";
            lbl.Foreground = Brushes.LightGray;
            lbl.Background = new SolidColorBrush(Brushes.Black.Color);
            lbl.Background.Opacity = 0.8;
            lbl.FontFamily = ResourceController.GetFont("Montserrat");
            
            Label pageCounter = (Label)LogicalTreeHelper.FindLogicalNode(container, "lbl_Page");
            pageCounter.Content = (this.CurrentDialog.Index + 1) + " / " + this.CurrentDialog.Entries.Count;
            pageCounter.Foreground = Brushes.LightGray;
            pageCounter.Background = new SolidColorBrush(Brushes.Black.Color);
            pageCounter.Background.Opacity = 0.8;
            pageCounter.FontFamily = ResourceController.GetFont("Montserrat");


            StackPanel panel = new StackPanel();
            panel.HorizontalAlignment = HorizontalAlignment.Right;
            panel.VerticalAlignment = VerticalAlignment.Center;
            foreach (DialogChoice choice in this.CurrentDialog.Current().Choices)
            {
                Button btn = new Button();
                btn.Content = choice.Text;
                btn.Tag = choice;
                btn.Padding = new Thickness(5);
                btn.Click += ChoiceClick;
                panel.Children.Add(btn);

                ((Button)this.Dialog.FindName("btn_Next")).IsEnabled = false;
            }
            container.Children.Add(panel);

            if (this.CurrentDialog.Index <= 0)
            {
                ((Button)this.Dialog.FindName("btn_Prev")).IsEnabled = false;
            }

            Game.Instance.WindowController.Update();

            this.TypingTicks = 0;
            this.TypingTimer.Elapsed -= TypeIncremental;
            this.TypingTimer.Elapsed += TypeIncremental;
            this.TypingTimer.Interval = 30;
            this.TypingTimer.Start();
        }

        private void ChoiceClick(object sender, RoutedEventArgs e)
        {
            ((DialogChoice)((Button)sender).Tag).Choose();

            this.NextDialog(sender, null);
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
        
        private void NextDialog(object sender, RoutedEventArgs e)
        {
            Grid container = (Grid)LogicalTreeHelper.FindLogicalNode(WindowController.Get<MainWindow>(), "grid_View");

            this.CurrentDialog.Current().EndEffectsFinished += NextDialogLogic;

            this.CurrentDialog.Current().StartEndEffects();
        }

        private void NextDialogLogic(object sender, EventArgs e)
        {
            WindowController.Get<MainWindow>().Dispatcher.Invoke(new System.Action(() =>
            {
                Grid container = (Grid)LogicalTreeHelper.FindLogicalNode(WindowController.Get<MainWindow>(), "grid_View");

                ((Button)this.Dialog.FindName("btn_Next")).Click -= NextDialog;
                ((Button)this.Dialog.FindName("btn_Prev")).Click -= PrevDialog;

                this.CurrentDialog.Current().EndEffectsFinished -= NextDialogLogic;

                if (!this.CurrentDialog.IsLastDialog())
                {
                    this.CurrentDialog.Next();

                    this.DrawDialog(this.CurrentDialog);
                }
                else
                {
                    this.RemoveDialog(sender, null);
                }
            }));
        }

        private void PrevDialog(object sender, RoutedEventArgs e)
        {
            Grid container = (Grid)LogicalTreeHelper.FindLogicalNode(WindowController.Get<MainWindow>(), "grid_View");

            if (this.CurrentDialog.Index > 0)
            {
                this.CurrentDialog.Current().EndEffectsFinished -= NextDialogLogic;

                this.CurrentDialog.Previous();

                ((Button)this.Dialog.FindName("btn_Next")).Click -= NextDialog;
                ((Button)this.Dialog.FindName("btn_Prev")).Click -= PrevDialog;

                this.DrawDialog(this.CurrentDialog);
            }
        }

        private void RemoveDialog(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Grid container = (Grid)LogicalTreeHelper.FindLogicalNode(WindowController.Get<MainWindow>(), "grid_View");

            Game.Instance.Player.Location.UnblurBackground();

            ((Button)this.Dialog.FindName("btn_Next")).Click -= NextDialog;
            ((Button)this.Dialog.FindName("btn_Prev")).Click -= PrevDialog;

            container.Children.RemoveRange(1, container.Children.Count);
            
            container.PreviewMouseRightButtonUp -= ToggleDialog;

            this.CurrentDialog = null;
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
