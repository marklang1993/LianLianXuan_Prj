namespace LianLianXuan_Prj.Model
{
    public class Score
    {
        private int _currentScore; // Current Score
        private int _refreshedTimes; // Times of Manually Refreshing
        private int _comboCount; // Count of Combo

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
            ++_comboCount;
        }

        /// <summary>
        /// Combo is interrupted
        /// </summary>
        public void ComboInterrupted()
        {
            _comboCount = 0;
        }

        /// <summary>
        /// Deduct scores by manually refreshing the grid
        /// </summary>
        public void Refresh()
        {
            ++_refreshedTimes;
        }

        /// <summary>
        /// Reset all score records
        /// </summary>
        public void Reset()
        {
            _currentScore = 0;
            _refreshedTimes = 0;
            _comboCount = 0;
        }

        /// <summary>
        /// Get Total Score
        /// </summary>
        /// <returns></returns>
        public int GetTotalScore()
        {
            return _currentScore - _refreshedTimes*DEDUCT_PER_REFRESH;
        }

        /// <summary>
        /// Get Combo Count
        /// </summary>
        /// <returns></returns>
        public int GetComboCount()
        {
            return _comboCount;
        }
    }
}
