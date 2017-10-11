using System;
using System.Collections.Generic;
using System.IO;


namespace LianLianXuan_Prj.Model
{
    public class ScoreBoard
    {
        /// <summary>
        /// Score struct used in Score Board
        /// </summary>
        public class Score : IComparable
        {
            public bool IsValid; // Is valid score record
            public string Name; // Name of Player
            public int TimeElapsed; // Used time in 1 round
            public int TotalScore; // Total score got in 1 round
            public int MaxCombos; // Maximum combos in 1 round
            public int TipTimes; // Times of using tips

            public Score()
            {
                IsValid = false;
                Name = null;
                TimeElapsed = 0;
                TotalScore = 0;
                MaxCombos = 0;
                TipTimes = 0;
            }

            public int CompareTo(object obj)
            {
                Score other = obj as Score;
                if (other == null) return -1;

                // If score is invalid, it will always be the least
                if (!other.IsValid)
                {
                    return -1;
                }

                int ret;
                // 1. Total Score
                ret = -1 * TotalScore.CompareTo(other.TotalScore);
                if (ret != 0) return ret;

                // 2. MaxCombos
                ret = -1 * MaxCombos.CompareTo(other.MaxCombos);
                if (ret != 0) return ret;

                // 3. TimeElapsed
                ret = TimeElapsed.CompareTo(other.TimeElapsed);
                if (ret != 0) return ret;

                // 4. TipTimes
                ret = TipTimes.CompareTo(other.TipTimes);
                if (ret != 0) return ret;

                return ret;
            }
        }


        /// <summary>
        /// === FILE FORMAT ===
        /// IsValid                 - int
        /// === If invalid, there will be no follwing items ===
        /// Length of Name String   - int
        /// Name                    - char[]
        /// TimeElapsed             - int
        /// TotalScore              - int
        /// MaxCombos               - int
        /// TipTimes                - int
        /// </summary>

        private readonly string _fileName; // Scoreboard related saving file 
        private List<Score> _scoreList; // Sorted

        private const int MAX_RECORDS = 10;


        public ScoreBoard()
        {
            // File processing
            _scoreList = new List<Score>();

            // Loading
            try
            {
                _readFile();
            }
            catch (Exception ex)
            {
                // Cannot found score board file
                FileStream file = new FileStream("score.lst", FileMode.Create);
                file.Close();

                for (int i = 0; i < MAX_RECORDS; ++i)
                {
                    _scoreList.Add(new Score());
                }
            }

            // Sort
            _scoreList.Sort();
        }

        /// <summary>
        /// Read ScoreBoard Saving file
        /// </summary>
        private void _readFile()
        {
            FileStream file = new FileStream("score.lst", FileMode.Open);

            // Score board file found -- Read
            BinaryReader binaryReader = new BinaryReader(file);
            for (int i = 0; i < MAX_RECORDS; ++i)
            {
                Score score = new Score();
                if (binaryReader.ReadInt32() != 0)
                {
                    // Valid Score
                    score.IsValid = true;

                    // Read Name
                    int lengthOfName;
                    lengthOfName = binaryReader.ReadInt32();
                    char[] nameString = new char[lengthOfName];
                    for (int j = 0; j < lengthOfName; ++j)
                    {
                        nameString[j] = binaryReader.ReadChar();
                    }
                    score.Name = new string(nameString);

                    // Read others
                    score.TimeElapsed = binaryReader.ReadInt32();
                    score.TotalScore = binaryReader.ReadInt32();
                    score.MaxCombos = binaryReader.ReadInt32();
                    score.TipTimes = binaryReader.ReadInt32();
                }

                _scoreList.Add(score);
            }

            // Close file
            file.Close();
        }


        /// <summary>
        /// Write ScoreBoard Saving file
        /// </summary>
        private void _writeFile()
        {
            FileStream file = new FileStream("score.lst", FileMode.Create);

            // Score board file found -- Write
            BinaryWriter binaryWriter = new BinaryWriter(file);
            for (int i = 0; i < MAX_RECORDS; ++i)
            {
                // 1. IsValid
                if (_scoreList[i].IsValid)
                {
                    binaryWriter.Write((int) 1);
                }
                else
                {
                    binaryWriter.Write((int) 0);
                    continue;
                }
                // 2. Name
                binaryWriter.Write(_scoreList[i].Name.Length);
                binaryWriter.Write(_scoreList[i].Name.ToCharArray());
                
                // 3. Others
                binaryWriter.Write(_scoreList[i].TimeElapsed);
                binaryWriter.Write(_scoreList[i].TotalScore);
                binaryWriter.Write(_scoreList[i].MaxCombos);
                binaryWriter.Write(_scoreList[i].TipTimes);
            }

            // Close file
            file.Close();
        }


        /// <summary>
        /// Update Score Board
        /// </summary>
        public void Update(LianLianXuan_Prj.Model.Score rawScore, int timeElapsed, string name)
        {
            // Create new score instance
            Score score = new Score();
            score.IsValid = true;
            score.Name = name;
            score.TotalScore = rawScore.GetTotalScore();
            score.MaxCombos = rawScore.GetMaxComboCount();
            score.TimeElapsed = timeElapsed;
            score.TipTimes = rawScore.GetTipTimes();

            // Add, Sort and Remove
            _scoreList.Add(score);
            _scoreList.Sort();
            _scoreList.RemoveAt(_scoreList.Count - 1);

            // Update saving file
            try
            {
                _writeFile();
            }
            catch (Exception ex)
            {
                ;
            }
        }


        /// <summary>
        /// Get Score List
        /// </summary>
        /// <returns></returns>
        public List<Score> GetList()
        {
            return _scoreList;
        }
    }
}
