using System.Windows.Forms;

namespace LianLianXuan_Prj.Controller
{
    public class Controller
    {
        private Model.Model _model;

        public Controller(Model.Model model)
        {
            _model = model;
        }

        /// <summary>
        /// Mouse Click Handler
        /// </summary>
        /// <param name="x">Mouse x position in pixel</param>
        /// <param name="y">Mouse y position in pixel</param>
        /// <param name="isRightClick">Is this click a right click</param>
        public void MouseClick(int x, int y, bool isRightClick)
        {
            if (isRightClick)
            {
                // Right
                _model.RightClickHandler(x, y);
            }
            else
            {
                // Left
                _model.LeftClickHandler(x, y);
            }
        }

        /// <summary>
        /// Key Up Handler
        /// </summary>
        /// <param name="key"></param>
        public void KeyUp(Keys key)
        {
            if (key == Keys.M)
            {
                // Flip BGM Player
                _model.FlipBGMPlayer();
            }
            else if (key == Keys.R)
            {
                if (_model.GetState() == Model.Model.GameState.PLAYING)
                {
                    // Randomize all blocks
                    _model.RandonmizeAllBlocks();
                }
            }
            else if (key == Keys.T)
            {
                if (_model.GetState() == Model.Model.GameState.PLAYING)
                {
                    // Give a tip
                    _model.ActiveTip();
                }
            }
            else if (key == Keys.Y)
            {
                if (_model.GetState() == Model.Model.GameState.END)
                {
                    // Restart Game
                    _model.RestartGame();
                }
            }
            
        }
    }
}
