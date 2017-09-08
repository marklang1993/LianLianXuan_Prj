using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianLianXuan_Prj.Model
{
    public struct Position
    {
        public int X;
        public int Y;

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Determine 2 positions are same
        /// </summary>
        /// <param name="pos">Other position</param>
        /// <returns></returns>
        public bool IsEqual(Position pos)
        {
            return pos.X == X && pos.Y == Y;
        }
    }


    public class Block
    {
        // Some constants
        public const int NULL_TYPE = -1;


        private Position _pos;
        private int _type;

        public Block(int x, int y, int type)
        {
            // Check
            if (type >= Model.IMAGES_CNT) throw new InvalidEnumArgumentException(@"Out of Type Index!");
            if (x < 0 && x >= Model.GRID_BLOCK_CNT_X) throw new InvalidEnumArgumentException(@"Out of X Coordinate!");
            if (y < 0 && y >= Model.GRID_BLOCK_CNT_Y) throw new InvalidEnumArgumentException(@"Out of Y Coordinate!");
            // Init.
            _pos = new Position(x, y);
            _type = type;
        }

        /// <summary>
        /// Get image ID
        /// </summary>
        /// <returns></returns>
        public int GetImageId()
        {
            return _type;
        }

        /// <summary>
        /// Change block to null block (after merging)
        /// </summary>
        /// <returns></returns>
        public void ToNullBlock()
        {
            if (IsNull()) throw new MethodAccessException(@"Alreadly a Null Block!");
            _type = NULL_TYPE;
        }

        /// <summary>
        /// Change block to valid block
        /// </summary>
        /// <param name="imageId">Image ID</param>
        public void ToValidBlock(int imageId)
        {
            if (!IsNull()) throw new MethodAccessException(@"Not a Null Block!");
            if (imageId < 0 && imageId >= Model.IMAGES_CNT) throw new InvalidEnumArgumentException(@"Out of Type Index!");
            _type = imageId;
        }

        /// <summary>
        /// Change current block position
        /// </summary>
        /// <param name="x">X Coordinate</param>
        /// <param name="y">Y Coordinate</param>
        public void ChangePos(int x, int y)
        {
            if (x < 0 && x >= Model.GRID_BLOCK_CNT_X) throw new InvalidEnumArgumentException(@"Out of X Coordinate!");
            if (y < 0 && y >= Model.GRID_BLOCK_CNT_Y) throw new InvalidEnumArgumentException(@"Out of Y Coordinate!");

            _pos = new Position(x, y);
        }

        /// <summary>
        /// Determine this block is a null block
        /// </summary>
        /// <returns></returns>
        public bool IsNull()
        {
            return _type == NULL_TYPE;
        }
    }
}
