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
        MessageBox.Show("Book Generated!");
        //
        //testFunction(1,2,true);
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
            //debugFunctionA();
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
        string text = string.Empty;
        for (int i = 0; i < newLines; i++)
        {
            text += "\n";
        }
        randomText.Append(text);
    }
    //halting problem theory paper
    public void testFunction(int a, int b, bool c)
    {
        //logForLoop(1,2);//finite loop
        //logForLoop(3,2,true);//infinite loop
        logForLoop(a, b, c);//test loop
    }
    public void logForLoop(int a = 0, int b = 1, bool log = false)
    {
        //log
        if(!log)//check o from log
        {
            while (a < b)//check l from log
            {
                MessageBox.Show(a.ToString() + " is less than " + b.ToString());
                //forFunction();
                a++;
            }
            //testFunction(a, b, log);//stack overflow
            //logForLoop(a, b, log);//stack overflow
            //detecting a stack overflow is about as close to solving the halting problem as we can get
        }
        else// check o from log
        {
            while (a > b)//check g from log
            {
                MessageBox.Show(a.ToString() + " is more than " + b.ToString());
                //forFunction();
                a++;
            }
            //testFunction(a, b, log);//stack overflow
            //logForLoop(a, b, log);//stack overflow
            //detecting a stack overflow is about as close to solving the halting problem as we can get
        }
    }
    public void forFunction()
    {
        //
    }
    public void thisIsASimplifiedForLoop(int a, int b)
    {
        //a for loop is just a while loop
        var c = 0;
        var d = 1;
        while (c < d)
        {
            c++;
        }
        //this would run once
        //
        //can the halting problem actually be solved?!
        //less than
        while (a < b)
        {
            a++;
        }
        //if a is ever to be greater than b then it will finish executing
        //if b is always greater than a it will run forever
        //
        //greater than
        while (a > b)
        {
            b--;
        }
        //if a is always greater than b it will finish executing
        //if b is never to be less than a it will run forever
        //
        //of course determining this for a large program would be a lot more challenging
        //but in simple terms it should be will it count up or down forever or not
        //
        //great now I gotta write a paper on the halting problem too
        //
        //in the context of an infinite set, how can you ever know if it would finish...
        //limitations imposed by the storing variables ie : Int32 or Int64
        //
        //or with a game as an example
        //can the player ever go everywhere and do everything at once? no? then it will run forever without a player
        //
        //examples ( less than )
        while (a < b)
        {
            a++;
            b--;
        }
        //yes will halt, will not run forever.
        while (a < b)
        {
            a--;
            b++;
        }
        //no wont halt, will run forever.
        while (a < b)
        {
            a++;
            b++;
        }
        //no wont halt, will run forever.
        while (a < b)
        {
            a--;
            b--;
        }
        //no wont halt, will run forever.
        //
        //examples ( greater than )
        while (a > b)
        {
            b--;
            a++;
        }
        //yes will halt, will not run forever.
        while (a > b)
        {
            b++;
            a--;
        }
        //no wont halt, will run forever.
        while (a > b)
        {
            b++;
            a++;
        }
        //no wont halt, will run forever.
        while (a > b)
        {
            b--;
            a--;
        }
        //no wont halt, will run forever.
        //
        //something something elsewhere up down left right maybe
        //two ends of an algorithm chasing in both directions to see if they can reach an upper limit
        //game design in theory is a perfect / prime example of the halting problem
        //in that without user interaction a game should run forever
        //
        //is the halting problem really a problem?
        //
        //in theory an algorith must exist to prove whether or not a program would or wouldn't run forever
        //it should look something like this
        //01010 - the program
        //11010 - would run forever
        //01011 - would halt or crash
        //simplified to a five bit binary description to showcase that the algorithm would have to search both ways through the program
        //to look for the answer
        //
        //writing papers in my programs...
        //
        //a thought arises, if it's so easy to make a program that will loop forever, shouldn't it be possible to determine that algorithmically?
        //
        //approximate AI + TAS INTERFACE + PROGRAM
        //
        //or AUTOMATIC INTERFACE TESTING ALL POSSIBLE INPUTS IN ALL POSSIBLE CONFIGURATIONS USING A TAS INTERFACE ON A PROGRAM
        //
        //
    }
}