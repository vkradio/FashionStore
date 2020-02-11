namespace FashionStoreWinForms.Widgets.PageViewSku
{
    partial class PanelViewSku
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PanelViewSku));
            this.PAN_Search = new System.Windows.Forms.Panel();
            this.B_Search = new System.Windows.Forms.Button();
            this.T_NamePart = new System.Windows.Forms.TextBox();
            this.PAN_Lists = new System.Windows.Forms.Panel();
            this.PAN_Search.SuspendLayout();
            this.SuspendLayout();
            // 
            // PAN_Search
            // 
            resources.ApplyResources(this.PAN_Search, "PAN_Search");
            this.PAN_Search.Controls.Add(this.B_Search);
            this.PAN_Search.Controls.Add(this.T_NamePart);
            this.PAN_Search.Name = "PAN_Search";
            // 
            // B_Search
            // 
            resources.ApplyResources(this.B_Search, "B_Search");
            this.B_Search.Name = "B_Search";
            this.B_Search.UseVisualStyleBackColor = true;
            this.B_Search.Click += new System.EventHandler(this.B_Search_Click);
            // 
            // T_NamePart
            // 
            resources.ApplyResources(this.T_NamePart, "T_NamePart");
            this.T_NamePart.Name = "T_NamePart";
            this.T_NamePart.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.T_NamePart_PreviewKeyDown);
            // 
            // PAN_Lists
            // 
            resources.ApplyResources(this.PAN_Lists, "PAN_Lists");
            this.PAN_Lists.Name = "PAN_Lists";
            // 
            // PanelViewSku
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PAN_Lists);
            this.Controls.Add(this.PAN_Search);
            this.Name = "PanelViewSku";
            this.PAN_Search.ResumeLayout(false);
            this.PAN_Search.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PAN_Search;
        private System.Windows.Forms.Panel PAN_Lists;
        private System.Windows.Forms.Button B_Search;
        private System.Windows.Forms.TextBox T_NamePart;
    }
}
