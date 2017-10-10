using System;

namespace LianLianXuan_Prj.Model
{
    public class Score
    {
        private DateTime _startTime; // Starting Time
        private int _currentScore; // Current Score
        private int _refreshedTimes; // Times of Manually Refreshing
        private int _tipTimes; // Times of Requesting a Tip
        private int _comboCount; // Count of Combo
        private int _comboCountMax; // Maximum count of Combo

        private const int ADD_PER_MERGED = 10;
        private const int DEDUCT_PER_REFRESH = 100;
        private const int DEDUCT_PER_TIP = 20;

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
            _currentScore += ADD_PER_MERGED * (_comboCount + 1);
            ++_comboCount;
            // Set Max Combo Count
            if (_comboCountMax < _comboCount)
            {
                _comboCountMax = _comboCount;
            }
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
        /// Deduct scores by giving a tip
        /// </summary>
        public void Tip()
        {
            ++_tipTimes;
        }

        /// <summary>
        /// Reset all score records
        /// </summary>
        public void Reset()
        {
            _startTime = DateTime.Now;
            _currentScore = 0;
            _refreshedTimes = 0;
            _tipTimes = 0;
            _comboCount = 0;
            _comboCountMax = 0;
        }

        /// <summary>
        /// Get Total Score
        /// </summary>
        /// <returns></returns>
        public int GetTotalScore()
        {
            return _currentScore 
                - _refreshedTimes*DEDUCT_PER_REFRESH
                - _tipTimes*DEDUCT_PER_TIP;
        }

        /// <summary>
        /// Get Total Ticks of starting time
        /// </summary>
        /// <returns></returns>
        public long GetTotalTicks()
        {
            return _startTime.Ticks;
        }

        /// <summary>
        /// Get Combo Count
        /// </summary>
        /// <returns></returns>
        public int GetComboCount()
        {
            return _comboCount;
        }

        /// <summary>
        /// Get Maximum Combo Count
        /// </summary>
        /// <returns></returns>
        public int GetMaxComboCount()
        {
            return _comboCountMax;
        }

        /// <summary>
        /// Get the times of asking tips
        /// </summary>
        /// <returns></returns>
        public int GetTipTimes()
        {
            return _tipTimes;
        }
    }
}
