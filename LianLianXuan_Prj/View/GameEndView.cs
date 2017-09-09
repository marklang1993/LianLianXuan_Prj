
using System.Drawing;

namespace LianLianXuan_Prj.View
{
    public class GameEndView : View
    {
        private readonly Bitmap _bgp; // Game End background picture
        private Model.Model _model; // model

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model">Game Model</param>
        public GameEndView(Model.Model model)
        {
            _bgp = new Bitmap(@"images/Win.jpg");
            // Init. Model
            _model = model;
        }

        public override void Paint(Graphics g)
        {
            // Determine the game state
            if (_model.GetState() == Model.Model.GameState.END)
            {
                Rectangle rect = new Rectangle(0, 0, 1070, 890);
                g.DrawImage(_bgp, rect);
            }
        }
    }
}
