using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLife.Actions
{
    public class PlannedAction
    {
        public GameAction DestinationAction { get; set; }

        public ActionEventArgs DestinationActionArgs { get; set; }

        public Queue<KeyValuePair<GameAction, ActionEventArgs>> ActionChain { get; set; }

        public event EventHandler<ActionEventArgs> StepBegin;

        public event EventHandler<ActionEventArgs> StepFinished;

        public PlannedAction()
        {
            this.ActionChain = new Queue<KeyValuePair<GameAction, ActionEventArgs>>();
        }

        private void TriggerStepBegin()
        {
            EventHandler<ActionEventArgs> handle = this.StepBegin;
            if (handle != null)
            {
                handle.Invoke(this, null);
            }
        }

        private void TriggerStepFinished()
        {
            EventHandler<ActionEventArgs> handle = this.StepFinished;
            if (handle != null)
            {
                handle.Invoke(this, null);
            }
        }

        public GameAction Step()
        {
            this.TriggerStepBegin();

            GameAction next = null;
            ActionEventArgs args = null;

            if (this.ActionChain.Count > 0)
            {
                var pair = this.ActionChain.Dequeue();

                next = pair.Key;

                args = pair.Value;
            }
            else
            {
                next = DestinationAction;

                args = this.DestinationActionArgs;
            }

            next.Performed += ActionFinished;
            next.Perform(args);

            return next;
        }

        private void ActionFinished(object sender, ActionEventArgs e)
        {
            this.TriggerStepFinished();
        }
    }
}
