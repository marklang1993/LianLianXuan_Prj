
using System.Drawing;

namespace LianLianXuan_Prj.View
{
    public class GameEndView : View
    {
        private readonly Bitmap _bgp; // Game End background picture
        private Rectangle _wigetSize;

        private Font _drawFont;
        private Brush _drawBrush;
        private StringFormat _stringFormat;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model">Game Model</param>
        public GameEndView(Model.Model model, Rectangle wigetSize)
            : base(model)
        {
            _bgp = new Bitmap(@"images/Win.jpg");
            _model = model;
            _wigetSize = wigetSize;

            _drawFont = new Font(@"黑体", 40);
            _drawBrush = new SolidBrush(Color.Aqua);
            _stringFormat = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            _stringFormat.Alignment = StringAlignment.Center;
            _stringFormat.LineAlignment = StringAlignment.Center;
        }

        public override void Paint(Graphics g)
        {
            // Check the game state
            if (_model.GetState() == Model.Model.GameState.END)
            {
                g.DrawImage(_bgp, _wigetSize);
                g.DrawString(@"最终得分：" + _model.GetScore().GetTotalScore(), _drawFont, _drawBrush, 540, 580);
                g.DrawString(@"连续炫技次数：" + _model.GetScore().GetMaxComboCount(), _drawFont, _drawBrush, 540, 660);
            }
        }
    }
}
