using System.Text;
using System.Text.RegularExpressions;
/// <summary>
/// Generates random text, including characters, strings, and paragraphs.
/// Provides methods for generating text with customizable character ranges, string lengths, and paragraph structures.
/// </summary>
public class RandomTextGenerator
{
    public Random random = new Random();
    public string characterString = string.Empty;
    public string filePath = string.Empty;
    public const int surrogateHighLow = 0xD800;
    public const int surrogateHighHigh = 0xDBFF;
    public const int surrogateLowLow = 0xDC00;
    public const int surrogateLowHigh = 0xDfff;
    public int rangeMin;
    public int rangeMax;
    public StringBuilder contentString = new StringBuilder();
    public StringBuilder titleString = new StringBuilder();
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
    /// <param name="textFile">Whether to generate a text file.</param>
    /// <param name="contentType">Whether to generate a character or paragraph.</param>
    /// <remarks>
    /// The two booleans "textFile" and "contentType" can be used to generate four different types of text:
    /// 00 = Character  -   just generates a random character stored in charString the variable.
    /// 01 = String     -   just generates a random string stored in title the variable.
    /// 10 = Paragraph  -   just generates a random paragraph stored in the content variable.
    /// 11 = Text File  -   just generates a random text file saved locally and stored in the content variable.
    /// Note: If the "textFile" and "contentType" parameters are not specified, the method will generate a text file by default.
    /// </remarks>
    public void GenerateRandomText(int titleLength = 16, int contentLength = 800, int lineLength = 80, int paragraphLength = 40, int minRange = 32, int maxRange = 65533, List<Range> ranges = null!, bool textFile = true, bool contentType = true)
    {
        contentString = new StringBuilder(contentLength);
        titleString = new StringBuilder(titleLength);
        int charStringLength;
        int lineCounter = 0;
        int charLineCounter = 0;
        int totalContentLength = 0;
        int totalLines = contentLength / lineLength;
        int currentLine = 0;
        int randomRange;
        Range selectedRange;
        // use different ranges
        //inlining everything within the following if/else statement improves efficiency by about 30-40%
        //but it heavily duplicates code...
        // TODO: Inline these function for performance optimization
        // Inlining these functions provides around a 0.7 second increase in speed when generating a text file with 80 million characters.
        // Current : ~2.6 seconds    Inlined : ~1.9 seconds
        if (ranges != null)
        {
            switch (textFile, contentType)
            {
                case (false, false):    // 00 = Character
                    ReturnRandomRange();    // TODO: Inline
                    characterString = GenerateRandomCharacter();
                    return;
                case (false, true):     // 01 = String
                    RandomRangeTitle();     // TODO: Inline
                    return;
                case (true, false):     // 10 = Paragraph
                    RandomRangeContent();   // TODO: Inline
                    return;
                case (true, true):      // 11 = Text File
                    RandomRangeTitle();     // TODO: Inline
                    AppendTitle();          // TODO: Inline
                    RandomRangeContent();   // TODO: Inline
                    break;
            }
        }
        else
        {
            rangeMin = minRange;
            rangeMax = maxRange;
            switch (textFile, contentType)
            {
                case (false, false):    // 00 = Character
                    characterString = GenerateRandomCharacter();
                    return;
                case (false, true):     // 01 = String
                    Title();                // TODO: Inline
                    return;
                case (true, false):     // 10 = Paragraph
                    Content();              // TODO: Inline
                    return;
                case (true, true):      // 11 = Text File
                    Title();                // TODO: Inline
                    AppendTitle();          // TODO: Inline
                    Content();              // TODO: Inline
                    break;
            }
        }
        /// <summary>
        /// Sets the rangeMin and rangeMax values to a random range within the ranges list.
        /// </summary>
        // TODO: Inline this function for performance optimization
        void ReturnRandomRange()
        {
            randomRange = random.Next(0, ranges.Count);
            selectedRange = ranges[randomRange];
            rangeMin = selectedRange.Start.Value;
            rangeMax = selectedRange.End.Value;
        }
        /// <summary>
        /// Generates a random title string within a custom set of ranges.
        /// </summary>
        // TODO: Inline this function for performance optimization
        void RandomRangeTitle()
        {
            for (int i = 0; i < titleLength; i++)
            {
                ReturnRandomRange();        // TODO: Inline
                GenerateTitle();            // TODO: Inline
            }
        }
        /// <summary>
        /// Generates a random paragraph string within a custom set of ranges.
        /// </summary>
        // TODO: Inline this function for performance optimization
        void RandomRangeContent()
        {
            for (int i = 0; i < contentLength; i++)
            {
                ReturnRandomRange();        // TODO: Inline
                GenerateContent();          // TODO: Inline
            }
        }
        /// <summary>
        /// Generates a random title string.
        /// </summary>
        // TODO: Inline this function for performance optimization
        void Title()
        {
            for (int i = 0; i < titleLength; i++)
            {
                GenerateTitle();            // TODO: Inline
            }
        }
        /// <summary>
        /// Generates a random paragraph string.
        /// </summary>
        // TODO: Inline this function for performance optimization
        void Content()
        {
            for (int i = 0; i < contentLength; i++)
            {
                GenerateContent();          // TODO: Inline
            }
        }
        /// <summary>
        /// Generates a random character and adds it to the title string.
        /// </summary>
        // TODO: Inline this function for performance optimization
        void GenerateTitle()
        {
            characterString = GenerateRandomCharacter();
            titleString.Append(characterString);
        }
        /// <summary>
        /// Adds the title to the content string, adds two new lines and then sets filename to false.
        /// </summary>
        // TODO: Inline this function for performance optimization
        void AppendTitle()
        {
            contentString.Append(titleString);
            contentString.AddNewLines(2); // DO NOT INLINE
        }
        /// <summary>
        /// Generates a new character for a random paragraph string.
        /// </summary>
        // TODO: Inline this function for performance optimization
        void GenerateContent()
        {
            characterString = GenerateRandomCharacter();
            charStringLength = characterString.Length;
            if (charLineCounter + charStringLength > lineLength)
            {
                currentLine++;
                if (currentLine >= totalLines) { return; }
                contentString.AddNewLines(1); // DO NOT INLINE
                charLineCounter = 0;
                lineCounter++;
            }
            if (lineCounter == paragraphLength)
            {
                contentString.AddNewLines(1); // DO NOT INLINE
                lineCounter = 0;
            }
            charLineCounter += charStringLength;
            totalContentLength += charStringLength;
            contentString.Append(characterString);
            if (totalContentLength >= contentLength) { return; }
        }
        // TODO: Inline the above functions to increase performance
        // NOTE: Moving the below code to it's own method only serves to slow down code execution
        try
        {
            //  Table of Excluded Characters can be found in GetInvalidFileNameChars
            string sanitizedTitle = Regex.Replace(titleString.ToString(), $"[{Regex.Escape(new string(Path.GetInvalidFileNameChars()))}]", " ");
            if (File.Exists(sanitizedTitle + ".txt")) // calculate the actual odds of generating the same filename twice.
            {
                int counter = 1;
                int baseRange = rangeMax - rangeMin; // default baseRange calculation
                if (ranges != null) // if using custom ranges
                {
                    baseRange = 0; // reset baseRange to 0 to calculate from custom ranges
                    foreach (Range range in ranges)
                    {
                        baseRange += range.End.Value - range.Start.Value;
                    }
                }
                double rangePower = Math.Pow(baseRange, baseRange); // baseRange ^ baseRange ^ baseRange ^ baseRange ^ titleLength
                double totalPower = Math.Pow(rangePower, rangePower); // rangePower ^ rangePower ^ titleLength
                double actualOdds = Math.Pow(totalPower, titleLength); // totalPower ^ titleLength
                while (File.Exists(sanitizedTitle + ".txt"))
                {
                    sanitizedTitle = sanitizedTitle + "_" + counter++.ToString("D2");
                }
                if (double.IsInfinity(actualOdds))
                {
                    MessageBox.Show($"The chances of that happening are virtually impossible! {actualOdds}\nOr {baseRange} ^ {baseRange} ^ {baseRange} ^ {baseRange} ^ {titleLength} to 1!");
                }
                else
                {
                    MessageBox.Show($"The chances of that happening were {actualOdds} to 1!\nOr {baseRange} ^ {baseRange} ^ {baseRange} ^ {baseRange} ^ {titleLength} to 1!");
                }
            }
            File.WriteAllText(filePath + sanitizedTitle + ".txt", contentString.ToString());
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
    /// Generates a single random character and returns it as a string.
    /// </summary>
    /// <returns>A single character as a string</returns>
    /// <remarks>
    /// Generates a new random character, excludes illegal characters and combines surrogate code points for bodies of text.
    /// </remarks>
    public string GenerateRandomCharacter()
    {
        int attempts = 0;
        const int maxAttempts = 1000;
        Int32 codePoint = random.Next(rangeMin, rangeMax + 1); // add 1 to the max range to include the last character in the range
        while (attempts < maxAttempts)
        {
            attempts++;
            if (codePoint >= surrogateHighLow && codePoint <= surrogateHighHigh) // high surrogate code point
            {
                return ((char)codePoint).ToString() + ((char)(surrogateLowLow + (codePoint - surrogateHighLow))).ToString();
            }
            if (codePoint >= surrogateLowLow && codePoint <= surrogateLowHigh) // low surrogate code point
            {
                return ((char)(surrogateHighLow + (codePoint - surrogateLowLow))).ToString() + ((char)codePoint).ToString();
            }
            return char.ConvertFromUtf32(codePoint);
        }
        MessageBox.Show("Failed to generate a valid character after " + maxAttempts + " attempts.");
        throw new InvalidOperationException("Failed to generate a valid character after " + maxAttempts + " attempts.");
    }
}