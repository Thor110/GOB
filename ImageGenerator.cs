using System;
using System.Collections;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

public class ImageGenerator
{
    public static void GenerateImage(string filePath, int width, int height)
    {
        // Create a new bitmap
        using (Bitmap bitmap = new Bitmap(width, height))
        {
            // Create a random number generator
            Random random = new Random();

            // Lock the bitmap bits
            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData data = bitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            // Get the pixel data pointer
            IntPtr ptr = data.Scan0;

            // Iterate over each pixel in the bitmap
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Calculate the pixel offset
                    int offset = (y * data.Stride) + (x * 4);

                    // Generate a random color
                    Color color = Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));

                    // Set the pixel color
                    Marshal.WriteInt32(ptr, offset, color.ToArgb());
                }
            }

            // Unlock the bitmap bits
            bitmap.UnlockBits(data);

            // Save the bitmap to a file
            bitmap.Save(filePath, ImageFormat.Png);
        }
    }
}