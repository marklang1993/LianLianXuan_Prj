using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LianLianXuan_Prj.Model
{
    public class Position
    {
        public int X;
        public int Y;

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool IsValid(int x, int y)
        {
            if (x < 0 || x >= Model.TOT_BLOCK_CNT_X) return false;
            if (y < 0 || y >= Model.TOT_BLOCK_CNT_Y) return false;
            return true;
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

        /// <summary>
        /// Return new position by move up 1 block
        /// </summary>
        /// <returns></returns>
        public Position Up()
        {
            return new Position(X, Y - 1);
        }

        /// <summary>
        /// Return new position by move down 1 block
        /// </summary>
        /// <returns></returns>
        public Position Down()
        {
            return new Position(X, Y + 1);
        }

        /// <summary>
        /// Return new position by move left 1 block
        /// </summary>
        /// <returns></returns>
        public Position Left()
        {
            return new Position(X - 1, Y);
        }

        /// <summary>
        /// Return new position by move right 1 block
        /// </summary>
        /// <returns></returns>
        public Position Right()
        {
            return new Position(X + 1, Y);
        }
    }
}
