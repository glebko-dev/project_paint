﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace itog_project
{
    public partial class Form1 : Form
    {
        bool draw = false;
        int currX = 0, brushSize = 10;
        Point oldPoint, point1, point2;
        Graphics g;
        Brush fillBrush = new SolidBrush(Color.White);
        Brush brush = new SolidBrush(Color.Black);
        Pen pen = new Pen(Color.Black, 10);

        Form2 addColor = new Form2();
        Form3 addLine = new Form3();
        Form4 addRect = new Form4();
        Form5 addEllipse = new Form5();

        List<PictureBox> colors = new List<PictureBox>() {
            new PictureBox() {Size = new Size(50, 50), BackColor = Color.Red},
            new PictureBox() {Size = new Size(50, 50), BackColor = Color.Green},
            new PictureBox() {Size = new Size(50, 50), BackColor = Color.Blue},
            new PictureBox() {Size = new Size(50, 50), BackColor = Color.Yellow},
            new PictureBox() {Size = new Size(50, 50), BackColor = Color.Purple},
            new PictureBox() {Size = new Size(50, 50), BackColor = Color.Black},
            new PictureBox() {Size = new Size(50, 50), BackColor = Color.White}
        };

        public Form1()
        {
            InitializeComponent();
            drawColors();

            Bitmap bitmap = new Bitmap(pictureBox4.Width, pictureBox4.Height);
            pictureBox4.Image = bitmap;

            g = Graphics.FromImage(bitmap);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.FillRectangle(fillBrush, new Rectangle(0, 0, pictureBox4.Size.Width, pictureBox4.Size.Height));
        }
        
        private void drawColors()
        {
            currX = 0;
            panel1.Controls.Clear();
            for (int i = 0; i < colors.Count; i++)
            {
                panel1.Controls.Add(colors[i]);
                panel1.Controls[i].Location = new Point(currX, 0);
                panel1.Controls[i].MouseDown += pictureClick;
                currX += 55;
            }
        }

        private void pictureBox4_MouseMove(object sender, MouseEventArgs e)
        {
            point1 = e.Location;
            label1.Text = $"X = {e.Location.X}; Y = {e.Location.Y}";
            if (draw)
            {
                g.FillEllipse(brush, e.Location.X - brushSize / 2, e.Location.Y - brushSize / 2, brushSize, brushSize);
                g.DrawLine(pen, oldPoint.X, oldPoint.Y, e.Location.X, e.Location.Y);
                pictureBox4.Invalidate();
            }
            oldPoint = new Point(e.Location.X, e.Location.Y);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            brushSize = Convert.ToInt32(textBox1.Text);
            pen.Width = brushSize;
        }

        private void pictureClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PictureBox p = (PictureBox)sender;
                brush = new SolidBrush(p.BackColor);
                pen.Color = p.BackColor;
            }
            else if (e.Button == MouseButtons.Right)
            {
                PictureBox p = (PictureBox)sender;
                colors.Remove(p);
                drawColors();
            }
        }

        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            draw = true;
        }

        private void pictureBox4_MouseUp(object sender, MouseEventArgs e)
        {
            draw = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            addLine.textBox1.Text = "0";
            addLine.textBox2.Text = "0";
            addLine.textBox3.Text = "0";
            addLine.textBox4.Text = "0";
            addLine.button1.Click += drawLine;
            addLine.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            addRect.textBox1.Text = "0";
            addRect.textBox2.Text = "0";
            addRect.textBox3.Text = "0";
            addRect.textBox4.Text = "0";
            addRect.button1.Click += drawRect;
            addRect.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            addEllipse.textBox1.Text = "0";
            addEllipse.textBox2.Text = "0";
            addEllipse.textBox3.Text = "0";
            addEllipse.textBox4.Text = "0";
            addEllipse.button1.Click += drawEllipse;
            addEllipse.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addColor.textBox1.Text = "0";
            addColor.textBox2.Text = "0";
            addColor.textBox3.Text = "0";
            addColor.button1.Click += colorChanged;
            addColor.Show();
        }

        private void colorChanged(object sender, EventArgs e)
        {
            brush = new SolidBrush(Color.FromArgb(Convert.ToInt32(addColor.textBox1.Text), Convert.ToInt32(addColor.textBox2.Text), Convert.ToInt32(addColor.textBox3.Text)));
            pen.Color = Color.FromArgb(Convert.ToInt32(addColor.textBox1.Text), Convert.ToInt32(addColor.textBox2.Text), Convert.ToInt32(addColor.textBox3.Text));
            panel1.Controls.Add(new PictureBox() {Size = new Size(50, 50), BackColor = Color.FromArgb(Convert.ToInt32(addColor.textBox1.Text), Convert.ToInt32(addColor.textBox2.Text), Convert.ToInt32(addColor.textBox3.Text)), Location = new Point(currX, 0)});
            panel1.Controls[panel1.Controls.Count - 1].MouseDown += pictureClick;
            currX += 55;
            addColor.button1.Click -= colorChanged;
        }

        private void drawLine(object sender, EventArgs e)
        {
            label3.Text = addLine.textBox3.Text;
            //point2 = new Point(Convert.ToInt32(addLine.textBox3.Text), Convert.ToInt32(addLine.textBox4.Text));
            //point1 = new Point(Convert.ToInt32(addLine.textBox1.Text), Convert.ToInt32(addLine.textBox2.Text));
            g.FillEllipse(brush, point1.X - brushSize / 2, point1.Y - brushSize / 2, brushSize, brushSize);
            g.DrawLine(pen, point1.X, point1.Y, point2.X, point2.Y);
            g.FillEllipse(brush, point2.X - brushSize / 2, point2.Y - brushSize / 2, brushSize, brushSize);
            pictureBox4.Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.R)
            {
                label3.Text = point1.X.ToString() + ' ' + point1.Y.ToString() + ' ' + point2.X.ToString() + ' ' + point2.Y.ToString();
                g.FillRectangle(brush, point1.X - point2.X / 2, point1.Y - point2.Y / 2, point2.X, point2.Y);
                pictureBox4.Invalidate();
            }
        }

        private void drawRect(object sender, EventArgs e)
        {
            point1 = new Point(Convert.ToInt32(addRect.textBox1.Text), Convert.ToInt32(addRect.textBox2.Text));
            point2 = new Point(Convert.ToInt32(addRect.textBox3.Text), Convert.ToInt32(addRect.textBox4.Text));
            g.FillRectangle(brush, point1.X, point1.Y, point2.X, point2.Y);
            pictureBox4.Invalidate();
        }

        private void drawEllipse(object sender, EventArgs e)
        {
            point1 = new Point(Convert.ToInt32(addEllipse.textBox1.Text), Convert.ToInt32(addEllipse.textBox2.Text));
            int rw = Convert.ToInt32(addEllipse.textBox3.Text), rh = Convert.ToInt32(addEllipse.textBox4.Text);
            g.FillEllipse(brush, point1.X, point1.Y, rw, rh);
            pictureBox4.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Image Files(*.JPG)|*.JPG|Image Files(*.PNG)|*.PNG|Image Files(*.BMP)|*.BMP|All files(*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
                pictureBox4.Image.Save(fileDialog.FileName);
        }
    }
}