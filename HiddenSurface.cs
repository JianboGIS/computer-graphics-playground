using System;
using System.Drawing;
using System.Drawing.Drawing2D;

using System.Windows.Forms;

namespace GraphicsGeometry
{
    /// <summary>
    /// 消隐：正方体的正等测（等轴测）投影 + 背面剔除。
    /// 先把三视图变成轴测投影：绕 z 轴旋转 45°，再绕 x 轴旋转 35.264°，然后正交投影；
    /// 用面法向量判断每个面是否朝向观察者（背面剔除），只绘制可见面，
    /// 被前面的面挡住的棱自然不会被画出来，即达到隐藏线消除的效果。
    /// </summary>
    internal class HiddenSurface
    {
        private Control _drawingSurface;   //绘图表面（主窗体）

        public HiddenSurface(Control drawingSurface)
        {
            _drawingSurface = drawingSurface;
        }

        /// <summary>
        /// 正方体的 8 个顶点坐标（单位：半个棱长，即 ±1）
        /// </summary>
        private static readonly int[] VX = { -1, 1, 1, -1, -1, 1, 1, -1 };
        private static readonly int[] VY = { -1, -1, 1, 1, -1, -1, 1, 1 };
        private static readonly int[] VZ = { -1, -1, -1, -1, 1, 1, 1, 1 };

        /// <summary>
        /// 6 个面，每个面按「从正方体外部看逆时针」的顺序记录 4 个顶点的下标，
        /// 依次为：底面、顶面、前面(y=-1)、后面(y=+1)、左面(x=-1)、右面(x=+1)。
        /// </summary>
        private static readonly int[,] FACE = new int[6, 4] {
            {0, 3, 2, 1}, {4, 5, 6, 7}, {0, 1, 5, 4}, {2, 3, 7, 6}, {0, 4, 7, 3}, {1, 2, 6, 5}
        };

        public void HiddenLine()
        {
            Graphics g = _drawingSurface.CreateGraphics();
            g.SmoothingMode = SmoothingMode.AntiAlias;

            double s = 100.0;                   //棱长的一半
            double cx = 420.0, cy = 250.0;      //正方体中心在屏幕上的位置

            // 正等测的两个旋转角
            double alpha = Math.PI / 4;                 //绕 z 轴 45°
            double beta = Math.Atan(1.0 / Math.Sqrt(2.0)); //绕 x 轴 35.264°

            double ca = Math.Cos(alpha), sa = Math.Sin(alpha);
            double cb = Math.Cos(beta), sb = Math.Sin(beta);

            // 把三维点变换到观察坐标系，并正交投影到屏幕
            double[] sx = new double[8];
            double[] sy = new double[8];
            for (int i = 0; i < 8; i++)
            {
                double x = VX[i] * s, y = VY[i] * s, z = VZ[i] * s;
                double x1 = x * ca - y * sa;          //绕 z 轴旋转
                double y1 = x * sa + y * ca;
                double x2 = x1;                       //绕 x 轴旋转
                double y2 = y1 * cb - z * sb;
                sx[i] = cx + x2;                      //正交投影（屏幕 y 轴向下，故取负）
                sy[i] = cy - y2;
            }

            Pen visiblePen = new Pen(Color.Blue, 2);
            for (int f = 0; f < 6; f++)
            {
                // 用面的前两条边叉乘求外法向量
                int i0 = FACE[f, 0], i1 = FACE[f, 1], i2 = FACE[f, 2];
                double ax = (VX[i1] - VX[i0]) * s, ay = (VY[i1] - VY[i0]) * s, az = (VZ[i1] - VZ[i0]) * s;
                double bx = (VX[i2] - VX[i1]) * s, by = (VY[i2] - VY[i1]) * s, bz = (VZ[i2] - VZ[i1]) * s;
                double nx = ay * bz - az * by;
                double ny = az * bx - ax * bz;
                double nz = ax * by - ay * bx;

                // 法向量做同样的旋转：n2 = Rx(beta) · Rz(alpha) · n
                double ny1 = nx * sa + ny * ca;
                double nz2 = ny1 * sb + nz * cb;

                if (nz2 <= 0) continue;   //法向量背向观察者 → 该面不可见，直接剔除

                Point[] poly = new Point[4];
                for (int k = 0; k < 4; k++)
                {
                    int idx = FACE[f, k];
                    poly[k] = new Point((int)(sx[idx] + 0.5), (int)(sy[idx] + 0.5));
                }
                g.DrawPolygon(visiblePen, poly);
            }

            Font font = new Font("宋体", 10);
            g.DrawString("消隐：正等测投影 + 背面剔除（只画出可见面上的棱）",
                font, Brushes.Black, 420 - 130, 480);
        }
    }
}
