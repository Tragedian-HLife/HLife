using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;

namespace HLife.GUI.Effects
{
    public class EffectStack
    {
        public List<Effect> Effects { get; set; }

        private int FinishedEffects { get; set; }

        public event EventHandler Started;

        public event EventHandler Finished;

        public EffectStack()
        {
            this.Effects = new List<Effect>();
        }

        public Effect Add(Effect add)
        {
            this.Effects.Add(add);

            add.Finished += EffectFinished;

            return add;
        }

        public bool Remove(Effect add)
        {
            if (this.Effects.Contains(add))
            {
                this.Effects.Remove(add);

                add.Finished -= EffectFinished;

                return true;
            }

            return false;
        }

        public void Prepare(Panel container = null)
        {
            foreach (Effect effect in this.Effects)
            {
                if (effect.GetType().BaseType == typeof(OverlayEffect))
                {
                    ((OverlayEffect)effect).Container = container;
                }

                effect.Prepare();
            }
        }

        public void Start()
        {
            if (this.Effects.Count == 0)
            {
                this.TriggerFinished();
            }

            this.TriggerStarted();

            foreach (Effect effect in this.Effects)
            {
                effect.Start();
            }
        }

        private void EffectFinished(object sender, ElapsedEventArgs e)
        {
            this.FinishedEffects++;

            if (this.FinishedEffects >= this.Effects.Count)
            {
                this.TriggerFinished();
            }
        }

        private void TriggerStarted()
        {
            EventHandler temp = this.Started;
            if (temp != null)
            {
                temp(this, null);
            }

            this.FinishedEffects = 0;
        }

        private void TriggerFinished()
        {
            EventHandler temp = this.Finished;
            if (temp != null)
            {
                temp(this, null);
            }

            this.FinishedEffects = 0;
        }
    }
}
