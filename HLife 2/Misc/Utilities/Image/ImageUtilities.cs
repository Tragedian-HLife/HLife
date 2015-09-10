using ImageMagick;
using ImageMagick.Defines;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HLife_2
{
    public static class ImageUtilities
    {
        public static Bitmap GetSubimage(Bitmap image, Rectangle rectangle)
        {
            Bitmap subimage = new Bitmap(image.Width, image.Height);

            Graphics.FromImage(subimage).DrawImage(
                image,
                new Rectangle(0, 0, image.Width, image.Height),
                rectangle,
                GraphicsUnit.Pixel);

            return subimage;
        }

        public static Bitmap CombineImages(Bitmap destination, Bitmap addition, Rectangle rectangle)
        {
            Graphics.FromImage(destination).DrawImage(addition, rectangle);

            return destination;
        }

        public static Bitmap Blur(Bitmap image, double blurSize)
        {
            MagickImage magick = new MagickImage(image);
            magick.Blur(0, blurSize);
            image = magick.ToBitmap();

            return image;
        }

        public static Bitmap FastBlur(Bitmap image, double percentage)
        {
            MagickImage magick = new MagickImage(image);
            magick.Resize(new Percentage(50.0 / percentage));
            //magick.Resize(new Percentage(100.0 / percentage));
            /*
            magick.Grayscale(ImageMagick.PixelIntensityMethod.Average);
            MagickImage temp = new MagickImage(Game.Instance.ResourceController.BuildPath(@"..\..\Global Resources\Assets\Images\black_50alpha.png"));
            temp.Alpha(AlphaOption.Activate);
            temp.Resize(magick.Width * 2, magick.Height * 2);
            magick.Composite(temp, CompositeOperator.Darken);
            */
            magick.Resize(new Percentage(200 * percentage));
            image = magick.ToBitmap();

            return image;
        }

        public static Bitmap TakeSnapshot(Control ctl)
        {
            Bitmap bmp = new Bitmap(ctl.Size.Width, ctl.Size.Height);

            using (Graphics g = System.Drawing.Graphics.FromImage(bmp))
            {
                g.CopyFromScreen
                (
                    ctl.PointToScreen(ctl.ClientRectangle.Location),
                    new Point(0, 0), ctl.ClientRectangle.Size
                );
            }

            return bmp;
        }
    }
}
