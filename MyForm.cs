using System;
using System.Windows.Forms;

namespace GraphicsGeometry
{
    /// <summary>
    /// 缩放系数输入对话框：通过两个数字框接受 X、Y 方向的缩放系数。
    /// </summary>
    public partial class MyForm : Form
    {
        private float xscale = 1;
        private float yscale = 1;

        public float Xscale
        {
            get { return this.xscale; }
        }

        public float Yscale
        {
            get { return this.yscale; }
        }

        public MyForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            xscale = (float)numericUpDown1.Value;
            yscale = (float)numericUpDown2.Value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }
    }
}
