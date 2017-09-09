using System;
using System.Threading;
using System.Windows.Forms;

namespace LianLianXuan_Prj.Model
{
    public class Model
    {
        // Some constants
        public const int TOT_BLOCK_CNT_X = 12; // Total
        public const int TOT_BLOCK_CNT_Y = 10;
        public const int GRID_BLOCK_CNT_X = 10; // Grid
        public const int GRID_BLOCK_CNT_Y = 8;
        public const int BLOCK_SIZE_X = 80; // Block size in pixels
        public const int BLOCK_SIZE_Y = 80;
        public const int BLOCK_MARGIN = 10; // Margin size
        public const int IMAGES_CNT = 40; // Count of images

        // Define GameState
        private enum GameState
        {
            START,
            PLAYING,
            PAUSE,
            END
        };

        // Members
        private Grid _grid; // The whole grid
        private Tuple _tuple; // The selected blocks' position (only 2 blocks)
        private GameState _gameState;

        /// <summary>
        /// Constructor
        /// </summary>
        public Model()
        {
            // Init.
            _gameState = GameState.START;
            _grid = new Grid();
            _tuple = new Tuple(_grid);

            _gameState = GameState.PLAYING;

            // Test();
        }

        /// <summary>
        /// Calculate Manhattan Heuristic
        /// </summary>
        /// <param name="curPos">Current position</param>
        /// <param name="goalPos">Goal position</param>
        /// <returns></returns>
        private int _getHeuristic(Position curPos, Position goalPos)
        {
            // Manhattan Heuristic
            int xDis = Math.Abs(goalPos.X - curPos.X);
            int yDis = Math.Abs(goalPos.Y - curPos.Y);

            return xDis + yDis;
        }

        /// <summary>
        /// Calculate Expected Cost as Priority
        /// </summary>
        /// <param name="curPos">Current position</param>
        /// <param name="goalPos">Goal position</param>
        /// <param name="curCost">Current cost from start point</param>
        /// <returns></returns>
        private int _calculateExpectCost(Position curPos, Position goalPos, int curCost)
        {
            return _getHeuristic(curPos, goalPos) + curCost;
        }

        /// <summary>
        /// Expand nodes by BFS in single direction
        /// </summary>
        /// <param name="searchQueue">Searching queue</param>
        /// <param name="newBlock">Block in new position</param>
        /// <param name="goalPos">Goal position</param>
        /// <param name="curCost">Current cost from start point</param>
        /// <param name="grid">Grid</param>
        private void _expandSingle(PriorityQueue searchQueue, Block newBlock, Position goalPos, int curCost, Grid grid)
        {
            if (newBlock != null) // If such block does not exist due to the invalid coordinates
            {
                Position newPos = newBlock.GetPos();
                if (newBlock.IsNull())
                {
                    // Empty Block, able to pass
                    searchQueue.Enqueue(_calculateExpectCost(newBlock.GetPos(), goalPos, curCost), newPos);
                }
            }
        }

        /// <summary>
        /// Expand nodes by BFS
        /// </summary>
        /// <param name="searchQueue">Searching queue</param>
        /// <param name="curPos">Current position</param>
        /// <param name="goalPos">Goal position</param>
        /// <param name="curCost">Current cost from start point</param>
        /// <param name="grid">Grid</param>
        private void _expandBFS(PriorityQueue searchQueue, Position curPos, Position goalPos, int curCost, Grid grid)
        {
            _expandSingle(searchQueue, grid.GetBlock(curPos.Up()), goalPos, curCost, grid);
            _expandSingle(searchQueue, grid.GetBlock(curPos.Down()), goalPos, curCost, grid);
            _expandSingle(searchQueue, grid.GetBlock(curPos.Left()), goalPos, curCost, grid);
            _expandSingle(searchQueue, grid.GetBlock(curPos.Right()), goalPos, curCost, grid);
        }

