namespace CodeToUml
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.mainUserControl1 = new CodeToUml.MainUserControl();
            this.SuspendLayout();
            // 
            // mainUserControl1
            // 
            this.mainUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainUserControl1.Location = new System.Drawing.Point(0, 0);
            this.mainUserControl1.Name = "mainUserControl1";
            this.mainUserControl1.Size = new System.Drawing.Size(296, 323);
            this.mainUserControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 323);
            this.Controls.Add(this.mainUserControl1);
            this.Name = "Form1";
            this.Text = "Code To UML";
            this.ResumeLayout(false);

        }

        #endregion

        private MainUserControl mainUserControl1;
    }
}

