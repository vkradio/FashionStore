using System;
using System.Drawing;
using System.Windows.Forms;

using ApplicationCore.Entities;

namespace FashionStoreWinForms.Widgets.Net
{
    public partial class FRM_CellSelector : Form
    {
        class NetButton: UserControl
        {
            CellInStock _cell;
            int         _x;
            int         _y;

            public NetButton(Control in_parent, CellInStock in_cell, int in_cellX, int in_cellY, int in_buttonX, int in_buttonY, int in_width, int in_height)
            {
                _cell = in_cell;
                _x = in_cellX;
                _y = in_cellY;

                Parent = in_parent;
                Text = _cell.Amount.ToString();
                Location = new Point(in_buttonX, in_buttonY);
                Size = new Size(in_width, in_height);
            }

            protected override void OnClick(EventArgs e)
            {
                base.OnClick(e);

                if (_cell.Amount == 0)
                {
                    System.Media.SystemSounds.Exclamation.Play();
                }
                else
                {
                    ((FRM_CellSelector)Parent).SelectedCell = _cell;
                    ((FRM_CellSelector)Parent).DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
            protected override void OnMouseEnter(EventArgs e)
            {
                base.OnMouseEnter(e);

                BackColor = _cell.Amount == 0 ? Color.Red : Color.LimeGreen;
                ((FRM_CellSelector)Parent).HightlightCellCaptions(_x, _y);
            }
            protected override void OnMouseLeave(EventArgs e)
            {
                base.OnMouseLeave(e);

                BackColor = Control.DefaultBackColor;
                ((FRM_CellSelector)Parent).UnhightlightAllCellCaptions();
            }
            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                StringFormat fmt = StringFormat.GenericDefault;
                fmt.Alignment = StringAlignment.Center;
                fmt.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(Text, Control.DefaultFont, SystemBrushes.ControlText, Width / 2, Height / 2, fmt);
                if (_cell.Amount != 0)
                    e.Graphics.DrawRectangle(Pens.LimeGreen, 0, 0, Width - 1, Height - 1);
            }
        };

        const int c_fieldWidth          = 35;
        const int c_fieldHeight         = 20;
        const int c_fieldGap            = 6;
        const int c_labelWidthThreshold = 7;

        SkuInStock  _skuInStock;
        Label[]     _labelsX;
        Label[]     _labelsY;

        void FRM_CellSelector_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }

        public FRM_CellSelector()
        {
            InitializeComponent();
        }
        public FRM_CellSelector(SkuInStock in_skuInStock) : this()
        {
            SuspendLayout();

            _skuInStock = in_skuInStock;

            Graphics g = CreateGraphics();

            int maxWidthCapX, maxWidthCapY;
            int[] widthsCapX, widthsCapY;
            NetUtil.MeasureNet(_skuInStock.Article.Matrix, g, c_fieldWidth, out maxWidthCapX, out maxWidthCapY, out widthsCapX, out widthsCapY);

            int totalWidth, totalHeight;
            NetUtil.RenderLabels(_skuInStock.Article.Matrix, this, maxWidthCapX, maxWidthCapY, widthsCapX, widthsCapY, c_labelWidthThreshold, c_fieldHeight, c_fieldGap, out totalWidth, out totalHeight, out _labelsX, out _labelsY);

            // Drawing buttons.
            int xPos = maxWidthCapY + c_labelWidthThreshold * 2;
            int
                cellsXCount = _skuInStock.Article.Matrix.CellsX.Count,
                cellsYCount = _skuInStock.Article.Matrix.CellsY.Count;
            for (int i = 0; i < cellsXCount; i++)
            {
                int yPos = c_fieldHeight + c_fieldGap;
                for (int j = 0; j < cellsYCount; j++)
                {
                    new NetButton(this, _skuInStock[i, j], i, j, xPos, yPos, c_fieldWidth, c_fieldHeight);
                    yPos += (c_fieldGap + c_fieldHeight);
                }
                xPos += (c_fieldGap + maxWidthCapX);
            }

            Width = totalWidth;
            Height = totalHeight;

            g.Dispose();

            ResumeLayout();
        }

        public CellInStock SelectedCell { get; private set; }
        public void HightlightCellCaptions(int in_x, int in_y)
        {
            System.Drawing.Font newFont = new System.Drawing.Font(_labelsX[0].Font, FontStyle.Bold | FontStyle.Underline);
            _labelsX[in_x].Font = newFont;
            _labelsY[in_y].Font = newFont;
        }
        public void UnhightlightAllCellCaptions()
        {
            System.Drawing.Font newFont = new Font(_labelsX[0].Font, FontStyle.Regular);
            foreach (Label label in _labelsX)
                label.Font = newFont;
            foreach (Label label in _labelsY)
                label.Font = newFont;
        }
    }
}
