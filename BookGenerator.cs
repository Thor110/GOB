using System.Text;
public class RandomTextGenerator
{
    private static Random random = new Random();
    private static string title = string.Empty;
    public static void GenerateTextFile(int titleLength, int contentLength, int lineLength, int paragraphLength)
    {
        title = GenerateRandomText(titleLength, true, titleLength, 0);
        string content = GenerateRandomText(contentLength, false, lineLength, paragraphLength);
        File.WriteAllText(title + ".txt", content);
    }
    private static string GenerateRandomText(int length, bool isFileName, int lineLength, int paragraphLength)
    {
        var randomText = new StringBuilder();
        int charCounter = 0;
        int lineCounter = 0;
        var charString = string.Empty;
        if (!isFileName)
        {
            randomText.Append($"{title}");
            UpdateNewLine(2);
        }
        for (var i = 0; i < length; i++)
        {
            charString = GenerateRandomCharacter(isFileName);
            if (!isFileName)
            {
                if (CheckTextLength())
                {
                    UpdateText();
                }
                else
                {
                    UpdateNewLine(1);
                    charCounter = 0;
                    lineCounter++;
                    if (lineCounter == paragraphLength)
                    {
                        UpdateNewLine(1);
                        lineCounter = 0;
                    }
                }
            }
            if (isFileName)
            {
                if (CheckTextLength())
                {
                    UpdateText();
                }
            }
        }
        bool CheckTextLength()
        {
            if (charCounter + charString.Length < lineLength)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        void UpdateText()
        {
            randomText.Append(charString);
            charCounter += charString.Length;
        }
        void UpdateNewLine(int newLines)
        {
            randomText.Append(newLine(newLines));
        }
        return randomText.ToString();
    }
    private static string GenerateRandomCharacter(bool isFileName)
    {
        var codePoint = random.Next(0x20, 0x10ffff); // valid Unicode code points
        if (codePoint >= 0xd800 && codePoint <= 0xdfff || isFileName && codePoint == 0x3A) // exclude surrogate code points or exclude colon (:) in file names
        {
            return GenerateRandomCharacter(isFileName);
        }
        return char.ConvertFromUtf32(codePoint);
    }
    private static string newLine(int newLines)
    {
        string text = string.Empty;
        for (var i = 0; i < newLines; i++)
        {
            text = text + "\n";
        }
        return text;
    }
}