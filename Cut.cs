using System;
using System.Drawing;

using System.Windows.Forms;

namespace GraphicsGeometry
{
    /// <summary>
    /// 二维裁剪算法：
    ///   Cohen-Sutherland 直线裁剪、中点分割裁剪、梁友栋-Barsky 裁剪、
    ///   Sutherland-Hodgman 多边形裁剪。
    /// 裁剪窗口由 pointsgroup 决定：pointsgroup[0] 给出左、下边界，
    /// pointsgroup[2] 给出右、上边界。
    /// 原图用红色画出，裁剪后的可见部分用黄色粗线覆盖。
    /// </summary>
    internal class Cut
    {
        private Control _drawingSurface;   //绘图表面（主窗体）
        private int WinPressNum;           //多边形裁剪时，当前被裁剪多边形的顶点数

        public Cut(Control drawingSurface)
        {
            _drawingSurface = drawingSurface;
        }

        /// <summary>
        /// 区域编码：把平面分成 9 个区域，用 4 位二进制码表示点的位置。
        /// 第 1 位(1)=左，第 2 位(2)=右，第 4 位(4)=下，第 8 位(8)=上。
        /// </summary>
        private int encode(int x, int y, Point[] pointsgroup)
        {
            int XL, XR, YD, YU;//裁剪窗口左、右、上、下边界
            XL = pointsgroup[0].X;
            XR = pointsgroup[2].X;
            YD = pointsgroup[0].Y;
            YU = pointsgroup[2].Y;
            int code = 0;
            if (x < XL) code |= 1;          //点在窗口左边界外侧
            else if (x > XR) code |= 2;     //点在窗口右边界外侧
            if (y < YD) code |= 4;          //点在窗口下边界外侧
            else if (y > YU) code |= 8;     //点在窗口上边界外侧
            return code;
        }

        // ======================= 4.1 Cohen-Sutherland =======================
        public void CohenCut1(int x1, int y1, int x2, int y2, Point[] pointsgroup)
        {
            int XL, XR, YD, YU;//裁剪窗口左、右、上、下边界
            XL = pointsgroup[0].X;
            XR = pointsgroup[2].X;
            YD = pointsgroup[0].Y;
            YU = pointsgroup[2].Y;
            int code1 = 0, code2 = 0;
            int x = 0, y = 0;
            Graphics g = _drawingSurface.CreateGraphics();
            g.DrawLine(Pens.Red, x1, y1, x2, y2);
            code1 = encode(x1, y1, pointsgroup);
            code2 = encode(x2, y2, pointsgroup);
            while (code1 != 0 || code2 != 0)
            {
                if ((code1 & code2) != 0)
                {
                    return; // 线段完全在裁剪区域外，直接返回
                }
                int code = code1;
                if (code == 0)
                {
                    code = code2;
                }
                if ((code & 1) != 0)
                {
                    x = XL;
                    y = y1 + (x - x1) * (y2 - y1) / (x2 - x1);
                }
                else if ((code & 2) != 0)
                {
                    x = XR;
                    y = y1 + (x - x1) * (y2 - y1) / (x2 - x1);
                }
                else if ((code & 4) != 0)
                {
                    y = YD;
                    x = x1 + (y - y1) * (x2 - x1) / (y2 - y1);
                }
                else if ((code & 8) != 0)
                {
                    y = YU;
                    x = x1 + (y - y1) * (x2 - x1) / (y2 - y1);
                }
                if (code == code1)
                {
                    x1 = x;
                    y1 = y;
                    code1 = encode(x1, y1, pointsgroup);
                }
                else
                {
                    x2 = x;
                    y2 = y;
                    code2 = encode(x2, y2, pointsgroup);
                }
            }
            Pen MyPen = new Pen(Color.Yellow, 3);
            g.DrawLine(MyPen, x1, y1, x2, y2);
        }

