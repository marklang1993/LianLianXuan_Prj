using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // Members
        private Grid _grid;

        /// <summary>
        /// Constructor
        /// </summary>
        public Model()
        {
            _grid = new Grid();

            Test();
        }

        private void ExpandBFS(Queue<Position> searchQueue, Position curPos, Grid _grid)
        {
            Block temp;
            // Up
            temp = _grid.GetBlock(curPos.Up());
            if (temp != null) // If such block does not exist due to the invalid coordinates
            {
                if (temp.IsNull())
                {
                    // Empty Block, able to pass
                    searchQueue.Enqueue(temp.GetPos());
                }
            }
            // Down
            temp = _grid.GetBlock(curPos.Down());
            if (temp != null) // If such block does not exist due to the invalid coordinates
            {
                if (temp.IsNull())
                {
                    // Empty Block, able to pass
                    searchQueue.Enqueue(temp.GetPos());
                }
            }
            // Left
            temp = _grid.GetBlock(curPos.Left());
            if (temp != null) // If such block does not exist due to the invalid coordinates
            {
                if (temp.IsNull())
                {
                    // Empty Block, able to pass
                    searchQueue.Enqueue(temp.GetPos());
                }
            }
            // Right
            temp = _grid.GetBlock(curPos.Right());
            if (temp != null) // If such block does not exist due to the invalid coordinates
            {
                if (temp.IsNull())
                {
                    // Empty Block, able to pass
                    searchQueue.Enqueue(temp.GetPos());
                }
            }
        }

        /// <summary>
        /// Deterimine two blocks can be connected
        /// </summary>
        /// <param name="startPos">Start block position</param>
        /// <param name="endPos">End block position</param>
        /// <param name="_grid">Grid</param>
        /// <returns></returns>
        private bool _isConnected(Position startPos, Position endPos, Grid _grid)
        {
            // Get blocks tuple
            Block start = _grid.GetBlock(startPos);
            Block end = _grid.GetBlock(endPos);

            // Check prerequisites: valid positions, valid blocks, same type and blocks in different positions
            if (start == null || end == null) return false;
            if (start.IsNull() || end.IsNull()) return false;
            if (!Block.TypeEqual(start, end)) return false;
            if (start.GetPos().IsEqual(end.GetPos())) return false;

            // Find path by BFS
            Queue<Position> searchQueue = new Queue<Position>();
            searchQueue.Enqueue(start.GetPos());
            while (true)
            {
                // failed
                if (searchQueue.Count == 0) return false;
                // found
                Position curPos = searchQueue.Dequeue();
                if (curPos.IsEqual(end.GetPos())) return true;
                // expand
                ExpandBFS(searchQueue, curPos, _grid);
            }
        }

        /// <summary>
        /// Get Map
        /// </summary>
        /// <returns></returns>
        public Block[][] GetMap()
        {
            return _grid.GetMap();
        }

        public void Test()
        {
            Console.WriteLine(_isConnected(new Position(1, 1), new Position(10, 1), _grid));
            Console.WriteLine(_isConnected(new Position(1, 1), new Position(9, 1), _grid));
            Console.WriteLine(_isConnected(new Position(8, 2), new Position(10, 2), _grid));
        }
    }
}
