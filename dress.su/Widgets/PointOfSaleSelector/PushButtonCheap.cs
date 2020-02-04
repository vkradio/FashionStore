using System.Drawing;
using System.Windows.Forms;

using dress.su.domain.Model;

namespace dress.su.Widgets.PointOfSaleSelector
{
    class PushButtonCheap: Control
    {
        bool        _active;
        PointOfSale _pointOfSale;

        public PushButtonCheap(Font in_font, PointOfSale in_pointOfSale)
        {
            // TODO: Подделать ситуацию с "большим" шрифтом, чтобы он вмещался в высоту кнопки.
            this.Size = new Size(100, SystemInformation.CaptionButtonSize.Height + 10);
            this.Font = in_font;

            _pointOfSale = in_pointOfSale;
            this.Text = _pointOfSale.Name;
        }

        protected override void OnPaint(PaintEventArgs in_pea)
        {
            base.OnPaint(in_pea);

            in_pea.Graphics.FillRectangle(_active ? new SolidBrush(Color.Yellow) : SystemBrushes.Control, 0, 0, Width - 1, Height - 1);
            in_pea.Graphics.DrawRectangle(Pens.Black, 0, 0, Width - 1, Height - 1);
            StringFormat strfmt = StringFormat.GenericDefault;
            strfmt.LineAlignment = StringAlignment.Center;
            strfmt.Alignment = StringAlignment.Center;
            in_pea.Graphics.DrawString(Text, Font, SystemBrushes.ControlText, Width / 2, Height / 2, strfmt);
        }

        public bool Active { get { return _active; } set { _active = value; Invalidate(); } }
        public PointOfSale PointOfSale { get { return _pointOfSale; } set { _pointOfSale = value; Invalidate(); } }
    };
}
