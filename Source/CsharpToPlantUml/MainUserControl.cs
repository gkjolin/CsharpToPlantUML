using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace CsharpToPlantUml
{
    public partial class MainUserControl : UserControl
    {
        public MainUserControl()
        {
            InitializeComponent();
        }

        public void AdjustSize()
        {
            // コントロールを、フォームに合わせて広げます
            textBox1.SetBounds(0, textBox1.Bounds.Y, Width, textBox1.Height);
            translationButton.SetBounds(0, translationButton.Bounds.Y, Width, translationButton.Height);
            textBox2.SetBounds(0, textBox2.Bounds.Y, Width, textBox2.Height);
        }

        public void OnLoad()
        {
            AdjustSize();
        }

        /// <summary>
        /// [変換]ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TranslationButton_Click(object sender, EventArgs e)
        {
            Translator translator = new Translator();
            textBox2.Text = translator.Translate(textBox1.Text);
        }

        private void MainUserControl_Resize(object sender, EventArgs e)
        {
            AdjustSize();
        }
    }
}
