namespace GraphicsGeometry
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuBasic = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDDA = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMidLine = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCircle = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTrans = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMove = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRotate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuScale = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCohen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMidCut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLiang = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSH = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFill = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProject = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHide = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBasic,
            this.mnuTrans,
            this.mnuCut,
            this.mnuFill,
            this.mnuProject,
            this.mnuHide,
            this.mnuExit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(884, 25);
            this.menuStrip1.TabIndex = 0;
            // 
            // mnuBasic
            // 
            this.mnuBasic.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDDA,
            this.mnuMidLine,
            this.mnuCircle});
            this.mnuBasic.Name = "mnuBasic";
            this.mnuBasic.Size = new System.Drawing.Size(92, 21);
            this.mnuBasic.Text = "基本图形生成";
            // 
            // mnuDDA
            // 
            this.mnuDDA.Name = "mnuDDA";
            this.mnuDDA.Size = new System.Drawing.Size(196, 22);
            this.mnuDDA.Text = "直线-DDA算法";
            this.mnuDDA.Click += new System.EventHandler(this.mnuDDA_Click);
            // 
            // mnuMidLine
            // 
            this.mnuMidLine.Name = "mnuMidLine";
            this.mnuMidLine.Size = new System.Drawing.Size(196, 22);
            this.mnuMidLine.Text = "直线-中点算法";
            this.mnuMidLine.Click += new System.EventHandler(this.mnuMidLine_Click);
            // 
            // mnuCircle
            // 
            this.mnuCircle.Name = "mnuCircle";
            this.mnuCircle.Size = new System.Drawing.Size(196, 22);
            this.mnuCircle.Text = "圆-Bresenham算法";
            this.mnuCircle.Click += new System.EventHandler(this.mnuCircle_Click);
            // 
            // mnuTrans
            // 
            this.mnuTrans.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMove,
            this.mnuRotate,
            this.mnuScale});
            this.mnuTrans.Name = "mnuTrans";
            this.mnuTrans.Size = new System.Drawing.Size(92, 21);
            this.mnuTrans.Text = "二维图像变换";
            // 
            // mnuMove
            // 
            this.mnuMove.Name = "mnuMove";
            this.mnuMove.Size = new System.Drawing.Size(160, 22);
            this.mnuMove.Text = "平移变换";
            this.mnuMove.Click += new System.EventHandler(this.TransMove_Click);
            // 
            // mnuRotate
            // 
            this.mnuRotate.Name = "mnuRotate";
            this.mnuRotate.Size = new System.Drawing.Size(160, 22);
            this.mnuRotate.Text = "旋转变换";
            this.mnuRotate.Click += new System.EventHandler(this.TransRotate_Click);
            // 
            // mnuScale
            // 
            this.mnuScale.Name = "mnuScale";
            this.mnuScale.Size = new System.Drawing.Size(160, 22);
            this.mnuScale.Text = "缩放变换";
            this.mnuScale.Click += new System.EventHandler(this.TransScale_Click);
            // 
            // mnuCut
            // 
            this.mnuCut.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCohen,
            this.mnuMidCut,
            this.mnuLiang,
            this.mnuSH});
            this.mnuCut.Name = "mnuCut";
            this.mnuCut.Size = new System.Drawing.Size(92, 21);
            this.mnuCut.Text = "二维图像裁剪";
            // 
            // mnuCohen
            // 
            this.mnuCohen.Name = "mnuCohen";
            this.mnuCohen.Size = new System.Drawing.Size(232, 22);
            this.mnuCohen.Text = "Cohen-Sutherland直线裁剪";
            this.mnuCohen.Click += new System.EventHandler(this.mnuCohen_Click);
            // 
            // mnuMidCut
            // 
            this.mnuMidCut.Name = "mnuMidCut";
            this.mnuMidCut.Size = new System.Drawing.Size(232, 22);
            this.mnuMidCut.Text = "中点分割裁剪";
            this.mnuMidCut.Click += new System.EventHandler(this.mnuMidCut_Click);
            // 
            // mnuLiang
            // 
            this.mnuLiang.Name = "mnuLiang";
            this.mnuLiang.Size = new System.Drawing.Size(232, 22);
            this.mnuLiang.Text = "梁友栋-Barsky裁剪";
            this.mnuLiang.Click += new System.EventHandler(this.mnuLiang_Click);
            // 
            // mnuSH
            // 
            this.mnuSH.Name = "mnuSH";
            this.mnuSH.Size = new System.Drawing.Size(232, 22);
            this.mnuSH.Text = "Sutherland-Hodgman多边形裁剪";
            this.mnuSH.Click += new System.EventHandler(this.mnuSH_Click);
            // 
            // mnuFill
            // 
            this.mnuFill.Name = "mnuFill";
            this.mnuFill.Size = new System.Drawing.Size(68, 21);
            this.mnuFill.Text = "图形填充";
            this.mnuFill.Click += new System.EventHandler(this.mnuFill_Click);
            // 
            // mnuProject
            // 
            this.mnuProject.Name = "mnuProject";
            this.mnuProject.Size = new System.Drawing.Size(44, 21);
            this.mnuProject.Text = "投影";
            this.mnuProject.Click += new System.EventHandler(this.mnuProject_Click);
            // 
            // mnuHide
            // 
            this.mnuHide.Name = "mnuHide";
            this.mnuHide.Size = new System.Drawing.Size(44, 21);
            this.mnuHide.Text = "消隐";
            this.mnuHide.Click += new System.EventHandler(this.mnuHide_Click);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(44, 21);
            this.mnuExit.Text = "退出";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(884, 22);
            this.statusStrip1.TabIndex = 1;
            // 
            // statusLabel1
            // 
            this.statusLabel1.Name = "statusLabel1";
            this.statusLabel1.Size = new System.Drawing.Size(320, 17);
            this.statusLabel1.Text = "请从菜单选择算法，然后在绘图区用鼠标操作";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "计算机图形学练习平台";
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuBasic;
        private System.Windows.Forms.ToolStripMenuItem mnuDDA;
        private System.Windows.Forms.ToolStripMenuItem mnuMidLine;
        private System.Windows.Forms.ToolStripMenuItem mnuCircle;
        private System.Windows.Forms.ToolStripMenuItem mnuTrans;
        private System.Windows.Forms.ToolStripMenuItem mnuMove;
        private System.Windows.Forms.ToolStripMenuItem mnuRotate;
        private System.Windows.Forms.ToolStripMenuItem mnuScale;
        private System.Windows.Forms.ToolStripMenuItem mnuCut;
        private System.Windows.Forms.ToolStripMenuItem mnuCohen;
        private System.Windows.Forms.ToolStripMenuItem mnuMidCut;
        private System.Windows.Forms.ToolStripMenuItem mnuLiang;
        private System.Windows.Forms.ToolStripMenuItem mnuSH;
        private System.Windows.Forms.ToolStripMenuItem mnuFill;
        private System.Windows.Forms.ToolStripMenuItem mnuProject;
        private System.Windows.Forms.ToolStripMenuItem mnuHide;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel1;
    }
}
