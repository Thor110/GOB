using System.Text;

public class RandomTextGenerator
{
    private static Random random = new Random();
    private static string title = string.Empty;
    public static void GenerateTextFile(int titleLength, int contentLength)
    {
        title = GenerateRandomText(titleLength, true);
        string filename = $"{title}";
        string content = GenerateRandomText(contentLength, false);
        File.WriteAllText(filename + ".txt", content);
    }
    private static string GenerateRandomText(int length, bool isFileName)
    {
        var randomText = new StringBuilder();
        int lineLength = 0;
        int lineNumber = 0;
        if (!isFileName)// write title as first line and then append two new lines if not generating the file name
        {
            randomText.Append($"{title}"); // append the title as the first line
            randomText.Append("\n\n"); // append two new lines after the title
        }
        for (var i = 0; i < length; i++)
        {
            var charString = GenerateRandomCharacter(isFileName);
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
    private static string GenerateRandomCharacter(bool isFileName)
    {
        var codePoint = random.Next(0x20, 0x10ffff); // valid Unicode code points
        if (codePoint >= 0xd800 && codePoint <= 0xdfff) // exclude surrogate code points
        {
            return GenerateRandomCharacter(isFileName); // recursive function call to avoid code duplication and while statements
        }
        if (isFileName && codePoint == 0x3A) // exclude colon (:) in file names
        {
            return GenerateRandomCharacter(isFileName); // ...
        }
        var charString = char.ConvertFromUtf32(codePoint);
        return charString;
    }
}