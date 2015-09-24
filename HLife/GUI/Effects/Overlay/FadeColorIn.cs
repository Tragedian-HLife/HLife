using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace HLife.GUI.Effects
{
    public class FadeColorIn
        : OverlayEffect
    {
        public Color Color { get; set; }

        public FadeColorIn(Panel container = null)
            : base(container)
        {
            this.Color = Brushes.Black.Color;

            this.Timer.Interval = 1;

            this.StepsPerInterval = 2;
        }

        public FadeColorIn(Color color, Panel container = null)
            : this(container)
        {
            this.Color = color;
        }

        public override void StartLogic()
        {
            this.Cycles = (int)(100.0 / (double)this.StepsPerInterval);

            this.Target.Background = new SolidColorBrush(this.Color);
        }

        public override void Update()
        {
            this.Target.Background.Opacity = (double)this.TimesElapsed / this.Cycles;
        }
    }
}
