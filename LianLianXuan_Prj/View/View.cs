using System.Drawing;

namespace LianLianXuan_Prj.View
{
    public abstract class View
    {
        protected Model.Model _model; // model

        public View(Model.Model model)
        {
            _model = model;
        }

        /// <summary>
        /// Draw all contexts of this graphics
        /// <param name="g">Graphics of widget</param>
        /// </summary>
        public abstract void Paint(Graphics g);
    }
}
