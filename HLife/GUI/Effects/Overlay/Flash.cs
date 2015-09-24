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
    public class Flash
        : OverlayEffect
    {
        public Color Color { get; set; }

        public Flash(Panel container = null)
            : base(container)
        {
            this.Color = Brushes.White.Color;

            this.Timer.Interval = 1;

            this.StepsPerInterval = 8;
        }

        public Flash(Color color, Panel container = null)
            : this(container)
        {
            this.Color = color;
        }

        public override void StartLogic()
        {
            this.Cycles = (int)(200.0 / (double)this.StepsPerInterval);

            this.Target.Background = new SolidColorBrush(this.Color);
        }

        public override void Update()
        {
            if (this.TimesElapsed <= this.HalfCycles)
            {
                this.Target.Background.Opacity = (double)this.TimesElapsed / this.HalfCycles;
            }
            else
            {
                this.Target.Background.Opacity = (double)(this.Cycles - this.TimesElapsed)  / this.HalfCycles;
            }
        }
    }
}
