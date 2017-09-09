using System.Runtime.InteropServices;

namespace LianLianXuan_Prj.Model
{
    public class BGMPlayer
    {
        public const uint SND_ASYNC = 0x0001;
        public const uint SND_FILENAME = 0x00020000;

        private bool _isPlaying;

        [DllImport("winmm.dll")]
        public static extern uint mciSendString(string lpstrCommand,
            string lpstrReturnString, uint uReturnLength, uint hWndCallback);

        public BGMPlayer()
        {
            _isPlaying = false;
        }

        public void Play()
        {
            mciSendString(@"close mp3_dev", null, 0, 0);
            mciSendString(@"open ""music\bgm.mp3"" alias mp3_dev", null, 0, 0);
            mciSendString("play mp3_dev repeat", null, 0, 0);

            _isPlaying = true;
        }

        public void Stop()
        {
            mciSendString(@"stop mp3_dev", null, 0, 0);
            _isPlaying = false;
        }

        public void Flip()
        {
            if (_isPlaying)
            {
                // Playing
                Stop();
            }
            else
            {
                // Stop
                Play();
            }
        }
    }
}
