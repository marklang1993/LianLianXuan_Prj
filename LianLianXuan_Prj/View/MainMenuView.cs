using System.Drawing;

namespace LianLianXuan_Prj.View
{
    public class MainMenuView : BackGroundPictureView
    {
        private readonly int _penSize;
        private readonly Pen _rectPen;

        public MainMenuView(Model.Model model, Rectangle drawSize, string picPath) : base(model, drawSize, picPath)
        {
            // Init.
            _penSize = 3;
            _rectPen = new Pen(Color.Blue, _penSize);
        }

        public override void Paint(Graphics g)
        {
            // Check the game state
            if (_model.GetState() == Model.Model.GameState.MAIN_MENU)
            {
                PaintBGP(g);

                Rectangle drawingArea = _model.GetMainMenuItemArea();
                if (drawingArea.X >= 0)
                {
                    drawingArea.X -= _penSize;
                    drawingArea.Y -= _penSize;
                    drawingArea.Width += _penSize;
                    drawingArea.Height += _penSize;
                    // Draw Selection Rectangle
                    g.DrawRectangle(_rectPen, drawingArea);
                }
                
            }
        }
    }
}
