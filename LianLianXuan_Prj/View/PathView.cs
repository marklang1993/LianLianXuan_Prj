using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using LianLianXuan_Prj.Model;

namespace LianLianXuan_Prj.View
{
    public class PathView : DelayedDisplayView
    {
        private Point[] _pointList;
        private Pen _linePen;
        private SolidBrush _rectBrush;

        public PathView(Model.Model model)
            : base(model)
        {
            _pointList = null;
            _linePen = new Pen(Color.Blue, 3);
            _rectBrush = new SolidBrush(Color.GreenYellow);
        }

        /// <summary>
        /// Convert grid position to graphics position for path
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private Point _positionConvertPath(Position pos)
        {
            Point pt = new Point(0, 0);
            pt.X = Model.Model.BLOCK_SIZE_X/2
                   + (Model.Model.BLOCK_SIZE_X + Model.Model.BLOCK_MARGIN)*pos.X;
            pt.Y = Model.Model.BLOCK_SIZE_Y/2
                   + (Model.Model.BLOCK_SIZE_Y + Model.Model.BLOCK_MARGIN)*pos.Y;
            return pt;
        }

        /// <summary>
        /// Convert graphics position to the position for painting merged block
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private Rectangle _positionConvertBlock(Position pos)
        {
            Rectangle rect;
            int ptX = pos.X - Model.Model.BLOCK_SIZE_X / 2;
            int ptY = pos.Y - Model.Model.BLOCK_SIZE_Y / 2;
            rect = new Rectangle(ptX, ptY, Model.Model.BLOCK_SIZE_X, Model.Model.BLOCK_SIZE_Y);
            return rect;
        }

        protected override void Draw(Graphics g)
        {
            if (_pointList == null) return;
            // Draw Block
            int endIdx = _pointList.Length - 1;
            Rectangle startRect = _positionConvertBlock(new Position(_pointList[0].X, _pointList[0].Y));
            Rectangle endRect = _positionConvertBlock(new Position(_pointList[endIdx].X, _pointList[endIdx].Y));
            g.FillRectangle(_rectBrush, startRect);
            g.FillRectangle(_rectBrush, endRect);
            // Draw path
            g.DrawLines(_linePen, _pointList);
        }

        protected override bool Update()
        {
            // Convert Next
            List<Position> pathNodes = _model.GetConnectedPath(); // Get raw path
            _model.AcquireResourceMutex(Model.Model.MutexType.PATH_NODES);
            if (pathNodes.Count == 0)
            {
                // No path node existed, release & return
                _model.ReleaseResourceMutex(Model.Model.MutexType.PATH_NODES);
                return false;
            }
            // Convert
            _pointList = new Point[pathNodes.Count];
            for (int i = 0; i < pathNodes.Count; ++i)
            {
                _pointList[i] = _positionConvertPath(pathNodes.ElementAt(i));
            }
            pathNodes.Clear(); // Clear the original list
            _model.ReleaseResourceMutex(Model.Model.MutexType.PATH_NODES);

            return true;
        }
    }
}
