# 计算机图形学练习平台

> C# WinForms 图形学算法演示平台 —— 基本图形生成 / 图形填充 / 二维裁剪 / 二维图形变换 / 投影 / 消隐

本项目是一个计算机图形学算法练习平台，把图形学中常见的经典算法集中到一个可交互的
WinForms 程序里，边取点边看图，便于对照算法原理检查绘制结果。
基于 .NET Framework 4.7.2 / Visual Studio 2022，纯 WinForms + GDI+，
**无第三方依赖、无需 NuGet 还原**。

顶部菜单切换算法，绘图区用鼠标取点，移动鼠标时以「橡皮筋」方式实时预览；
多边形的顶点输入以**右键结束**（状态栏会给出当前算法的操作提示）。

---

## 功能

| 菜单 | 菜单 ID | 功能 | 实现 |
| --- | --- | --- | --- |
| 基本图形生成 → 直线-DDA 算法 | 1 | DDA 直线生成 | `drawLine.DDALine` |
| 基本图形生成 → 直线-中点算法 | 2 | 中点直线生成 | `drawLine.MidLine` |
| 基本图形生成 → 圆-Bresenham 算法 | 5 | Bresenham 画圆（八分圆） | `drawCircle.Bresenham_Circle` |
| 图形填充 | 3 | 扫描线填充（活性边表） | `FillWay.ScanLineFill` |
| 二维图像裁剪 → Cohen-Sutherland | 21 | 编码 + 逐边求交 | `Cut.CohenCut1` |
| 二维图像裁剪 → 中点分割 | 22 | 二分逼近可见端点 | `Cut.MidCut1` |
| 二维图像裁剪 → 梁友栋-Barsky | 23 | 参数化 t 区间求交 | `Cut.LiangCut1` |
| 二维图像裁剪 → Sutherland-Hodgman | 24 | 逐边流水线裁剪 | `Cut.WindowCut1` |
| 二维图像变换 → 平移 | 11 | 两点击位移为平移向量 | `Form1_MouseClick` |
| 二维图像变换 → 旋转 | 12 | 两点击连线夹角为旋转角 | `Matrix` 复合变换 |
| 二维图像变换 → 缩放 | 13 | 对话框输入 xs / ys | `MyForm` + `Matrix` |
| 投影 | 31 | 正方体三视图（正投影） | `Project.ThreeView` |
| 消隐 | 32 | 正等测投影 + 背面剔除 | `HiddenSurface.HiddenLine` |
| 退出 | — | 关闭程序 | — |

> 最初的设计只确定了 MenuID = 1 / 2 / 5 / 11 / 12 / 13，其余编号按同样的分段规律补齐（见 `Form1.cs` 菜单处理函数）。

## 操作方式

| 算法 | 鼠标操作 |
| --- | --- |
| DDA 直线 / 中点直线 | 左键点两点确定直线；按下第一点后移动鼠标可橡皮筋预览 |
| Bresenham 画圆 | 左键第一点定圆心，第二点定半径；移动鼠标可预览圆 |
| 扫描线填充 | 左键依次点击多边形各顶点，**右键结束并填充** |
| 直线裁剪（Cohen / 中点 / 梁友栋） | 选中菜单后先显示裁剪窗口（灰框），再左键点两点确定待裁剪直线 |
| 多边形裁剪（Sutherland-Hodgman） | 左键依次点击多边形顶点，**右键结束并裁剪** |
| 平移 / 旋转 | 左键点两点，由两点确定平移量 / 旋转角 |
| 缩放 | 弹出对话框输入 X、Y 缩放系数后确定 |

绘图配色沿用本平台一贯的约定：原图 **红色**，裁剪结果用 **黄色粗线** 覆盖，填充用 **蓝色**，画圆用 **绿色**。

## 目录结构

```
计算机图形学练习平台/
├── 计算机图形学练习平台.sln      # 解决方案（用 VS 打开这个）
├── GraphicsGeometry.csproj      # 项目文件（命名空间/程序集名 GraphicsGeometry）
├── Program.cs                   # 程序入口
├── Form1.cs                     # 主窗体逻辑：全局变量、菜单处理、橡皮筋、鼠标点击
├── Form1.Designer.cs            # 主窗体设计器代码（菜单栏 + 状态栏）
├── Form1.resx                   # 主窗体资源
├── MyForm.cs                    # 缩放系数输入对话框
├── MyForm.Designer.cs           # 对话框设计器代码
├── MyForm.resx                  # 对话框资源
├── drawLine.cs                  # DDA 直线算法、中点直线算法
├── drawCircle.cs                # Bresenham 画圆算法
├── FillWay.cs                   # 扫描线填充算法
├── Edge.cs                      # 扫描线填充的边结构（XMin / YMin / YMax / K）
├── Cut.cs                       # 四种二维裁剪算法
├── Project.cs                   # 三视图正投影
├── HiddenSurface.cs             # 消隐（正等测投影 + 背面剔除）
├── App.config                   # 运行时配置
├── Properties/                  # 程序集信息与资源
└── .gitignore
```

