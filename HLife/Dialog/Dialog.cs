﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HLife.GUI.Effects;
using System.Windows.Controls;

namespace HLife
{
    public class Dialog
    {
        public string RawText { get; set; }

        public string ParsedText { get; set; }

        public string Image { get; set; }

        public List<Effect> BeginEffects { get; protected set; }

        public List<Effect> EndEffects { get; protected set; }

        private int FinishedEndEffects { get; set; }

        private int FinishedBeginEffects { get; set; }

        public event EventHandler BeginEffectsFinished;

        public event EventHandler EndEffectsFinished;

        public Dialog()
        {
            this.BeginEffects = new List<Effect>();
            this.EndEffects = new List<Effect>();

            this.FinishedEndEffects = 0;
        }

        public Effect AddBeginEffect(Effect add)
        {
            this.BeginEffects.Add(add);

            add.Finished += BeginEffectFinished;

            return add;
        }

        public Effect AddEndEffect(Effect add)
        {
            this.EndEffects.Add(add);

            add.Finished += EndEffectFinished;

            return add;
        }

        public void StartBeginEffects(Panel container)
        {
            if (this.BeginEffects.Count == 0)
            {
                EventHandler temp = this.BeginEffectsFinished;
                if (temp != null)
                {
                    temp(this, null);
                }
            }

            foreach (Effect effect in this.BeginEffects)
            {
                effect.Container = container;
                effect.Start();
            }
        }

        public void StartEndEffects(Panel container)
        {
            if(this.EndEffects.Count ==0)
            {
                EventHandler temp = this.EndEffectsFinished;
                if (temp != null)
                {
                    temp(this, null);
                }
            }

            foreach (Effect effect in this.EndEffects)
            {
                effect.Container = container;
                effect.Start();
            }
        }

        private void EndEffectFinished(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.FinishedEndEffects++;

            if(this.FinishedEndEffects >= this.EndEffects.Count)
            {
                EventHandler temp = this.EndEffectsFinished;
                if (temp != null)
                {
                    temp(this, null);
                }

                this.FinishedEndEffects = 0;
            }
        }

        private void BeginEffectFinished(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.FinishedBeginEffects++;

            if (this.FinishedBeginEffects >= this.BeginEffects.Count)
            {
                EventHandler temp = this.BeginEffectsFinished;
                if (temp != null)
                {
                    temp(this, null);
                }

                this.FinishedBeginEffects = 0;
            }
        }
    }
}
