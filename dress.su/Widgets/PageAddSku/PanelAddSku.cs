﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using dress.su.domain.Model;
using dress.su.Forms;
using dress.su.Sys;
using dress.su.Widgets.Net;
using MainLibrary.database.orm;

namespace dress.su.Widgets.PageAddSku
{
    public partial class PanelAddSku : UserControl
    {
        const string c_errPartPrice                 = " Допустимо только целое неотрицательное число в рублях.";
        const string c_errPartCellAmount            = " Допустимо только положительное целое число.";
        const string c_errInvalidPriceOfPurchase    = "Неверное значение цены закупки." + c_errPartPrice;
        const string c_errInvalidPriceOfSell        = "Неверное значение плановой цены продажи." + c_errPartPrice;
        const string c_errInvalidAmount             = "Задано неверное количество товара." + c_errPartCellAmount;
        const string c_errInvalidCellAmount         = "Задано неверное количество товара в ячейке {0}:{1}." + c_errPartCellAmount;
        const string c_msgSaved                     = "Товар сохранен.";

        int         _oldNetTypeIndex    = -1;
        int         _defaultHeight;
        int         _defaultWidth;
        bool        _modified;
        
        Article     _article;
        SkuInStock  _skuInStock;

        bool TryParsePrices(out int out_priceOfPurchase, out int out_priceOfSell)
        {
            out_priceOfPurchase = out_priceOfSell = 0;
            if (!int.TryParse(T_PriceOfPurchase.Text, out out_priceOfPurchase) || out_priceOfPurchase < 0)
            {
                T_PriceOfPurchase.Focus();
                MessageBox.Show(this, c_errInvalidPriceOfPurchase, "Отказ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (!int.TryParse(T_PriceOfSell.Text, out out_priceOfSell) || out_priceOfSell < 0)
            {
                T_PriceOfSell.Focus();
                MessageBox.Show(this, c_errInvalidPriceOfSell, "Отказ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true;
        }
        bool CreateNewArticle()
        {
            int priceOfPurchase, priceOfSell;
            bool success = TryParsePrices(out priceOfPurchase, out priceOfSell);
            if (!success)
                return false;

            _article = new Article()
            {
                Name = T_Name.Text.Trim(),
                Matrix = DressMatrix.Restore(((LiteBizItem)CB_NetType.SelectedItem).Value),
                PriceOfPurchase = priceOfPurchase,
                PriceOfSell = priceOfSell
            };
            _article.Flush();
            _skuInStock = SkuInStock.CreateNew(_article, Registry.CurrentPointOfSale);

            return true;
        }
        bool UpdateArticle()
        {
            int priceOfPurchase, priceOfSell;
            bool success = TryParsePrices(out priceOfPurchase, out priceOfSell);
            if (!success)
                return false;

            _article.PriceOfPurchase = priceOfPurchase;
            _article.PriceOfSell = priceOfSell;
            _article.Flush();

            return true;
        }
        bool SaveCells()
        {
            string err = FormToNet();

            if (err != null)
            {
                MessageBox.Show(this, err, "Отказ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            _skuInStock.Flush();
            return true;
        }
        void NetToForm()
        {
            T_PriceOfPurchase.Text = _article.PriceOfPurchase.ToString();
            T_PriceOfSell.Text = _article.PriceOfSell.ToString();
            CB_NetType.SelectBizItem(_article.Matrix.Id);

            RedrawNet(_skuInStock.Article.Matrix);

            if (_skuInStock.Article.Matrix.CellsX.Count == 1 && _skuInStock.Article.Matrix.CellsY.Count == 1)
                T_Amount.Text = _skuInStock[0, 0].ToString();
            else
            {
                NetPanel netPan = (NetPanel)PAN_Net.Controls[0];

                for (int i = 0; i < _skuInStock.Article.Matrix.CellsX.Count; i++)
                    for (int j = 0; j < _skuInStock.Article.Matrix.CellsY.Count; j++)
                        netPan[i, j] = _skuInStock[i, j].Amount;
            }
        }
        string TextBoxToCell(TextBox in_textBox, CellInStock in_cell)
        {
            if (in_textBox.Text == string.Empty)
            {
                in_cell.Amount = 0;
                return null;
            }

            int value;
            if (!int.TryParse(in_textBox.Text, out value) || value < 0)
                return string.Format(c_errInvalidCellAmount, in_cell.X, in_cell.Y);
            else
            {
                in_cell.Amount = value;
                return null;
            }
        }
        string FormToNet()
        {
            string err;

            if (_skuInStock.Article.Matrix.CellsX.Count == 1 && _skuInStock.Article.Matrix.CellsY.Count == 1)
            {
                err = TextBoxToCell(T_Amount, _skuInStock[0, 0]);
                if (err != null)
                {
                    T_Amount.Focus();
                    return c_errInvalidAmount;
                }
            }
            else
            {
                NetPanel netPan = (NetPanel)PAN_Net.Controls[0];

                for (int i = 0; i < _skuInStock.Article.Matrix.CellsX.Count; i++)
                    for (int j = 0; j < _skuInStock.Article.Matrix.CellsY.Count; j++)
                    {
                        TextBox txtBox = netPan.FindCellTextBox(i, j);
                        err = TextBoxToCell(txtBox, _skuInStock[i, j]);
                        if (err != null)
                        {
                            txtBox.Focus();
                            return err;
                        }
                    }
            }

            return null;
        }
        void OnNetValueChange()
        {
            _modified = true;
            T_Amount.Text = ((NetPanel)PAN_Net.Controls[0]).TotalCount.ToString();
        }
        void RedrawNet(DressMatrix in_netType)
        {
            PAN_Net.Controls.Clear();
            T_Amount.Text = string.Empty;
            if (in_netType.CellsX.Count == 1 && in_netType.CellsY.Count == 1)
            {
                T_Amount.ReadOnly = false;
                Width = _defaultWidth;
                Height = _defaultHeight;
            }
            else
            {
                T_Amount.ReadOnly = true;
                NetPanel net = new NetPanel(in_netType) { Parent = PAN_Net };
                Width = Math.Max(_defaultWidth, net.TotalWidth);
                Height = Math.Max(_defaultHeight, net.TotalHeight + PAN_SkuWONet.Height);
                net.Size = new System.Drawing.Size(Math.Min(net.TotalHeight, PAN_Net.Width), PAN_Net.Height);
                if (_defaultWidth > net.TotalWidth)
                    net.Location = new System.Drawing.Point(_defaultWidth / 2 - net.TotalWidth / 2, 0);
                net.ValueChanged += (s, e) => OnNetValueChange();
            }
        }
        void RedrawNet()
        {
            DressMatrix mtx = DressMatrix.Restore(((LiteBizItem)CB_NetType.SelectedItem).Value);
            RedrawNet(mtx);
        }
        void TryUpdateMargin()
        {
            int priceOfPurchase, priceOfSale;
            if (int.TryParse(T_PriceOfPurchase.Text, out priceOfPurchase) &&
                int.TryParse(T_PriceOfSell.Text, out priceOfSale))
            {
                T_Margin.Text = (priceOfSale - priceOfPurchase).ToString();
            }
        }

        void LU_Search_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GlobalController.SearchSkuAction();
        }
        void T_PriceOfPurchase_TextChanged(object sender, EventArgs e)
        {
            _modified = true;
            TryUpdateMargin();
        }
        void T_PriceOfSell_TextChanged(object sender, EventArgs e)
        {
            _modified = true;
            TryUpdateMargin();
        }
        void T_Name_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                B_Search_Click(sender, null);
        }
        /// <summary>
        /// Выбор типа сетки.
        /// <remarks>Доступен только для вновь добавляемого артикула.</remarks>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CB_NetType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CB_NetType.SelectedIndex == _oldNetTypeIndex)
                return;

            RedrawNet();
            _modified = true;

            _oldNetTypeIndex = CB_NetType.SelectedIndex;
        }
        /// <summary>
        /// Поиск артикула.
        /// <remarks>Форма переключается в состояние редактирования товара на складе.</remarks>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void B_Search_Click(object sender, EventArgs e)
        {
            bool canSearch = true;
            
            if (string.IsNullOrWhiteSpace(T_Name.Text))
            {
                MessageBox.Show(this, "Не задано наименование.", "Отказ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                T_Name.Focus();
                return;
            }

            Cursor oldCursor = this.Cursor;
            try
            {
                this.Cursor = Cursors.WaitCursor;
                B_Search.Enabled = false;
                Application.DoEvents();

                List<Article> articles = Article.RestoreByName(T_Name.Text.Trim());
                if (articles.Count == 0)
                {
                    _article = null;

                    // Спрашиваем о начале создания нового артикула.
                    if (MessageBox.Show(this, "Такого артикула нет. Начать оформление нового с таким наименованием?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _modified = true;

                        T_Name.ReadOnly = true;
                        B_Search.Enabled = false;
                        canSearch = false;
                        PAN_SkuParams.Enabled = true;
                        T_Amount.Focus();
                    }
                    else
                    {
                        T_Name.Focus();
                        return;
                    }
                }
                //else if (articles.Count > 1)
                //{
                //    // Выводим форму выбора артикулов по закупочной цене.
                //}
                else
                {
                    using (FRM_SelectArticleByPriceOfPurchase frm = new FRM_SelectArticleByPriceOfPurchase(articles))
                    {
                        if (frm.ShowDialog(this) != DialogResult.OK)
                        {
                            T_Name.Focus();
                            return;
                        }

                        _modified = true;

                        if (frm.NewPriceEntered)
                        {
                            _article = new Article()
                            {
                                Name = articles[0].Name,
                                PriceOfPurchase = frm.NewPrice,
                                Matrix = articles[0].Matrix
                            };
                        }
                        else
                        {
                            _article = frm.SelectedArticle;
                        }

                        // Выводим его сетку и даем пользователю возможность заполнять.
                        T_Name.ReadOnly = true;
                        T_PriceOfPurchase.ReadOnly = true;
                        B_Search.Enabled = false;
                        canSearch = false;
                        B_Reset.Enabled = false;
                        PAN_SkuParams.Enabled = true;

                        CB_NetType.SelectBizItem(_article.Matrix.Id);
                        CB_NetType.Enabled = false;

                        _skuInStock = SkuInStock.Restore(_article, Registry.CurrentPointOfSale);
                        NetToForm();
                        if (_skuInStock.Article.Matrix.CellsX.Count == 1 && _skuInStock.Article.Matrix.CellsY.Count == 1)
                            T_Amount.Focus();
                        else
                            T_PriceOfPurchase.Focus();
                    }
                }
            }
            finally
            {
                B_Search.Enabled = canSearch;
                this.Cursor = oldCursor;
            }
        }
        /// <summary>
        /// Сохранение введенных данных.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void B_Save_Click(object sender, EventArgs e)
        {
            bool success;
            try
            {
                B_Save.Enabled = false;
                Application.DoEvents();

                if (_article == null || _article.Id == 0)
                    success = CreateNewArticle();
                else
                    success = UpdateArticle();
                if (!success)
                    return;

                if (SaveCells())
                {
                    B_Reset.Enabled = false;
                    _modified = false;

                    MessageBox.Show(this, c_msgSaved, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            finally
            {
                B_Save.Enabled = true;
            }
        }
        /// <summary>
        /// Возврат формы к состоянию поиска артикула.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void B_Reset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Сбросить все введенные значения?", "Вопрос", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            _article = null;
            _skuInStock = null;
            foreach (Control ctrl in PAN_SkuParams.Controls)
            {
                TextBox txt = ctrl as TextBox;
                if (txt != null)
                {
                    txt.Text = string.Empty;
                    continue;
                }
                ComboBox cb = ctrl as ComboBox;
                if (cb != null)
                {
                    if (cb.Items.Count != 0)
                        cb.SelectedIndex = 0;
                    continue;
                }
            }

            PAN_SkuParams.Enabled = false;
            T_Name.ReadOnly = false;
            B_Search.Enabled = true;
            T_PriceOfPurchase.ReadOnly = false;

            _modified = false;
        }

        public PanelAddSku()
        {
            InitializeComponent();

            PAN_SkuParams.Enabled = false;

            _defaultWidth = Width;
            _defaultHeight = Height;

            LB_Sku.Text = string.Format("Товар в наличии ({0})", Registry.CurrentPointOfSale.Name);
            LU_Search.Text = string.Format("Нажмите сюда, чтобы посмотреть товар, продать или перебросить ({0})", Registry.CurrentPointOfSale.Name);

            Width = Math.Max(Math.Max(LB_Sku.Width, LU_Search.Width), Width);

            CB_NetType.Items.AddRange(LiteBizItem.FromTable(DressMatrix.ReadAll()).ToArray<LiteBizItem>());
            if (CB_NetType.Items.Count != 0)
                CB_NetType.SelectedIndex = 0;

            _modified = false;
        }

        public TextBox T_NameExt { get { return T_Name; } }
        public bool Modified { get { return _modified; } }
    }
}
