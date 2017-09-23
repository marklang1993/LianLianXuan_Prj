using System;
using System.Drawing;
using System.Text;
using LianLianXuan_Prj.Model;

namespace LianLianXuan_Prj.View
{
    public class ScoreView : View
    {
        public const int SCORE_VIEW_HEIGHT = 40;

        private RectangleF _drawLayout; // Offset of drawing

        private Font _drawFont;
        private Brush _drawBrush;
        private StringFormat _stringFormat;

        public ScoreView(Model.Model model, Size stageSize)
            : base(model)
        {
            _drawLayout = new RectangleF(
                0, 
                stageSize.Height - SCORE_VIEW_HEIGHT,
                stageSize.Width,
                SCORE_VIEW_HEIGHT
                );

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
               Score score = _model.GetScore();
               TimeSpan timeElapsed = new TimeSpan(DateTime.Now.Ticks - score.GetTotalTicks());
               StringBuilder sb = new StringBuilder(@"目前得分：");
               sb.Append(score.GetTotalScore().ToString());
               sb.Append(@"     目前连续炫技次数：");
               sb.Append(score.GetComboCount().ToString());
               sb.Append(@"     炫技时间：");
               sb.Append((int)timeElapsed.TotalSeconds);
               sb.Append(@" 秒");
               g.DrawString(sb.ToString(), _drawFont, _drawBrush, _drawLayout, _stringFormat);
           }
        }
    }
}
