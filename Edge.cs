namespace GraphicsGeometry
{
    /// <summary>
    /// 扫描线填充用的「边」结构（活性边表 AEL 中的一项）。
    /// 约定：(xLo,yLo) 为边的下端顶点，(xHi,yHi) 为上端顶点，且 yLo &lt; yHi（水平边不建边）。
    /// </summary>
    public class Edge
    {
        /// <summary>当前扫描线与该边的交点横坐标（初始为下端点的 x）</summary>
        public float XMin;

        /// <summary>边的最小 y 坐标（下端点）</summary>
        public int YMin;

        /// <summary>边的最大 y 坐标（上端点）</summary>
        public int YMax;

        /// <summary>边的斜率倒数 Δx/Δy（即 1/k）</summary>
        public float K;

        public Edge(int xLo, int yLo, int xHi, int yHi)
        {
            XMin = xLo;
            YMin = yLo;
            YMax = yHi;
            K = (float)(xHi - xLo) / (float)(yHi - yLo);
        }
    }
}
