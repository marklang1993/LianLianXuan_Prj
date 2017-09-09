using System;

namespace LianLianXuan_Prj.Model
{
    public class Tuple
    {
        private Position firstBlock;
        private Position secondBlock;

        private Grid _grid;

        public Tuple(Grid grid)
        {
            _grid = grid;
        }

        /// <summary>
        /// Select a block
        /// </summary>
        /// <param name="pos">Select block position</param>
        /// <returns></returns>
        public bool Select(Position pos)
        {
            // Check selected block is valid
            if (_grid.GetBlock(pos).IsNull()) return false;

            // Set block
            if (firstBlock == null && secondBlock == null)
            {
                // No blocks are selected
                firstBlock = new Position(pos.X, pos.Y);
                return true;
            }

            if (secondBlock == null)
            {
                // First block is selected
                secondBlock = new Position(pos.X, pos.Y);
                return true;
            }

            // 2 blocks are selected
            return false;
        }

        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            firstBlock = null;
            secondBlock = null;
        }

        /// <summary>
        /// Is tuple constructed
        /// </summary>
        /// <returns></returns>
        public bool IsTuple()
        {
            return firstBlock != null && secondBlock != null;
        }

        /// <summary>
        /// Get first position
        /// </summary>
        /// <returns></returns>
        public Position GetFirst()
        {
            return firstBlock;
        }

        /// <summary>
        /// Get second position
        /// </summary>
        /// <returns></returns>
        public Position GetSecond()
        {
            return secondBlock;
        }
    }
}
