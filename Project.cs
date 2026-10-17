using System;
using System.Drawing;

using System.Windows.Forms;

namespace GraphicsGeometry
{
    /// <summary>
    /// 投影：把边长为 100 的正方体分别向三个坐标面作正投影，得到三视图。
    ///   主视图（V 面）—— 沿 y 轴投影，反映 x 与 z；
    ///   俯视图（H 面）—— 沿 z 轴投影，反映 x 与 y，放在主视图正下方；
    ///   左视图（W 面）—— 沿 x 轴投影，反映 y 与 z，放在主视图正右方。
    /// </summary>
    internal class Project
    {
        private Control _drawingSurface;   //绘图表面（主窗体）

        public Project(Control drawingSurface)
        {
            _drawingSurface = drawingSurface;
        }

        public void ThreeView()
        {
            Graphics g = _drawingSurface.CreateGraphics();
            Pen pen = new Pen(Color.Black, 1);
            Font font = new Font("宋体", 10);

            int s = 100;        //正方体棱长
            int gap = 60;       //视图之间的间隔
            int ox = 260;       //主视图左上角
            int oy = 180;

            // 主视图：x 向右、z 向上
            g.DrawRectangle(pen, ox, oy, s, s);
            g.DrawString("主视图", font, Brushes.Black, ox + s / 2 - 24, oy + s + 8);

            // 俯视图：x 仍向右、y 向下（长对正）
            g.DrawRectangle(pen, ox, oy + s + gap, s, s);
            g.DrawString("俯视图", font, Brushes.Black, ox + s / 2 - 24, oy + 2 * s + gap + 8);

            // 左视图：y 向右、z 向上（高平齐）
            g.DrawRectangle(pen, ox + s + gap, oy, s, s);
            g.DrawString("左视图", font, Brushes.Black, ox + s + gap + s / 2 - 24, oy + s + 8);

            // 投影连线：长对正、高平齐
            Pen dash = new Pen(Color.Gray, 1);
            dash.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            g.DrawLine(dash, ox, oy + s + gap, ox + s + gap + s, oy + s + gap);
            g.DrawLine(dash, ox + s + gap, oy, ox + s + gap, oy + s + gap + s);

            g.DrawString("三视图（正投影）：正方体棱长 " + s + "，三视图均为正方形",
                font, Brushes.Black, ox - 60, oy - 40);
        }
    }
}
