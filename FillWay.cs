using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using System.Windows.Forms;

namespace GraphicsGeometry
{
    /// <summary>
    /// 多边形填充算法：扫描线填充（活性边表法）。
    /// 逐条扫描线求出与多边形各边的交点，排序后两两配对，
    /// 每对交点之间画一条水平线段，即完成该扫描行的填充。
    /// </summary>
    internal class FillWay
    {
        private Control _drawingSurface;   //绘图表面（主窗体）

        public FillWay(Control drawingSurface)
        {
            _drawingSurface = drawingSurface;
        }

        /// <summary>
        /// 扫描线填充。
        /// </summary>
        /// <param name="g">绘图表面</param>
        /// <param name="group">多边形顶点数组，group[0..PressNum-1] 为有效顶点</param>
        /// <param name="PressNum">多边形顶点个数</param>
        /// <param name="FirstX">配对交点时暂存段起点的 x</param>
        /// <param name="FirstY">备用参数（保持与调用处一致的签名）</param>
        public void ScanLineFill(Graphics g, Point[] group, int PressNum, int FirstX, int FirstY)
        {
            var edgeList = new List<GraphicsGeometry.Edge>();
            int yu = 0;
            int yd = 1024;
            group[PressNum] = group[0];//闭合多边形
            for (int i = 0; i < PressNum; i++)
            {
                if (group[i].Y > yu)
                    yu = group[i].Y;
                if (group[i].Y < yd)
                    yd = group[i].Y;
                if (group[i].Y != group[i + 1].Y)
                {
                    if (group[i].Y > group[i + 1].Y)
                    {
                        // 边的下端点为 group[i+1]，上端点为 group[i]
                        edgeList.Add(new GraphicsGeometry.Edge(group[i + 1].X, group[i + 1].Y, group[i].X, group[i].Y));
                    }
                    else
                    {
                        edgeList.Add(new GraphicsGeometry.Edge(group[i].X, group[i].Y, group[i + 1].X, group[i + 1].Y));
                    }
                }
            }
            g = _drawingSurface.CreateGraphics();
            for (int y = yd; y <= yu; y++)
            {
                // 取出与当前扫描线相交的边，按交点 x 排序（x 相同时按斜率排序）
                var sorted = from item in edgeList
                             where y < item.YMax && y >= item.YMin
                             orderby item.XMin, item.K
                             select item;
                int flag = 0;
                foreach (var item in sorted)
                {
                    if (flag == 0)
                    {
                        FirstX = (int)(item.XMin + 0.5);
                        flag++;
                    }
                    else
                    {
                        g.DrawLine(Pens.Blue, (int)(item.XMin + 0.5), y, FirstX - 1, y);
                        flag = 0;
                    }
                }
                // 更新活性边的交点 x：x = x + 1/k
                for (int i = 0; i < edgeList.Count; i++)
                {
                    if (y < edgeList[i].YMax - 1 && y > edgeList[i].YMin)
                    {
                        edgeList[i].XMin += edgeList[i].K;
                    }
                }
            }
        }
    }
}