        // ======================= 4.2 中点分割裁剪 =======================
        public void MidCut1(int x1, int y1, int x2, int y2, Point[] pointsgroup)
        {
            Graphics g = _drawingSurface.CreateGraphics();//绘图表面
            g.DrawLine(Pens.Red, x1, y1, x2, y2);
            Point p1, p2;
            if (LineISOutOFWindow(x1, y1, x2, y2, pointsgroup))
            {
                return;
            }
            p1 = FindNearestPoint(x1, y1, x2, y2, pointsgroup);
            if (PointIsOutOfWindow(p1.X, p1.Y, pointsgroup))
            {
                return;
            }
            p2 = FindNearestPoint(x2, y2, x1, y1, pointsgroup);
            Pen MyPen = new Pen(Color.Yellow, 3);
            g.DrawLine(MyPen, p1.X, p1.Y, p2.X, p2.Y);
        }

        private bool LineISOutOFWindow(int x1, int y1, int x2, int y2, Point[] pointsgroup)
        {
            int XL, XR, YD, YU;//裁剪窗口左、右、上、下边界
            XL = pointsgroup[0].X;
            XR = pointsgroup[2].X;
            YD = pointsgroup[0].Y;
            YU = pointsgroup[2].Y;
            if ((x1 < XL && x2 < XL) || (x1 > XR && x2 > XR) || (y1 < YD && y2 < YD) || (y1 > YU && y2 > YU))//线段完全在裁剪区域外
            {
                return true;
            }
            return false;
        }

