using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms.VisualStyles;

public class RandomTextGenerator
{
    private static Random random = new Random();
    private static string title = string.Empty;
    public static void GenerateTextFile(int titleLength, int contentLength)
    {
        title = GenerateRandomText(titleLength, true);
        var filename = $"{title}.txt";
        var content = GenerateRandomText(contentLength, false);
        File.WriteAllText(filename, content);
    }
    private static string GenerateRandomText(int length, bool isFileName)
    {
        var randomText = new StringBuilder();
        int lineLength = 0;
        int lineNumber = 0;
        if (!isFileName)//write title as first line and then append two new lines
        {
            randomText.Append($"{title}");
            randomText.Append("\n\n");
        }
        for (var i = 0; i < length; i++)
        {
            var codePoint = random.Next(0x20, 0x10ffff); // valid Unicode code points
            if (codePoint >= 0xd800 && codePoint <= 0xdfff) // exclude surrogate code points
            {
                codePoint = random.Next(0x20, 0x10ffff);
            }
            if (isFileName && codePoint == 0x3A) // exclude colon (:) in file names
            {
                codePoint = random.Next(0x20, 0x10ffff);
            }
            var charString = char.ConvertFromUtf32(codePoint);
            var charLength = charString.Length;
            if (lineLength + charLength > 80 && !isFileName) // insert newline every 80 characters
            {
                randomText.Append("\n");
                lineLength = 0; // update line length counter
                lineNumber += 1; // update line number counter
            }
            if (lineNumber == 40) // insert newline every 40 lines
            {
                randomText.Append("\n");
                lineNumber = 0; // reset line number counter
            }
            randomText.Append(charString);
            lineLength += charLength;
        }
        return randomText.ToString();
    }
}