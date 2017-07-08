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
            // translationButton.SetBounds(0, translationButton.Bounds.Y, Width, translationButton.Height);
            textBox2.SetBounds(0, textBox2.Bounds.Y, Width, textBox2.Height);
            textBox3.SetBounds(0, textBox3.Bounds.Y, Width, textBox3.Height);
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
            CodeToPibotBuilder codeToPibotBuilder = new CodeToPibotBuilder();
            Pibot pibot = codeToPibotBuilder.Translate(textBox1.Text);
            textBox2.Text = new PibotToUmlBuilder().Build(pibot);
        }

        private void MainUserControl_Resize(object sender, EventArgs e)
        {
            AdjustSize();
        }

        /// <summary>
        /// [クリアー]ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearsButton_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            UmlToPibotBuilder builder1 = new UmlToPibotBuilder()
            {
                isDeleteFontTag = deleteFontTagCheckBox.Checked
            };
            Pibot pibot = builder1.Build(textBox2.Text);
            textBox3.Text = new PibotToUmlBuilder().Build(pibot);
        }
    }
}
