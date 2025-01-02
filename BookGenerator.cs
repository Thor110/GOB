using System.Text;
public class RandomTextGenerator
{
    private static Random random = new Random();
    private static string title = string.Empty;
    public static void GenerateTextFile(int titleLength, int contentLength, int lineLength, int paragraphLength)
    {
        title = GenerateRandomText(titleLength, true, 0, 0);
        string content = GenerateRandomText(contentLength, false, lineLength, paragraphLength);
        File.WriteAllText(title + ".txt", content);
    }
    private static string GenerateRandomText(int length, bool isFileName, int lineWidth, int paragraphLength)
    {
        var randomText = new StringBuilder();
        if (!isFileName)
        {
            int lineLength = 0;
            int lineNumber = 0;
            randomText.Append($"{title}");
            randomText.Append(newLine(2));
            for (var i = 0; i < length; i++)
            {
                var charString = GenerateRandomCharacter(isFileName);
                if (lineLength + charString.Length > lineWidth)
                {
                    randomText.Append(newLine(1));
                    lineLength = 0;
                    lineNumber++;
                    if (lineNumber == paragraphLength)
                    {
                        randomText.Append(newLine(1));
                        lineNumber = 0;
                    }
                }
                randomText.Append(charString);
                lineLength += charString.Length;
            }

        }
        else
        {
            for (var i = 0; i < length; i++) // length could exceed given length due to some unicode characters counting as two, doesn't really matter.
            {
                randomText.Append(GenerateRandomCharacter(isFileName));
            }
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
    private static string newLine(int number) //overkill new line function
    {
        string text = string.Empty;
        for (var i = 0; i < number; i++)
        {
            text = text + "\n";
        }
        return text;
    }
}