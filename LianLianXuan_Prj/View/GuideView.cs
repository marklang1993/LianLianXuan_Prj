using System.Drawing;

namespace LianLianXuan_Prj.View
{
    public class GuideView : BackGroundPictureView
    {
        public GuideView(Model.Model model, Rectangle drawSize, string picPath)
            : base(model, drawSize, picPath)
        {
        }

        public override void Paint(Graphics g)
        {
            // Check the game state
            if (_model.GetState() == Model.Model.GameState.GUIDE)
            {
                PaintBGP(g);
            }
        }
    }
}
