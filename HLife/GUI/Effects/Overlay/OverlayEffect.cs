using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HLife.GUI.Effects
{
    public class OverlayEffect
        : Effect
    {
        /// <summary>
        /// The Control in which the Target will reside.
        /// </summary>
        public Panel Container { get; set; }

        /// <summary>
        /// The Control to which to apply the effect.
        /// </summary>
        public Label Target { get; set; }

        public OverlayEffect()
        {
            this.Target = new Label();
            this.Target.VerticalAlignment = VerticalAlignment.Stretch;
            this.Target.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.Target.Background = new SolidColorBrush(Brushes.White.Color);
            this.Target.Background.Opacity = 0;
        }

        public OverlayEffect(Panel container)
            : this()
        {
            this.Container = container;
        }

        public override void Prepare()
        {
            base.Prepare();

            WindowController.Get<MainWindow>().Dispatcher.Invoke(new System.Action(() => { this.Container.Children.Add(this.Target); }));
        }

        public override void Start()
        {
            WindowController.Get<MainWindow>().Dispatcher.Invoke(new System.Action(() =>
            {
                this.Target.Background.Opacity = 1;
            }));

            base.Start();
        }

        public override void Stop()
        {
            base.Stop();

            WindowController.Get<MainWindow>().Dispatcher.Invoke(new System.Action(() =>
            {
                this.Container.Children.Remove(this.Target);
                this.Target.Background.Opacity = 0;
            }));

            this.Container.Children.Remove(this.Target);
        }
    }
}
