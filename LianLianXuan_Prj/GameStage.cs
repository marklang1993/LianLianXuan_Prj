using System;
using System.Drawing;
using System.Windows.Forms;
using LianLianXuan_Prj.View;

namespace LianLianXuan_Prj
{
    public partial class GameStage : Form
    {
        private const int FPS = 30;

        private const int DRAW_OFFSET_W = 20;
        private const int DRAW_OFFSET_H = 40;

        private ViewManager _viewManager;
        private Model.Model _mainModel;
        private Controller.Controller _mainController;

        public GameStage()
        {
            InitializeComponent();

            // Init. all data members
            // 1. View
            int drawWidth = (Model.Model.TOT_BLOCK_CNT_X - 1)*Model.Model.BLOCK_MARGIN
                            + Model.Model.TOT_BLOCK_CNT_X*Model.Model.BLOCK_SIZE_X;
            int drawHeight = (Model.Model.TOT_BLOCK_CNT_Y - 1) * Model.Model.BLOCK_MARGIN
                             + Model.Model.TOT_BLOCK_CNT_Y * Model.Model.BLOCK_SIZE_Y
                             + ScoreView.SCORE_VIEW_HEIGHT;
            int wigetWidth = drawWidth + DRAW_OFFSET_W;
            int wigetHeight = drawHeight + DRAW_OFFSET_H;
            this.Size = new Size(wigetWidth, wigetHeight); // Reset size of the main form
            Rectangle drawSize = new Rectangle(0, 0, drawWidth, drawHeight);
            _viewManager = new ViewManager(this.CreateGraphics(), drawSize);
            // 2. Model
            _mainModel = new Model.Model();
            // - Bind all Views
            _viewManager.Bind(new MainMenuView(_mainModel, drawSize));
            _viewManager.Bind(new TipView(_mainModel));
            _viewManager.Bind(new MainView(_mainModel));
            _viewManager.Bind(new GameEndView(_mainModel, drawSize));
            _viewManager.Bind(new ScoreView(_mainModel, new Size(drawWidth, drawHeight)));
            _viewManager.Bind(new PathView(_mainModel));
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

        private void GameStage_MouseMove(object sender, MouseEventArgs e)
        {
            // Mouse Moving
            _mainController.MouseMove(e.X, e.Y);
        }

        private void GameStage_KeyUp(object sender, KeyEventArgs e)
        {
            // Keyboard Key Up
            _mainController.KeyUp(e.KeyCode);
        }

    }
}
