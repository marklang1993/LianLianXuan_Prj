using System;
using System.Drawing;
using System.Text;

namespace LianLianXuan_Prj.View
{
    public class MainView : View
    {
        private Bitmap[] blockImages; // block images

        public MainView()
        {
            // Load all block images
            blockImages = new Bitmap[Model.Model.IMAGES_CNT];
            for (int i = 0; i < Model.Model.IMAGES_CNT; ++i)
            {
                StringBuilder sb = new StringBuilder(@"images\");
                sb.Append((i + 1).ToString());
                sb.Append(".jpg");
                blockImages[i] = new Bitmap(sb.ToString());
            }
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
            g.DrawImage(blockImages[imageID], rect);
        }

        public override void Paint(Graphics g)
        {
            for (int i = 1; i <= 10; ++i)
            {
                for (int j = 1; j <= 8; ++j)
                {
                    int id = ((i*j - 1) + i - 1)%40;
                    _paintBlock(g, i, j, id);
                }
            }
        }
    }
}
