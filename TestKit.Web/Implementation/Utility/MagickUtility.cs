using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ImageMagick;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestKit.Web.Implementation.Utility
{
    public class MagickUtility
    {
        protected static readonly string BaselineFolder = Environment.GetEnvironmentVariable("BaselinePath");
        protected static readonly string TempFolder = Path.Combine(BaselineFolder, "Temp");
        public static string ConvertImagesToGif(Dictionary<string,string> images, int[] resolution)
        {
            using (MagickImageCollection collection = new MagickImageCollection())
            {
                string finalName = string.Empty;               
                int count = 0;
                foreach (var item in images)
                {     
                    if(count==0)
                    {
                        finalName =Path.GetFileNameWithoutExtension(item.Key);
                    }                                  
                    string resizeImage=ResizeToFixedSize(item.Key,resolution);
                    string waterMarkImage = AddTextToImage(resizeImage, item.Value);
                    collection.Add(waterMarkImage);
                    collection[count].AnimationDelay = 200;
                    count++;
                }           

                // Optionally reduce colors
                QuantizeSettings settings = new QuantizeSettings();
                settings.Colors = 256;
                collection.Quantize(settings);

                // Optionally optimize the images (images should have the same size).
                collection.Optimize();

                // Save gif
                string gif = TempFolder + "\\final_" + finalName + ".gif";
                collection.Write(gif);
                return gif;
            }
        }

        public static string ResizeToFixedSize(string imageFile, int[] resolution)
        {
            // Read from file
            using (MagickImage image = new MagickImage(imageFile))
            {
                MagickGeometry size = new MagickGeometry(resolution[0], resolution[1]);
                // This will resize the image to a fixed size without maintaining the aspect ratio.
                // Normally an image will be resized to fit inside the specified size.
                size.IgnoreAspectRatio = true;
                image.Resize(size);
                // Save the result
                string outputImage = TempFolder + "\\resize_" + Path.GetFileName(imageFile);
                image.Write(outputImage);
                return outputImage;
            }
        }

        public static string AddTextToImage(string imageFile, string text)
        {
            var image = new MagickImage(imageFile);
            using (var imgText = new MagickImage())
            {
                var drawable = new DrawableText(0, 10, text);
                var gravity = new DrawableGravity(Gravity.North);
                var font = new DrawableFont("Arial");
                var antialias = new DrawableTextAntialias(true);
                var size = new DrawableFontPointSize(50);
                var color = new DrawableFillColor(Color.Snow);
                var strokeColor = new DrawableStrokeColor(Color.OrangeRed);
                image.Draw(drawable, gravity, font, antialias, size, color, strokeColor);
            }
            // Save the result
            string outputImage = TempFolder + "\\waterMark_" + Path.GetFileName(imageFile);
            image.Write(outputImage);
            return outputImage;
        }

        public static byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, ImageFormat.Gif);
                return ms.ToArray();
            }
        }
    }
}