## 编译运行

1. 用 Visual Studio 2022 打开 `计算机图形学练习平台.sln`
2. 目标框架：**.NET Framework 4.7.2**
3. `F5` 或 `Ctrl+F5` 运行
4. 从顶部菜单选择算法，然后在白色绘图区用鼠标取点

## 文件与功能对照

| 功能模块 | 涉及的类 / 方法 | 本仓库文件 |
| --- | --- | --- |
| 平台搭建与全局变量定义 | `Form1` 全局变量 | `Form1.cs` |
| 橡皮筋效果 | `Form1_MouseMove` | `Form1.cs` |
| 鼠标点击处理 | `Form1_MouseClick` | `Form1.cs` |
| DDA 算法 | `drawLine.cs - DDALine` | `drawLine.cs` |
| 中点算法 | `drawLine.cs - MidLine` | `drawLine.cs` |
| Bresenham 画圆 | `drawCircle.cs - Bresenham_Circle` | `drawCircle.cs` |
| 扫描线填充 | `FillWay.cs - ScanLineFill` | `FillWay.cs` + `Edge.cs` |
| Cohen-Sutherland | `Cut.cs - CohenCut1 / encode` | `Cut.cs` |
| 中点分割 | `Cut.cs - MidCut1 / FindNearestPoint` | `Cut.cs` |
| 梁友栋-Barsky | `Cut.cs - LiangCut1` | `Cut.cs` |
| Sutherland-Hodgman | `Cut.cs - WindowCut1 / EdgeClipping` | `Cut.cs` |
| 二维图形变换 | `TransMove_Click` / `TransRotate_Click` / `TransScale_Click` | `Form1.cs` + `MyForm.cs` |

## 算法要点

### 1. DDA 直线算法（`drawLine.DDALine`）

先按斜率把直线归入四个对称区间（`flag = 1~4`），再做坐标变换，把问题统一到
0°~45° 区间：步长取坐标增量较大的一轴，另一轴按斜率累加并四舍五入。

$$m=\frac{y_1-y_0}{x_1-x_0},\qquad x_{k+1}=x_k+1,\quad y_{k+1}=y_k+m$$

四种变换依次为：不改（0~45°）、关于 X 轴对称（Y 取负）、关于 $y=x$ 对称（交换 x 与 y）、
关于 $y=-x$ 对称（X 取负）。垂直线与水平线单独分支处理。

### 2. 中点直线算法（`drawLine.MidLine`）

在 0°~45° 区间上每步 $x$ 加 1，用判别式 $d$ 决定 $y$ 是否加 1，全程整数运算：

$$d_0=\Delta x-2\Delta y,\qquad
\begin{cases}
d>0: & d \leftarrow d-2\Delta y \quad (\text{y 不变})\\
d\le 0: & y\leftarrow y+1,\ d \leftarrow d-2(\Delta y-\Delta x)
\end{cases}$$

### 3. Bresenham 画圆（`drawCircle.Bresenham_Circle`）

半径取圆心到第二点的距离四舍五入，判别式 $d_0=3-2r$，
循环绘制八分圆并作八向对称映射，当 $x>y$ 时结束：

$$\begin{cases}
d<0: & d\leftarrow d+4x+6 \quad (\text{取正右方点})\\
d\ge 0: & y\leftarrow y-1,\ d\leftarrow d+4(x-y)+10 \quad (\text{取右下方点})
\end{cases}$$

### 4. 扫描线填充（`FillWay.ScanLineFill`）

逐条扫描线求出与各非水平边的交点，排序后两两配对，在每对交点之间画一条水平线段：

$$
\text{边结构 }(Y_{\min},Y_{\max},X_{\min},K=1/k),\qquad
\text{更新：}X_{\min}\leftarrow X_{\min}+\frac{1}{k}
$$

配对段右端取 `FirstX - 1`，避免与下一条扫描线的起点重复覆盖。

### 5. 二维裁剪（`Cut.cs`）

