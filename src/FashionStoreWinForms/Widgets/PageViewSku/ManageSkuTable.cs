using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using ApplicationCoreLegacy.Entities;
using FashionStoreWinForms.Sys;

namespace FashionStoreWinForms.Widgets.PageViewSku
{
    public partial class ManageSkuTable : UserControl
    {
        readonly DataTable   _source;
        readonly int         _itemsHeight;

        public ManageSkuTable()
        {
            InitializeComponent();
        }
        public ManageSkuTable(DataTable in_dataSource, int in_maxPositions) : this()
        {
            _source = in_dataSource;

            DataTable pointsOfSale = PointOfSale.ReadTable(Registry.CurrentPointOfSale.Id, true);
            
            int posY = PAN_Head.Height;
            int maxRows = in_maxPositions == 0 ? _source.Rows.Count : Math.Min(in_maxPositions, _source.Rows.Count);
            for (int i = 0; i < maxRows; i++)
            {
                ManageSkuMiniPanel item = new ManageSkuMiniPanel(_source.Rows[i], this, pointsOfSale)
                {
                    Location = new Point(0, posY)
                };
                posY += item.Height;
                _itemsHeight += item.Height;
            }
        }
        public ManageSkuTable(DataTable in_dataSource) : this(in_dataSource, 0) {}

        public int ItemsHeight { get { return _itemsHeight; } }
        public int TotalHeight { get { return PAN_Head.Height + _itemsHeight; } }

        public void UpdateHeight()
        {
            Height = TotalHeight;
        }
    }
}
