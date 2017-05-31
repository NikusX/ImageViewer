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

        void vivodBmp (float[] pxl, int width, int height)
        {
            Bitmap bmpVivod = new Bitmap(width, height);
            int count2 = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (count2 < pxl.Length)
                    {
                        int r, g, b;
                        int a = Convert.ToInt32(pxl[count2]);
                        if (checkBox1.Checked)
                            r = Convert.ToInt32(pxl[count2 + 1]);
                        else r = 0;
                        if (checkBox2.Checked)
                            g = Convert.ToInt32(pxl[count2 + 2]);
                        else g = 0;
                        if (checkBox3.Checked)
                            b = Convert.ToInt32(pxl[count2 + 3]);
                        else b = 0;
                        Color clrpxl = Color.FromArgb(a, r, g, b);
                        bmpVivod.SetPixel(x, y, clrpxl);
                        count2 += 4;
                    }
                }
            }
            pictureBox1.Image = bmpVivod;
            pictureBox1.Invalidate();
        }

        private void изФайлаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
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
                vivodBmp(fltpxl, bmpVvod.Width, bmpVvod.Height);
            }
            else return;
        }

        private void вФайлToolStripMenuItem_Click(object sender, EventArgs e)
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
            else return;
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
            else return;
        }

        private void изToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            OpenFileDialog pnfl = new OpenFileDialog();
            pnfl.Filter = "nik(*.nik)|*.nik";
            if (pnfl.ShowDialog() == DialogResult.OK)
            {
                string[] stroka = File.ReadAllLines(pnfl.FileName);
                string[] resolution = stroka[0].Split('\t');
                int count = 0;
                width = Convert.ToInt32(resolution[0]);
                height = Convert.ToInt32(resolution[1]);
                fltpxl = new float[width * height * 4];
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (count < fltpxl.Length)
                        {
                            string[] argb = stroka[x + y * width + 1].Split('\t');
                            fltpxl[count] = (float)(Convert.ToDouble(argb[0]));
                            fltpxl[count + 1] = (float)(Convert.ToDouble(argb[1]));
                            fltpxl[count + 2] = (float)(Convert.ToDouble(argb[2]));
                            fltpxl[count + 3] = (float)(Convert.ToDouble(argb[3]));
                            count += 4;
                        }
                    }
                }
            }
            else return;
            vivodBmp(fltpxl, width, height);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            vivodBmp(fltpxl, width, height);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            vivodBmp(fltpxl, width, height);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            vivodBmp(fltpxl, width, height);
        }
    }
}
