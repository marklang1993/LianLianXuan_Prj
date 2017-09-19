using System;
using System.Drawing;
using Tuple = LianLianXuan_Prj.Model.Tuple;

namespace LianLianXuan_Prj.View
{
    public class TipView : View
    {
        private DateTime _curTime;
        private Point[] _pairList;
        private Pen _boarderPen;

        public const int DISPLAY_TIME = 1000;

        public TipView(Model.Model model)
            : base(model)
        {
            _curTime = DateTime.Now;
            _pairList = null;
            _boarderPen = new Pen(Color.FromArgb(255, 200, 0), MainView.BOARDER_STROKE_SIZE);
        }

        public override void Paint(Graphics g)
        {
            // Check the game state
            if (_model.GetState() == Model.Model.GameState.PLAYING)
            {
                // Draw
                TimeSpan timeSpan = new TimeSpan(DateTime.Now.Ticks - _curTime.Ticks);
                if (timeSpan.TotalMilliseconds < DISPLAY_TIME)
                {
                    // Still in its lifetime
                    if (_pairList != null)
                    {
                        MainView.PaintBoarder(g, _boarderPen, _pairList[0].X, _pairList[0].Y);
                        MainView.PaintBoarder(g, _boarderPen, _pairList[1].X, _pairList[1].Y);
                    }
                    return;
                }

                // Convert Next
                Tuple tipTuple = _model.GetTip(); // Get raw pair
                _model.AcquireResourceMutex();
                if (tipTuple == null)
                {
                    // No path node existed, release & return
                    _model.ReleaseResourceMutex();
                    return;
                }
                // Convert
                _pairList = new Point[2];
                _pairList[0] = new Point(tipTuple.GetFirst().X, tipTuple.GetFirst().Y);
                _pairList[1] = new Point(tipTuple.GetSecond().X, tipTuple.GetSecond().Y);
                _model.ReleaseResourceMutex();

                // Update current time
                _curTime = DateTime.Now;
            }
        }
    }
}
