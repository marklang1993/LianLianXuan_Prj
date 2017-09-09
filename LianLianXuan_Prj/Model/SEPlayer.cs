using System.Media;

namespace LianLianXuan_Prj.Model
{
    public class SEPlayer
    {
        private SoundPlayer _spClicked;
        private SoundPlayer _spFailed;
        private SoundPlayer _spMerged;
        private SoundPlayer _spCancel;
        private SoundPlayer _spRefresh;

        public SEPlayer()
        {
            _spClicked = new SoundPlayer();
            _spFailed = new SoundPlayer();
            _spMerged = new SoundPlayer();
            _spCancel = new SoundPlayer();
            _spRefresh = new SoundPlayer();

            _spClicked.SoundLocation = @"music\clicked.wav";
            _spFailed.SoundLocation = @"music\failed.wav";
            _spMerged.SoundLocation = @"music\merged.wav";
            _spCancel.SoundLocation = @"music\cancel.wav";
            _spRefresh.SoundLocation = @"music\refresh.wav";
        }

        public void Clicked()
        {
            _spClicked.Play();
        }

        public void Failed()
        {
            _spFailed.Play();
        }

        public void Merged()
        {
            _spMerged.Play();
        }

        public void Cancel()
        {
            _spCancel.Play();
        }

        public void Refresh()
        {
            _spRefresh.Play();
        }
    }
}
