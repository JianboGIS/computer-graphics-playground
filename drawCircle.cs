using System;
using System.Drawing;

using System.Windows.Forms;

namespace GraphicsGeometry
{
    /// <summary>
    /// 圆的生成算法：Bresenham 画圆（八分圆 + 八向对称）。
    /// </summary>
    internal class drawCircle
    {
        private Control _drawingSurface;   //绘图表面（主窗体）

        public drawCircle(Control drawingSurface)
        {
            _drawingSurface = drawingSurface;
        }

        /// <summary>
        /// Bresenham 画圆算法。(x0,y0) 为圆心，(x1,y1) 为圆上（或圆附近）一点，
        /// 两点距离四舍五入得到半径，再用判别式 d 逐点生成 45° 圆弧并作八向对称映射。
        /// </summary>
        public void Bresenham_Circle(Graphics g, int x0, int y0, int x1, int y1)
        {
            int r, d, x, y;
            g = _drawingSurface.CreateGraphics();
            r = (int)(Math.Sqrt((x1 - x0) * (x1 - x0) + (y1 - y0) * (y1 - y0)) + 0.5);//计算半径
            x = 0;
            y = r;
            d = 3 - 2 * r;
            while (x <= y)
            {
                g.DrawRectangle(Pens.Green, x0 + x, y0 + y, 1, 1);
                g.DrawRectangle(Pens.Green, x0 - x, y0 + y, 1, 1);
                g.DrawRectangle(Pens.Green, x0 + x, y0 - y, 1, 1);
                g.DrawRectangle(Pens.Green, x0 - x, y0 - y, 1, 1);
                g.DrawRectangle(Pens.Green, x0 + y, y0 + x, 1, 1);
                g.DrawRectangle(Pens.Green, x0 - y, y0 + x, 1, 1);
                g.DrawRectangle(Pens.Green, x0 + y, y0 - x, 1, 1);
                g.DrawRectangle(Pens.Green, x0 - y, y0 - x, 1, 1);
                x = x + 1;
                if (d <= 0)
                {
                    d = d + 4 * x + 6;
                }
                else
                {
                    y = y - 1;
                    d = d + 4 * (x - y) + 10;
                }
            }
        }
    }
}
