using System;
using System.Collections.Generic;
using System.Linq;

namespace LianLianXuan_Prj.Model
{
    public class Grid
    {
        private Block[][] _map; // 2D Map of blocks

        public Grid()
        {
            Reset();
        }

        /// <summary>
        /// Return serialized blocks in play area (10 * 8)
        /// </summary>
        /// <returns></returns>
        private Block[] _serialize()
        {
            Block[] serializedBlocks = new Block[Model.GRID_BLOCK_CNT_X * Model.GRID_BLOCK_CNT_Y];

            int cnt = 0;
            for (int j = 1; j < Model.TOT_BLOCK_CNT_Y - 1; ++j)
            {
                for (int i = 1; i < Model.TOT_BLOCK_CNT_X - 1; ++i)
                {
                    serializedBlocks[cnt] = _map[i][j];
                    ++cnt;
                }
            }
            return serializedBlocks;
        }

        /// <summary>
        /// Deserialize block array
        /// </summary>
        /// <param name="serializedBlocks">Serialized Blocks</param>
        private void _deserialize(Block[] serializedBlocks)
        {
            // Check
            if (serializedBlocks.Length != Model.GRID_BLOCK_CNT_X * Model.GRID_BLOCK_CNT_Y)
                throw new Exception(@"SerializedBlocks Has Invalid Length.");

            int cnt = 0;
            for (int j = 1; j < Model.TOT_BLOCK_CNT_Y - 1; ++j)
            {
                for (int i = 1; i < Model.TOT_BLOCK_CNT_X - 1; ++i)
                {
                    _map[i][j] = serializedBlocks[cnt];
                    ++cnt;
                }
            }
        }

        /// <summary>
        /// Reset Grid
        /// </summary>
        public void Reset()
        {
            // Init. map
            _map = new Block[Model.TOT_BLOCK_CNT_X][];
            for (int i = 0; i < Model.TOT_BLOCK_CNT_X; ++i)
            {
                _map[i] = new Block[Model.TOT_BLOCK_CNT_Y];
            }
            // Init. all blocks to null blocks
            for (int j = 0; j < Model.TOT_BLOCK_CNT_Y; ++j)
            {
                for (int i = 0; i < Model.TOT_BLOCK_CNT_X; ++i)
                {
                    _map[i][j] = new Block(i, j, Block.NULL_TYPE);
                }
            }

            // Init. all blocks in play area to valid blocks
            Block[] serializedBlocks = _serialize();
            for (int i = 0; i < Model.GRID_BLOCK_CNT_X * Model.GRID_BLOCK_CNT_Y; ++i)
            {
                serializedBlocks[i].ToValidBlock(i / 2);
            }
            _deserialize(serializedBlocks);
            // Randomize
            Randomize();
        }

        /// <summary>
        /// Randomize all blocks in play area
        /// </summary>
        public void Randomize()
        {
            // Serialize blocks in current play area
            List<Block> serializedBlocks = new List<Block>();
            serializedBlocks.AddRange(_serialize());
            // Randomize
            Block[] randomizedBlocks = new Block[Model.GRID_BLOCK_CNT_X * Model.GRID_BLOCK_CNT_Y];
            //Random prng = new Random(10);
            Random prng = new Random();
            int cnt = 0;
            while (serializedBlocks.Count != 0)
            {
                int nextIndex = prng.Next(serializedBlocks.Count);
                randomizedBlocks[cnt] = serializedBlocks.ElementAt(nextIndex);
                serializedBlocks.RemoveAt(nextIndex);
                ++cnt;
            }
            // Reset Position
            cnt = 0;
            for (int j = 1; j < Model.TOT_BLOCK_CNT_Y - 1; ++j)
            {
                for (int i = 1; i < Model.TOT_BLOCK_CNT_X - 1; ++i)
                {
                    randomizedBlocks[cnt].ChangePos(i,j);
                    ++cnt;
                }
            }
            // Deserialize
            _deserialize(randomizedBlocks);
        }

        /// <summary>
        /// Check all blocks in play areas are invalid
        /// </summary>
        /// <returns></returns>
        public bool IsAllInvalid()
        {
            bool ret = true;

            for (int j = 1; j < Model.TOT_BLOCK_CNT_Y - 1; ++j)
            {
                for (int i = 1; i < Model.TOT_BLOCK_CNT_X - 1; ++i)
                {
                    ret = ret && _map[i][j].IsNull();
                }
            }

            return ret;
        }

        /// <summary>
        /// Get Map
        /// </summary>
        /// <returns></returns>
        public Block[][] GetMap()
        {
            return _map;
        }

        /// <summary>
        /// Get Block by Position
        /// </summary>
        /// <param name="pos">Position of Block</param>
        /// <returns>NOTE: return null if coordinates are invalid.</returns>
        public Block GetBlock(Position pos)
        {
            if (!Position.IsValid(pos.X, pos.Y)) return null;
            return _map[pos.X][pos.Y];
        }
    }
}
