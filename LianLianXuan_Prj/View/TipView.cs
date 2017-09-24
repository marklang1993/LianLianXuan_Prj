using System.Drawing;
using LianLianXuan_Prj.Model;
using Tuple = LianLianXuan_Prj.Model.Tuple;

namespace LianLianXuan_Prj.View
{
    public class TipView : DelayedDisplayView
    {
        private Point[] _pairList;
        private readonly Pen _boarderPen;


        public TipView(Model.Model model)
            : base(model)
        {
            _pairList = null;
            _boarderPen = new Pen(Color.FromArgb(255, 200, 0), MainView.BOARDER_STROKE_SIZE);
        }

        protected override void Draw(Graphics g)
        {
            if (_pairList == null) return; // Validate
            MainView.PaintBoarder(g, _boarderPen, _pairList[0].X, _pairList[0].Y);
            MainView.PaintBoarder(g, _boarderPen, _pairList[1].X, _pairList[1].Y);
        }

        protected override bool Update()
        {
            // Convert Next
            Tip tip = _model.GetTip(); // Get raw Tip
            Tuple tipTuple = tip.Tuple;
            _model.AcquireResourceMutex(Model.Model.MutexType.TIP);
            if (tipTuple == null)
            {
                // No path node existed, release & return
                _model.ReleaseResourceMutex(Model.Model.MutexType.TIP);
                return false;
            }
            if (tip.IsActive == false)
            {
                // Tip is not activated, release & return
                _model.ReleaseResourceMutex(Model.Model.MutexType.TIP);
                return false;
            }
            // Convert
            _pairList = new Point[2];
            _pairList[0] = new Point(tipTuple.GetFirst().X, tipTuple.GetFirst().Y);
            _pairList[1] = new Point(tipTuple.GetSecond().X, tipTuple.GetSecond().Y);
            _model.ReleaseResourceMutex(Model.Model.MutexType.TIP);

            return true;
        }
    }
}
