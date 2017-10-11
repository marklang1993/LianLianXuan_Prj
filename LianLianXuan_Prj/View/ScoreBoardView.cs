using System.Collections.Generic;
using System.Drawing;
using LianLianXuan_Prj.Model;

namespace LianLianXuan_Prj.View
{
    public class ScoreBoardView : View
    {
        private readonly Bitmap _bgp; // Scoreboard background picture
        private Rectangle _drawSize;

        private Font _drawFont;
        private Brush _titleDrawBrush;
        private Brush _scoreDrawBrush;
        private StringFormat _stringFormat;

        private RectangleF _baseLayout;

        public const int NAME_MAX_LENGTH = 15; // Maximum length a name strings


        public ScoreBoardView(Model.Model model, Rectangle drawSize)
            : base(model)
        {
            _bgp = new Bitmap(@"images/ScoreBoard.jpg");

            _drawFont = new Font(@"黑体", 16);
            _titleDrawBrush = new SolidBrush(Color.Red);
            _scoreDrawBrush = new SolidBrush(Color.Aqua);
            _stringFormat = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            _stringFormat.Alignment = StringAlignment.Center;
            _stringFormat.LineAlignment = StringAlignment.Center;

            _drawSize = drawSize;
            _baseLayout = new RectangleF(0, 100, 120, 40);
        }

        private void _layoutMoveNextHorizontal(ref RectangleF layout)
        {
            layout.X += _baseLayout.Width;
        }

        private void _layoutMoveNextVertical(ref RectangleF layout)
        {
            layout.X = _baseLayout.X;
            layout.Y += _baseLayout.Height;
        }

        public override void Paint(Graphics g)
        {
            // Determine the game state
            if (_model.GetState() == Model.Model.GameState.SCOREBOARD)
            {
                List<ScoreBoard.Score> list = _model.GetScoreList();
                RectangleF curLayout = _baseLayout;

                // Paint Background
                g.DrawImage(_bgp, _drawSize);

                // Paint Title
                g.DrawString(@"排名", _drawFont, _titleDrawBrush, curLayout, _stringFormat);
                _layoutMoveNextHorizontal(ref curLayout);
                g.DrawString(@"英雄大名", _drawFont, _titleDrawBrush, curLayout, _stringFormat);
                _layoutMoveNextHorizontal(ref curLayout);
                g.DrawString(@"炫技得分", _drawFont, _titleDrawBrush, curLayout, _stringFormat);
                _layoutMoveNextHorizontal(ref curLayout);
                g.DrawString(@"连连炫次数", _drawFont, _titleDrawBrush, curLayout, _stringFormat);
                _layoutMoveNextHorizontal(ref curLayout);
                g.DrawString(@"炫技时间", _drawFont, _titleDrawBrush, curLayout, _stringFormat);
                _layoutMoveNextHorizontal(ref curLayout);
                g.DrawString(@"提示次数", _drawFont, _titleDrawBrush, curLayout, _stringFormat);


                // Paint Scores
                int rank = 1;
                foreach (ScoreBoard.Score score in list)
                {
                    if (score.IsValid)
                    {
                        // Draw 1 score record
                        _layoutMoveNextVertical(ref curLayout);
                        g.DrawString(rank.ToString(), _drawFont, _scoreDrawBrush, curLayout, _stringFormat);
                        _layoutMoveNextHorizontal(ref curLayout);
                        g.DrawString(score.Name, _drawFont, _scoreDrawBrush, curLayout, _stringFormat);
                        _layoutMoveNextHorizontal(ref curLayout);
                        g.DrawString(score.TotalScore.ToString(), _drawFont, _scoreDrawBrush, curLayout, _stringFormat);
                        _layoutMoveNextHorizontal(ref curLayout);
                        g.DrawString(score.MaxCombos.ToString(), _drawFont, _scoreDrawBrush, curLayout, _stringFormat);
                        _layoutMoveNextHorizontal(ref curLayout);
                        g.DrawString(score.TimeElapsed.ToString(), _drawFont, _scoreDrawBrush, curLayout, _stringFormat);
                        _layoutMoveNextHorizontal(ref curLayout);
                        g.DrawString(score.TipTimes.ToString(), _drawFont, _scoreDrawBrush, curLayout, _stringFormat);

                        ++rank;
                    }
                }

            }
        }
    }
}