        /// <summary>
        /// Determine finding path is finished
        /// </summary>
        /// <param name="curPos">Current position</param>
        /// <param name="goalPos">Goal position</param>
        /// <returns></returns>
        private bool _isArrived(Position curPos, Position goalPos)
        {
            if (curPos.Up().IsEqual(goalPos)) return true;
            if (curPos.Down().IsEqual(goalPos)) return true;
            if (curPos.Left().IsEqual(goalPos)) return true;
            if (curPos.Right().IsEqual(goalPos)) return true;
            return false;
        }

        /// <summary>
        /// Deterimine two blocks can be connected
        /// </summary>
        /// <param name="startPos">Start block position</param>
        /// <param name="endPos">End block position</param>
        /// <param name="grid">Grid</param>
        /// <returns></returns>
        private bool _isConnected(Position startPos, Position endPos, Grid grid)
        {
            // Get blocks tuple
            Block start = grid.GetBlock(startPos);
            Block end = grid.GetBlock(endPos);

            // Check prerequisites: valid positions, valid blocks, same type and blocks in different positions
            if (start == null || end == null) return false;
            if (start.IsNull() || end.IsNull()) return false;
            if (!Block.TypeEqual(start, end)) return false;
            if (start.GetPos().IsEqual(end.GetPos())) return false;

            // Find path by BFS
            PriorityQueue searchQueue = new PriorityQueue();
            int currentCost = 0;
            searchQueue.Enqueue(_calculateExpectCost(startPos, endPos, currentCost), start.GetPos());
            while (true)
            {
                // failed
                if (searchQueue.IsEmpty()) return false;
                // found
                Position curPos = searchQueue.Dequeue();
                if (_isArrived(curPos, end.GetPos())) return true;
                // inc. current cost
                ++currentCost;
                // expand
                _expandBFS(searchQueue, curPos, endPos, currentCost, grid);
            }
        }

        private void _gameEnd()
        {
            
        }

        /// <summary>
        /// For DEBUG use only
        /// </summary>
        public void Test()
        {
            // Must set PRNG in grid by using seed = 10
            Console.WriteLine(_isConnected(new Position(1, 1), new Position(10, 1), _grid));
            Console.WriteLine(_isConnected(new Position(1, 7), new Position(2, 8), _grid));
            Console.WriteLine(_isConnected(new Position(6, 4), new Position(8, 4), _grid));
            Console.WriteLine(_isConnected(new Position(1, 1), new Position(9, 1), _grid));
            Console.WriteLine(_isConnected(new Position(8, 2), new Position(10, 2), _grid));
        }

        /// <summary>
        /// Get Map
        /// </summary>
        /// <returns></returns>
        public Block[][] GetMap()
        {
            return _grid.GetMap();
        }

        /// <summary>
        /// Get tuple with selected blocks
        /// </summary>
        /// <returns></returns>
        public Tuple GetSelectedBlocksTuple()
        {
            return _tuple;
        }

        /// <summary>
        /// Mouse Right Click Handler
        /// </summary>
        /// <param name="xMouse"></param>
        /// <param name="yMouse"></param>
        public void RightClickHandler(int xMouse, int yMouse)
        {
            _tuple.Clear();
        }

        /// <summary>
        /// Mouse Left Click Handler
        /// </summary>
        /// <param name="xMouse"></param>
        /// <param name="yMouse"></param>
        public void LeftClickHandler(int xMouse, int yMouse)
        {
            // Select a block
            int x = xMouse / (Model.BLOCK_SIZE_X + Model.BLOCK_MARGIN);
            int y = yMouse / (Model.BLOCK_SIZE_Y + Model.BLOCK_MARGIN);
            Position blockPosition = new Position(x, y);
            if (!_tuple.Select(blockPosition)) return;

            // Check is connected AND required to merge
            if (_tuple.IsTuple())
            {
                Position startPos = _tuple.GetFirst();
                Position endPos = _tuple.GetSecond();
                if (_isConnected(startPos, endPos, _grid))
                {
                    // Connected, need to be merged
                    _tuple.Clear(); // Clear tuple

                    _grid.GetBlock(startPos).ToNullBlock();
                    _grid.GetBlock(endPos).ToNullBlock();

                    // Goto check game end
                    _gameEnd();
                }
            }
        }
    }
}
