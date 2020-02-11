using System;
using System.Drawing;
using System.Windows.Forms;

using ApplicationCore.Entities;

namespace FashionStoreWinForms.Widgets.Net
{
    static class NetUtil
    {
        public struct Value
        {
            public int X;
            public int Y;
            public string NameX;
            public string NameY;
        };
        public struct LabelValueX
        {
            public int X;
            public string NameX;
        };
        public struct LabelValueY
        {
            public int Y;
            public string NameY;
        };

        public static void MeasureNet(DressMatrix in_netType, Graphics in_graphics, int in_fieldWidth, out int out_maxWidthCapX, out int out_maxWidthCapY, out int[] out_widthCapX, out int[] out_widthCapY)
        {
            // 1. Measure the width of the leftmost column with captions by Y.
            out_maxWidthCapY = 0;
            out_widthCapY = new int[in_netType.CellsY.Count];
            for (int i = 0; i < in_netType.CellsY.Count; i++)
            {
                int width = (int)in_graphics.MeasureString(in_netType.CellsY[i], Label.DefaultFont).Width;
                out_widthCapY[i] = width;
                out_maxWidthCapY = Math.Max(out_maxWidthCapY, width);
            }

            // 2. Measure the width of every other column, taking the widthest caption by X, or
            //    the standard TextBox width.
            out_maxWidthCapX = 0;
            out_widthCapX = new int[in_netType.CellsX.Count];
            for (int i = 0; i < in_netType.CellsX.Count; i++)
            {
                int width = (int)in_graphics.MeasureString(in_netType.CellsX[i], Label.DefaultFont).Width;
                out_widthCapX[i] = width;
                out_maxWidthCapX = Math.Max(out_maxWidthCapX, width);
            }
            out_maxWidthCapX = Math.Max(out_maxWidthCapX, in_fieldWidth);
        }

        public static void RenderLabels(DressMatrix in_netType, Control in_container, int in_maxWidthCapX, int in_maxWidthCapY, int[] in_widthCapX, int[] in_widthCapY, int in_labelWidthThreshold, int in_fieldHeight, int in_fieldGap, out int out_totalWidth, out int out_totalHeight, out Label[] out_labelsX, out Label[] out_labelsY)
        {
            int labelHeight;
            using (Graphics g = in_container.CreateGraphics())
                labelHeight = (int)g.MeasureString("A", Label.DefaultFont).Height;

            out_labelsX = new Label[in_netType.CellsX.Count];
            out_labelsY = new Label[in_netType.CellsY.Count];

            int xPos = in_maxWidthCapY + in_labelWidthThreshold;
            for (int i = 0; i < in_netType.CellsX.Count; i++)
            {
                out_labelsX[i] = new Label()
                {
                    Text = in_netType.CellsX[i],
                    Parent = in_container,
                    Location = new Point(xPos + in_maxWidthCapX / 2 - in_widthCapX[i] / 2, in_fieldGap),
                    TextAlign = ContentAlignment.TopCenter,
                    AutoSize = true,
                    Tag = new LabelValueX() { X = i, NameX = in_netType.CellsX[i] }
                };
                xPos += (in_fieldGap + in_maxWidthCapX);
            }
            out_totalWidth = xPos + in_fieldGap; // + in_maxWidthCapX + in_labelWidthThreshold;

            int yPos = in_fieldHeight + in_fieldGap + in_fieldHeight / 2 - labelHeight / 2;
            for (int i = 0; i < in_netType.CellsY.Count; i++)
            {
                out_labelsY[i] = new Label()
                {
                    Text = in_netType.CellsY[i],
                    Parent = in_container,
                    Location = new Point(in_fieldGap + in_maxWidthCapY / 2 - in_widthCapY[i] / 2, yPos),
                    TextAlign = ContentAlignment.TopCenter,
                    AutoSize = true,
                    Tag = new LabelValueY() { Y = i, NameY = in_netType.CellsY[i] }
                };
                yPos += (in_fieldGap + in_fieldHeight);
            }
            out_totalHeight = yPos + in_fieldGap * 2 + in_fieldHeight;
        }
        public static void RenderLabels(DressMatrix in_netType, Control in_container, int in_maxWidthCapX, int in_maxWidthCapY, int[] in_widthCapX, int[] in_widthCapY, int in_labelWidthThreshold, int in_fieldHeight, int in_fieldGap, out int out_totalWidth, out int out_totalHeight)
        {
            Label[] labelsXStub, labelsYStub;
            RenderLabels(in_netType, in_container, in_maxWidthCapX, in_maxWidthCapY, in_widthCapX, in_widthCapY, in_labelWidthThreshold, in_fieldHeight, in_fieldGap, out out_totalWidth, out out_totalHeight, out labelsXStub, out labelsYStub);
        }
    };
}
