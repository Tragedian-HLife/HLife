using ImageMagick;
using ImageMagick.Defines;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace HLife
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
            magick.Resize(new Percentage(200 * percentage));

            return magick.ToBitmap();


            /*
            magick.Grayscale(ImageMagick.PixelIntensityMethod.Average);
            MagickImage temp = new MagickImage(Game.Instance.ResourceController.BuildPath(@"..\..\Global Resources\Assets\Images\black_50alpha.png"));
            temp.Alpha(AlphaOption.Activate);
            temp.Resize(magick.Width * 2, magick.Height * 2);
            magick.Composite(temp, CompositeOperator.Darken);
            */
        }

        public static Bitmap FastBlur(Uri image, double percentage)
        {
            MagickImage magick = new MagickImage(new Bitmap(image.AbsolutePath));
            magick.Resize(new Percentage(50.0 / percentage));
            magick.Resize(new Percentage(200 * percentage));

            return magick.ToBitmap();
        }

        public static BitmapImage FastBlur(BitmapImage image, double percentage)
        {
            MagickImage magick = new MagickImage(ImageUtilities.BitmapImage2Bitmap(image));
            magick.Resize(new Percentage(50.0 / percentage));
            magick.Resize(new Percentage(200 * percentage));

            return magick.ToBitmap().ToBitmapImage();
        }

        /*
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
        */

        public static Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }

        public static BitmapImage ToBitmapImage(this Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

        public static BitmapImage ToImageSource(this Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
    }
}
