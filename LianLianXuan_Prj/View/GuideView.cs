using System.Drawing;

namespace LianLianXuan_Prj.View
{
    public class GuideView : View
    {
        private readonly Bitmap _bgp; // Guide background picture
        private Rectangle _drawSize;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model">Game Model</param>
        /// <param name="drawSize">Drawing Size</param>
        public GuideView(Model.Model model, Rectangle drawSize) : base(model)
        {
            _bgp = new Bitmap(@"images/Guide.jpg");
            _drawSize = drawSize;
        }

        public override void Paint(Graphics g)
        {
            // Check the game state
            if (_model.GetState() == Model.Model.GameState.GUIDE)
            {
                g.DrawImage(_bgp, _drawSize);
            }
        }
    }
}
