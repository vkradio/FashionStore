using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using dress.su.domain.Model;

namespace FashionStoreWinForms.Widgets.PointOfSaleSelector
{
    public partial class PointOfSalePanel : UserControl
    {
        public class PointOfSaleEventArgs: EventArgs
        {
            public PointOfSale PointOfSale { get; set; }
        };
        public class BeforePointOfSaleChangedEventArgs: EventArgs
        {
            bool _allowChange;

            public bool AllowChange { get { return _allowChange; } set { _allowChange = value; } }
        };

        List<PointOfSale> _pointsOfSale = new List<PointOfSale>();

        void OnPointOfSaleChange(PointOfSale in_selectedPointOfSale)
        {
            if (PointOfSaleChanged != null)
                PointOfSaleChanged(this, new PointOfSaleEventArgs() { PointOfSale = in_selectedPointOfSale });
        }
        bool CanPointOfSaleChange()
        {
            if (BeforePointOfSaleChanged != null)
            {
                BeforePointOfSaleChangedEventArgs args = new BeforePointOfSaleChangedEventArgs();
                BeforePointOfSaleChanged(this, args);
                return args.AllowChange;
            }
            return true;
        }

        void Button_Click(object in_sender, EventArgs in_ea)
        {
            bool changed = false;
            foreach (Control ctrl in Controls)
            {
                PushButtonCheap btn = ctrl as PushButtonCheap;
                if (btn != null && btn != in_sender && btn.Active)
                    changed = true;
            }

            if (changed)
            {
                bool canChange = CanPointOfSaleChange();
                if (canChange)
                {
                    foreach (Control ctrl in Controls)
                    {
                        PushButtonCheap btn = ctrl as PushButtonCheap;
                        if (btn != null)
                            btn.Active = false;
                    }

                    ((PushButtonCheap)in_sender).Active = true;
                    OnPointOfSaleChange((PointOfSale)((PushButtonCheap)in_sender).PointOfSale);
                }
            }
        }

        public PointOfSalePanel()
        {
            InitializeComponent();
        }

        public IList<PointOfSale> PointsOfSale { get { return _pointsOfSale; } }

        public void UpdatePointsOfSale()
        {
            Controls.Clear();

            float maxWidth = 40.0f;
            foreach (PointOfSale pos in _pointsOfSale)
            {
                PushButtonCheap posButton = new PushButtonCheap(this.Font, pos);
                posButton.Parent = this;
                posButton.Top = _paddingTop;

                Graphics g = this.CreateGraphics();
                SizeF size = g.MeasureString(pos.Name, posButton.Font);
                maxWidth = Math.Max(maxWidth, size.Width);
            }

            int posX = _paddingLeft;
            foreach (Control btn in Controls)
            {
                btn.Width = (int)maxWidth + 10;
                btn.Left = posX;
                posX += btn.Width + 10;
                btn.Click += (s, e) => Button_Click(s, e);
            }
            if (Controls.Count > 0)
                ((PushButtonCheap)Controls[0]).Active = true;
        }

        public int PaddingLeft
        {
            get { return _paddingLeft; }
            set
            {
                if (_paddingLeft != value)
                {
                    foreach (Control btn in Controls)
                        btn.Left += (value - _paddingLeft);
                    _paddingLeft = value;
                }
            }
        }
        public int PaddingTop
        {
            get { return _paddingTop; }
            set
            {
                if (_paddingTop != value)
                {
                    foreach (Control btn in Controls)
                        btn.Top += (value - _paddingTop);
                    _paddingTop = value;
                }
            }
        }

        public event EventHandler<PointOfSaleEventArgs> PointOfSaleChanged;
        public event EventHandler<BeforePointOfSaleChangedEventArgs> BeforePointOfSaleChanged;
    };
}
