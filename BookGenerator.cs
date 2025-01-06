using System.Text;
using System.Diagnostics;
/// <summary>
/// Generates random text, including characters, strings, and paragraphs.
/// Provides methods for generating text with customizable character ranges, string lengths, and paragraph structures.
/// </summary>
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
    public const int surrogateLow = 55296;
    public const int surrogateHigh = 57343;
    // Surrogate Code Points ( Low - High )
    // 55296    - 57343
    // 0xd800   - 0xdfff
    public int stringLength;
    public int line;
    public int paragraph;
    public int[] excludedCharacters = { 92, 47, 58, 42, 63, 34, 60, 62, 124 };
    //  1,112,055 all possible UniCode characters included
    //  Table of Excluded Characters ( Hex, Dec, Sym ) 2,056 Excluded Characters including Surrogate Code Points
    //  0x5C    0x2F    0x3A    0x2A    0x3F    0x22    0x3C    0x3E    0x7C
    //  92      47      58      42      63      34      60      62      124
    //  \       /       :       *       ?       "       <       >       |
    public bool isFileName = true;
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
    public void MultipleTextFiles(int titleLength = 16, int contentLength = 800, int lineLength = 80, int paragraphLength = 40, int minRange = 40, int maxRange = 1114111, int operations = 1)
    {
        //using (var stopwatch = new StopwatchWrapper())
        //{
            for (int i = 0; i < operations; i++)
            {
                //Debug.WriteLine("Iteration : " + i.ToString());
                GenerateTextFile(titleLength, contentLength, lineLength = 80, paragraphLength = 40, minRange = 32, maxRange = 1114111);
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
    public void GenerateTextFile(int titleLength = 16, int contentLength = 800, int lineLength = 80, int paragraphLength = 40, int minRange = 32, int maxRange = 1114111)
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
    }
    /// <summary>
    /// Setup for generating a single character.
    /// </summary>
    /// <param name="minRange">The minimum range of the generated character in UniCode</param>
    /// <param name="maxRange">The maximum range of the generated character in UniCode</param>
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
    /// <summary>
    /// Setup for generating a single string.
    /// </summary>
    /// <param name="length">The length of the string</param>
    public void setupString(int length)
    {
        stringLength = length;
    }
    /// <summary>
    /// Setup for generating a single paragraph.
    /// </summary>
    /// <param name="lineLength">The length of each line</param>
    /// <param name="paragraphLength">The length of each paragraph</param>
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
    /// <summary>
    /// Generates a single random character and returns it as a string.
    /// If the generated character is a surrogate code point or an excluded character, generates a new character.
    /// </summary>
    /// <param name="minRange">The minimum range of the generated character in UniCode</param>
    /// <param name="maxRange">The maximum range of the generated character in UniCode</param>
    /// <param name="fileName">Whether to exclude characters that are illegal for filenames</param>
    /// <returns>A single character as a string</returns>
    /// <remarks>
    /// If the generated character is a surrogate code point or an excluded character, generates a new character.
    /// </remarks>
    public string GenerateSingleCharacter(int minRange = 32, int maxRange = 1114111, bool fileName = false)
    {
        setupCharacter(minRange, maxRange);
        isFileName = fileName;
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
    /// If any of the generated characters are surrogate code points or excluded characters, generates a new character.
    /// </remarks>
    public string GenerateSingleString(int stringLength = 16, int minRange = 32, int maxRange = 1114111, bool fileName = false)
    {
        setupCharacter(minRange, maxRange);
        setupString(stringLength);
        isFileName = fileName;
        return GenerateRandomString();
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
    /// <param name="fileName">Whether to exclude characters that are illegal for filenames</param>
    /// <returns>A single paragraph as a string</returns>
    /// <remarks>
    /// If any of the generated characters are surrogate code points or excluded characters, generates a new character.
    /// </remarks>
    public string GenerateSingleParagraph(int contentLength = 800, int lineLength = 80, int paragraphLength = 40, int minRange = 32, int maxRange = 1114111, bool fileName = false)
    {
        setupCharacter(minRange, maxRange);
        setupString(contentLength);
        setupParagraph(lineLength, paragraphLength);
        isFileName = fileName;
        return GenerateRandomParagraph();
    }
    /// <summary>
    /// Generates a single random character and returns it as a string.
    /// </summary>
    /// <returns>A single character as a string</returns>
    /// <remarks>
    /// If the generated character is a surrogate code point or an excluded character, generates a new character.
    /// </remarks>
    public string GenerateRandomCharacter()
    {
        Int32 codePoint = random.Next(rangeMin, rangeMax); // exclude surrogate code points or exclude \/:*?"<>| in file names
        if (codePoint >= surrogateLow && codePoint <= surrogateHigh || isFileName && excludedCharacters.Contains(codePoint))
        {
            return GenerateRandomCharacter();
        }
        return char.ConvertFromUtf32(codePoint);
    }
    /// <summary>
    /// Generates a single random string and returns it as a string.
    /// </summary>
    /// <returns>A single string as a string</returns>
    /// <remarks>
    /// If any of the generated characters are surrogate code points or excluded characters, generates a new character.
    /// </remarks>
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
    /// <summary>
    /// Generates a single random paragraph and returns it as a string.
    /// </summary>
    /// <returns>A single paragraph as a string</returns>
    /// <remarks>
    /// If any of the generated characters are surrogate code points or excluded characters, generates a new character.
    /// </remarks>
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
    /// <summary>
    /// Updates the string after each character is generated.
    /// </summary>
    public void UpdateString()
    {
        charCounter += charStringLength;//previous length
        charLineCounter += charStringLength;//previous length
        randomText.Append(charString);//add character to string
        charString = GenerateRandomCharacter();//generate
        charStringLength = charString.Length;//next length
    }
    /// <summary>
    /// Adds new lines to the string when required.
    /// </summary>
    /// <param name="newLines">The number of new lins to add</param>
    public void AddNewLines(int newLines)
    {
        for (int i = 0; i < newLines; i++)
        {
            randomText.Append(Environment.NewLine);
        }
    }
}