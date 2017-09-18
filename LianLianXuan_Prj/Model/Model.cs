﻿using System;
using System.Drawing.Text;

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
        private Score _score;


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
            _score = new Score();

            _gameState = GameState.PLAYING;
            _bgmPlayer.Play();

            // Test();
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
        /// Determine given line is horizontal line or vertical
        /// </summary>
        /// <param name="line">Tuple of line</param>
        /// <returns></returns>
        private bool _isHorizontal(Tuple line)
        {
            Position first = line.GetFirst();
            Position second = line.GetSecond();
            if (first.X == second.X)
            {
                return false;
            } 
            else if (first.Y == second.Y)
            {
                return true;
            }
            throw new Exception();
        }

        /// <summary>
        /// Check intersection between 2 lines
        /// </summary>
        /// <param name="lineA">Line A</param>
        /// <param name="lineB">Line B</param>
        /// <returns></returns>
        private bool _isIntersect(Tuple lineA, Tuple lineB)
        {
            Position intersection = null;
            bool isHorizontalLineA = _isHorizontal(lineA);
            bool isHorizontalLineB = _isHorizontal(lineB);
            Position startA = lineA.GetFirst();
            Position endA = lineA.GetSecond();
            Position startB = lineB.GetFirst();
            Position endB = lineB.GetSecond();
            // Validation
            if (startA.X > endA.X) throw new Exception();
            if (startA.Y > endA.Y) throw new Exception();
            if (startB.X > endB.X) throw new Exception();
            if (startB.Y > endB.Y) throw new Exception();

            if (isHorizontalLineA == isHorizontalLineB)
            {
                // Same direction
                if (isHorizontalLineA)
                {
                    // Horizontally - Y is fixed, X is varying
                    if (startA.Y != startB.Y) return false; // Parallel but not overlap
                    // On the same line
                    if (startA.X <= startB.X && endA.X >= startB.X)
                    {
                        intersection = new Position(startB.X, startB.Y);
                        return true;
                    }
                    else if (startA.X <= endB.X && endA.X >= endB.X)
                    {
                        intersection = new Position(endB.X, endB.Y);
                        return true;
                    }
                    return false;
                }
                else
                {
                    // Vertically - X is fixed, Y is varying
                    if (startA.X != startB.X) return false; // Parallel but not overlap
                    // On the same line
                    if (startA.Y <= startB.Y && endA.Y >= startB.Y)
                    {
                        intersection = new Position(startB.X, startB.Y);
                        return true;
                    }
                    else if (startA.Y <= endB.Y && endA.Y >= endB.Y)
                    {
                        intersection = new Position(endB.X, endB.Y);
                        return true;
                    }
                    return false;
                }
            }
            else
            {
                // Different directions
                if (isHorizontalLineA == false)
                {
                    // Line A is Vertical, Line B is Horizontal
                    intersection = new Position(startA.X, startB.Y);
                }
                else
                {
                    // Line A is Horizontal, Line B is Vertical
                    intersection = new Position(startB.X, startA.Y);
                }
                // Check the intersection is on both lines
                if (!(startA.X <= intersection.X && endA.X >= intersection.X)) return false;
                if (!(startB.X <= intersection.X && endB.X >= intersection.X)) return false;
                if (!(startA.Y <= intersection.Y && endA.Y >= intersection.Y)) return false;
                if (!(startB.Y <= intersection.Y && endB.Y >= intersection.Y)) return false;
                return true;
            }
        }

        /// <summary>
        /// Check is there a path by give line segment
        /// </summary>
        /// <param name="line"></param>
        /// <param name="grid"></param>
        /// <returns></returns>
        private bool _isExistedPath(Tuple line, Grid grid)
        {
            bool isHorizontalLine = _isHorizontal(line);
            Position start = line.GetFirst();
            Position end = line.GetSecond();

            if (isHorizontalLine)
            {
                // Horizontal Line
                for (int i = start.X; i <= end.X; ++i)
                {
                    if (!grid.GetBlock(new Position(i, start.Y)).IsNull())
                    {
                        return false;
                    }
                }
            }
            else
            {
                // Vertical Line
                for (int i = start.Y; i <= end.Y; ++i)
                {
                    if (!grid.GetBlock(new Position(start.X, i)).IsNull())
                    {
                        return false;
                    }
                }

            }
            return true;
        }

        /// <summary>
        /// Check is there a path with 2 turning points
        /// </summary>
        /// <param name="hStartTuple"></param>
        /// <param name="vStartTuple"></param>
        /// <param name="hEndTuple"></param>
        /// <param name="vEndTuple"></param>
        /// <param name="grid"></param>
        /// <returns></returns>
        private bool _isTuringTwice(Tuple hStartTuple, Tuple vStartTuple, Tuple hEndTuple, Tuple vEndTuple, Grid grid)
        {
            Tuple lineSegment;

            // Both horizontal lines - Base: hStartTuple
            Position hStartA = hStartTuple.GetFirst();
            Position hStartB = hStartTuple.GetSecond();
            Position hEndA = hEndTuple.GetFirst();
            Position hEndB = hEndTuple.GetSecond();
            // Validation
            if (hStartA.X > hStartB.X) throw new Exception();
            if (hEndA.Y > hEndB.Y) throw new Exception();
            // Enumerate all possible line segments between lines: hStart and hEnd
            lineSegment = new Tuple(grid);
            for (int i = hStartA.X; i <= hStartB.X; ++i)
            {
                lineSegment.Clear();
                if (i >= hEndA.X && i <= hEndB.X)
                {
                    // Existed that path
                    if (hStartA.Y <= hEndA.Y)
                    {
                        // hStartA is upper line
                        lineSegment.Select(new Position(i, hStartA.Y));
                        lineSegment.Select(new Position(i, hEndA.Y));
                    }
                    else
                    {
                        // hEndA is upper line
                        lineSegment.Select(new Position(i, hEndA.Y));
                        lineSegment.Select(new Position(i, hStartA.Y));
                    }
                    // Check this line segment is a valid path
                    if (_isExistedPath(lineSegment, grid)) return true;
                }
            }

            // Both vertical lines - Base: vStartTuple
            Position vStartA = vStartTuple.GetFirst();
            Position vStartB = vStartTuple.GetSecond();
            Position vEndA = vEndTuple.GetFirst();
            Position vEndB = vEndTuple.GetSecond();
            // Validation
            if (vStartA.X > vStartB.X) throw new Exception();
            if (vEndA.Y > vEndB.Y) throw new Exception();
            // Enumerate all possible line segments between lines: vStart and vEnd
            lineSegment = new Tuple(grid);
            for (int i = vStartA.Y; i <= vStartB.Y; ++i)
            {
                lineSegment.Clear();
                if (i >= vEndA.Y && i <= vEndB.Y)
                {
                    // Existed that path
                    if (vStartA.X <= vEndA.X)
                    {
                        // vStartA is left line
                        lineSegment.Select(new Position(vStartA.X, i));
                        lineSegment.Select(new Position(vEndA.X, i));
                    }
                    else
                    {
                        // vEndA is left line
                        lineSegment.Select(new Position(vEndA.X, i));
                        lineSegment.Select(new Position(vStartA.X, i));
                    }
                    // Check this line segment is a valid path
                    if (_isExistedPath(lineSegment, grid)) return true;
                }
            }

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

            // Expand vertically and horizontally
            Tuple hStartTuple = _expandHorizontal(startPos, grid, start.GetImageId());
            Tuple vStartTuple = _expandVertical(startPos, grid, start.GetImageId());
            Tuple hEndTuple = _expandHorizontal(endPos, grid, end.GetImageId());
            Tuple vEndTuple = _expandVertical(endPos, grid, end.GetImageId());

            bool ret = false;
            // #1 No turing
            ret = _isIntersect(hStartTuple, hEndTuple);
            ret = ret || _isIntersect(hStartTuple, hEndTuple);
            // #2 Turing once
            ret = ret || _isIntersect(hStartTuple, vEndTuple);
            ret = ret || _isIntersect(vStartTuple, hEndTuple);
            // #3 Turing twice
            ret = ret || _isTuringTwice(hStartTuple, vStartTuple, hEndTuple, vEndTuple, grid);
            
            return ret;
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
        /// Get total score
        /// </summary>
        /// <returns></returns>
        public int GetTotalScore()
        {
            return _score.GetTotalScore();
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
            // Check the game state
            if (_gameState != GameState.PLAYING) return;

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
            // Check the game state
            if (_gameState != GameState.PLAYING) return;

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

                    _score.Merged();
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

            _score.Refresh();
            _sePlayer.Refresh(); // Refresh SE
        }

        public void RestartGame()
        {
            // Restart Game
            _grid.Reset();
            _score.Reset();
            _tuple.Clear();

            _gameState = GameState.PLAYING;
        }
    }
}
