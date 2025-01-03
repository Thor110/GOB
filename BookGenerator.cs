using System.Text;
public class RandomTextGenerator
{
    private static Random random = new Random();
    private static string title = string.Empty;
    private static int rangeMin;
    private static int rangeMax;
    private static int surrogateLow = 55296;
    private static int surrogateHigh = 57343;
    private static StringBuilder randomText = new StringBuilder();
    private static int charCounter = 0;
    private static int lineCounter = 0;
    private static string charString = string.Empty;
    // Surrogate Code Points ( Low - High )
    // 55296 - 57343
    // 0xd800 - 0xdfff
    private static int[] excludedCharacters = { 92, 47, 58, 42, 63, 34, 60, 62, 124 };
    //  Table of Excluded Characters ( Hex, Dec, Sym )
    //  0x5C    0x2F    0x3A    0x2A    0x3F    0x22    0x3C    0x3E    0x7C
    //  92      47      58      42      63      34      60      62      124
    //  \       /       :       *       ?       "       <       >       |
    public static void GenerateTextFile(int titleLength, int contentLength, int lineLength, int paragraphLength, int minRange, int maxRange)
    {
        rangeMin = minRange;
        rangeMax = maxRange;
        title = GenerateRandomText(titleLength, true, titleLength, 0);
        string content = GenerateRandomText(contentLength, false, lineLength, paragraphLength);
        File.WriteAllText(title + ".txt", content);
    }
    private static string GenerateRandomText(int length, bool isFileName, int lineLength, int paragraphLength)
    {
        if (!isFileName)
        {
            AddNewLines(2);
        }
        for (var i = 0; i < length; i++)
        {
            charString = GenerateRandomCharacter(isFileName);
            CheckLineLength(lineLength, isFileName, paragraphLength);
        }
        return randomText.ToString();
    }
    private static string GenerateRandomCharacter(bool isFileName)
    {
        Int32 codePoint = random.Next(rangeMin, rangeMax); // exclude surrogate code points or exclude \/:*?"<>| in file names
        if (codePoint >= 0xd800 && codePoint <= 0xdfff || isFileName && excludedCharacters.Contains(codePoint))
        {
            return GenerateRandomCharacter(isFileName);
        }
        return char.ConvertFromUtf32(codePoint);
    }
    private static void CheckLineLength(int lineLength, bool isFileName, int paragraphLength)
    {
        if (charCounter + charString.Length >= lineLength && !isFileName)
        {
            AddNewLines(1);
            charCounter = 0;
            lineCounter++;
        }
        if (lineCounter == paragraphLength && !isFileName)
        {
            AddNewLines(1);
            lineCounter = 0;
        }
        randomText.Append(charString);
        charCounter += charString.Length;
    }
    private static void AddNewLines(int newLines)
    {
        string text = string.Empty;
        for (var i = 0; i < newLines; i++)
        {
            text += "\n";
        }
        randomText.Append(text);
    }
}