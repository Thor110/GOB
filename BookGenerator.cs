using System.Text;
using System.Diagnostics;
/// <summary>
/// Generates random text, including characters, strings, and paragraphs.
/// Provides methods for generating text with customizable character ranges, string lengths, and paragraph structures.
/// </summary>
public class RandomTextGenerator
{
    public Random random = new Random();
    public string charString = string.Empty;
    public string title = string.Empty;
    public string content = string.Empty;
    public int charStringLength;
    public int rangeMin;
    public int rangeMax;
    public int charCounter;
    public int lineCounter;
    public int charLineCounter;
    public const int surrogateHighLow = 0xD800;
    public const int surrogateHighHigh = 0xDBFF;
    public const int surrogateLowLow = 0xDC00;
    public const int surrogateLowHigh = 0xDfff;
    public int stringLength;
    public int line;
    public int paragraph;
    public int[] excludedCharacters = { 92, 47, 58, 42, 63, 34, 60, 62, 124 };
    //  1,114,079 all possible UniCode characters included excluding control characters 0-32
    //  Table of Excluded Characters ( Hex, Dec, Sym ) 2,056 Excluded Characters including Surrogate Code Points
    //  0x5C    0x2F    0x3A    0x2A    0x3F    0x22    0x3C    0x3E    0x7C
    //  92      47      58      42      63      34      60      62      124
    //  \       /       :       *       ?       "       <       >       |
    public bool isFileName = true;
    public int totalContentLength;
    public int totalLines;
    public int currentLine;
    /// <summary>
    /// StopwatchWrapper class for the timer function.
    /// </summary>
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
    /// <summary>
    /// Timer for testing execution time.
    /// </summary>
    /// <param name="titleLength">The length of the title</param>
    /// <param name="contentLength">The length of the paragraph</param>
    /// <param name="lineLength">The length of each line</param>
    /// <param name="paragraphLength">The length of each paragraph</param>
    /// <param name="minRange">The minimum range of the generated character in UniCode</param>
    /// <param name="maxRange">The maximum range of the generated character in UniCode</param>
    /// <param name="operations">The number of times to execute the function</param>
    /// <param name="ranges">Lists all chosen ranges selected by the user</param>
    /// <remarks>
    /// Used when generating multiple text files.
    /// </remarks>
    public void MultipleTextFiles(int titleLength = 16, int contentLength = 800, int lineLength = 80, int paragraphLength = 40, int minRange = 40, int maxRange = 65533, int operations = 1, List<Range> ranges = null!)
    {
        //using (var stopwatch = new StopwatchWrapper())
        //{
        for (int i = 0; i < operations; i++)
        {
            //Debug.WriteLine("Iteration : " + i.ToString());
            GenerateTextFile(titleLength, contentLength, lineLength, paragraphLength, minRange, maxRange, ranges!);
            //stopwatch.Test();
        }
        //}
    }
    /// <summary>
    /// Generates a single random text file and saves it to the directory the program is executed from.
    /// </summary>
    /// <param name="titleLength">The length of the title</param>
    /// <param name="contentLength">The length of the paragraph</param>
    /// <param name="lineLength">The length of each line</param>
    /// <param name="paragraphLength">The length of each paragraph</param>
    /// <param name="minRange">The minimum range of the generated character in UniCode</param>
    /// <param name="maxRange">The maximum range of the generated character in UniCode</param>
    /// <param name="ranges">Lists all chosen ranges selected by the user</param>
    /// <remarks>
    /// Generates random text files.
    /// </remarks>
    public void GenerateTextFile(int titleLength = 16, int contentLength = 800, int lineLength = 80, int paragraphLength = 40, int minRange = 32, int maxRange = 65533, List<Range> ranges = null!)
    {
        // demote variable to exist here instead? that way they just exist when the function is called and don't need to be reset.
        isFileName = true;
        // reset specific values
        title = "";
        content = "";
        charLineCounter = 0;
        lineCounter = 0;
        charCounter = 0;
        charStringLength = 0;
        totalContentLength = 0;
        totalLines = contentLength / lineLength;
        currentLine = 0;
        int randomRange;
        Range selectedRange;
        // use different ranges
        if (ranges != null)
        {
            for (int i = 0; i < titleLength; i++)
            {
                ReturnRandomRange();
                GenerateTitle();
            }
            ReturnTitle();
            for (int i = 0; i < contentLength; i++)
            {
                ReturnRandomRange();
                GenerateContent();
            }
        }
        else
        {
            rangeMin = minRange;
            rangeMax = maxRange;
            for (int i = 0; i < titleLength; i++)
            {
                GenerateTitle();
            }
            ReturnTitle();
            for (int i = 0; i < contentLength; i++)
            {
                GenerateContent();
            }
        }
        // try catch exceptions
        try
        {
            if (File.Exists(title + ".txt"))
            {
                int counter = 1;
                string newTitle = title;
                // odds calculation is completely different if using custom preset ranges
                int baseRange = maxRange - minRange + 1; // calculate the actual odds of generating the same file twice.
                double rangePower = Math.Pow(baseRange, baseRange); // baseRange ^ baseRange ^ baseRange ^ baseRange ^ titleLength
                double totalPower = Math.Pow(rangePower, rangePower); // rangePower ^ rangePower ^ titleLength
                double actualOdds = Math.Pow(totalPower, titleLength); // totalPower ^ titleLength
                // if using custom ranges it would be
                // double customOdds = Math.Pow(actualOdds, ranges.Count);
                // I think
                // The odds also become different if using surrogate code points
                while (File.Exists(newTitle + ".txt"))
                {
                    newTitle = title + "_" + counter.ToString();
                    counter++;
                }
                File.WriteAllText(newTitle + ".txt", content);
                if (double.IsInfinity(actualOdds))
                {
                    MessageBox.Show($"The chances of that happening are virtually impossible! {actualOdds}\nOr {baseRange} ^ {baseRange} ^ {baseRange} ^ {baseRange} ^ {titleLength} to 1!");
                }
                else
                {
                    MessageBox.Show($"The chances of that happening were {actualOdds} to 1!\nOr {baseRange} ^ {baseRange} ^ {baseRange} ^ {baseRange} ^ {titleLength} to 1!");
                }
            }
            else
            {
                File.WriteAllText(title + ".txt", content);
            }
        }
        catch (ArgumentException e)
        {
            MessageBox.Show($"Processing failed: {e.Message}");
            File.AppendAllText("error.log", $"{DateTime.Now} : {e.Message} : Processing failed! " + Environment.NewLine);
            File.AppendAllText("error.log", "ArgumentException: this is thrown when a method is invoked with an argument that is invalid or outside the acceptable range.");
        }
        catch (UnauthorizedAccessException e)
        {
            MessageBox.Show("Unauthorized access exception: " + e.Message);
            File.AppendAllText("error.log", $"{DateTime.Now} : {e.Message} : Unauthorized access exception!" + Environment.NewLine);
            File.AppendAllText("error.log", "UnauthorizedAccessException: this is thrown when the application does not have the necessary permissions to access a file or resource.");
        }
        catch (IOException e)
        {
            MessageBox.Show("IO exception: " + e.Message);
            File.AppendAllText("error.log", $"{DateTime.Now} : {e.Message} : IO exception!" + Environment.NewLine);
            File.AppendAllText("error.log", "IOException: this is thrown when an I/ O operation fails, such as when trying to write to a file that is already in use or when there is not enough disk space.");
        }
        catch (Exception e)
        {
            MessageBox.Show("Unexpected exception: " + e.Message);
            File.AppendAllText("error.log", $"{DateTime.Now} : {e.Message} : Unexpected exception!" + Environment.NewLine);
            File.AppendAllText("error.log", "Exception: this is a catch-all for any other unexpected exceptions that may occur.");
        }
        // enclosed methods to reduce code duplication
        // consider promoting to reusable methods
        // that does however mean promoting the ranges variable which results in it being passed to the class then created rather than just being passed.
        void ReturnRandomRange()
        {
            randomRange = random.Next(0, ranges.Count);
            selectedRange = ranges[randomRange];
            rangeMin = selectedRange.Start.Value;
            rangeMax = selectedRange.End.Value;
        }
        void GenerateContent()
        {
            charString = GenerateRandomCharacter();
            charStringLength = charString.Length;
            if (charLineCounter + charStringLength > lineLength)
            {
                currentLine++;
                if (currentLine >= totalLines) { return; }
                AddNewLines(1);
                charLineCounter = 0;
                lineCounter++;
            }
            if (lineCounter == paragraphLength)
            {
                AddNewLines(1);
                lineCounter = 0;
            }
            charCounter += charStringLength;
            charLineCounter += charStringLength;
            content += charString;
            totalContentLength += charStringLength;
            if (totalContentLength >= contentLength) { return; }
        }
    }
    /// <summary>
    /// Generates a random character and adds it to the title string.
    /// </summary>
    public void GenerateTitle()
    {
        charString = GenerateRandomCharacter();
        title += charString;
    }
    /// <summary>
    /// Adds the title to the content string, adds two new lines and then sets filename to false.
    /// </summary>
    public void ReturnTitle()
    {
        content += title;
        AddNewLines(2);
        isFileName = false;
    }
    /// <summary>
    /// Adds new lines to the string when required.
    /// </summary>
    /// <param name="newLines">The number of new lins to add</param>
    public void AddNewLines(int newLines)
    {
        // consider extracting this method into it's own class and making iy a string method so that it can add new lines to any string it is passed.
        for (int i = 0; i < newLines; i++)
        {
            content += Environment.NewLine;
        }
    }
    /// <summary>
    /// Generates a single random character and returns it as a string.
    /// </summary>
    /// <returns>A single character as a string</returns>
    /// <remarks>
    /// Generates a new random character, excludes illegal characters and surrogate code poitns in filenames and combines surrogate code points for bodies of text.
    /// </remarks>
    public string GenerateRandomCharacter()
    {
        Int32 codePoint = random.Next(rangeMin, rangeMax);
        if (isFileName)
        {
            if (codePoint >= surrogateHighLow && codePoint <= surrogateLowHigh) // exclude surrogate code points in file names when using the surrogate code points presets
            {
                codePoint = random.Next(32, 126); // use the basic ASCII range for this case
            }
            if (excludedCharacters.Contains(codePoint)) // exclude surrogate code points and illegal characters \/:*?"<>| in file names
            {
                return GenerateRandomCharacter();
            }
        }
        else
        {
            if (codePoint >= surrogateHighLow && codePoint <= surrogateHighHigh)
            {
                int lowSurrogate = surrogateLowLow + (codePoint - surrogateHighLow);
                string highSurrogateString = ((char)codePoint).ToString();
                string lowSurrogateString = ((char)lowSurrogate).ToString();
                charStringLength = lowSurrogateString.Length;
                return highSurrogateString + lowSurrogateString; // can this be one line somewhere?
            }
            if (codePoint >= surrogateLowLow && codePoint <= surrogateLowHigh)
            {
                int highSurrogate = surrogateHighLow + (codePoint - surrogateLowLow);
                string highSurrogateString = ((char)highSurrogate).ToString();
                string lowSurrogateString = ((char)codePoint).ToString();
                charStringLength = highSurrogateString.Length;
                return highSurrogateString + lowSurrogateString; // can this be one line somewhere?
            }
        }
        return char.ConvertFromUtf32(codePoint);
    }
    /*
     * Everything below is the old method and individual reusable methods for returning a single character, string or paragraph.
     * Might or might not be worth updating.
     * Updated GenerateSingleString, now just pass ranges or not.
     */
    /// <summary>
    /// Generates a single random character and returns it as a string.
    /// If the generated character is a surrogate code point or an excluded character, generates a new character.
    /// </summary>
    /// <param name="minRange">The minimum range of the generated character in UniCode</param>
    /// <param name="maxRange">The maximum range of the generated character in UniCode</param>
    /// <param name="fileName">Whether to exclude characters that are illegal for filenames</param>
    /// <returns>A single character as a string</returns>
    /// <remarks>
    /// Sets up the required variables and then generates a random character to return as a string.
    /// </remarks>
    public string GenerateSingleCharacter(int minRange = 32, int maxRange = 65533, bool fileName = false)
    {
        isFileName = fileName;
        setupCharacter(minRange, maxRange);
        return GenerateRandomCharacter();
    }
    /// <summary>
    /// Generates a single random string and returns it as a string.
    /// If any of the generated characters are surrogate code points or excluded characters, generates a new character.
    /// </summary>
    /// <param name="stringLength">The length of the string</param>
    /// <param name="minRange">The minimum range of the generated character in UniCode</param>
    /// <param name="maxRange">The maximum range of the generated character in UniCode</param>
    /// <param name="fileName">Whether to exclude characters that are illegal for filenames</param>
    /// <returns>A single string as a string</returns>
    /// <remarks>
    /// Sets up the required variables and then generates a random string to return as a string.
    /// </remarks>
    public string GenerateSingleString(int stringLength = 16, int minRange = 32, int maxRange = 65533, bool fileName = false)
    {
        isFileName = fileName;
        // old setupString block
        charStringLength = 0;
        charString = "";
        content = "";
        rangeMin = minRange;
        rangeMax = maxRange;
        charCounter = 0;
        // ^ old setupString block
        title = ""; // reset title variable
        for (int i = 0; i < stringLength; i++)
        {
            GenerateTitle();
        }
        return title;
    }
    /// <summary>
    /// Generates a single random paragraph and returns it as a string.
    /// If any of the generated character are surrogate code points or excluded characters, generates a new character.
    /// </summary>
    /// <param name="contentLength">The length of the paragraph</param>
    /// <param name="lineLength">The length of each line</param>
    /// <param name="paragraphLength">The length of each paragraph</param>
    /// <param name="minRange">The minimum range of the generated character in UniCode</param>
    /// <param name="maxRange">The maximum range of the generated character in UniCode</param>
    /// <returns>A single paragraph as a string</returns>
    /// <remarks>
    /// Sets up the required variables and then generates a random paragraph to return as a string.
    /// </remarks>
    public string GenerateSingleParagraph(int contentLength = 800, int lineLength = 80, int paragraphLength = 40, int minRange = 32, int maxRange = 65533)
    {
        setupCharacter(minRange, maxRange);
        stringLength = contentLength;
        //setupParagraph(lineLength, paragraphLength);
        paragraph = paragraphLength;
        line = lineLength;
        charCounter = 0;
        lineCounter = 0;
        charLineCounter = 0;
        charStringLength = 0;
        charLineCounter += charStringLength;
        charStringLength = charString.Length;
        // ^ old setupParagraph block
        return GenerateRandomParagraph();
    }
    /// <summary>
    /// Setup for generating a single character.
    /// </summary>
    /// <param name="minRange">The minimum range of the generated character in UniCode</param>
    /// <param name="maxRange">The maximum range of the generated character in UniCode</param>
    /// <remarks>
    /// Sets up the required variables for generating a character.
    /// </remarks>
    public void setupCharacter(int minRange, int maxRange)
    {
        charStringLength = 0;
        charString = "";
        content = "";
        rangeMin = minRange;
        rangeMax = maxRange;
        charCounter = 0;
        charString = GenerateRandomCharacter(); // Generate the first random character and ensure it is added to the Length value. - Must be done here.
        charStringLength = charString.Length;
    }
    /// <summary>
    /// Generates a single random string and returns it as a string.
    /// </summary>
    /// <returns>A single string as a string</returns>
    /// <remarks>
    /// Check line length when generating a random string.
    /// </remarks>
    public string GenerateRandomString()
    {
        while (charCounter + charStringLength < stringLength)
        {
            if (charCounter + charStringLength > stringLength + 1) // account for zero ( + 1 )
            {
                break;
            }
            UpdateString();
        }
        return content;
    }
    /// <summary>
    /// Generates a single random paragraph and returns it as a string.
    /// </summary>
    /// <returns>A single paragraph as a string</returns>
    /// <remarks>
    /// Check line length and paragraph length when generating a random paragraph.
    /// </remarks>
    public string GenerateRandomParagraph()
    {
        while (charCounter + charStringLength < stringLength + 1) // account for zero ( + 1 )
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
        return content;
    }
    /// <summary>
    /// Updates the string after each character is generated.
    /// </summary>
    public void UpdateString()
    {
        charCounter += charStringLength;//previous length
        charLineCounter += charStringLength;//previous length
        content += charString;//add character to string
        charString = GenerateRandomCharacter();//generate
        charStringLength = charString.Length;//next length
    }
}