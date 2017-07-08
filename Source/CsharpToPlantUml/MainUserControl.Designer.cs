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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainUserControl));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.translationButton = new System.Windows.Forms.Button();
            this.clearsButton = new System.Windows.Forms.Button();
            this.deleteFontTagCheckBox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
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
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(3, 165);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox2.Size = new System.Drawing.Size(162, 117);
            this.textBox2.TabIndex = 1;
            // 
            // translationButton
            // 
            this.translationButton.Location = new System.Drawing.Point(68, 116);
            this.translationButton.Name = "translationButton";
            this.translationButton.Size = new System.Drawing.Size(97, 23);
            this.translationButton.TabIndex = 2;
            this.translationButton.Text = "変換";
            this.translationButton.UseVisualStyleBackColor = true;
            this.translationButton.Click += new System.EventHandler(this.TranslationButton_Click);
            // 
            // clearsButton
            // 
            this.clearsButton.Location = new System.Drawing.Point(7, 116);
            this.clearsButton.Name = "clearsButton";
            this.clearsButton.Size = new System.Drawing.Size(55, 23);
            this.clearsButton.TabIndex = 3;
            this.clearsButton.Text = "クリアー";
            this.clearsButton.UseVisualStyleBackColor = true;
            this.clearsButton.Click += new System.EventHandler(this.ClearsButton_Click);
            // 
            // deleteFontTagCheckBox
            // 
            this.deleteFontTagCheckBox.AutoSize = true;
            this.deleteFontTagCheckBox.Checked = true;
            this.deleteFontTagCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.deleteFontTagCheckBox.Location = new System.Drawing.Point(7, 288);
            this.deleteFontTagCheckBox.Name = "deleteFontTagCheckBox";
            this.deleteFontTagCheckBox.Size = new System.Drawing.Size(147, 16);
            this.deleteFontTagCheckBox.TabIndex = 4;
            this.deleteFontTagCheckBox.Text = "<font>～</font>タグ削除";
            this.deleteFontTagCheckBox.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 310);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "再変換";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(7, 362);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox3.Size = new System.Drawing.Size(162, 117);
            this.textBox3.TabIndex = 6;
            // 
            // MainUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.deleteFontTagCheckBox);
            this.Controls.Add(this.clearsButton);
            this.Controls.Add(this.translationButton);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "MainUserControl";
            this.Size = new System.Drawing.Size(333, 482);
            this.Resize += new System.EventHandler(this.MainUserControl_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button translationButton;
        private System.Windows.Forms.Button clearsButton;
        private System.Windows.Forms.CheckBox deleteFontTagCheckBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox3;
    }
}
