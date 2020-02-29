using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace курсовая_работа
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ResultTextBox.Multiline = true;
            ResultTextBox.ScrollBars = ScrollBars.Vertical;
            OriginalTextBox.Multiline = true;
            OriginalTextBox.ScrollBars = ScrollBars.Vertical;
            this.KeyPreview = true;
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            this.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
        }

        private void Form1_KeyUp (object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) OKButton.PerformClick();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            ResultTextBox.Clear();
            DivideText(this);
            OKButton.Enabled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            OKButton.Enabled = true;
            OKButton.Visible = true;
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            ResultTextBox.Clear();
            ResultTextBox.Text = Syllables.DivideText(ReadFile(this));
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveFile(this);
        }

        /// <summary>
        /// Сохранение разделенного текста в файле.
        /// </summary>
        /// <param name="form1"> Форма главного окна</param>
        static void SaveFile(Form1 form1)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
            saveFileDialog1.RestoreDirectory = true;

            try
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog1.FileName, Convert.ToString(form1.ResultTextBox.Text));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Чтение текста из окна формы.
        /// </summary>
        /// <param name="form1"> Форма главного окна</param>
        static void DivideText(Form1 form1)
        {
            string text = form1.OriginalTextBox.Text;
            form1.ResultTextBox.Text += Syllables.DivideText(text);
        }

        /// <summary>
        /// Чтение текста из файла.
        /// </summary>
        /// <param name="form1"> Форма главного окна</param>
        /// <returns> Текст, записанный в файле</returns>
        static string ReadFile(Form1 form1)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    var fileStream = openFileDialog.OpenFile();
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
                return fileContent;
            }
        }
    }
}

