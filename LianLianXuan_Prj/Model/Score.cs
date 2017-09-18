namespace LianLianXuan_Prj.Model
{
    public class Score
    {
        private int _currentScore; // Current Score
        private int _refreshedTimes; // Times of Manually Refreshing

        private const int ADD_PER_MERGED = 10;
        private const int DEDUCT_PER_REFRESH = 40;

        /// <summary>
        /// Constructor
        /// </summary>
        public Score()
        {
            Reset();
        }

        /// <summary>
        /// Add scores by merging 2 blocks
        /// </summary>
        public void Merged()
        {
            _currentScore += ADD_PER_MERGED;
        }

        /// <summary>
        /// Deduct scores by manually refreshing the grid
        /// </summary>
        public void Refresh()
        {
            ++_refreshedTimes;
        }

        public void Reset()
        {
            _currentScore = 0;
            _refreshedTimes = 0;
        }

        public int GetTotalScore()
        {
            return _currentScore - _refreshedTimes*DEDUCT_PER_REFRESH;
        }
    }
}
