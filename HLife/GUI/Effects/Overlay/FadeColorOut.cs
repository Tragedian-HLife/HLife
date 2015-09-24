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
    public class FadeColorOut
        : OverlayEffect
    {
        public Color Color { get; set; }

        public FadeColorOut(Panel container = null)
            : base(container)
        {
            this.Color = Brushes.Black.Color;

            this.Timer.Interval = 1;

            this.StepsPerInterval = 1;
        }

        public FadeColorOut(Color color, Panel container = null)
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
            this.Target.Background.Opacity = (double)(this.Cycles - this.TimesElapsed) / this.Cycles;
        }
    }
}
