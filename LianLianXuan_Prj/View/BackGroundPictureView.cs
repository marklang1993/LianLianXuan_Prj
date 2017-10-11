using System.Drawing;

namespace LianLianXuan_Prj.View
{
    public abstract class BackGroundPictureView : View
    {
        protected readonly Bitmap _bgp; // Guide background picture
        protected Rectangle _drawSize;

        protected BackGroundPictureView(Model.Model model, Rectangle drawSize, string picPath) : base(model)
        {
            _bgp = new Bitmap(picPath);
            _drawSize = drawSize;
        }

        protected void PaintBGP(Graphics g)
        {
            g.DrawImage(_bgp, _drawSize);
        }

    }
}
