using System;
using System.Drawing;

using System.Windows.Forms;

namespace GraphicsGeometry
{
    /// <summary>
    /// 直线生成算法：DDA 算法与中点算法。
    /// 两个算法都先把直线按斜率归入四个对称区间之一（flag = 1~4），
    /// 再做坐标变换，最后在 0°~45° 区间内用统一公式逐点生成，实现四向对称。
    /// </summary>
    internal class drawLine
    {
        private Control _drawingSurface;   //绘图表面（主窗体）

        public drawLine(Control drawingSurface)
        {
            _drawingSurface = drawingSurface;
        }

        /// <summary>
        /// DDA 直线算法：以步长驱动，每步在坐标增量较大的一轴上移动一个像素，
        /// 另一轴按斜率累加并四舍五入。
        /// </summary>
        public void DDALine(Graphics g, int x0, int y0, int x1, int y1)
        {
            int x, flag;
            float m, y;
            g = _drawingSurface.CreateGraphics();
            if (x0 == x1 && y0 == y1) return;//端点重叠，不画，这种情况不可忽略，否则程序会有缺陷。
            if (x0 == x1)//垂直线
            {
                if (y0 > y1)//保证y0为最小
                {
                    x = x0;
                    y0 = y1;
                    y1 = x;
                }
                for (x = y0; x <= y1; x++)
                {
                    g.DrawRectangle(Pens.Red, x1, x, 1, 1);//画点函数，在(x1,x)处画红点
                }
                return;
            }
            if (y0 == y1)//水平线
            {
                if (x0 > x1)//保证x0为最小
                {
                    x = x0;
                    x0 = x1;
                    x1 = x;
                }
                for (x = x0; x <= x1; x++)
                {
                    g.DrawRectangle(Pens.Red, x, y0, 1, 1);//画点函数，在(x,y1)处画红点
                }
                return;
            }
            if (x0 > x1)//保证x0为最小,按照算法，(x0,y0)是左端点，如果不满足，则交换
            {
                x = x0;
                x0 = x1;
                x1 = x;
                x = y0;
                y0 = y1;
                y1 = x;
            }
            flag = 0;//记录线的种类
            if (x1 - x0 >= y1 - y0 && y1 - y0 >= 0)
            {
                flag = 1;//0~45度,线段一，不做转换
            }
            if (x1 - x0 > y0 - y1 && y0 - y1 > 0)//45~90度,线段二，做转换
            {
                flag = 2;//关于X轴对称，Y加负号
                y0 = -y0;
                y1 = -y1;
            }
            if (y1 - y0 > x1 - x0)//90~135度,线段三，做转换,关于y=x对称，交换x和y
            {
                flag = 3;
                x = x0;
                x0 = y0;
                y0 = x;
                x = x1;
                x1 = y1;
                y1 = x;
            }
            if (y0 - y1 > x1 - x0)//135~180度,线段四，做转换,关于y=-x对称，X加负号
            {
                flag = 4;
                x = x0;
                x0 = -y0;
                y0 = x;
                x = x1;
                x1 = -y1;
                y1 = x;
            }
            m = (float)(y1 - y0) / (float)(x1 - x0);//斜率
            for (x = x0, y = (float)y0; x <= x1; x++, y = y + m)
            {
                if (flag == 1) g.DrawRectangle(Pens.Red, x, (int)(y + 0.5), 1, 1);
                if (flag == 2) g.DrawRectangle(Pens.Red, x, -(int)(y + 0.5), 1, 1);
                if (flag == 3) g.DrawRectangle(Pens.Red, (int)(y + 0.5), x, 1, 1);
                if (flag == 4) g.DrawRectangle(Pens.Red, -(int)(y + 0.5), x, 1, 1);
            }
        }

        /// <summary>
        /// 中点直线算法：在 0°~45° 区间上每步 x 加 1，
        /// 用判别式 d 决定 y 是否加 1（整数运算，无浮点乘法）。
        /// </summary>
        public void MidLine(Graphics g, int x0, int y0, int x1, int y1)
        {
            int x, y, d, flag;
            g = _drawingSurface.CreateGraphics();
            if (x0 == x1 && y0 == y1) return;//端点重叠，不画，这种情况不可忽略，否则程序会有缺陷。
            if (x0 == x1)//垂直线
            {
                if (y0 > y1)//保证y0为最小
                {
                    x = x0;
                    y0 = y1;
                    y1 = x;
                }
                for (x = y0; x <= y1; x++)
                {
                    g.DrawRectangle(Pens.Blue, x1, x, 1, 1);//画点函数，在(x1,x)处画蓝点
                }
                return;
            }
            if (y0 == y1)//水平线
            {
                if (x0 > x1)//保证x0为最小
                {
                    x = x0;
                    x0 = x1;
                    x1 = x;
                }
                for (x = x0; x <= x1; x++)
                {
                    g.DrawRectangle(Pens.Blue, x, y0, 1, 1);//画点函数，在(x,y1)处画蓝点
                }
                return;
            }
            if (x0 > x1)//保证x0为最小,按照算法，(x0,y0)是左端点，如果不满足，则交换
            {
                x = x0;
                x0 = x1;
                x1 = x;
                x = y0;
                y0 = y1;
                y1 = x;
            }
            flag = 0;//记录线的种类
            if (x1 - x0 >= y1 - y0 && y1 - y0 >= 0)
            {
                flag = 1;//0~45度,线段一，不做转换
            }
            if (x1 - x0 > y0 - y1 && y0 - y1 > 0)//斜率 0~-45度,线段二，做转换
            {
                flag = 2;//关于X轴对称，Y加负号
                y0 = -y0;
                y1 = -y1;
            }
            if (y1 - y0 > x1 - x0)//45~90度,线段三，做转换,关于y=x对称，交换x和y
            {
                flag = 3;
                x = x0;
                x0 = y0;
                y0 = x;
                x = x1;
                x1 = y1;
                y1 = x;
            }
            if (y0 - y1 > x1 - x0)//-90~-45度,线段四，做转换,关于y=-x对称，X加负号
            {
                flag = 4;
                x = x0;
                x0 = -y0;
                y0 = x;
                x = x1;
                x1 = -y1;
                y1 = x;
            }
            x = x0;
            y = y0;
            d = (x1 - x0) - 2 * (y1 - y0);
            while (x < x1 + 1)
            {
                if (flag == 1) g.DrawRectangle(Pens.Red, x, y, 1, 1);
                if (flag == 2) g.DrawRectangle(Pens.Red, x, -y, 1, 1);
                if (flag == 3) g.DrawRectangle(Pens.Red, y, x, 1, 1);
                if (flag == 4) g.DrawRectangle(Pens.Red, -y, x, 1, 1);
                x++;
                if (d > 0)
                {
                    d = d - 2 * (y1 - y0);
                }
                else
                {
                    y++;
                    d = d - 2 * ((y1 - y0) - (x1 - x0));
                }
            }
        }
    }
}
