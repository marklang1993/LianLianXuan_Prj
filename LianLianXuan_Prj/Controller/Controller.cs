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
        /// Mouse Moving Handler
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MouseMove(int x, int y)
        {
            if (_model.GetState() == Model.Model.GameState.MAIN_MENU)
            {
                _model.MainMenuMouseMoveHandler(x, y);
            }
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
                if (_model.GetState() == Model.Model.GameState.PLAYING)
                {
                    _model.GameRoundRightClickHandler(x, y);
                }
            }
            else
            {
                // Left
                if (_model.GetState() == Model.Model.GameState.PLAYING)
                {
                    _model.GameRoundLeftClickHandler(x, y);
                }
                else if (_model.GetState() == Model.Model.GameState.MAIN_MENU)
                {
                    // Main Menu
                    _model.MainMenuLeftClickHandler(x, y);
                }
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
            else if (key == Keys.N)
            {
                if (_model.GetState() == Model.Model.GameState.END)
                {
                    // Return to MainMenu
                    _model.ReturnToMainMenu();
                }
            }
            else if (key == Keys.Q)
            {
                if (_model.GetState() == Model.Model.GameState.PLAYING ||
                    _model.GetState() == Model.Model.GameState.SCOREBOARD ||
                    _model.GetState() == Model.Model.GameState.GUIDE)
                {
                    // Restart Game
                    _model.ReturnToMainMenu();
                }
            }
            
        }
    }
}
