using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HLife.GUI.Effects;
using System.Windows.Controls;
using HLife.Choices;

namespace HLife.Dialogs
{
    public class Dialog
    {
        public string RawText { get; set; }

        public string ParsedText { get; set; }

        public string Image { get; set; }

        public List<DialogChoice> Choices { get; set; }

        protected List<EffectStack> BeginEffects { get; set; }

        protected List<EffectStack> EndEffects { get; set; }

        private int CurrentEffectStack { get; set; }

        public event EventHandler BeginEffectsFinished;

        public event EventHandler EndEffectsFinished;

        public Dialog()
        {
            this.BeginEffects = new List<EffectStack>();
            this.BeginEffects.Add(new EffectStack());
            this.BeginEffects.Last().Finished += BeginEffectFinished;

            this.EndEffects = new List<EffectStack>();
            this.EndEffects.Add(new EffectStack());
            this.EndEffects.Last().Finished += EndEffectFinished;

            this.Choices = new List<DialogChoice>();
        }

        public void ClearBeginEffects()
        {
            foreach(EffectStack stack in this.BeginEffects)
            {
                stack.Finished -= BeginEffectFinished;
            }

            this.BeginEffects.Clear();
        }

        public void ClearEndEffects()
        {
            foreach (EffectStack stack in this.EndEffects)
            {
                stack.Finished -= EndEffectFinished;
            }

            this.EndEffects.Clear();
        }

        public Effect AddBeginEffect(Effect add, int stack = 0)
        {
            while(this.BeginEffects.Count <= stack)
            {
                this.BeginEffects.Add(new EffectStack());

                this.BeginEffects.Last().Finished += BeginEffectFinished;
            }

            this.BeginEffects[stack].Add(add);

            return add;
        }

        public Effect AddEndEffect(Effect add, int stack = 0)
        {
            while (this.EndEffects.Count <= stack)
            {
                this.EndEffects.Add(new EffectStack());

                this.EndEffects.Last().Finished += EndEffectFinished;
            }

            this.EndEffects[stack].Add(add);

            return add;
        }

        public void PrepareBeginEffects(Panel container)
        {
            foreach (EffectStack effectStack in this.BeginEffects)
            {
                effectStack.Prepare(container);
            }
        }

        public void PrepareEndEffects(Panel container)
        {
            foreach (EffectStack effectStack in this.EndEffects)
            {
                effectStack.Prepare(container);
            }
        }

        public void StartBeginEffects()
        {
            if (this.BeginEffects.Count == 0)
            {
                EventHandler temp = this.BeginEffectsFinished;
                if (temp != null)
                {
                    temp(this, null);
                }
            }

            this.CurrentEffectStack = 0;

            this.BeginEffects[this.CurrentEffectStack].Start();
        }

        public void StartEndEffects()
        {
            if (this.EndEffects.Count == 0)
            {
                EventHandler temp = this.EndEffectsFinished;
                if (temp != null)
                {
                    temp(this, null);
                }
            }

            this.CurrentEffectStack = 0;

            this.EndEffects[this.CurrentEffectStack].Start();
        }

        private void EndEffectFinished(object sender, EventArgs e)
        {
            this.CurrentEffectStack++;

            if (this.CurrentEffectStack >= this.EndEffects.Count)
            {
                EventHandler temp = this.EndEffectsFinished;
                if (temp != null)
                {
                    temp(this, null);
                }

                this.CurrentEffectStack = 0;
            }
            else
            {
                this.EndEffects[this.CurrentEffectStack].Start();
            }
        }

        private void BeginEffectFinished(object sender, EventArgs e)
        {
            this.CurrentEffectStack++;

            if (this.CurrentEffectStack >= this.BeginEffects.Count)
            {
                EventHandler temp = this.BeginEffectsFinished;
                if (temp != null)
                {
                    temp(this, null);
                }

                this.CurrentEffectStack = 0;
            }
            else
            {
                this.BeginEffects[this.CurrentEffectStack].Start();
            }
        }
    }
}
