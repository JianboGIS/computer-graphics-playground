using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GraphicsGeometry
{
    /// <summary>
    /// 主窗体：计算机图形学练习平台。
    /// 顶部菜单切换不同算法，鼠标左键取点绘图，移动鼠标时用「橡皮筋」方式实时预览，
    /// 右键结束多边形的顶点输入。
    /// </summary>
    public partial class Form1 : Form
    {
        // ============ 1.2 平台搭建与全局变量定义 ============
        Color BackColor1 = Color.White;
        Color ForeColor1 = Color.Black;
        public int MenuID, PressNum, FirstX, FirstY, OldX, OldY;//菜单ID，按下的次数，第一个点的坐标，第二个点的坐标
        public int R, XL, XR, YU, YD; //裁剪窗口的上下左右边界
        Point[] group = new Point[100];//存储点
        Point[] pointsgroup = new Point[4];//创建一个有4个点的点数组用来存储裁剪窗口的坐标

        // 各算法对象
        private drawLine drawLine;
        private drawCircle drawCircle;
        private FillWay fillWay;
        private Cut cut;
        private Project project;
        private HiddenSurface hide;

        public Form1()
        {
            InitializeComponent();

            // 各算法对象在窗体构造时统一初始化，绘图表面即本窗体
            drawLine = new drawLine(this);
            drawCircle = new drawCircle(this);
            fillWay = new FillWay(this);
            cut = new Cut(this);
            project = new Project(this);
            hide = new HiddenSurface(this);

            InitClipWindow();//初始化裁剪窗口
        }

        /// <summary>
        /// 初始化裁剪窗口 pointsgroup：矩形 (100,100) (200,100) (200,200) (100,200)。
        /// 四个顶点按「左下 → 右下 → 右上 → 左上」存放，
        /// 因此 pointsgroup[0] 给出左、下边界，pointsgroup[2] 给出右、上边界。
        /// </summary>
        private void InitClipWindow()
        {
            pointsgroup[0] = new Point(100, 100);
            pointsgroup[1] = new Point(200, 100);
            pointsgroup[2] = new Point(200, 200);
            pointsgroup[3] = new Point(100, 200);
            XL = pointsgroup[0].X;
            XR = pointsgroup[2].X;
            YD = pointsgroup[0].Y;
            YU = pointsgroup[2].Y;
        }

        /// <summary>
        /// 切换算法模式：记录菜单 ID、清空按点次数，并在状态栏给出操作提示。
        /// </summary>
        private void StartMode(int menuId, string hint)
        {
            MenuID = menuId;
            PressNum = 0;
            OldX = 0;
            OldY = 0;
            if (menuId == 3 || menuId == 24)
            {
                // 多边形（填充 / 多边形裁剪）重新开始取点
                for (int i = 0; i < group.Length; i++)
                {
                    group[i] = new Point(0, 0);
                }
            }
            statusLabel1.Text = hint;
        }

        /// <summary>
        /// 把裁剪窗口画出来，便于观察裁剪结果。
        /// </summary>
        private void DrawClipWindow()
        {
            Graphics g = CreateGraphics();
            g.DrawRectangle(new Pen(Color.Gray, 1), XL, YD, XR - XL, YU - YD);
        }

        // ============ 1.3 核心交互：橡皮筋效果 (Form1_MouseMove) ============
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            Pen Backpen = new Pen(BackColor1, 1);
            Pen Mypen = new Pen(ForeColor1, 1);

            // 针对直线 (MenuID 1, 2) 的橡皮筋效果
            if ((MenuID == 1 || MenuID == 2) && PressNum == 1)
            {
                if (!(e.X == OldX && e.Y == OldY))
                {
                    g.DrawLine(Backpen, FirstX, FirstY, OldX, OldY); // 擦除旧线段
                    g.DrawLine(Mypen, FirstX, FirstY, e.X, e.Y);      // 绘制新线段
                    OldX = e.X; OldY = e.Y;
                }
            }

            // 针对圆 (MenuID 5) 的橡皮筋效果
            if (MenuID == 5 && PressNum == 1)
            {
                if (!(e.X == OldX && e.Y == OldY))
                {
                    // 擦除旧圆
                    double r = Math.Sqrt((FirstX - OldX) * (FirstX - OldX) + (FirstY - OldY) * (FirstY - OldY));
                    int r1 = (int)(r + 0.5);
                    g.DrawEllipse(Backpen, FirstX - r1, FirstY - r1, r1 * 2, r1 * 2);
                    // 绘制新圆
                    r = Math.Sqrt((FirstX - e.X) * (FirstX - e.X) + (FirstY - e.Y) * (FirstY - e.Y));
                    r1 = (int)(r + 0.5);
                    g.DrawEllipse(Mypen, FirstX - r1, FirstY - r1, r1 * 2, r1 * 2);
                    OldX = e.X; OldY = e.Y;
                }
            }
        }

        // ============ 1.4 鼠标点击处理 (Form1_MouseClick) ============
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            // 算法对象已在 Form1 构造函数中统一初始化
            Graphics g = CreateGraphics();

            if (MenuID == 1 || MenuID == 2) // 画直线 (DDA 或 中点)
            {
                if (PressNum == 0) { FirstX = e.X; FirstY = e.Y; OldX = e.X; OldY = e.Y; } // 第一点
                else
                {
                    if (MenuID == 1) drawLine.DDALine(g, FirstX, FirstY, e.X, e.Y);
                    if (MenuID == 2) drawLine.MidLine(g, FirstX, FirstY, e.X, e.Y);
                }
                PressNum++;
                if (PressNum >= 2) PressNum = 0; // 绘制完成，重置
                return;
            }

            if (MenuID == 5) // 画圆 (Bresenham)
            {
                if (PressNum == 0) { FirstX = e.X; FirstY = e.Y; } // 圆心
                else
                {
                    if (FirstX == e.X && FirstY == e.Y) return;
                    drawCircle.Bresenham_Circle(g, FirstX, FirstY, e.X, e.Y);
                }
                PressNum++;
                if (PressNum >= 2) PressNum = 0;
                return;
            }

            if (MenuID == 3) // 扫描线填充：左键依次取多边形顶点，右键结束并填充
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (PressNum > 0) g.DrawLine(Pens.Black, group[PressNum - 1], e.Location);
                    if (PressNum >= group.Length) PressNum = 0;
                    group[PressNum] = e.Location;
                    PressNum++;
                }
                else if (e.Button == MouseButtons.Right && PressNum >= 3)
                {
                    fillWay.ScanLineFill(g, group, PressNum, FirstX, FirstY);
                    PressNum = 0;
                }
                return;
            }

            if (MenuID == 21 || MenuID == 22 || MenuID == 23) // 直线裁剪：两点确定被裁剪线段
            {
                if (PressNum == 0) { FirstX = e.X; FirstY = e.Y; OldX = e.X; OldY = e.Y; }
                else
                {
                    if (MenuID == 21) cut.CohenCut1(FirstX, FirstY, e.X, e.Y, pointsgroup);
                    if (MenuID == 22) cut.MidCut1(FirstX, FirstY, e.X, e.Y, pointsgroup);
                    if (MenuID == 23) cut.LiangCut1(FirstX, FirstY, e.X, e.Y, pointsgroup);
                }
                PressNum++;
                if (PressNum >= 2) PressNum = 0;
                return;
            }

            if (MenuID == 24) // 多边形裁剪：左键依次取多边形顶点，右键结束并裁剪
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (PressNum > 0) g.DrawLine(Pens.Red, group[PressNum - 1], e.Location);
                    if (PressNum >= group.Length) PressNum = 0;
                    group[PressNum] = e.Location;
                    PressNum++;
                }
                else if (e.Button == MouseButtons.Right && PressNum >= 3)
                {
                    cut.WindowCut1(g, group, PressNum, FirstX, FirstY, pointsgroup);
                    PressNum = 0;
                }
                return;
            }

            if (MenuID == 11 || MenuID == 12) // 平移 / 旋转：两点确定变换量
            {
                if (MenuID == 11)
                {
                    if (PressNum == 0)  //保留第一点
                    {
                        FirstX = e.X;
                        FirstY = e.Y;
                    }
                    else    //第二点，确定平移量，改变图形参数
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            pointsgroup[i].X += e.X - FirstX;
                            pointsgroup[i].Y += e.Y - FirstY;
                        }
                        g.DrawPolygon(Pens.Blue, pointsgroup);
                    }
                }
                else
                {
                    if (PressNum == 0)  //保留第一点
                    {
                        FirstX = e.X;
                        FirstY = e.Y;
                    }
                    else    //第二点，确定旋转角，改变图形参数
                    {
                        // 向量 (FirstX,FirstY) → (e.X,e.Y) 与水平方向的夹角即为旋转角
                        double angle = Math.Atan2(e.Y - FirstY, e.X - FirstX) * 180.0 / Math.PI;
                        Matrix myMatrix = new Matrix();          //建立矩阵变量，为计算复合矩阵做准备
                        myMatrix.Translate(-100, -100);          //根据旋转中心（矩形左下角），建立平移矩阵
                        myMatrix.Rotate((float)angle, MatrixOrder.Append);   //右乘旋转矩阵
                        myMatrix.Translate(100, 100, MatrixOrder.Append);    //右乘平移矩阵
                        g.Transform = myMatrix;                  //用得到的复合矩阵对图形进行变换
                        g.DrawPolygon(Pens.Blue, pointsgroup);   //画变换后的图形
                        g.ResetTransform();
                    }
                }
                PressNum++;
                if (PressNum >= 2)
                {
                    PressNum = 0;           //完毕，清零，为下一次做准备
                }
                return;
            }
        }

        // ============ 菜单：基本图形生成 ============
        private void mnuDDA_Click(object sender, EventArgs e)
        {
            StartMode(1, "DDA 直线算法：点击两点确定一条直线，移动鼠标可预览");
        }

        private void mnuMidLine_Click(object sender, EventArgs e)
        {
            StartMode(2, "中点直线算法：点击两点确定一条直线，移动鼠标可预览");
        }

        private void mnuCircle_Click(object sender, EventArgs e)
        {
            StartMode(5, "Bresenham 画圆算法：第一点定圆心，第二点定半径");
        }

        // ============ 菜单：图形填充 ============
        private void mnuFill_Click(object sender, EventArgs e)
        {
            StartMode(3, "扫描线填充：左键依次点击多边形顶点，右键结束并填充");
        }

        // ============ 菜单：二维图像裁剪 ============
        private void mnuCohen_Click(object sender, EventArgs e)
        {
            StartMode(21, "Cohen-Sutherland 裁剪：点击两点确定待裁剪直线（灰框为裁剪窗口）");
            DrawClipWindow();
        }

        private void mnuMidCut_Click(object sender, EventArgs e)
        {
            StartMode(22, "中点分割裁剪：点击两点确定待裁剪直线（灰框为裁剪窗口）");
            DrawClipWindow();
        }

        private void mnuLiang_Click(object sender, EventArgs e)
        {
            StartMode(23, "梁友栋-Barsky 裁剪：点击两点确定待裁剪直线（灰框为裁剪窗口）");
            DrawClipWindow();
        }

        private void mnuSH_Click(object sender, EventArgs e)
        {
            StartMode(24, "Sutherland-Hodgman 多边形裁剪：左键依次点击多边形顶点，右键结束并裁剪");
            DrawClipWindow();
        }

        // ============ 菜单：二维图形变换 ============
        private void TransMove_Click(object sender, EventArgs e)
        {
            StartMode(11, "平移变换：点击两点，以两点的位移作为平移向量");
            TransInitGraph();
        }

        private void TransRotate_Click(object sender, EventArgs e)
        {
            StartMode(12, "旋转变换：点击两点，两点连线与水平方向的夹角即为旋转角");
            MenuID = 12;
            PressNum = 0;
            Graphics g = CreateGraphics();  //创建图形设备
            pointsgroup[0] = new Point(100, 100);
            pointsgroup[1] = new Point(200, 100);
            pointsgroup[2] = new Point(200, 200);
            pointsgroup[3] = new Point(100, 200);
            g.DrawPolygon(Pens.Red, pointsgroup);   //画出被旋转图形
        }

        private void TransScale_Click(object sender, EventArgs e)
        {
            MenuID = 13;
            PressNum = 0;
            statusLabel1.Text = "缩放变换：在弹出的对话框中输入 X、Y 方向的缩放系数";
            float xs, ys;
            MyForm myf = new MyForm();  //创建对话框对象
            if (myf.ShowDialog() == DialogResult.Cancel) //打开建立的对话框，接受变换系数
            {
                myf.Close();    //如果选择的是“取消”，则关闭对话框，退出
                return;
            }
            xs = myf.Xscale;
            ys = myf.Yscale;
            myf.Close();
            Graphics g = CreateGraphics();  //创建图形设备
            pointsgroup[0] = new Point(100, 100);   //画原图形
            pointsgroup[1] = new Point(200, 100);
            pointsgroup[2] = new Point(200, 200);
            pointsgroup[3] = new Point(100, 200);
            g.DrawPolygon(Pens.Red, pointsgroup);   //原图形存在与图形设备g中
            Matrix myMatrix = new Matrix();     //建立矩阵变量，为计算复合矩阵做准备
            myMatrix.Translate(-100, -100);     //根据缩放中心，建立平移矩阵
            myMatrix.Scale(xs, ys, MatrixOrder.Append);     //右乘缩放矩阵
            myMatrix.Translate(100, 100, MatrixOrder.Append);   //右乘平移矩阵
            g.Transform = myMatrix;      //用得到的符合矩阵对图形进行变换
            g.DrawPolygon(Pens.Blue, pointsgroup);   //画变换后的图形
            g.ResetTransform();
        }

        /// <summary>
        /// 把被变换的矩形（pointsgroup）还原成初始位置并画出来。
        /// </summary>
        private void TransInitGraph()
        {
            Graphics g = CreateGraphics();
            pointsgroup[0] = new Point(100, 100);
            pointsgroup[1] = new Point(200, 100);
            pointsgroup[2] = new Point(200, 200);
            pointsgroup[3] = new Point(100, 200);
            g.DrawPolygon(Pens.Red, pointsgroup);
        }

        // ============ 菜单：投影、消隐、退出 ============
        private void mnuProject_Click(object sender, EventArgs e)
        {
            StartMode(31, "投影：正方体的三视图（正投影）");
            project.ThreeView();
        }

        private void mnuHide_Click(object sender, EventArgs e)
        {
            StartMode(32, "消隐：正方体正等测投影 + 背面剔除（只画可见棱）");
            hide.HiddenLine();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
