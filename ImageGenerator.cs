using System.Drawing.Imaging;
using System.Runtime.InteropServices;

public class ImageGenerator
{
    private static Random random = new Random(); // Create a random number generator
    public static void GenerateImage(string filePath, int width, int height)
    {
        // Create a new bitmap
        using (Bitmap bitmap = new Bitmap(width, height))
        {
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
                    Color color = GenerateRandomColor();

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
    public static Color GenerateRandomColor(int range = 255)
    {
        // Generate a random color
        // Possible color values
        // RGB  | 0 - 255   (Default)
        // PMS  | 0 - 1000
        // CMYK | 0 - 100
        Color color = Color.FromArgb(random.Next(range), random.Next(range), random.Next(range));
        return color;
    }
}