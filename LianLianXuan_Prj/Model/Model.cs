using System;

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
        public enum GameState
        {
            START,
            PLAYING,
            PAUSE,
            END
        };

        // Members
        private BGMPlayer _bgmPlayer;
        private SEPlayer _sePlayer;
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
            _bgmPlayer = new BGMPlayer();
            _sePlayer = new SEPlayer();
            _grid = new Grid();
            _tuple = new Tuple(_grid);

            _gameState = GameState.PLAYING;
            //_bgmPlayer.Play();

            // Test();
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
        /// Get binary tuple of start and end positions given current position by expanding horizontally
        /// </summary>
        /// <param name="curPos">Current position</param>
        /// <param name="grid">Grid</param>
        /// <param name="curType">Current block type</param>
        /// <returns></returns>
        private Tuple _expandHorizontal(Position curPos, Grid grid, int curType)
        {
            int startPos, endPos;
            int i, j;
            j = curPos.Y;
            Tuple tuple = new Tuple(grid);

            // From current to leftmost
            startPos = curPos.X;
            i = startPos - 1;
            while (i >= 0)
            {
                Block curExploredBlock = grid.GetBlock(new Position(i, j));
                if (curExploredBlock.IsNull())
                {
                    // Null block can be passed
                    startPos = i;
                }
                else if (curExploredBlock.GetImageId() == curType)
                {
                    // Block with same type can be passed
                    startPos = i;

                }
                else
                {
                    // Cannot passed, terminated
                    break;
                }
                --i;
            }

            // From current to rightmost
            endPos = curPos.X;
            i = endPos + 1;
            while (i < Model.TOT_BLOCK_CNT_X)
            {
                Block curExploredBlock = grid.GetBlock(new Position(i, j));
                if (curExploredBlock.IsNull())
                {
                    // Null block can be passed
                    endPos = i;
                }
                else if (curExploredBlock.GetImageId() == curType)
                {
                    // Block with same type can be passed
                    endPos = i;

                }
                else
                {
                    // Cannot passed, terminated
                    break;
                }
                ++i;
            }

            bool ret = true;
            ret = ret && tuple.Select(new Position(startPos, j));
            ret = ret && tuple.Select(new Position(endPos, j));
            if (!ret)
            {
                throw new Exception();
            }
            return tuple;
        }

        /// <summary>
        /// Get binary tuple of start and end positions given current position by expanding vertically
        /// </summary>
        /// <param name="curPos">Current position</param>
        /// <param name="grid">Grid</param>
        /// <param name="curType">Current block type</param>
        /// <returns></returns>
        private Tuple _expandVertical(Position curPos, Grid grid, int curType)
        {
            int startPos, endPos;
            int i, j;
            i = curPos.X;
            Tuple tuple = new Tuple(grid);

            // From current to upmost
            startPos = curPos.Y;
            j = startPos - 1;
            while (j >= 0)
            {
                Block curExploredBlock = grid.GetBlock(new Position(i, j));
                if (curExploredBlock.IsNull())
                {
                    // Null block can be passed
                    startPos = j;
                }
                else if (curExploredBlock.GetImageId() == curType)
                {
                    // Block with same type can be passed
                    startPos = j;

                }
                else
                {
                    // Cannot passed, terminated
                    break;
                }
                --j;
            }

            // From current to downmost
            endPos = curPos.Y;
            j = endPos + 1;
            while (j < Model.TOT_BLOCK_CNT_Y)
            {
                Block curExploredBlock = grid.GetBlock(new Position(i, j));
                if (curExploredBlock.IsNull())
                {
                    // Null block can be passed
                    endPos = j;
                }
                else if (curExploredBlock.GetImageId() == curType)
                {
                    // Block with same type can be passed
                    endPos = j;

                }
                else
                {
                    // Cannot passed, terminated
                    break;
                }
                ++j;
            }

            bool ret = true;
            ret = ret && tuple.Select(new Position(i, startPos));
            ret = ret && tuple.Select(new Position(i, endPos));
            if (!ret)
            {
                throw new Exception();
            }
            return tuple;
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

            // Expand vertically and horizontally
            Tuple hStartTuple = _expandHorizontal(startPos, grid, start.GetImageId());
            Tuple vStartTuple = _expandVertical(startPos, grid, start.GetImageId());
            Tuple hEndTuple = _expandHorizontal(endPos, grid, end.GetImageId());
            Tuple vEndTuple = _expandVertical(endPos, grid, end.GetImageId());

            
            return true;

/*            // Find path by BFS
            PriorityQueue searchQueue = new PriorityQueue();
            int currentCost = 0;
            searchQueue.Enqueue(_calculateExpectCost(startPos, endPos, currentCost), start.GetPos());
            while (true)
            {
                // failed
                if (searchQueue.IsEmpty()) return false;
                if (searchQueue.Count() > 500) return false;
                // found
                Position curPos = searchQueue.Dequeue();
                if (_isArrived(curPos, end.GetPos())) return true;
                // inc. current cost
                ++currentCost;
                // expand
                _expandBFS(searchQueue, curPos, endPos, currentCost, grid);
            }*/
        }

        /// <summary>
        /// Game End Related Processing
        /// </summary>
        private void _gameEnd()
        {
            // Check
            if (_grid.IsAllInvalid())
            {
                // Game is end
                _gameState = GameState.END;
            }
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
        /// Get Current Game State
        /// </summary>
        /// <returns></returns>
        public GameState GetState()
        {
            return _gameState;
        }

        /// <summary>
        /// Mouse Right Click Handler
        /// </summary>
        /// <param name="xMouse"></param>
        /// <param name="yMouse"></param>
        public void RightClickHandler(int xMouse, int yMouse)
        {
            _tuple.Clear();

            _sePlayer.Cancel(); // Cancel SE
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
                _tuple.Clear(); // Clear tuple

                if (_isConnected(startPos, endPos, _grid))
                {
                    // Connected, need to be merged
                    _grid.GetBlock(startPos).ToNullBlock();
                    _grid.GetBlock(endPos).ToNullBlock();
                    _sePlayer.Merged(); // Merged SE

                    // Goto check game end
                    _gameEnd();
                }
                else
                {
                    _sePlayer.Failed(); // Failed SE
                }
            }
            else
            {
                _sePlayer.Clicked(); // Click SE
            }
        }

        /// <summary>
        /// Flip BGM Player
        /// </summary>
        public void FlipBGMPlayer()
        {
            _bgmPlayer.Flip();
        }

        /// <summary>
        /// Randomize all blocks
        /// </summary>
        public void RandonmizeAllBlocks()
        {
            _grid.Randomize();

            _sePlayer.Refresh(); // Refresh SE
        }

        public void RestartGame()
        {
            // Restart Game
            _grid.Reset();
            _tuple.Clear();
            _gameState = GameState.PLAYING;
        }
    }
}
