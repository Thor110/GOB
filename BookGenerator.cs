using System.Diagnostics;
using System.Text;
public class RandomTextGenerator
{
    public Random random = new Random();
    public StringBuilder randomText = new StringBuilder();
    public string charString = string.Empty;
    public int charStringLength;
    public int rangeMin;
    public int rangeMax;
    public int charCounter;
    public int lineCounter;
    public int charLineCounter;
    public int surrogateLow = 55296;
    public int surrogateHigh = 57343;
    // Surrogate Code Points ( Low - High )
    // 55296    - 57343
    // 0xd800   - 0xdfff
    public int stringLength;
    public int line;
    public int paragraph;
    public int[] excludedCharacters = { 92, 47, 58, 42, 63, 34, 60, 62, 124 };
    //  Table of Excluded Characters ( Hex, Dec, Sym )
    //  0x5C    0x2F    0x3A    0x2A    0x3F    0x22    0x3C    0x3E    0x7C
    //  92      47      58      42      63      34      60      62      124
    //  \       /       :       *       ?       "       <       >       |
    public bool isFileName = true;
    public void GenerateTextFile(int titleLength, int contentLength, int lineLength, int paragraphLength, int minRange, int maxRange)
    {
        setupCharacter(minRange, maxRange);
        setupString(titleLength);
        isFileName = true;
        string title = GenerateRandomString();
        setupString(contentLength);
        charString = GenerateRandomCharacter();
        setupParagraph(lineLength, paragraphLength);
        isFileName = false;
        AddNewLines(2);
        string content = GenerateRandomParagraph();
        File.WriteAllText(title + ".txt", content);
    }
    public void setupCharacter(int minRange, int maxRange)
    {
        charStringLength = 0;
        randomText.Clear();
        rangeMin = minRange;
        rangeMax = maxRange;
        charCounter = 0;
        charString = GenerateRandomCharacter();
        charStringLength = charString.Length;
    }
    public void setupString(int length)
    {
        stringLength = length;
    }
    public void setupParagraph(int lineLength, int paragraphLength)
    {
        paragraph = paragraphLength;
        line = lineLength;
        charCounter = 0;
        lineCounter = 0;
        charLineCounter = 0;
        charLineCounter += charStringLength;
        charStringLength = charString.Length;
    }
    public string GenerateSingleCharacter(int minRange = 32, int maxRange = 1114111, bool fileName = false)
    {
        setupCharacter(minRange, maxRange);
        isFileName = fileName;
        return GenerateRandomCharacter();
    }
    public string GenerateSingleString(int titleLength = 16, int minRange = 32, int maxRange = 1114111, bool fileName = false)
    {
        setupCharacter(minRange, maxRange);
        setupString(titleLength);
        isFileName = fileName;
        return GenerateRandomString();
    }
    public string GenerateSingleParagraph(int contentLength = 800, int lineLength = 80, int paragraphLength = 40, int minRange = 32, int maxRange = 1114111, bool fileName = false)
    {
        setupCharacter(minRange, maxRange);
        setupString(contentLength);
        setupParagraph(lineLength, paragraphLength);
        isFileName = fileName;
        return GenerateRandomParagraph();
    }
    public string GenerateRandomCharacter()
    {
        Int32 codePoint = random.Next(rangeMin, rangeMax); // exclude surrogate code points or exclude \/:*?"<>| in file names
        if (codePoint >= surrogateLow && codePoint <= surrogateHigh || isFileName && excludedCharacters.Contains(codePoint))
        {
            return GenerateRandomCharacter();
        }
        return char.ConvertFromUtf32(codePoint);
    }
    public string GenerateRandomString()
    {
        while(charCounter + charStringLength < stringLength) // length keeps exceeding required length
        {
            UpdateString();
            if (charCounter + charStringLength > stringLength)
            {
                break;
            }
        }
        return randomText.ToString();
        //a string doesn't seem to
    }
    public string GenerateRandomParagraph()
    {
        while(charCounter + charStringLength < stringLength) // length keeps exceeding required length
        {
            UpdateString();
            if (charLineCounter + charStringLength > line)
            {
                AddNewLines(1);
                charLineCounter = 0;
                lineCounter++;
            }
            if (lineCounter == paragraph)
            {
                AddNewLines(1);
                lineCounter = 0;
            }
            if (charCounter + charStringLength > stringLength)
            {
                break;
            }
        }
        return randomText.ToString();
        //a paragraph always exceeds 80 per line
    }
    public void UpdateString()
    {
        charCounter += charStringLength;//previous
        charLineCounter += charStringLength;//previous
        randomText.Append(charString);//add
        charString = GenerateRandomCharacter();//generate
        charStringLength = charString.Length;//next
    }
    public void AddNewLines(int newLines)
    {
        string text = string.Empty;
        for (int i = 0; i < newLines; i++)
        {
            text += "\n";
        }
        randomText.Append(text);
    }
}