| 算法 | 思路 |
| --- | --- |
| Cohen-Sutherland | 窗口把平面分成 9 区、4 位编码（1 左 / 2 右 / 4 下 / 8 上）；`code1 & code2 ≠ 0` 直接舍弃，`code1 | code2 == 0` 直接接受，否则按编码确定求交边、用交点替换窗口外端点后重编码 |
| 中点分割 | 从一端出发不断二分，中点在外侧就抛弃外侧半段，逐步逼近离端点最近的窗口内可见点；两个方向各求一次，两点之间即可见段 |
| 梁友栋-Barsky | 线段参数化 $t\in[0,1]$，四个边界条件化为 $t$ 的区间约束，取 $t_{start}=\max$、$t_{end}=\min$，若 $t_{start}<t_{end}$ 则代入得裁剪后端点 |
| Sutherland-Hodgman | 用窗口的左 → 上 → 右 → 下四条边依次裁剪多边形，每次按「内内输出后点、内外输出交点、外内输出交点+后点、外外不输出」规则生成新顶点表，流水线处理完即结果 |

### 6. 二维图形变换（`Form1.cs`）

平移直接对 `pointsgroup` 的四个顶点加位移量；旋转与缩放都用
「平移 → 旋转/缩放 → 反向平移」的复合矩阵实现绕任意点 $(100,100)$ 的变换：

```csharp
Matrix myMatrix = new Matrix();
myMatrix.Translate(-100, -100);              // 把变换中心移到原点
myMatrix.Rotate(angle, MatrixOrder.Append);  // 或 Scale(xs, ys, ...)
myMatrix.Translate(100, 100, MatrixOrder.Append);
g.Transform = myMatrix;
g.DrawPolygon(Pens.Blue, pointsgroup);
```

### 7. 投影与消隐（`Project.cs` / `HiddenSurface.cs`）

三视图为正投影：正方体分别向三个坐标面投影，按「长对正、高平齐」布置主视图、俯视图、左视图。
消隐用正等测（轴测）投影 + 背面剔除：绕 z 轴旋转 45°、绕 x 轴旋转 35.264° 后正交投影，
再由面法向量与视线方向的夹角判断面是否可见，只绘制可见面的边框，
被挡住的棱自然不会被画出。

## 实现口径与差异说明

对本实现在细节上做了如下处理，均已核实：

1. **`MidLine` 的区间判据（已修正）**。原实现的代码中，`flag = 2` 的判据写作
   `x1 - x0 > y0 - y1 && y1 - y0 >= 0`、`flag = 4` 写作 `y1 - y0 > x0 - x1`，
   与同一算法中 `DDALine` 的写法（`y0 - y1 > 0` / `y0 - y1 > x1 - x0`）及注释
   「关于 X 轴对称，Y 加负号」不自洽：按原写法逐字实现时，只有水平线与垂直线能画出来，
   斜线要么画成水平线、要么完全不画。参考图中确有斜线被画出，
   因此判定为笔误，本仓库按与 DDA 一致的四向对称判据实现。
2. **`Edge.XMin` 取浮点型**。原实现未给出该字段类型，但算法描述为
   「更新 AEL 中每条边的 xmin：xmin = xmin + 1/k」，$1/k$ 通常不是整数，
   故取 `float`；若取 `int` 会因逐行截断而破坏填充形状。
3. **`MidCut1` 保持原有写法**，因此点击后**不会出现黄色可见段**——这与参考图一致
   （图中只有红线与窗口框，没有黄线）。原因见下节「已知问题」。
4. **裁剪窗口颜色**取灰色。参考图中窗口颜色不易辨认，
   无法确定原程序的实际取值，这里统一用中性灰，便于区分窗口与待裁剪线段。
5. **投影 / 消隐为扩展实现**。平台设计之初提到「设计多级菜单系统以调用不同算法」，
   菜单栏中确有「投影」「消隐」两项，但没有对应的算法代码，
   本仓库按经典做法给出可运行的实现（三视图正投影、正等测 + 背面剔除）。
6. **状态栏为补充**。早期版本的窗体没有状态栏，这里增加一行状态栏用于提示
   当前算法的鼠标操作方式（尤其是「右键结束多边形」），不影响任何绘图逻辑。
7. 其余代码（`DDALine`、`Bresenham_Circle`、`ScanLineFill`、四种裁剪、`TransScale_Click`
   等）与原写法逐字一致。裁剪窗口初始值沿用
   `(100,100) (200,100) (200,200) (100,200)`。

## 已知问题（保留原有写法，未作改动）

- **中点分割裁剪不显示可见段**。`FindNearestPoint` 二分结束后，
  `x1` 记录的是最后一个窗口**外**的点、`x2` 才是窗口**内**的点，
  但该处返回的是 `if (!PointIsOutOfWindow(x1, y1)) p = (x2, y2); else p = (x1, y1);`，
  在 `x1` 位于窗口外时返回了窗口外的点，随后 `MidCut1` 中的
  `if (PointIsOutOfWindow(p1.X, p1.Y, pointsgroup)) return;` 直接返回，故没有黄色结果。
  如需修复，把该分支改为「返回窗口内的那个点」即可（例如
  `if (!PointIsOutOfWindow(x2, y2, pointsgroup)) { p.X = x2; p.Y = y2; } else { p.X = x1; p.Y = y1; }`）。
