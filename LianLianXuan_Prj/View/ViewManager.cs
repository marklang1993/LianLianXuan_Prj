using System.Collections.Generic;
using System.Drawing;

namespace LianLianXuan_Prj.View
{
    public class ViewManager
    {
        private readonly Graphics _drawingGraphics; // Graphics from wiget (NOT double bufferred graphics)
        private readonly Rectangle _wigetSize;
        private readonly List<View> _allViews;   // All Views

        public ViewManager(Graphics g, Rectangle wigetSize)
        {
            _allViews = new List<View>();
            _drawingGraphics = g;
            _wigetSize = wigetSize;
        }

        /// <summary>
        /// Add a View instance to the ViewManager
        /// </summary>
        /// <param name="viewComponent">View Instance</param>
        public void Bind(View viewComponent)
        {
            if (viewComponent != null)
            {
                _allViews.Add(viewComponent);
            }
        }

        /// <summary>
        /// Update all View components
        /// </summary>
        public void Update()
        {
            // Double Buffered Drawing Initialized
            BufferedGraphicsContext currentBufferedGraphicsContext = BufferedGraphicsManager.Current;
            BufferedGraphics bufferedGraphics = currentBufferedGraphicsContext.Allocate(_drawingGraphics, _wigetSize);
            Graphics drawingGrphics = bufferedGraphics.Graphics;

            // Draw background
            // drawingGrphics.FillRectangle(new SolidBrush(Color.White), _wigetSize);
            // Draw all views
            foreach (var viewComponent in _allViews)
            {
                viewComponent.Paint(drawingGrphics);
            }
            // Render the composed screen and Dispose the Buffer
            bufferedGraphics.Render();
            bufferedGraphics.Dispose();
        }

    }
}
