using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stegano
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }


        //открыть файл
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.PNG)|*.png";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dialog.FileName.ToString();
                pictureBox1.ImageLocation = textBox1.Text;
            }

        }

        // выбор цвета внедрения сообщения
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton1 = (RadioButton)sender;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton2 = (RadioButton)sender;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton3 = (RadioButton)sender;
        }

        //русский язык
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton4 = (RadioButton)sender;
        }

        //английский язык
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton5 = (RadioButton)sender;
        }

        // кодирование
        private void button2_Click(object sender, EventArgs e)
        {
            string alph = "АаБбВвГгДдЕеЁёЖжЗзИиЙйКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщЪъЫыЬьЭэЮюЯя 0123456789.,?!;:-/=_+()";
            // для английского языка
            if (radioButton5.Checked)
            {
                Bitmap img = new Bitmap(textBox1.Text);
                string txt = textBox2.Text;
                int len = textBox2.Text.Length;
                if (len != 0 && img != null)
                {
                    int n = img.Height;
                    int m = img.Width;
                    int x = 0;
                    int y = n - 1;
                    // внедряем текст, начиная с левого нижнего угла картинки
                    for (int i = 0; i < len; i++)
                    {
                        int c = txt[i];
                        for (int j = 0; j < 8; j++)
                        {
                            if (x >= m)
                            {
                                y--;
                                x = 0;
                            }
                            Color p = img.GetPixel(x, y);
                            int r = p.R;
                            int g = p.G;
                            int b = p.B;
                            if (radioButton1.Checked)
                            {
                                r = ((r & 254) | ((c & (1 << j)) > 0 ? 1 : 0));
                            }
                            if (radioButton2.Checked)
                            {
                                g = ((g & 254) | ((c & (1 << j)) > 0 ? 1 : 0));
                            }
                            if (radioButton3.Checked)
                            {
                                b = ((b & 254) | ((c & (1 << j)) > 0 ? 1 : 0));
                            }
                            p = Color.FromArgb(r, g, b);
                            img.SetPixel(x, y, p);
                            x++;
                        }
                    }
                    SaveFileDialog saveFile = new SaveFileDialog();
                    saveFile.Filter = "Image files (*.PNG)|*.png";

                    if (saveFile.ShowDialog() == DialogResult.OK)
                    {
                        pictureBox1.ImageLocation = textBox1.Text;
                        textBox4.Text = saveFile.FileName.ToString();
                        pictureBox2.ImageLocation = textBox4.Text;

                        img.Save(textBox4.Text);
                    }


                }

            }

            // для русского языка
            if (radioButton4.Checked)
            {
                Bitmap img = new Bitmap(textBox1.Text);
                string txt = textBox2.Text;
                int len = textBox2.Text.Length;
                if (len != 0 && img != null)
                {
                    int n = img.Height;
                    int m = img.Width;
                    int x = 0;
                    int y = n - 1;
                    int c = 0; 
                    for (int i = 0; i < len; i++)
                    {
                        for (int z = 0; z < alph.Length; z++)
                        {
                            if (txt[i] == alph[z])
                            {
                                 c = z;
                            }
                        }
                        for (int j = 0; j < 8; j++)
                        {
                            if (x >= m)
                            {
                                y--;
                                x = 0;
                            }
                            Color p = img.GetPixel(x, y);
                            int r = p.R;
                            int g = p.G;
                            int b = p.B;
                            if (radioButton1.Checked)
                            {
                                r = ((r & 254) | ((c & (1 << j)) > 0 ? 1 : 0));
                            }
                            if (radioButton2.Checked)
                            {
                                g = ((g & 254) | ((c & (1 << j)) > 0 ? 1 : 0));
                            }
                            if (radioButton3.Checked)
                            {
                                b = ((b & 254) | ((c & (1 << j)) > 0 ? 1 : 0));
                            }
                            p = Color.FromArgb(r, g, b);
                            img.SetPixel(x, y, p);
                            x++;
                        }
                    }
                }
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "Image files (*.PNG)|*.png";

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.ImageLocation = textBox1.Text;
                    textBox4.Text = saveFile.FileName.ToString();
                    pictureBox2.ImageLocation = textBox4.Text;

                    img.Save(textBox4.Text);
                }
            }

    }



        //декодирование
        private void button3_Click(object sender, EventArgs e)
        {
            string alph = "АаБбВвГгДдЕеЁёЖжЗзИиЙйКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщЪъЫыЬьЭэЮюЯя 0123456789.,?!;:-/=_+()";
            // для английского языка
            if (radioButton5.Checked)
            {
                Bitmap img = new Bitmap(textBox1.Text);
                string txt = "";
                int len = Convert.ToInt32(textBox3.Text);
                if (img != null)
                {
                    int n = img.Height;
                    int m = img.Width;
                    // аналогично, считываем зашифрованный текст, начиная с левого нижнего угла картинки
                    int x = 0;
                    int y = n - 1;
                    for (int i = 0; i < len; i++)
                    {
                        int c = 0;

                        for (int j = 0; j < 8; j++)
                        {
                            if (x >= m)
                            {
                                y--;
                                x = 0;
                            }
                            Color p = img.GetPixel(x, y);
                            int r = p.R;
                            int g = p.G;
                            int b = p.B;

                            if (radioButton1.Checked)
                            {
                                c = c | ((r & 1) << j);
                            }
                            if (radioButton2.Checked)
                            {
                                c = c | ((g & 1) << j);
                            }
                            if (radioButton3.Checked)
                            {
                                c = c | ((b & 1) << j);
                            }
                            x++;
                        }
                        txt += (char)(c);
                    }

                    txt.Reverse();

                    textBox2.Text = txt;
                }
            }

            //для русского языка
            if (radioButton4.Checked)
            {
                Bitmap img = new Bitmap(textBox1.Text);
                string txt = "";
                int len = Convert.ToInt32(textBox3.Text);
                if (img != null)
                {
                    int n = img.Height;
                    int m = img.Width;
                    // аналогично, считываем зашифрованный текст, начиная с левого нижнего угла картинки
                    int x = 0;
                    int y = n - 1;
                    for (int i = 0; i < len; i++)
                    {
                        int c = 0;

                        for (int j = 0; j < 8; j++)
                        {
                            if (x >= m)
                            {
                                y--;
                                x = 0;
                            }
                            Color p = img.GetPixel(x, y);
                            int r = p.R;
                            int g = p.G;
                            int b = p.B;

                            if (radioButton1.Checked)
                            {
                                c = c | ((r & 1) << j);
                            }
                            if (radioButton2.Checked)
                            {
                                c = c | ((g & 1) << j);
                            }
                            if (radioButton3.Checked)
                            {
                                c = c | ((b & 1) << j);
                            }
                            x++;
                        }
                        txt += alph[c];
                    }
                    txt.Reverse();

                    textBox2.Text = txt;
                }

            }
        }

        //размер сообщения
        private void button4_Click(object sender, EventArgs e)
        {
            textBox3.Text = textBox2.Text.Length.ToString();
        }

    }
}
