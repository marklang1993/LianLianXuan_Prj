namespace LianLianXuan_Prj
{
    partial class NameInputDialogue
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ConfirmBtn = new System.Windows.Forms.Button();
            this.TipLbl = new System.Windows.Forms.Label();
            this.InputTxtBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ConfirmBtn
            // 
            this.ConfirmBtn.Location = new System.Drawing.Point(82, 143);
            this.ConfirmBtn.Name = "ConfirmBtn";
            this.ConfirmBtn.Size = new System.Drawing.Size(116, 30);
            this.ConfirmBtn.TabIndex = 0;
            this.ConfirmBtn.Text = "确定";
            this.ConfirmBtn.UseVisualStyleBackColor = true;
            this.ConfirmBtn.Click += new System.EventHandler(this.ConfirmBtn_Click);
            // 
            // TipLbl
            // 
            this.TipLbl.AutoSize = true;
            this.TipLbl.Font = new System.Drawing.Font("SimSun", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TipLbl.Location = new System.Drawing.Point(34, 29);
            this.TipLbl.Name = "TipLbl";
            this.TipLbl.Size = new System.Drawing.Size(164, 21);
            this.TipLbl.TabIndex = 1;
            this.TipLbl.Text = "输入你的名字：";
            // 
            // InputTxtBox
            // 
            this.InputTxtBox.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.InputTxtBox.Location = new System.Drawing.Point(56, 82);
            this.InputTxtBox.MaxLength = 15;
            this.InputTxtBox.Name = "InputTxtBox";
            this.InputTxtBox.Size = new System.Drawing.Size(157, 26);
            this.InputTxtBox.TabIndex = 2;
            this.InputTxtBox.Text = "NO NAME";
            // 
            // NameInputDialogue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 197);
            this.Controls.Add(this.InputTxtBox);
            this.Controls.Add(this.TipLbl);
            this.Controls.Add(this.ConfirmBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NameInputDialogue";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NameInputDialogue";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConfirmBtn;
        private System.Windows.Forms.Label TipLbl;
        private System.Windows.Forms.TextBox InputTxtBox;
    }
}