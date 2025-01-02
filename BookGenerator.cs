using System.Text;
public class RandomTextGenerator
{
    private static Random random = new Random();
    private static string title = string.Empty; // title is stored as a variable so that it can be added to the first line of the text file
    public static void GenerateTextFile(int titleLength, int contentLength, int lineLength)
    {
        title = GenerateRandomText(titleLength, true, 0);
        //string filename = $"{title}";
        string content = GenerateRandomText(contentLength, false, lineLength);
        File.WriteAllText(title + ".txt", content);
    }
    private static string GenerateRandomText(int length, bool isFileName, int lineWidth)
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
            //var charLength = charString.Length; // what is more efficient? using this to store the value or using two gets for Length?
            if (lineLength + charString.Length > lineWidth && !isFileName) // insert newline every n characters
            {
                randomText.Append("\n");
                lineLength = 0; // update line length counter
                lineNumber += 1; // update line number counter
                if (lineNumber == lineWidth / 2) // insert newline every n/2 lines
                {
                    randomText.Append("\n");
                    lineNumber = 0; // reset line number counter
                }
            }
            randomText.Append(charString);
            lineLength += charString.Length;
        }
        return randomText.ToString();
    }
    private static string GenerateRandomCharacter(bool isFileName)
    {
        var codePoint = random.Next(0x20, 0x10ffff); // valid Unicode code points
        if (codePoint >= 0xd800 && codePoint <= 0xdfff || isFileName && codePoint == 0x3A) // exclude surrogate code points or exclude colon (:) in file names
        {
            return GenerateRandomCharacter(isFileName); // recursive function call to avoid code duplication and while statements
        }
        return char.ConvertFromUtf32(codePoint);
    }
}