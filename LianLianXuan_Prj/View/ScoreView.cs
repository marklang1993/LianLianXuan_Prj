using System;
using System.Drawing;
using System.Text;

namespace LianLianXuan_Prj.View
{
    public class ScoreView : View
    {
        public const int SCORE_VIEW_HEIGHT = 40;

        private RectangleF _drawLayout; // Offset of drawing
        private Size _stageSize; // MainStage Size

        private String _scoreStringPrefix;
        private Font _drawFont;
        private Brush _drawBrush;
        private StringFormat _stringFormat;

        public ScoreView(Model.Model model, Size stageSize)
            : base(model)
        {
            _stageSize = new Size(stageSize.Width, stageSize.Height);
            _drawLayout = new RectangleF(
                0, 
                stageSize.Height - SCORE_VIEW_HEIGHT,
                stageSize.Width,
                SCORE_VIEW_HEIGHT
                );

            _scoreStringPrefix = @"目前得分：";
            _drawFont = new Font(@"黑体", 16);
            _drawBrush = new SolidBrush(Color.Aqua);
            _stringFormat = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            _stringFormat.Alignment = StringAlignment.Center;
            _stringFormat.LineAlignment = StringAlignment.Center;
        }

        public override void Paint(Graphics g)
        {
           // Check the game state
           if (_model.GetState() == Model.Model.GameState.PLAYING)
           {
               StringBuilder sb = new StringBuilder(_scoreStringPrefix);
               sb.Append(_model.GetTotalScore().ToString());
               g.DrawString(sb.ToString(), _drawFont, _drawBrush, _drawLayout, _stringFormat);
           }
        }
    }
}
