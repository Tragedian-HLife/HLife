using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HLife_2
{
    public partial class MapWindow : Form
    {
        private Point StartPoint { get; set; }

        private Point EndPoint { get; set; }

        private Point LastDelta { get; set; }

        private Point LastDeltaEffective { get; set; }

        private Point EndCursor { get; set; }

        private Timer Timer { get; set; }

        public MapWindow()
        {
            InitializeComponent();

            this.Timer = new Timer();

            this.MouseDown += StartDrag;

            this.MouseUp += EndDrag;

            this.Timer.Tick += Momentum;
        }

        private void MapWindow_Load(object sender, EventArgs e)
        { }

        private void StartDrag(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.StartPoint = e.Location;

                this.EndPoint = this.AutoScrollPosition;

                this.LastDelta = new Point();

                this.LastDeltaEffective = new Point();

                this.MouseMove += Drag;
            }
        }

        private void EndDrag(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.StartPoint = e.Location;

                this.EndPoint = this.AutoScrollPosition;

                this.LastDeltaEffective = new Point();

                //this.LastDelta = new Point(30, 30);

                this.MouseMove -= Drag;

                //this.Timer.Start();
            }
        }

        private void Drag(object sender, MouseEventArgs e)
        {
            int dragThreshhold = 5;

            if (e.Button == MouseButtons.Left)
            {
                // d_final = Mouse Down Pos - Cur Mouse Pos - Last Scroll Pos
                int x_f = this.StartPoint.X - e.X - this.EndPoint.X;
                int y_f = this.StartPoint.Y - e.Y - this.EndPoint.Y;

                int deltaX = x_f - this.StartPoint.X;
                int deltaY = y_f - this.StartPoint.Y;

                if (Math.Abs(deltaX) > dragThreshhold ||
                    Math.Abs(deltaY) > dragThreshhold)
                {
                    this.SuspendLayout();
                    this.AutoScrollPosition = new Point(x_f, y_f);
                    this.ResumeLayout(true);

                    if (e.Clicks != 999)
                    {
                        this.LastDelta = new Point(deltaX, deltaY);
                    }
                }
                else
                {
                    if (e.Clicks != 999)
                    {
                        this.LastDelta = new Point();
                    }
                }
            }
        }

        private void Momentum(object sender, EventArgs e)
        {
            if(Math.Abs(this.LastDelta.X) < 5 && Math.Abs(this.LastDelta.Y) < 5)
            {
                this.EndPoint = this.AutoScrollPosition;

                this.Timer.Stop();
            }

            this.LastDelta = new Point((int)((double)this.LastDelta.X * 0.9), (int)((double)this.LastDelta.Y * 0.9));

            this.LastDeltaEffective = new Point(
                this.LastDeltaEffective.X + this.LastDelta.X,
                this.LastDeltaEffective.Y + this.LastDelta.Y);

            this.Drag(this, new MouseEventArgs(
                MouseButtons.Left, 
                999,
                this.StartPoint.X + this.LastDeltaEffective.X, 
                this.StartPoint.Y + this.LastDeltaEffective.Y, 
                0));
        }
    }
}
