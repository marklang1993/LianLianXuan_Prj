using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianLianXuan_Prj.Model
{
    public class Grid
    {
        private Block[][] _map; // 2D Map of blocks

        public Grid()
        {
            // Init. map
            _map = new Block[Model.TOT_BLOCK_CNT_X][];
            for (int i = 0; i < Model.TOT_BLOCK_CNT_X; ++i)
            {
                _map[i] = new Block[Model.TOT_BLOCK_CNT_Y];
            }
            // Init. all blocks to null blocks
            for (int j = 1; j < Model.TOT_BLOCK_CNT_Y - 1; ++j)
            {
                for (int i = 1; i < Model.TOT_BLOCK_CNT_X - 1; ++i)
                {
                    _map[i][j] = new Block(i, j, Block.NULL_TYPE);
                }
            }

            // Init. all blocks in play area to valid blocks
            Block[] serializedBlocks = _serialize();
            for (int i = 0; i < Model.GRID_BLOCK_CNT_X*Model.GRID_BLOCK_CNT_Y; ++i)
            {
                serializedBlocks[i].ToValidBlock(i / 2);
            }
            _deserialize(serializedBlocks);
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
        /// Get Map
        /// </summary>
        /// <returns></returns>
        public Block[][] GetMap()
        {
            return _map;
        }
    
    }
}
