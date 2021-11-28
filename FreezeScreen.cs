using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snipping_Tool
{
    class FreezeScreen : Form
    {
        public Rectangle canvasBounds = Screen.GetBounds(Point.Empty);
        public Image snapshot;
        DialogResult dialog;

        public FreezeScreen(Image image)
        {
            this.DoubleBuffered = true;
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //this.ShowInTaskbar = false;
            this.BackgroundImage = image;
            this.KeyDown += FreezeScreen_KeyDown;
            this.Shown += FreezeScreen_Shown;
            //image.Save("myfile.png", ImageFormat.Png);
            
        }

        private void FreezeScreen_Shown(object sender, EventArgs e)
        {
            Canvas canvas = new Canvas();
            canvas.FormClosing += Canvas_FormClosing;
            canvas.Show();
        }

        private void FreezeScreen_KeyDown(object sender, KeyEventArgs e)
        {
            MessageBox.Show("Freeze");
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Canvas_FormClosing(object sender, FormClosingEventArgs e)
        {
            Canvas canvas = sender as Canvas;
            dialog = canvas.DialogResult;
            if (dialog == System.Windows.Forms.DialogResult.OK)
            {
                this.canvasBounds = canvas.GetRectangle();
                snapshot = GetSnapShot();
            }

            DialogResult = dialog;
            if (snapshot == null)
                DialogResult = DialogResult.Cancel;
            Close();
        }

        public Image GetSnapShot()
        {
            if (canvasBounds.Height <= 0 || canvasBounds.Width <= 0)
                return null;
            using (Image image = new Bitmap(canvasBounds.Width, canvasBounds.Height))
            {
                using (Graphics graphics = Graphics.FromImage(image))
                {
                    graphics.CopyFromScreen(new Point(canvasBounds.Left, canvasBounds.Top), Point.Empty, canvasBounds.Size);
                    Clipboard.SetImage(image);
                    return image;
                }
            }
        }
    }
}
