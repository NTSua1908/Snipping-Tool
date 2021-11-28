using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snipping_Tool
{
    class ScreenCapture
    {
        private bool isTaking = false; //Check is taking snapshot


        private Bitmap getScreen()
        {
            var bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                               Screen.PrimaryScreen.Bounds.Height,
                               PixelFormat.Format32bppArgb);

            // Create a graphics object from the bitmap.
            var gfxScreenshot = Graphics.FromImage(bmpScreenshot);

            // Take the screenshot from the upper left corner to the right bottom corner.
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                        Screen.PrimaryScreen.Bounds.Y,
                                        0,
                                        0,
                                        Screen.PrimaryScreen.Bounds.Size,
                                        CopyPixelOperation.SourceCopy);

            return bmpScreenshot;
        }

        public void SetCanvas()
        {
            if (isTaking)
                return;
            isTaking = true;
            using (FreezeScreen freeze = new FreezeScreen(getScreen()))
            {
                DialogResult result = freeze.ShowDialog();
                if ( result == System.Windows.Forms.DialogResult.OK)
                {
                    isTaking = false;
                }
            }
        }
    }
}
