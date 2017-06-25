namespace CsharpToPlantUml
{
    partial class MainUserControl
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.translationButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(165, 110);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "        /// <summary>\r\n        /// スコアのスクリプトのキャッシュ\r\n        /// </summary>\r\n     " +
    "   /// <returns></returns>\r\n        public static Score GetScoreScript()\r\n";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(3, 189);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox2.Size = new System.Drawing.Size(162, 117);
            this.textBox2.TabIndex = 1;
            // 
            // translationButton
            // 
            this.translationButton.Location = new System.Drawing.Point(3, 134);
            this.translationButton.Name = "translationButton";
            this.translationButton.Size = new System.Drawing.Size(162, 23);
            this.translationButton.TabIndex = 2;
            this.translationButton.Text = "変換";
            this.translationButton.UseVisualStyleBackColor = true;
            this.translationButton.Click += new System.EventHandler(this.TranslationButton_Click);
            // 
            // MainUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.translationButton);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "MainUserControl";
            this.Size = new System.Drawing.Size(168, 309);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button translationButton;
    }
}
