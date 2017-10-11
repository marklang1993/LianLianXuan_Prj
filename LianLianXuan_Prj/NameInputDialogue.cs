using System;
using System.Text;
using System.Windows.Forms;
using LianLianXuan_Prj.View;

namespace LianLianXuan_Prj
{
    public partial class NameInputDialogue : Form
    {
        private StringBuilder _nameString;

        public NameInputDialogue(StringBuilder nameString)
        {
            _nameString = nameString;

            InitializeComponent();
        }

        private void ConfirmBtn_Click(object sender, EventArgs e)
        {
            // Validate
            string rawInput = InputTxtBox.Text;
            if (System.Text.Encoding.Default.GetBytes(rawInput).Length >=
                ScoreBoardView.NAME_MAX_LENGTH)
            {
                MessageBox.Show(
                    this,
                    @"名字太长，请控制在10个英文字母或者5个汉字以内。",
                    @"名字太长",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            else if (rawInput.Length == 0)
            {
                MessageBox.Show(
                    this,
                    @"请输入名字。",
                    @"请输入名字",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            _nameString.Clear();
            _nameString.Append(rawInput);
            
            this.Close();
        }

    }
}
