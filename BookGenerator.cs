using System.Text;
using System.Diagnostics;
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
    //  Table of Excluded Characters ( Hex, Dec, Sym ) 2,056 Characters including Surrogate Code Points
    //  0x5C    0x2F    0x3A    0x2A    0x3F    0x22    0x3C    0x3E    0x7C
    //  92      47      58      42      63      34      60      62      124
    //  \       /       :       *       ?       "       <       >       |
    public bool isFileName = true;
    public class StopwatchWrapper : IDisposable
    {
        private readonly Stopwatch stopwatch;
        public StopwatchWrapper()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }
        public void Dispose()
        {
            stopwatch.Stop();
            Debug.WriteLine($"Function Disposal took {stopwatch.ElapsedMilliseconds} milliseconds to execute.");
        }
        public void Test()
        {
            Debug.WriteLine($"Code block took {stopwatch.ElapsedMilliseconds} milliseconds to execute.");
            File.AppendAllText("timer.log", $"Code block took: {stopwatch.ElapsedMilliseconds} milliseconds to execute." + Environment.NewLine);
        }
    }
    public void TimedTextFile(int titleLength, int contentLength, int lineLength, int paragraphLength, int minRange, int maxRange, int operations)
    {
        using (var stopwatch = new StopwatchWrapper())
        {
            for (int i = 0; i < operations; i++)
            {
                Debug.WriteLine("Iteration : " + i.ToString());
                GenerateTextFile(titleLength, contentLength, lineLength, paragraphLength, minRange, maxRange);
                stopwatch.Test();
            }
        }
    }
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
        try
        {
            if (File.Exists(title + ".txt"))
            {
                int counter = 1;
                string newTitle = title;
                while (File.Exists(newTitle + ".txt"))
                {
                    newTitle = title + "_" + counter.ToString();
                    counter++;
                }
                //likelihood of the file already existing is 1,112,055^1,112,055^titleLength(default 16) = 1,236,666,323,025^16 / 1
                File.WriteAllText(newTitle + ".txt", content);
                MessageBox.Show("The chances of that happening were 1,236,666,323,025^16 to 1");
                MessageBox.Show("or 2,992,544,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000 to 1");
                //2,992,544,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000,000
                //1,236,666,323,025^16
                //2.992544e+193
            }
            else
            {
                File.WriteAllText(title + ".txt", content);
            }
        }
        catch (ArgumentException e)
        {
            MessageBox.Show($"Processing failed: {e.Message}");
            File.AppendAllText("error.log", $"Processing failed: {e.Message}{DateTime.Now}" + Environment.NewLine);
        }
        catch (UnauthorizedAccessException e)
        {
            Debug.WriteLine("Unauthorized access exception: " + e.Message);
            File.AppendAllText("error.log", $"Processing failed: {e.Message}{DateTime.Now}" + Environment.NewLine);
        }
        catch (IOException e)
        {
            Debug.WriteLine("IO exception: " + e.Message);
            File.AppendAllText("error.log", $"Processing failed: {e.Message}{DateTime.Now}" + Environment.NewLine);
        }
        catch (Exception e)
        {
            Debug.WriteLine("Unexpected exception: " + e.Message);
            File.AppendAllText("error.log", $"Processing failed: {e.Message}{DateTime.Now}" + Environment.NewLine);
        }
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
        while(charCounter + charStringLength < stringLength)
        {
            if (charCounter + charStringLength > stringLength)
            {
                break;
            }
            UpdateString();
        }
        return randomText.ToString();
    }
    public string GenerateRandomParagraph()
    {
        while(charCounter + charStringLength < stringLength)
        {
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
            UpdateString();
        }
        return randomText.ToString();
    }
    public void UpdateString()
    {
        charCounter += charStringLength;//previous length
        charLineCounter += charStringLength;//previous length
        randomText.Append(charString);//add character to string
        charString = GenerateRandomCharacter();//generate
        charStringLength = charString.Length;//next length
    }
    public void AddNewLines(int newLines)
    {
        for (int i = 0; i < newLines; i++)
        {
            randomText.Append(Environment.NewLine);
        }
    }
}