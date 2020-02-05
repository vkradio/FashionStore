using ApplicationCore.Entities;

namespace FashionStoreWinForms.Widgets.PointOfSaleSelector
{
    partial class PointOfSalePanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        PushButtonCheap _demoButton;
        int                 _paddingTop     = 10;
        int                 _paddingLeft    = 10;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // PointOfSalePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "PointOfSalePanel";
            this.Size = new System.Drawing.Size(377, 51);

            this._paddingLeft = 10;
            this._paddingTop = 10;

            this._demoButton = new PushButtonCheap(this.Font, new PointOfSale() { Name = "Кнопка" });
            this._demoButton.Parent = this;
            this._demoButton.Left = _paddingLeft;
            this._demoButton.Top = _paddingTop;

            this.ResumeLayout(false);

        }

        #endregion
    }
}
