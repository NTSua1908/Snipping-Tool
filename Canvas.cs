using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snipping_Tool
{
    class Canvas : Form
    {
        Point startPos;      // mouse-down position
        Point currentPos;    // current mouse position
        bool drawing;
        //ScreenSelector selector;

        public Canvas()
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.BackColor = Color.White;
            this.Opacity = 0.60;
            //this.ShowInTaskbar = false;
            this.Cursor = Cursors.Cross;
            this.MouseDown += Canvas_MouseDown;
            this.MouseMove += Canvas_MouseMove;
            this.MouseUp += Canvas_MouseUp;
            this.Paint += Canvas_Paint;
            this.KeyDown += Canvas_KeyDown;
            this.DoubleBuffered = true;
            this.TransparencyKey = Color.Black;
            this.TopMost = true;

            //selector = new ScreenSelector();
            //selector.Location = new Point(0, 0);
            //selector.Dock = DockStyle.Fill;
            //selector.BackColor = Color.Blue;
            //selector.MouseDown += Canvas_MouseDown;
            //selector.MouseMove += Canvas_MouseMove;
            //selector.MouseUp += Canvas_MouseUp;
            //selector.Paint += Canvas_Paint;
            //selector.KeyDown += Canvas_KeyDown;
            //this.Controls.Add(selector);
            //pic.
        }

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show("Canvas");
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle(
                Math.Min(startPos.X, currentPos.X),
                Math.Min(startPos.Y, currentPos.Y),
                Math.Abs(startPos.X - currentPos.X),
                Math.Abs(startPos.Y - currentPos.Y));
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            currentPos = startPos = e.Location;
            drawing = true;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            currentPos = e.Location;
            if (drawing) this.Invalidate();
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            if (drawing)
            {
                e.Graphics.FillRectangle(Brushes.Black, GetRectangle());
                //e.Graphics.DrawRectangle(Pens.Red, GetRectangle());
            }
        }
    }
}
