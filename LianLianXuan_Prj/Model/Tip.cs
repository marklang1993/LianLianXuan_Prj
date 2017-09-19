namespace LianLianXuan_Prj.Model
{
    public class Tip
    {
        public Tuple Tuple;
        public bool IsActive;

        public Tip()
        {
            Reset();
        }

        public void Reset()
        {
            Tuple = null;
            IsActive = false;
        }
    }
}
