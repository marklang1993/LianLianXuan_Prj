namespace LianLianXuan_Prj
{
    partial class GameStage
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
            this.components = new System.ComponentModel.Container();
            this.ViewUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // ViewUpdateTimer
            // 
            this.ViewUpdateTimer.Tick += new System.EventHandler(this.ViewUpdateTimer_Tick);
            // 
            // GameStage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "GameStage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "连连炫Project";
            this.Load += new System.EventHandler(this.GameStage_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GameStage_KeyUp);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GameStage_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GameStage_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer ViewUpdateTimer;
    }
}

