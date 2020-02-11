using System;
using System.Drawing;
using System.Windows.Forms;

using ApplicationCore.Entities;

namespace FashionStoreWinForms.Widgets.Net
{
    public partial class NetPanel : UserControl
    {
        const int c_fieldWidth          = 35;
        const int c_gap                 = 6;
        const int c_height              = 20;
        const int c_labelWidthThreshold = 7;

        DressMatrix _netType;
        int[]       _widthCapX;
        int[]       _widthCapY;
        int         _totalWidth;
        int         _totalHeight;
        Color       _textBoxBackColor = TextBox.DefaultBackColor;

        void SwitchLabelFont(Label in_label, bool in_toBold)
        {
            in_label.Font = new Font(in_label.Font, in_toBold ? FontStyle.Bold | FontStyle.Underline : FontStyle.Regular);
        }
        void OnValueChange()
        {
            if (ValueChanged != null)
                ValueChanged(this, null);
        }
        void CellTextChanged(TextBox in_textBox)
        {
            int value;
            if (int.TryParse(in_textBox.Text, out value))
            {
                if (value == 0)
                    in_textBox.BackColor = _textBoxBackColor;
                else if (value > 0)
                    in_textBox.BackColor = Color.LimeGreen;
                else
                    in_textBox.BackColor = Color.Red;
            }
            else if (in_textBox.Text == string.Empty)
                in_textBox.BackColor = _textBoxBackColor;
            else
                in_textBox.BackColor = Color.Red;

            OnValueChange();
        }
        void ActiveCellChanged(TextBox in_textBox)
        {
            NetUtil.Value cell = (NetUtil.Value)in_textBox.Tag;
            foreach (Control ctrl in Controls)
                if (ctrl is Label)
                {
                    Label label = (Label)ctrl;

                    if (ctrl.Tag is NetUtil.LabelValueX)
                    {
                        NetUtil.LabelValueX valueX = (NetUtil.LabelValueX)ctrl.Tag;
                        SwitchLabelFont(label, valueX.X == cell.X);
                    }
                    else
                    {
                        NetUtil.LabelValueY valueY = (NetUtil.LabelValueY)ctrl.Tag;
                        SwitchLabelFont(label, valueY.Y == cell.Y);
                    }
                }
        }

        public NetPanel()
        {
            InitializeComponent();
        }
        public NetPanel(DressMatrix in_netType) : this()
        {
            _netType = in_netType;

            Graphics g = CreateGraphics();
            
            // 1. Measure sizes.
            int maxWidthCapX, maxWidthCapY;
            NetUtil.MeasureNet(_netType, g, c_fieldWidth, out maxWidthCapX, out maxWidthCapY, out _widthCapX, out _widthCapY);

            // 2. Render labels.
            NetUtil.RenderLabels(_netType, this, maxWidthCapX, maxWidthCapY, _widthCapX, _widthCapY, c_labelWidthThreshold, c_height, c_gap, out _totalWidth, out _totalHeight);

            // 3. Render text labels of size chart.
            int xPos = maxWidthCapY + c_labelWidthThreshold * 2;
            for (int i = 0; i < _netType.CellsX.Count; i++)
            {
                int yPos = c_height + c_gap;
                for (int j = 0; j < _netType.CellsY.Count; j++)
                {
                    TextBox tbox = new TextBox()
                    {
                        Parent = this,
                        Location = new Point(xPos, yPos),
                        Size = new Size(c_fieldWidth, c_height),
                        Tag = new NetUtil.Value()
                        {
                            X = i,
                            Y = j,
                            NameX = _netType.CellsX[i],
                            NameY = _netType.CellsY[j]
                        }
                    };
                    tbox.TextChanged += (s, e) => CellTextChanged((TextBox)s);
                    tbox.GotFocus += (s, e) => ActiveCellChanged((TextBox)s);
                    yPos += (c_gap + c_height);
                }
                xPos += (c_gap + maxWidthCapX);
            }
            foreach (Control ctrl in Controls)
                if (ctrl is TextBox)
                {
                    _textBoxBackColor = ctrl.BackColor;
                    break;
                }

            g.Dispose();
        }

        public TextBox FindCellTextBox(int in_x, int in_y)
        {
            foreach (Control ctrl in Controls)
            {
                TextBox txtBox = ctrl as TextBox;
                if (txtBox != null)
                {
                    NetUtil.Value cell = (NetUtil.Value)txtBox.Tag;
                    if (cell.X == in_x && cell.Y == in_y)
                        return txtBox;
                }
            }
            return null;
        }

        public int TotalWidth { get { return _totalWidth; } }
        public int TotalHeight { get { return _totalHeight; } }
        public int this[string in_x, string in_y]
        {
            get
            {
                foreach (Control ctrl in Controls)
                {
                    TextBox txtBox = ctrl as TextBox;
                    if (txtBox != null)
                    {
                        NetUtil.Value cell = (NetUtil.Value)txtBox.Tag;
                        if (cell.NameX == in_x && cell.NameY == in_y)
                        {
                            int value;
                            if (int.TryParse(txtBox.Text, out value) && value >= 0)
                                return value;
                            else
                                return 0;
                        }
                    }
                }
                return 0;
            }
        }
        public int this[int in_x, int in_y]
        {
            get
            {
                TextBox txtBox = FindCellTextBox(in_x, in_y);

                int value;

                return int.TryParse(txtBox.Text, out value) && value >= 0 ?
                    value :
                    0;
            }
            set
            {
                FindCellTextBox(in_x, in_y).Text = value.ToString();
            }
        }
        public int TotalCount
        {
            get
            {
                int sum = 0;
                foreach (Control ctrl in Controls)
                {
                    TextBox txtBox = ctrl as TextBox;
                    if (txtBox != null)
                    {
                        NetUtil.Value cell = (NetUtil.Value)txtBox.Tag;
                        int value;
                        if (int.TryParse(txtBox.Text, out value) && value >= 0)
                            sum += value;
                    }
                }
                return sum;
            }
        }

        public EventHandler ValueChanged;
    }
}
