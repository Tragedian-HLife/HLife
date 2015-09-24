using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Shapes;

namespace HLife.GUI.Effects
{
    public class Effect
    {
        /// <summary>
        /// Timer to control the effect.
        /// </summary>
        protected Timer Timer { get; set; }

        /// <summary>
        /// Number of times the timer has elapsed.
        /// </summary>
        public int TimesElapsed { get; protected set; }

        /// <summary>
        /// Number of times the timer should be allowed to elapse.
        /// </summary>
        public int Cycles { get; set; }

        /// <summary>
        /// Number of times the entire effect should be played.
        /// </summary>
        public int SuperCycles { get; set; }

        /// <summary>
        /// Number of times the entire effect should be played.
        /// </summary>
        public int SuperCycle { get; protected set; }

        /// <summary>
        /// Cycles / 2.
        /// </summary>
        protected double HalfCycles { get; set; }

        /// <summary>
        /// The Control in which the Target will reside.
        /// </summary>
        public Panel Container { get; set; }

        /// <summary>
        /// The Control to which to apply the effect.
        /// </summary>
        public Label Target { get; set; }

        /// <summary>
        /// The speed of the effect.
        /// Defaults to 1.
        /// </summary>
        public int StepsPerInterval { get; set; }

        public bool DisposeAtEnd { get; set; }
        
        /// <summary>
        /// Fired once the effect reaches the set number of cycles.
        /// </summary>
        public event ElapsedEventHandler Finished;

        public Effect()
        {
            this.TimesElapsed = 0;

            this.Cycles = 1;

            this.SuperCycles = 1;

            this.SuperCycle = 0;

            this.StepsPerInterval = 1;

            this.Timer = new Timer(100);

            this.Timer.Elapsed += Timer_Elapsed;

            this.Target = new Label();
            this.Target.VerticalAlignment = VerticalAlignment.Stretch;
            this.Target.HorizontalAlignment = HorizontalAlignment.Stretch;

            this.DisposeAtEnd = true;
        }

        public Effect(Panel container)
            : this()
        {
            this.Container = container;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Target.Dispatcher.Invoke(new System.Action(this.Update));

            this.TimesElapsed++;

            if(this.TimesElapsed >= this.Cycles)
            {
                this.SuperCycle++;
                this.TimesElapsed = 0;

                if (this.SuperCycle >= this.SuperCycles)
                {
                    this.Target.Dispatcher.Invoke(new System.Action(this.Stop));

                    ElapsedEventHandler handler = this.Finished;
                    if (handler != null)
                    {
                        handler(this, e);
                    }
                }
            }
        }

        public virtual void Update()
        { }

        public virtual void StartLogic()
        { }

        public virtual void StopLogic()
        { }

        public void Start()
        {
            this.Target.Dispatcher.Invoke(new System.Action(() => { this.Container.Children.Add(this.Target); }));

            this.Target.Dispatcher.Invoke(new System.Action(this.StartLogic));

            this.HalfCycles = (double)this.Cycles / 2.0;

            this.Timer.AutoReset = this.Cycles > 1;

            this.Timer.Start();
        }

        public void Stop()
        {
            this.Timer.Stop();

            this.Target.Dispatcher.Invoke(new System.Action(this.StopLogic));

            this.TimesElapsed = 0;
            this.SuperCycle = 0;

            this.Container.Children.Remove(this.Target);
        }

        public void Pause()
        {
            this.Timer.Stop();
        }

        public void Restart()
        {
            this.Timer.Stop();

            this.Timer.Start();
        }
    }
}
