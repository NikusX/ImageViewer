using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        float[] fltpxl;
        int width;
        int height;

        private void изФайлаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Bitmap bmpVvod = new Bitmap(openFileDialog1.OpenFile());
                Color pxlclr1 = new Color();
                fltpxl = new float[bmpVvod.Width * bmpVvod.Height * 4];
                int count1 = 0;
                width = bmpVvod.Width;
                height = bmpVvod.Height;
                for (int y = 0; y < bmpVvod.Height; y++)
                {
                    for (int x = 0; x < bmpVvod.Width; x++)
                    {
                        pxlclr1 = bmpVvod.GetPixel(x, y);
                        fltpxl[count1] = pxlclr1.A;
                        fltpxl[count1 + 1] = pxlclr1.R;
                        fltpxl[count1 + 2] = pxlclr1.G;
                        fltpxl[count1 + 3] = pxlclr1.B;
                        count1 += 4;
                    }
                }
                Bitmap bmpVivod = new Bitmap(bmpVvod.Width, bmpVvod.Height);
                Color pxlclr2;
                int count2 = 0;
                for (int y = 0; y < bmpVvod.Height; y++)
                {
                    for (int x = 0; x < bmpVvod.Width; x++)
                    {
                        pxlclr2 = Color.FromArgb((int)fltpxl[count2], (int)fltpxl[count2 + 1], (int)fltpxl[count2 + 2], (int)fltpxl[count2 + 3]);
                        bmpVivod.SetPixel(x, y, pxlclr2);
                        count2 += 4;
                    }
                }
                pictureBox1.Image = bmpVivod;
            }
        }

        private void вФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap bmpSave = new Bitmap(pictureBox1.Image);
                saveFileDialog1.Filter = "bmp(*.bmp)|*.bmp|jpg(*.jpg)|*.jpg|png(*.png)|*.png|All files (*.*)|*.*";
                ImageFormat format = ImageFormat.Bmp;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fn = Path.GetExtension(saveFileDialog1.FileName);
                    switch (fn)
                    {
                        case ".jpg":
                            format = ImageFormat.Jpeg;
                            break;
                        case ".png":
                            format = ImageFormat.Png;
                            break;
                    }
                    pictureBox1.Image.Save(saveFileDialog1.FileName, format);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не загружен файл!");
            }
        }

        private void вNikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count = 0;
            SaveFileDialog svfl = new SaveFileDialog();
            svfl.Filter = "nik| *.nik";
            if (svfl.ShowDialog() == DialogResult.OK)
            {
                StreamWriter str = new StreamWriter(svfl.FileName);
                str.WriteLine(width.ToString() + '\t' + height.ToString() + '\t');
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (count < fltpxl.Length)
                        {
                            str.WriteLine(fltpxl[count + 0].ToString() + '\t' + fltpxl[count + 1].ToString() + '\t' + fltpxl[count + 2].ToString() + '\t' + fltpxl[count + 3].ToString());
                            count += 4;
                        }
                    }
                }
                str.Close();
            }
        }

        private void изToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            OpenFileDialog pnfl = new OpenFileDialog();
            pnfl.Filter = "nik(*.nik)|*.nik";
            if(pnfl.ShowDialog() == DialogResult.OK)
            {
                string[] str = File.ReadAllLines(pnfl.FileName);
                string[] raz = str[0].Split('\t');
                int count = 0;
                Color pxlclr2;
                width = Convert.ToInt32(raz[0]);
                height = Convert.ToInt32(raz[1]);
                Bitmap bmpVivod = new Bitmap(width, height);
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        string[] d = str[x + y * width + 1].Split('\t');
                        fltpxl[count] = (float)(Convert.ToDouble((d[0])));
                        fltpxl[count+1] = (float)(Convert.ToDouble((d[1])));
                        fltpxl[count+2] = (float)(Convert.ToDouble((d[2])));
                        fltpxl[count+3] = (float)(Convert.ToDouble((d[3])));
                        count += 4;
                    }
                }
                int count2 = 0;
                for (int y1 = 0; y1 < height; y1++)
                {
                    for (int x1 = 0; x1 < width; x1++)
                    {
                        pxlclr2 = Color.FromArgb((int)fltpxl[count2], (int)fltpxl[count2 + 1], (int)fltpxl[count2 + 2], (int)fltpxl[count2 + 3]);
                        bmpVivod.SetPixel(x1, y1, pxlclr2);
                        count2 += 4;
                    }
                }
            }
        }
    }
}
