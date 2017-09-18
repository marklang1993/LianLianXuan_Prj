using System;
using System.Drawing;
using System.Text;
using LianLianXuan_Prj.Model;
using Tuple = LianLianXuan_Prj.Model.Tuple;

namespace LianLianXuan_Prj.View
{
    public class MainView : View
    {
        private readonly Bitmap[] _blockImages; // block images

        private const int BOARDER_STROKE_SIZE = 3; // boarder stroke size in pixel

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model">Game Model</param>
        public MainView(Model.Model model)
            : base(model)
        {
            // Load all block images
            _blockImages = new Bitmap[Model.Model.IMAGES_CNT];
            for (int i = 0; i < Model.Model.IMAGES_CNT; ++i)
            {
                StringBuilder sb = new StringBuilder(@"images\");
                sb.Append((i + 1).ToString());
                sb.Append(".jpg");
                _blockImages[i] = new Bitmap(sb.ToString());
            }
            // Init. Model
            _model = model;
        }

        private void _paintBlock(Graphics g, int x, int y, int imageID)
        {
            // ZERO is illegal coordinates
            if (x == 0 || y == 0)
            {
                Console.WriteLine("X = 0 OR Y = 0!");
                return;
            }
            // Check imageID
            if (imageID < -1 || imageID >= Model.Model.IMAGES_CNT)
            {
                Console.WriteLine("imageID Error!");
                return;
            }

            // Empty image
            if (imageID == -1) return;

            int posX = x*(Model.Model.BLOCK_MARGIN + Model.Model.BLOCK_SIZE_X);
            int posY = y*(Model.Model.BLOCK_MARGIN + Model.Model.BLOCK_SIZE_Y);
            Rectangle rect = new Rectangle(posX, posY, Model.Model.BLOCK_SIZE_X, Model.Model.BLOCK_SIZE_Y);
            g.DrawImage(_blockImages[imageID], rect);
        }

        private void _paintBoarder(Graphics g, int x, int y)
        {
            // ZERO is illegal coordinates
            if (x == 0 || y == 0)
            {
                Console.WriteLine("X = 0 OR Y = 0!");
                return;
            }

            // Paint boarder
            int xPixel = x * (Model.Model.BLOCK_MARGIN + Model.Model.BLOCK_SIZE_X) - BOARDER_STROKE_SIZE;
            int yPixel = y * (Model.Model.BLOCK_MARGIN + Model.Model.BLOCK_SIZE_Y) - BOARDER_STROKE_SIZE;
            int width = Model.Model.BLOCK_SIZE_X + BOARDER_STROKE_SIZE;
            int height = Model.Model.BLOCK_SIZE_Y + BOARDER_STROKE_SIZE;
            g.DrawRectangle(new Pen(Color.GreenYellow, BOARDER_STROKE_SIZE), xPixel, yPixel, width, height);
        }

        public override void Paint(Graphics g)
        {
            // Determine the game state
            if (_model.GetState() == Model.Model.GameState.PLAYING)
            {
                // Paint all blocks
                Block[][] map = _model.GetMap();
                for (int i = 1; i <= 10; ++i)
                {
                    for (int j = 1; j <= 8; ++j)
                    {
                        _paintBlock(g, i, j, map[i][j].GetImageId());
                    }
                }

                // Paint boarder of selected blocks
                Tuple tuple = _model.GetSelectedBlocksTuple();
                Position firstPos = tuple.GetFirst();
                Position secondPos = tuple.GetSecond();
                if (firstPos != null) _paintBoarder(g, firstPos.X, firstPos.Y);
                if (secondPos != null) _paintBoarder(g, secondPos.X, secondPos.Y);
            }
        }
    }
}
