using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LianLianXuan_Prj.View;

namespace LianLianXuan_Prj
{
    public partial class GameStage : Form
    {
        private const int FPS = 30;

        private View.ViewManager _viewManager;
        private Model.Model _mainModel;
        private Controller.Controller _mainController;

        public GameStage()
        {
            InitializeComponent();

            // Init. all data members
            // 1. View
            int wigetWidth = (Model.Model.TOT_BLOCK_CNT_X - 1)*Model.Model.BLOCK_MARGIN
                             + Model.Model.TOT_BLOCK_CNT_X * Model.Model.BLOCK_SIZE_X;
            int wigetHeight = (Model.Model.TOT_BLOCK_CNT_Y - 1) * Model.Model.BLOCK_MARGIN
                             + Model.Model.TOT_BLOCK_CNT_Y * Model.Model.BLOCK_SIZE_Y;
            this.Size = new Size(wigetWidth, wigetHeight); // Reset size of the main form
            Rectangle wigetSize = new Rectangle(0, 0, wigetWidth, wigetHeight);
            _viewManager = new ViewManager(this.CreateGraphics(), wigetSize);
            // 2. Model
            _mainModel = new Model.Model();
            // - Bind all Views
            _viewManager.Bind(new MainView(_mainModel));
            // 3. Controller
            _mainController = new Controller.Controller(_mainModel);

            // Set Timer
            ViewUpdateTimer.Interval = 1000 / FPS;

        }

        private void GameStage_Load(object sender, EventArgs e)
        {
            // Start ViewUpdateTimer
            ViewUpdateTimer.Start();
        }

        private void ViewUpdateTimer_Tick(object sender, EventArgs e)
        {
            // Update View
            _viewManager.Update();
        }

        private void GameStage_MouseClick(object sender, MouseEventArgs e)
        {
            // Mouse Click
            if (e.Button == MouseButtons.Right)
            {
                _mainController.MouseClick(e.X, e.Y, true);
            }
            else if (e.Button == MouseButtons.Left)
            {
                _mainController.MouseClick(e.X, e.Y, false);
            }
            
        }

        
    }
}
