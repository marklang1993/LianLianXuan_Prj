namespace LianLianXuan_Prj.Controller
{
    public class Controller
    {
        private Model.Model _model;

        public Controller(Model.Model model)
        {
            _model = model;
        }

        public void MouseClick(int x, int y, bool isRightClick)
        {
            if (isRightClick)
            {
                _model.RightClickHandler(x, y);
            }
            else
            {
                _model.LeftClickHandler(x, y);
            }
            
        }
    }
}