        private bool PointIsOutOfWindow(int x, int y, Point[] pointsgroup)
        {
            int XL, XR, YD, YU;//裁剪窗口左、右、上、下边界
            XL = pointsgroup[0].X;
            XR = pointsgroup[2].X;
            YD = pointsgroup[0].Y;
            YU = pointsgroup[2].Y;
            if (x < XL || x > XR || y < YD || y > YU)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 从 (x1,y1) 出发向 (x2,y2) 方向二分，逐步逼近离 (x1,y1) 最近的窗口内可见点。
        /// </summary>
        private Point FindNearestPoint(int x1, int y1, int x2, int y2, Point[] pointsgroup)
        {
            int x = 0, y = 0;
            Point p = new Point(0, 0);
            if (!PointIsOutOfWindow(x1, y1, pointsgroup))
            {
                p.X = x1;
                p.Y = y1;
                return p;
            }
            while (!(Math.Abs(x1 - x2) <= 1 && Math.Abs(y1 - y2) <= 1))
            {
                x = (x1 + x2) / 2;
                y = (y1 + y2) / 2;
                if (PointIsOutOfWindow(x, y, pointsgroup))
                {
                    x1 = x;
                    y1 = y;
                }
                else
                {
                    x2 = x;
                    y2 = y;
                }
            }
            if (!PointIsOutOfWindow(x1, y1, pointsgroup))
            {
                p.X = x2;
                p.Y = y2;
            }
            else
            {
                p.X = x1;
                p.Y = y1;
            }
            return p;
        }

        // ======================= 4.3 梁友栋-Barsky 裁剪 =======================
        public void LiangCut1(int x1, int y1, int x2, int y2, Point[] pointsgroup)
        {
            Graphics g = _drawingSurface.CreateGraphics();//绘图表面
            g.DrawLine(Pens.Red, x1, y1, x2, y2);
            int XL, XR, YD, YU;//裁剪窗口左、右、上、下边界
            XL = pointsgroup[0].X;
            XR = pointsgroup[2].X;
            YD = pointsgroup[0].Y;
            YU = pointsgroup[2].Y;
            float tsX, tsY, teX, teY;
            if (x1 == x2)
            {
                tsX = 0;
                teX = 1;//垂直线段
            }
            else if (x1 < x2)
            {
                tsX = (float)(XL - x1) / (float)(x2 - x1);
                teX = (float)(XR - x1) / (float)(x2 - x1);
            }
            else
            {
                tsX = (float)(XR - x1) / (float)(x2 - x1);
                teX = (float)(XL - x1) / (float)(x2 - x1);
            }
            if (y1 == y2)
            {
                tsY = 0;
                teY = 1;//水平线段
            }
            else if (y1 < y2)
            {
                tsY = (float)(YD - y1) / (float)(y2 - y1);
                teY = (float)(YU - y1) / (float)(y2 - y1);
            }
            else
            {
                tsY = (float)(YU - y1) / (float)(y2 - y1);
                teY = (float)(YD - y1) / (float)(y2 - y1);
            }
            tsX = Math.Max(tsX, tsY);//进入窗口的最晚时间
            tsX = Math.Max(tsX, 0);
            teX = Math.Min(teX, teY);//离开窗口的最早时间
            teX = Math.Min(teX, 1);
            if (tsX < teX)
            {
                int xx1, yy1, xx2, yy2;
                xx1 = (int)(x1 + tsX * (x2 - x1));
                yy1 = (int)(y1 + tsX * (y2 - y1));
                xx2 = (int)(x1 + teX * (x2 - x1));
                yy2 = (int)(y1 + teX * (y2 - y1));
                Pen MyPen = new Pen(Color.Yellow, 3);
                g.DrawLine(MyPen, xx1, yy1, xx2, yy2);
            }
        }

        // ======================= 4.4 Sutherland-Hodgman 多边形裁剪 =======================
        public void WindowCut1(Graphics g, Point[] group, int PressNum, int FirstX, int FirstY, Point[] pointsgroup)
        {
            WinPressNum = PressNum;
            group[WinPressNum] = group[0];//闭合多边形
            EdgeClipping(0, group, pointsgroup);
            EdgeClipping(1, group, pointsgroup);
            EdgeClipping(2, group, pointsgroup);
            EdgeClipping(3, group, pointsgroup);
            g = _drawingSurface.CreateGraphics();
            Pen MyPen = new Pen(Color.Yellow, 3);
            for (int i = 0; i < WinPressNum; i++)
            {
                g.DrawLine(MyPen, group[i], group[i + 1]);
            }
        }

        /// <summary>
        /// 用窗口的一条边界对多边形做一次单边裁剪。
        /// linecode：0=左边界 x=XL，1=上边界 y=YU，2=右边界 x=XR，3=下边界 y=YD。
        /// </summary>
        private void EdgeClipping(int linecode, Point[] group, Point[] pointsgroup)
        {
            int XL, XR, YD, YU;//裁剪窗口左、右、上、下边界
            XL = pointsgroup[0].X;
            XR = pointsgroup[2].X;
            YD = pointsgroup[0].Y;
            YU = pointsgroup[2].Y;
            float x, y;
            int n, i, number1;
            Point[] q = new Point[200];
            number1 = 0;
            if (linecode == 0)//x=XL 用窗口左边来裁剪多边形
            {
                for (n = 0; n < WinPressNum; n++)
                {
                    if (group[n].X < XL && group[n + 1].X < XL)
                    {
                        //对外，不输出
                    }
                    if (group[n].X >= XL && group[n + 1].X >= XL)
                    {
                        //内内，输出后点
                        q[number1++] = group[n + 1];
                    }
                    if (group[n].X >= XL && group[n + 1].X < XL)
                    {
                        //内外，输出交点
                        y = group[n].Y + (float)(group[n + 1].Y - group[n].Y) / (float)(group[n + 1].X - group[n].X) * (float)(XL - group[n].X);
                        q[number1].X = XL;
                        q[number1++].Y = (int)y;
                    }
                    if (group[n].X < XL && group[n + 1].X >= XL)
                    {
                        //外内，输出交点、后点
                        y = group[n].Y + (float)(group[n + 1].Y - group[n].Y) / (float)(group[n + 1].X - group[n].X) * (float)(XL - group[n].X);
                        q[number1].X = XL;
                        q[number1++].Y = (int)y;
                        q[number1++] = group[n + 1];
                    }
                }
                for (i = 0; i < number1; i++)//裁剪结果存入group数组
                {
                    group[i] = q[i];
                }
                group[number1] = q[0];
                WinPressNum = number1;
            }
            if (linecode == 1)//y=YU 用窗口顶边来裁剪多边形
            {
                for (n = 0; n < WinPressNum; n++)
                {
                    if (group[n].Y >= YU && group[n + 1].Y >= YU)//外外，不输出
                    {
                    }
                    if (group[n].Y < YU && group[n + 1].Y < YU)//内内，输出后点
                    {
                        q[number1++] = group[n + 1];
                    }
                    if (group[n].Y < YU && group[n + 1].Y >= YU)//内外，输出交点
                    {
                        x = group[n].X + (float)(group[n + 1].X - group[n].X) / (float)(group[n + 1].Y - group[n].Y) * (float)(YU - group[n].Y);
                        q[number1].X = (int)x;
                        q[number1++].Y = YU;
                    }
                    if (group[n].Y >= YU && group[n + 1].Y < YU)//外内，输出交点、后点
                    {
                        x = group[n].X + (float)(group[n + 1].X - group[n].X) / (float)(group[n + 1].Y - group[n].Y) * (float)(YU - group[n].Y);
                        q[number1].X = (int)x;
                        q[number1++].Y = YU;
                        q[number1++] = group[n + 1];
                    }
                }
                for (i = 0; i < number1; i++)
                {
                    group[i] = q[i];
                }
                group[number1] = q[0];
                WinPressNum = number1;
            }
            if (linecode == 2)//x=XR 用窗口右边来裁剪多边形
            {
                for (n = 0; n < WinPressNum; n++)
                {
                    if (group[n].X >= XR && group[n + 1].X >= XR)//外外，不输出
                    {
                    }
                    if (group[n].X < XR && group[n + 1].X < XR)//内内，输出后点
                    {
                        q[number1++] = group[n + 1];
                    }
                    if (group[n].X < XR && group[n + 1].X >= XR)//内外，输出交点
                    {
                        y = group[n].Y + (float)(group[n + 1].Y - group[n].Y) / (float)(group[n + 1].X - group[n].X) * (float)(XR - group[n].X);
                        q[number1].X = XR;
                        q[number1++].Y = (int)y;
                    }
                    if (group[n].X >= XR && group[n + 1].X < XR)//外内，输出交点、后点
                    {
                        y = group[n].Y + (float)(group[n + 1].Y - group[n].Y) / (float)(group[n + 1].X - group[n].X) * (float)(XR - group[n].X);
                        q[number1].X = XR;
                        q[number1++].Y = (int)y;
                        q[number1++] = group[n + 1];
                    }
                }
                for (i = 0; i < number1; i++)
                {
                    group[i] = q[i];
                }
                group[number1] = q[0];
                WinPressNum = number1;
            }
            if (linecode == 3)//y=YD 用窗口底边来裁剪多边形
            {
                for (n = 0; n < WinPressNum; n++)
                {
                    if (group[n].Y < YD && group[n + 1].Y < YD)//外外，不输出
                    {
                    }
                    if (group[n].Y >= YD && group[n + 1].Y >= YD)//内内，输出后点
                    {
                        q[number1++] = group[n + 1];
                    }
                    if (group[n].Y >= YD && group[n + 1].Y < YD)//内外，输出交点
                    {
                        x = group[n].X + (float)(group[n + 1].X - group[n].X) / (float)(group[n + 1].Y - group[n].Y) * (float)(YD - group[n].Y);
                        q[number1].X = (int)x;
                        q[number1++].Y = YD;
                    }
                    if (group[n].Y < YD && group[n + 1].Y >= YD)//外内，输出交点、后点
                    {
                        x = group[n].X + (float)(group[n + 1].X - group[n].X) / (float)(group[n + 1].Y - group[n].Y) * (float)(YD - group[n].Y);
                        q[number1].X = (int)x;
                        q[number1++].Y = YD;
                        q[number1++] = group[n + 1];
                    }
                }
                for (i = 0; i < number1; i++)
                {
                    group[i] = q[i];
                }
                group[number1] = q[0];
                WinPressNum = number1;
            }
        }
    }
}
