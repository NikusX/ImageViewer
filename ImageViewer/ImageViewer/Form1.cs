using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                
                Bitmap bmpVvod = new Bitmap(openFileDialog1.OpenFile());
                Color pxlclr1 = new Color();
                fltpxl = new float[bmpVvod.Width * bmpVvod.Height * 4];
                int count1 = 0;
                for (int y = 0; y < pictureBox1.Height; y++)
                {
                    for (int x = 0; x < pictureBox1.Width; x++)
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
                for (int y = 0; y < pictureBox1.Height; y++)
                {
                    for (int x = 0; x < pictureBox1.Width; x++)
                    {
                        pxlclr2 = Color.FromArgb((int)fltpxl[count2], (int)fltpxl[count2 + 1], (int)fltpxl[count2 + 2], (int)fltpxl[count2 + 3]);
                        bmpVivod.SetPixel(x, y, pxlclr2);
                        count2 += 4;
                    }
                }
                pictureBox1.Image = bmpVivod;
            }
        }
    }
}
