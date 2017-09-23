using System;
using System.Drawing;

namespace LianLianXuan_Prj.View
{
    public abstract class DelayedDisplayView : View
    {
        private DateTime _curTime;
        public const int DISPLAY_TIME = 1000;


        protected DelayedDisplayView(Model.Model model)
            : base(model)
        {
            _curTime = DateTime.Now;
        }

        /// <summary>
        /// Actual Drawing function
        /// </summary>
        protected abstract void Draw(Graphics g);

        /// <summary>
        /// Drawing parameters update function
        /// </summary>
        protected abstract bool Update();

        /// <summary>
        /// Delayed Drawing framework
        /// </summary>
        private void DelayDraw(Graphics g)
        {
            // Draw
            TimeSpan timeSpan = new TimeSpan(DateTime.Now.Ticks - _curTime.Ticks);
            if (timeSpan.TotalMilliseconds < DISPLAY_TIME)
            {
                // Still in its lifetime
                Draw(g);
                return;
            }

            // Update drawing parameters
            if (Update())
            {
                // Update current time
                _curTime = DateTime.Now;   
            }
        }

        public override void Paint(Graphics g)
        {
            // Check the game state
            if (_model.GetState() == Model.Model.GameState.PLAYING)
            {
                DelayDraw(g);
            }
        }

    }
}
