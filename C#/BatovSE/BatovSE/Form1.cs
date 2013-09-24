using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatovSE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] xy = new int[8];
            Random rnd = new Random(); 
            for (int i = 0; i < 8; ++i)
            {
                xy[i] = rnd.Next(10,200);
            }
              System.Drawing.Graphics g;
              g = pictureBox1.CreateGraphics();
              g.Clear(Color.White);
              g.FillRectangle(Brushes.Black, xy[6], xy[7], 5, 5);
              DrawTriagle(g,xy[0], xy[1], xy[2], xy[3], xy[4], xy[5]);
              if (GeomLib.isInsideTriagle(xy[6], xy[7], xy[0], xy[1], xy[2], xy[3], xy[4], xy[5]))
              {
                  textBox1.Text = "Point is inside triagle!!!";
              }
              else
              {
                  textBox1.Text = "Point is not inside triagle";
              }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        public void DrawTriagle(System.Drawing.Graphics g,int x1, int y1, int x2, int y2, int x3, int y3)
        {
            System.Drawing.Pen pen1 = new System.Drawing.Pen(Color.Red, 2F);
            g.DrawLine(pen1, x1, y1, x2, y2);
            g.DrawLine(pen1, x2, y2, x3, y3);
            g.DrawLine(pen1, x3, y3, x1, y1);
        }
        

    }
}