- **图形不响应重绘**。所有绘制都通过 `CreateGraphics()` 直接画在窗体上，
  没有在 `OnPaint` 中重放，窗体被遮挡、最小化或改变大小后图形不会自动恢复。
- **扫描线填充的扫描范围**。`ScanLineFill` 中 `yu` 初值为 0、`yd` 初值为 1024，
  多边形顶点 y 坐标超过 1024 时会漏填。
- **多边形顶点上限 100**（`group` 数组长度）。
- 菜单项「投影」「消隐」缺少现成算法代码，本仓库为扩展实现。

## 关键函数汇总

| 序号 | 类名 | 函数名 | 输入参数 | 输出参数 | 主要功能描述 |
| --- | --- | --- | --- | --- | --- |
| 1 | `drawLine` | `DDALine` | g, x0, y0, x1, y1 | — | DDA 直线生成（步长 + 斜率累加） |
| 2 | `drawLine` | `MidLine` | g, x0, y0, x1, y1 | — | 中点直线生成（整数判别式） |
| 3 | `drawCircle` | `Bresenham_Circle` | g, x0, y0, x1, y1 | — | Bresenham 画圆 + 八向对称 |
| 4 | `Edge` | `Edge` | xLo, yLo, xHi, yHi | — | 建立边结构（YMin/YMax/XMin/K） |
| 5 | `FillWay` | `ScanLineFill` | g, group, PressNum, FirstX, FirstY | — | 扫描线填充多边形 |
| 6 | `Cut` | `encode` | x, y, pointsgroup | 4 位区域编码 | Cohen-Sutherland 区域编码 |
| 7 | `Cut` | `CohenCut1` | x1, y1, x2, y2, pointsgroup | — | Cohen-Sutherland 直线裁剪 |
| 8 | `Cut` | `MidCut1` | x1, y1, x2, y2, pointsgroup | — | 中点分割裁剪 |
| 9 | `Cut` | `FindNearestPoint` | x1, y1, x2, y2, pointsgroup | 可见端点 | 二分逼近最近的窗口内可见点 |
| 10 | `Cut` | `LiangCut1` | x1, y1, x2, y2, pointsgroup | — | 梁友栋-Barsky 裁剪 |
| 11 | `Cut` | `WindowCut1` | g, group, PressNum, FirstX, FirstY, pointsgroup | — | Sutherland-Hodgman 多边形裁剪 |
| 12 | `Cut` | `EdgeClipping` | linecode, group, pointsgroup | — | 用一条窗口边界裁剪多边形 |
| 13 | `Project` | `ThreeView` | — | — | 正方体三视图正投影 |
| 14 | `HiddenSurface` | `HiddenLine` | — | — | 正等测投影 + 背面剔除消隐 |
| 15 | `Form1` | `Form1_MouseMove` | sender, e | — | 橡皮筋实时预览 |
| 16 | `Form1` | `Form1_MouseClick` | sender, e | — | 按 MenuID 分派取点与绘图 |
| 17 | `MyForm` | `Xscale` / `Yscale` | — | float | 缩放系数的只读访问器 |

## 验证说明

本机无法编译 C#（`csc.exe` 被安全策略拦截、未安装 .NET SDK），因此采用以下等效验证手段：

1. **逐行等价翻译校验**：用 Python 把 `DDALine`、`MidLine`、`Bresenham_Circle`、`ScanLineFill`、
   四种裁剪与消隐算法按 C# 源码逐行翻译（对齐整数除法的向零截断、`(int)(x+0.5)` 的取整、
   LINQ `OrderBy` 的稳定排序等细节），渲染成 PNG 后与各算法的预期图形逐项比对：
   直线四种斜率、圆、填充、Cohen/梁友栋/Sutherland-Hodgman 的黄色裁剪结果均正确，
   中点分割确实无黄色结果（与已知问题一节所述一致），消隐得到正确的正等测立方体（9 条可见棱）。
2. **静态一致性检查**：csproj 收录文件与磁盘文件一一对应；Designer 中注册的 16 个事件处理器
   全部已实现；两个设计器文件引用的 26 个控件字段全部已声明；跨类调用的 10 个算法方法全部存在。

仍建议在 VS 2022 中打开解决方案实跑一遍，确认交互体验。
