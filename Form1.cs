using ALTViewer;
using Microsoft.Win32;

namespace VideoLOB
{
    /// <summary>
    /// Library of Babel generater form.
    /// </summary>
    public partial class Form1 : Form
    {
        RandomTextGenerator TextGenerator = new RandomTextGenerator();
        //Timer TestTimer = new TestTimer();
        private ToolTip tooltip = new ToolTip();
        private Type[] excludedControlTypes = new Type[] { typeof(Panel), typeof(TableLayoutPanel), typeof(FlowLayoutPanel), typeof(Label), typeof(Button) };
        private int titleLength;
        private int contentLength;
        private int lineLength;
        private int paragraphLength;
        private int minRange;
        private int maxRange;
        private int operations;
        private string custom = "Custom";
        private bool generateMultipleFiles;
        private List<Range> presetRanges = new List<Range>();
        private string folderPath = "";
        private RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Gallery\Settings")!;
        private string selectedLanguage = "";
        private (string language, int min, int max, string description) selectedPreset;
        private Range rangePreset = new Range();
        // matrix variables
        FullScreen fullScreen;
        Boolean matrixRunning = true;
        Boolean matrixStop;
        private class StreamColumn
        {
            public int X;
            public int Y;
            public int Length;
            public List<string> Glyphs = new List<string>();
        }
        private List<StreamColumn> columns = new List<StreamColumn>();
        private Random random = new Random();
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private int charSize = 16;
        private Font font = new Font("Segoe", 9, FontStyle.Bold);
        private readonly Dictionary<string, Font> FontObjectCache = new Dictionary<string, Font>();
        private readonly List<(int min, int max, string fontName)> RangeToBestFont = new List<(int, int, string)>
        {
            // PRIORITY: Highest code points (Supplementary Planes) first for faster checks
            // EMOJIS & PICTOGRAPHS (Presets 100-105: Emojis, Dingbats, etc.)
            (0x1F000, 0x1FAFF, "Segoe UI Emoji"),
            // ANCIENT SCRIPTS (Presets 27, 28, 30-37, 48: Hieroglyphs, Cuneiform, etc.)
            (0x10000, 0x13FFF, "Segoe UI Historic"),
            // CJK EXTENSION PLANES (Chinese/Japanese/Korean)
            (0x3400, 0x4DBF, "Microsoft YaHei"), // CJK Unified Ideographs Extension A (Preset 96)
            // COMMON SYMBOLS & OTHER COMPLEX SCRIPTS (BMP)
            (0x2500, 0x27FF, "Segoe UI Symbol"), // Box Drawing, Geometric Shapes, Arrows, Dingbats (Presets 65-68, 55)
            (0x0E00, 0x0FFF, "Nirmala UI"),      // Indic/Thai/Tibetan scripts (Presets 10, 23, 45)
            (0x0900, 0x0DFF, "Nirmala UI"),      // Devanagari, Gurmukhi, Telugu, etc. (Presets 5, 12, 13, 17-20, 43)
            (0x0600, 0x07FF, "Times New Roman"), // Arabic/Syriac/Thaana (Presets 2, 41, 42). T_N_R has good Arabic.
            (0x0400, 0x05FF, "Times New Roman"), // Cyrillic/Armenian (Presets 4, 40)
            (0x0370, 0x03FF, "Times New Roman"), // Greek (Presets 6, 29)
            // KOREAN/JAPANESE HANGUL (Presets 8, 9, 52, 53, 99)
            (0xAC00, 0xD7AF, "Malgun Gothic"),
            (0x3040, 0x4E00, "Meiryo"), // Hiragana/Katakana
            // FINAL BROAD-COVERAGE FALLBACKS
            (0x0000, 0xFFFF, "Arial Unicode MS"), // Catch-all for basic multilingual plane
            (0x0000, 0xFFFF, "Microsoft Sans Serif"), // Last resort
        };
        public Form1()
        {
            InitializeComponent();
            InitializeRegistry();
            InitializeTooltips();
            InitializePresets();
            fullScreen = new FullScreen(this);
            this.KeyDown += Escape_KeyDown!;
            timer.Interval = 50;    // ~20fps, smooth  
            timer.Tick += (s, e) => { UpdateColumns(); this.Invalidate(); };
        }
        /// <summary>
        /// Initialises the registry.
        /// </summary>
        private void InitializeRegistry()
        {
            if (key != null)
            {
                folderPath = key.GetValue("Directory")!.ToString()!;
                TextGenerator.filePath = folderPath;
                directoryBox.Text = folderPath;
            }
            else
            {
                key = Registry.CurrentUser.CreateSubKey(@"Gallery\Settings");
                folderPath = Application.StartupPath;
                directoryBox.Text = folderPath;
                key.SetValue("Directory", folderPath);
            }
            key.Close();
        }
        /// <summary>
        /// Initialises tooltips for all controls.
        /// </summary>
        private void InitializeTooltips()
        {
            components = new System.ComponentModel.Container();
            tooltip = new ToolTip(components);
            foreach (Control control in Controls)
            {
                if (excludedControlTypes.Contains(control.GetType()) != true)
                {
                    control.MouseEnter += new EventHandler(tooltip_MouseEnter);
                    control.MouseLeave += new EventHandler(tooltip_MouseLeave);
                }
            }
        }
        /// <summary>
        /// Initialises Unicode presets and other controls.
        /// </summary>
        private void InitializePresets()
        {
            foreach (var range in unicodeRanges.Values) { comboBox1.Items.Add(range.language); }
            comboBox1.SelectedIndex = 58; // "Basic Unicode Range"
            channelsBox.SelectedIndex = 0;
            depthBox.SelectedIndex = 0;
        }
        /// <summary>
        /// Tooltip mouse event handlers.
        /// </summary>
        void tooltip_MouseEnter(object? sender, EventArgs e)
        {
            Control control = (Control)sender!;
            if (control.AccessibleDescription != null) { tooltip.Show(control.AccessibleDescription.ToString(), control); }
            else { tooltip.Show("No description available", control); }
        }
        void tooltip_MouseLeave(object? sender, EventArgs e) { tooltip.Hide((Control)sender!); }
        /// <summary>
        /// Output directory folder browser button.
        /// </summary>
        private void output_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                folderPath = folderBrowserDialog.SelectedPath;
                if (!folderPath.EndsWith("\\")) { folderPath += "\\"; } // If not root directory, complete directory string
                TextGenerator.filePath = folderPath;
                directoryBox.Text = folderPath;
                key = Registry.CurrentUser.OpenSubKey(@"Gallery\Settings", true)!;
                key.SetValue("Directory", folderPath);
                key.Close();
            }
        }
        /// <summary>
        /// Seed string generator (incomplete function)
        /// </summary>
        private string GenerateSeedString()
        {
            // Generate a seed string that represents the video
            // This can be done using a combination of algorithms, such as hash functions and lossy compression
            // For simplicity, let's just use a random string
            return Guid.NewGuid().ToString();
        }
        /// <summary>
        /// Audio generator button.
        /// </summary>
        private void GenerateAudioButton_Click(object sender, EventArgs e)
        {
            //string seedString = GenerateSeedString();
            //seedText.Text = seedString;
            CollectNameVariables();
            string filePath;
            string message;
            // Define the audio parameters
            int sampleRate = (int)sampleNumeric.Value;// Sample rate in Hz
            int frequency = (int)frequencyNumeric.Value;// Frequency in Hz
            int duration = (int)durationNumeric.Value;// Duration in seconds
            // default numChannels and bitDepth for WAV header
            short numChannels = Convert.ToInt16(channelsBox.SelectedItem.ToString());
            short bitDepth = Convert.ToInt16(depthBox.SelectedItem.ToString());
            if (generateMultipleFiles)
            {
                message = "Audio Files Generated!";
                for (int i = 0; i < operations; i++)
                {
                    TextGenerator.GenerateRandomText(titleLength, contentLength, lineLength, paragraphLength, minRange, maxRange, presetRanges.Count != 0 ? presetRanges! : null!, false, true);
                    filePath = folderPath + TextGenerator.titleString + ".wav";
                    AudioGenerator.GenerateAudio(filePath, sampleRate, frequency, duration, numChannels, bitDepth);
                }
            }
            else
            {
                message = "Audio Generated!";
                TextGenerator.GenerateRandomText(titleLength, contentLength, lineLength, paragraphLength, minRange, maxRange, presetRanges.Count != 0 ? presetRanges! : null!, false, true);
                filePath = folderPath + TextGenerator.titleString + ".wav";
                AudioGenerator.GenerateAudio(filePath, sampleRate, frequency, duration, numChannels, bitDepth);
            }
            MessageBox.Show(message);
        }
        /// <summary>
        /// Image generator button.
        /// </summary>
        private void GenerateImageButton_Click(object sender, EventArgs e)
        {
            CollectNameVariables();
            string filePath;
            string message;
            int width = (int)widthNumeric.Value;
            int height = (int)heightNumeric.Value;
            if (generateMultipleFiles)
            {
                message = "Images Generated!";
                for (int i = 0; i < operations; i++)
                {
                    TextGenerator.GenerateRandomText(titleLength, contentLength, lineLength, paragraphLength, minRange, maxRange, presetRanges.Count != 0 ? presetRanges! : null!, false, true);
                    filePath = folderPath + TextGenerator.titleString + ".png";
                    ImageGenerator.GenerateImage(filePath, width, height);
                }
            }
            else
            {
                message = "Image Generated!";
                TextGenerator.GenerateRandomText(titleLength, contentLength, lineLength, paragraphLength, minRange, maxRange, presetRanges.Count != 0 ? presetRanges! : null!, false, true);
                filePath = folderPath + TextGenerator.titleString + ".png";
                ImageGenerator.GenerateImage(filePath, width, height);
            }
            MessageBox.Show(message);
        }
        /// <summary>
        /// Video generator button.
        /// </summary>
        private void GenerateVideoButton_Click(object sender, EventArgs e)
        {
            //string seedString = GenerateSeedString();
            //seedText.Text = seedString;
            CollectNameVariables();
            string filePath;
            string message;
            int width = (int)widthNumeric.Value;
            int height = (int)heightNumeric.Value;
            int frameRate = (int)videoFramerateNumeric.Value;
            int duration = (int)videoDurationNumeric.Value;
            if (generateMultipleFiles)
            {
                message = "Video Files Generated!";
                for (int i = 0; i < operations; i++)
                {
                    TextGenerator.GenerateRandomText(titleLength, contentLength, lineLength, paragraphLength, minRange, maxRange, presetRanges.Count != 0 ? presetRanges! : null!, false, true);
                    filePath = folderPath + TextGenerator.titleString + ".avi";
                    VideoGenerator.GenerateVideo(filePath, width, height, frameRate, duration);
                }
            }
            else
            {
                message = "Video Generated!";
                TextGenerator.GenerateRandomText(titleLength, contentLength, lineLength, paragraphLength, minRange, maxRange, presetRanges.Count != 0 ? presetRanges! : null!, false, true);
                filePath = folderPath + TextGenerator.titleString + ".avi";
                VideoGenerator.GenerateVideo(filePath, width, height, frameRate, duration);
            }
            MessageBox.Show(message);
        }
        /// <summary>
        /// 3D Model generator button.
        /// </summary>
        private void GenerateModelButton_Click(object sender, EventArgs e)
        {
            CollectNameVariables();
            int numVertices = (int)verticesNumeric.Value;
            int numFaces = (int)facesNumeric.Value;
            int scale = (int)scaleNumeric.Value;
            bool generateSolid = checkBox1.Checked;
            string filePath;
            string message;
            if (generateMultipleFiles)
            {
                message = "Models Generated!";
                for (int i = 0; i < operations; i++)
                {
                    TextGenerator.GenerateRandomText(titleLength, contentLength, lineLength, paragraphLength, minRange, maxRange, presetRanges.Count != 0 ? presetRanges! : null!, false, true);
                    filePath = folderPath + TextGenerator.titleString + ".obj";
                    ModelGenerator.GenerateModel(numVertices, numFaces, scale, filePath, generateSolid);
                }
            }
            else
            {
                message = "Model Generated!";
                TextGenerator.GenerateRandomText(titleLength, contentLength, lineLength, paragraphLength, minRange, maxRange, presetRanges.Count != 0 ? presetRanges! : null!, false, true);
                filePath = folderPath + TextGenerator.titleString + ".obj";
                ModelGenerator.GenerateModel(numVertices, numFaces, scale, filePath, generateSolid);
            }
            MessageBox.Show(message);
        }
        /// <summary>
        /// Book generator button.
        /// </summary>
        private void GenerateBookButton_Click(object sender, EventArgs e)
        {
            CollectNameVariables();
            string message;
            if (comboBox2.Items.Count == 1)
            {
                MessageBox.Show("Add more than one preset to the list.");
                return;
            }
            if (generateMultipleFiles)
            {
                message = "Books Generated!";
                //TestTimer.StartTimer($"GenerateRandomText * {operations}");
                for (int i = 0; i < operations; i++) { TextGenerator.GenerateRandomText(titleLength, contentLength, lineLength, paragraphLength, minRange, maxRange, presetRanges.Count != 0 ? presetRanges! : null!, true, true); }
                //TestTimer.StopTimer($"GenerateRandomText * {operations}");
            }
            else
            {
                message = "Book Generated!";
                //TestTimer.StartTimer("GenerateRandomText");
                TextGenerator.GenerateRandomText(titleLength, contentLength, lineLength, paragraphLength, minRange, maxRange, presetRanges.Count != 0 ? presetRanges! : null!, true, true);
                //TestTimer.StopTimer("GenerateRandomText");
            }
            MessageBox.Show(message);
        }
        /// <summary>
        /// Collet named variables for generating filenames.
        /// </summary>
        private void CollectNameVariables()
        {
            titleLength = (int)titleNumeric.Value;
            contentLength = (int)contentNumeric.Value;
            lineLength = (int)lineNumeric.Value;
            paragraphLength = (int)paragraphNumeric.Value;
            minRange = trackBarMin.Value;
            maxRange = trackBarMax.Value;
        }
        /// <summary>
        /// Minimum Unicode Range
        /// </summary>
        private void trackBarMin_Scroll(object sender, EventArgs e)
        {
            if (trackBarMin.Value > trackBarMax.Value)
            {
                trackBarMax.Value = trackBarMin.Value;
                updateLabel2();
            }
            updateLabel1();
            updateDescription();
        }
        /// <summary>
        /// Maximum Unicode Range
        /// </summary>
        private void trackBarMax_Scroll(object sender, EventArgs e)
        {
            if (trackBarMax.Value < trackBarMin.Value)
            {
                trackBarMin.Value = trackBarMax.Value;
                updateLabel1();
            }
            updateLabel2();
            updateDescription();
        }
        /// <summary>
        /// Update description and Unicode combo box.
        /// </summary>
        private void updateDescription()
        {
            comboBox1.Text = custom;
            richTextBox1.Text = custom;
        }
        /// <summary>
        /// Update minimum Unicode range label.
        /// </summary>
        private void updateLabel1() { label1.Text = trackBarMin.Value.ToString(); }
        /// <summary>
        /// Update maximum Unicode range label.
        /// </summary>
        private void updateLabel2() { label2.Text = trackBarMax.Value.ToString(); }
        /// <summary>
        /// Unicode presets dictionary.
        /// </summary>
        private Dictionary<int, (string language, int min, int max, string description)> unicodeRanges = new Dictionary<int, (string language, int min, int max, string description)>
        {
            // Unicode Consortium
            // https://home.unicode.org/
            // International Organization for Standardization (ISO)
            // World Wide Web Consortium (W3C)
            // https://www.ssec.wisc.edu/~tomw/java/unicode.html
            // Unicode language info ^
            // https://www.vertex42.com/ExcelTips/unicode-symbols.html
            // More Unicode language info ^
            // Consider loading direct
            { 1, ("ASCII", 32, 126, "ASCII is an acronym for American Standard Code for Information Interchange, is a character encoding standard for electronic communication.") },
            { 2, ("Arabic", 0x0600, 0x077F, "Arabic is a Semitic language that first appeared in the mid-ninth century BCE in Northern Arabia and Sahara southern Levant.") },
            { 3, ("Chinese", 0x4E00, 0x9FFF, "Chinese is a group of languages spoken natively by the ethnic Han Chinese majority and many minority ethnic groups in China.") },
            { 4, ("Cyrillic", 0x0400, 0x04FF, "The Cyrillic alphabet is a writing system used in many languages across Eurasia. It's also known as the Slavonic script.") },
            { 5, ("Devanagari", 0x0900, 0x097F, "Devanagari is an Indic script used in the Indian subcontinent.") },
            { 6, ("Greek", 0x0370, 0x03FF, "Greek is the official language of Greece and Cyprus and one of the 24 official languages of the European Union.") },
            { 7, ("Hebrew", 0x0590, 0x05FF, "Hebrew is a Northwest Semitic language that was spoken in ancient Israel and Judah, and is now the official language of Israel.") },
            { 8, ("Japanese (Hiragana & Katakana)", 0x3040, 0x30FF, "Hiragana and Katakana are parts of the Japanese writing system, along with Kanji.") },
            { 9, ("Korean", 0xAC00, 0xD7AF, "Korean is the native language for about 81 million people, mostly of Korean descent.") },
            { 10, ("Thai", 0x0E00, 0x0E7F, "Thai is the most spoken of over 60 languages of Thailand by both number of native and overall speakers.") },
            { 11, ("Turkish", 0x00C0, 0x00FF, "Turkish language, the major member of the Turkic language family, spoken in Turkey, Cyprus, and elsewhere in Europe and the Middle East.") },
            { 12, ("Tamil", 0x0B80, 0x0BFF, "Tamil is a Dravidian language natively spoken by the Tamil people of South Asia.") },
            { 13, ("Bengali", 0x0980, 0x09FF, "Bengali is the national language of Bangladesh and the official language of the Indian states of West Bengal and Tripura. It's also known as Bangla to its speakers.") },
            { 14, ("Gurmukhi", 0x0A00, 0x0A7F, "Commonly regarded as a Sikh script, Gurmukhi is used in Punjab, India as the official script of the Punjabi language.") },
            { 15, ("Khmer", 0x1780, 0x17FF, "Khmer is an Austroasiatic language spoken natively by the Khmer people. This language is an official language and national language of Cambodia.") },
            { 16, ("Myanmar", 0x1000, 0x109F, "Myanmar is a Unicode block containing characters for the Burmese, Mon, Shan, Palaung, and the Karen languages of Myanmar, as well as the Aiton and Phake languages of Northeast India. It is also used to write Pali and Sanskrit in Myanmar.") },
            { 17, ("Sinhala", 0x0D80, 0x0DFF, "Sinhala is one of the official and national languages of Sri Lanka.") },
            { 18, ("Telugu", 0x0C00, 0x0C7F, "Telugu is a classical Dravidian language native to the Indian states of Andhra Pradesh and Telangana, where it is also the official language.") },
            { 19, ("Kannada", 0x0C80, 0x0CFF, "A classical Dravidian language spoken predominantly by the people of Karnataka in southwestern India, with minorities in all neighbouring states.") },
            { 20, ("Malayalam", 0x0D00, 0x0D7F, "Malayalam is a Dravidian language spoken in the Indian state of Kerala and the union territories of Lakshadweep and Puducherry by the Malayali people.") },
            { 21, ("Georgian", 0x10A0, 0x10FF, "Georgian is the most widely spoken Kartvelian language. It is the official language of Georgia and the native or primary language of 88% of its population.") },
            { 22, ("Ethiopic", 0x1200, 0x137F, "Ethiopic is a Unicode block containing characters for writing the Geʽez, Tigrinya, Amharic, Tigre, Harari, Gurage and other Ethiosemitic languages.") },
            { 23, ("Oriya", 0x0B00, 0x0B7F, "Oriya is an Indo-European language spoken by about 30 million people in India.") },
            { 24, ("Sundanese", 0x1B80, 0x1BBF, "Sundanese is a Malayo-Polynesian language spoken by the Sundanese people.") },
            { 25, ("Javanese", 0xA980, 0xA9DF, "Javanese is a Malayo-Polynesian language of the Austronesian language family spoken primarily by the Javanese people from the central and eastern parts of the island of Java, Indonesia.") },
            { 26, ("Cham", 0xAA00, 0xAA5F, "Cham is a Malayo-Polynesian language of the Austronesian family, spoken by the Chams of Southeast Asia.") },
            { 27, ("Egyptian Hieroglyphs", 0x13000, 0x1342F, "Ancient Egyptian hieroglyphs were the formal writing system used in Ancient Egypt for writing the Egyptian language.") },
            { 28, ("Cuneiform", 0x12000, 0x123FF, "Cuneiform is a logo-syllabic writing system that was used to write several languages of the Ancient Near East.") },
            { 29, ("Greek", 0x0370, 0x03FF, "Greek is the official language of Greece and Cyprus and one of the 24 official languages of the European Union.") },
            { 30, ("Ancient Roman", 0x10190, 0x101CF, "Ancient Roman is a Unicode block containing characters used to represent the Latin alphabet as used in ancient Rome.") },
            { 31, ("Mayan Hieroglyphs", 0x13000, 0x1342F, "Maya script, also known as Maya glyphs, is historically the native writing system of the Maya civilization of Mesoamerica and is the only Mesoamerican writing system that has been substantially deciphered.") },
            { 32, ("Old Italic", 0x10300, 0x1034F, "Old Italic is a Unicode block containing a unified repertoire of several Old Italic scripts used in various parts of Italy starting about 700 BCE, including the Etruscan alphabet and others that were derived from it (or cognate with it).") },
            { 33, ("Gothic", 0x10330, 0x1034F, "Gothic is a Unicode block containing characters for writing the East Germanic Gothic language.") },
            { 34, ("Runic", 0x16A0, 0x16FF, "Runic is a Unicode block containing characters for writing Futhark runic inscriptions.") },
            { 35, ("Old Persian", 0x103A0, 0x103DF, "Old Persian is one of two directly attested Old Iranian languages and is the ancestor of Middle Persian.") },
            { 36, ("Phoenician", 0x10900, 0x1091F, "Phoenician belongs to the Canaanite languages and as such is quite similar to Biblical Hebrew and other languages of the group.") },
            { 37, ("Sogdian", 0x10F00, 0x10F2F, "The Sogdian language was an Eastern Iranian language spoken mainly in the Central Asian region of Sogdia, located in modern-day Uzbekistan, Tajikistan, Kazakhstan and Kyrgyzstan; it was also spoken by some Sogdian immigrant communities in ancient China.") },
            { 38, ("Old South Arabian", 0x10A60, 0x10A7F, "Old South Arabian is a group of four closely related extinct languages spoken in the far southern portion of the Arabian Peninsula.") },
            { 39, ("Avestan", 0x10B00, 0x10B3F, "Avestan is an Iranian language that belongs to the Indo-European language family. It's closely related to Sanskrit.") },
            { 40, ("Armenian", 0x0530, 0x058F, "Armenians are an ethnic group and nation native to the Armenian highlands of West Asia.") },
            { 41, ("Syriac", 0x0700, 0x074F, "Syriac is a Semitic language that is a dialect of Aramaic and is spoken in parts of the Middle East.") },
            { 42, ("Thaana", 0x0780, 0x07BF, "Thaana, Tãna, Taana or Tāna ( ތާނަ ) is the present writing system of the Maldivian language spoken in the Maldives.") },
            { 43, ("Gujarati", 0x0A80, 0x0AFF, "Gujarati is an Indo-Aryan language native to the Indian state of Gujarat and spoken predominantly by the Gujarati people.") },
            { 44, ("Lao", 0x0E80, 0x0EFF, "Laos, officially the Lao People's Democratic Republic (LPDR), is the only landlocked country in Southeast Asia.") },
            { 45, ("Tibetan", 0x0F00, 0x0FFF, "Tibetan is a language spoken in Tibet, parts of China, India, Nepal, Pakistan, Bhutan, and Mongolia. It is part of the Tibeto-Burman language family, which some linguists consider a branch of the Sino-Tibetan language group.") },
            { 46, ("Hangul Jamo", 0x1100, 0x11FF, "Hangul Jamo is a Unicode block containing positional (choseong, jungseong, and jongseong) forms of the Hangul consonant and vowel clusters.") },
            { 47, ("Cherokee", 0x13A0, 0x13FF, "The Cherokee people are one of the Indigenous peoples of the Southeastern Woodlands of the United States.") },
            { 48, ("Ogham", 0x1680, 0x169F, "Ogham is an alphabet that appears on monumental inscriptions dating from the 4th to the 6th century AD, and in manuscripts dating from the 6th to the 9th century.") },
            { 49, ("Mongolian", 0x1800, 0x18AF, "Mongolia, a nation bordered by China and Russia, is known for vast, rugged expanses and nomadic culture.") },
            { 50, ("Kanbun", 0x3190, 0x319F, "Kanbun (漢文 'Han writing') is a system for writing Literary Chinese used in Japan from the Nara period until the 20th century.") },
            { 51, ("Bopomofo", 0x3100, 0x31BF, "Bopomofo, also called Zhuyin Fuhao or simply Zhuyin, is a transliteration system for Standard Chinese and other Sinitic languages.") },
            { 52, ("Japanesee (Hiragana)", 0x3040, 0x309F, "Hiragana is a Japanese syllabary, part of the Japanese writing system, along with Katakana as well as Kanji.") },
            { 53, ("Japanesee (Katakana)", 0x30A0, 0x30FF, "Katakana is a Japanese syllabary, part of the Japanese writing system, along with Hiragana as well as Kanji.") },
            { 54, ("Greek Extended", 0x1F00, 0x1FFF, "Greek Extended is a Unicode block containing the accented vowels necessary for writing polytonic Greek.") },
            { 55, ("Dingbats", 0x2700, 0x27BF, "Dingbats is a Unicode block containing dingbats (or typographical ornaments, like the ❦ FLORAL HEART character).") },
            { 56, ("Kangxi Radicals", 0x2F00, 0x2FDF, "A set of 214 radicals that were collated in the 18th-century Kangxi Dictionary to aid categorization of Chinese characters.") },
            { 57, ("Currency Symbols", 0x20A0, 0x20CF, "Currency symbols are visual representations of a currency unit. They are often used on price tags and receipts.") },
            { 58, ("Latin-1 Supplement & Extended A + B", 0x0081, 0x024F, "The language of ancient Rome and its empire, widely used historically as a language of scholarship and administration.") },
            { 59, ("Basic Unicode Range", 32, 0xFFFD, "The basic range of Unicode characters.") },
            { 60, ("Unified Canadian Aboriginal", 0x1400, 0x167F, "Canadian syllabic writing, or simply syllabics, is a family of writing systems used in a number of indigenous Canadian languages of the Algonquian, Inuit, and Athabaskan language families.") },
            { 61, ("High Surrogates", 0xD800, 0xDBFF, "High Surrogate Code Points. Surrogates are bit patterns used in the UTF-16 encoding of Unicode code points to indicate that a particular 16-bit field does not encode a complete code point by itself, and must be combined with the following or preceding 16-bit field to produce a double-width encoding of some code point.") },
            { 62, ("Low Surrogates", 0xDC00, 0xDFFF, "Low Surrogate Code Points. Surrogates are bit patterns used in the UTF-16 encoding of Unicode code points to indicate that a particular 16-bit field does not encode a complete code point by itself, and must be combined with the following or preceding 16-bit field to produce a double-width encoding of some code point.") },
            { 63, ("All Surrogates", 0xD800, 0xDFFF, "High & Low Surrogate Code Points. Surrogates are bit patterns used in the UTF-16 encoding of Unicode code points to indicate that a particular 16-bit field does not encode a complete code point by itself, and must be combined with the following or preceding 16-bit field to produce a double-width encoding of some code point.") },
            { 64, ("Full Unicode Range", 32, 1114111, "The full range of the Unicode values, most of which are unused as of 2025.") },
            { 65, ("Box Drawing", 0x2500, 0x257F, "Characters for creating box-like structures, including borders, corners, and intersections, used for drawing tables, frames, and other rectangular shapes. Includes horizontal, vertical, and diagonal lines, as well as corners and junctions.") },
            { 66, ("Block Elements", 0x2580, 0x259F, "Characters for creating larger patterns and designs using blocks of different sizes and shapes used for creating charts, graphs, and other visual representations of data. Includes blocks of different sizes, shapes, and shading, as well as quarter blocks and other fractional blocks.") },
            { 67, ("Geometric Shapes", 0x25A0, 0x25FF, "Characters representing various geometric shapes, including squares, circles, triangles, and more used for creating diagrams, illustrations, and other visual aids. Includes shapes with different orientations, sizes, and fills, as well as shapes with holes or cutouts.") },
            { 68, ("Arrows", 0x2190, 0x21FF, "Characters representing different types of arrows, including single-headed, double-headed, and curved arrows, used for indicating direction, movement, or relationships between objects. Includes arrows of different lengths, orientations, and styles, as well as arrows with different types of heads and tails.") },
            { 69, ("Mathematical Operators", 0x2200, 0x22FF, "Characters for mathematical operators, including symbols for arithmetic, algebra, and other mathematical operations.") },
            { 70, ("IPA Extensions", 0x0250, 0x02AF, "Characters for representing phonetic transcriptions using the International Phonetic Alphabet (IPA).") },
            { 71, ("Number Forms", 0x2150, 0x218F, "Characters for writing numbers in different forms, including Roman numerals and other specialized number systems.") },
            { 72, ("Letterlike Symbols", 0x2100, 0x214F, "Characters that resemble letters but are used as symbols, including characters for numbers, fractions, and other mathematical concepts.") },
            { 73, ("Spacing Modifier Letters", 0x02B0, 0x02FF, "Characters that modify the spacing of adjacent characters, used in phonetic transcriptions and other specialized contexts.") },
            { 74, ("Latin Extended Additional", 0x1E00, 0x1EFF, "Additional characters for writing in the Latin script, including letters and symbols not found in the basic Latin alphabet.") },
            { 75, ("General Punctuation", 0x2000, 0x206F, "Characters for general punctuation, including symbols for quotation marks, dashes, and other common punctuation marks.") },
            { 76, ("Superscripts and Subscripts", 0x2070, 0x209F, "Characters for writing superscripts and subscripts, used in mathematical and scientific notation.") },
            { 77, ("Miscellaneous Technical", 0x2300, 0x23FF, "Characters for technical and scientific notation, including symbols for units, measurements, and other specialized concepts.") },
            { 78, ("Enclosed Alphanumerics", 0x2460, 0x24FF, "Characters for enclosing alphanumeric characters, including symbols for parentheses, brackets, and other enclosing characters.") },
            { 79, ("Miscellaneous Symbols", 0x2600, 0x26FF, "Characters for miscellaneous symbols, including symbols for weather, emotions, and other concepts.") },
            { 80, ("Braille Patterns", 0x2800, 0x28FF, "Characters for Braille patterns, including symbols for reading and writing in Braille.") },
            { 81, ("CJK Radicals Supplement", 0x2E80, 0x2EFF, "Characters for CJK (Chinese, Japanese, and Korean) radicals, including symbols for components of CJK characters.") },
            { 82, ("Ideographic Description Characters", 0x2FF0, 0x2FFF, "Characters for describing ideographs, including symbols for components and variants of CJK characters.") },
            { 83, ("CJK Symbols and Punctuation", 0x3000, 0x303F, "Characters for CJK symbols and punctuation, including symbols for punctuation, currency, and other concepts.") },
            { 84, ("Enclosed CJK Letters and Months", 0x3200, 0x32FF, "Characters for enclosing CJK letters and months, including symbols for parentheses, brackets, and other enclosing characters.") },
            { 85, ("CJK Compatibility", 0x3300, 0x33FF, "Characters for CJK compatibility, including symbols for compatibility with older character sets and systems.") },
            { 86, ("Yi Syllables", 0xA000, 0xA48F, "Characters for writing in the Yi script, including syllables and other characters.") },
            { 87, ("Yi Radicals", 0xA490, 0xA4CF, "Characters for Yi radicals, including symbols for components of Yi characters.") },
            { 88, ("CJK Compatibility Ideographs", 0xF900, 0xFAFF, "Characters for CJK compatibility ideographs, including symbols for compatibility with older character sets and systems.") },
            { 89, ("Alphabetic Presentation Forms", 0xFB00, 0xFB4F, "Characters for alphabetic presentation forms, including symbols for displaying letters and other characters in a specific way.") },
            { 90, ("Arabic Presentation Forms, A", 0xFB50, 0xFDFF, "Characters for Arabic presentation forms, including symbols for displaying Arabic text in a specific way.") },
            { 91, ("Combining Half Marks", 0xFE20, 0xFE2F, "Characters for combining half marks, including symbols for modifying adjacent characters.") },
            { 92, ("CJK Compatibility Forms", 0xFE30, 0xFE4F, "Characters for CJK compatibility forms, including symbols for compatibility with older character sets and systems.") },
            { 93, ("Small Form Variants", 0xFE50, 0xFE6F, "Characters for small form variants, including symbols for displaying characters in a smaller size.") },
            { 94, ("Arabic Presentation Forms, B", 0xFE70, 0xFEFE, "Characters for Arabic presentation forms, including symbols for displaying Arabic text in a specific way.") },
            { 95, ("Halfwidth and Fullwidth Forms", 0xFF00, 0xFFEF, "Characters for halfwidth and fullwidth forms, including symbols for displaying characters in a specific width.") },
            { 96, ("CJK Unified Ideographs Extension A", 0x3400, 0x4DB5, "Additional characters for writing in Chinese, Japanese, and Korean, including symbols for extended ideographs and variants.") },
            { 97, ("CJK Unified Ideographs", 0x4E00, 0x9FFF, "Characters for writing in Chinese, Japanese, and Korean, including symbols for common ideographs and characters used in these languages.") },
            { 98, ("Hangul Compatibility Jamo", 0x3130, 0x318F, "Characters for compatibility with older Hangul character sets, including jamo and other symbols for writing in the Hangul script.") },
            { 99, ("Hangul Syllables", 0xAC00, 0xD7A3, "Characters for writing in the Hangul script, including syllables and other symbols for representing words and phrases in Korean.") },
            { 100, ("Emoticons", 0x1F600, 0x1F64F, "Unicode block containing faces, smiles, gestures, and other common emoticons.") },
            { 101, ("Miscellaneous Symbols & Pictographs", 0x1F300, 0x1F5FF, "Unicode block containing weather symbols, plants, animals, food, and other pictographs.") },
            { 102, ("Transport & Map Symbols", 0x1F680, 0x1F6FF, "Unicode block containing vehicles, transportation, map symbols, and related pictographs.") },
            { 103, ("Supplemental Symbols & Pictographs", 0x1F900, 0x1F9FF, "Unicode block containing additional faces, hands, animals, body parts, and other pictographs.") },
            { 104, ("Symbols & Pictographs Extended-A", 0x1FA70, 0x1FAFF, "Unicode block containing more modern symbols, including gestures, objects, and extended pictographs.") },
            { 105, ("All Emojis", 0x1F300, 0x1FAFF, "Combined range of all emoji blocks for faces, objects, animals, symbols, transport, and more. Presets : [Emoticons, Miscellaneous Symbols & Pictographs, Transport & Map Symbols, Supplemental Symbols & Pictographs & Symbols & Pictographs Extended-A]") }
        };
        //Excluded Character Preset Ranges
        //Excluded because it only contains one character with no appearance.
        //{ 100, ("Specials A", 0xFEFF, 0xFEFF, "Characters for special purposes, including symbols for non-printing characters and other control codes.") },
        //Excluded because it only contains five characters, three of which have no appearance.
        //{ 101, ("Specials B", 0xFFF0, 0xFFFD, "Characters for special purposes, including symbols for non-printing characters and other control codes.") },
        //Excluded because it only contains two characters that are just a square.
        //{ 102, ("Private Use", 0xE000, 0xF8FF, "Characters for private use, including symbols for custom or proprietary characters.") },
        //Excluded because they don't contain many symbols and aren't practical for generating bodies of text with.
        //{ 103, ("Combining Diacritical Marks", 0x0300, 0x036F, "Characters that combine with other characters to form accented or modified characters, used in many languages.") },
        //{ 104, ("Combining Marks for Symbols", 0x20D0, 0x20FF, "Characters that combine with symbols to form modified symbols, used in mathematical and technical contexts.") },
        //{ 105, ("Control Pictures", 0x2400, 0x243F, "Characters for representing control characters, including symbols for tab, newline, and other control codes.") },
        //{ 106, ("Optical Character Recognition", 0x2440, 0x245F, "Characters for optical character recognition (OCR) systems, including symbols for recognizing printed characters.") },
        /// <summary>
        /// Unicode combobox index changed.
        /// </summary>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedLanguage = (string)comboBox1.SelectedItem;
            selectedPreset = unicodeRanges.First(x => x.Value.language == selectedLanguage).Value;
            rangePreset = selectedPreset.min..selectedPreset.max;
            richTextBox1.Text = selectedPreset.description;
            SetUnicodeRange(selectedPreset.min, selectedPreset.max);
        }
        /// <summary>
        /// Set Unicode Range function
        /// </summary>
        /// <param name="min">Minimum range of the Unicode characters.</param>
        /// <param name="max">Maximum range of the Unicode characters.</param>
        private void SetUnicodeRange(int min, int max)
        {
            trackBarMin.Value = min;
            trackBarMax.Value = max;
            updateLabel1();
            updateLabel2();
        }
        /// <summary>
        /// Multiple files checkbox checked.
        /// </summary>
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            textFileNumeric.Enabled = checkBox2.Checked;
            generateMultipleFiles = checkBox2.Checked;
            if (generateMultipleFiles) { operations = (int)textFileNumeric.Value; }
        }
        private void textFileNumeric_ValueChanged(object sender, EventArgs e) { operations = (int)textFileNumeric.Value; }
        private void addButton_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == custom) { MessageBox.Show("Select a preset to add to the list."); }
            else if (comboBox1.Text == "Basic Unicode Range") { MessageBox.Show("The Basic Unicode Range preset cannot be added to the list as it contains all the basic possible values.\n\nJust generate a file using this range instead."); }
            else if (comboBox1.Text == "Full Unicode Range") { MessageBox.Show("The Full Unicode Range preset cannot be added to the list as it contains all the extended possible values.\n\nJust generate a file using this range instead."); }
            else
            {
                if (comboBox2.Items.Contains(comboBox1.SelectedItem)) { MessageBox.Show("That preset has already been added to the list."); }
                else
                {
                    comboBox2.Items.Add(comboBox1.SelectedItem);
                    AddRange(rangePreset);
                }
            }
        }
        private void clearButton_Click(object sender, EventArgs e)
        {
            if (comboBox2.Items.Count > 0)
            {
                comboBox2.Items.Clear(); // the dropdown retains its height?
                comboBox2.Text = "Array of Presets";
                ClearRanges();
            }
            else { MessageBox.Show("Nothing has been added to the list."); }
        }
        private void AddRange(Range newRange) { presetRanges.Add(newRange); }
        private void ClearRanges() { presetRanges.Clear(); }
        // Buttons specifically for testing the reusable methods that return a single character, string or paragraph.
        // do not test with large paragraphs....
        private void button1_Click(object sender, EventArgs e) { generate_Buttons(false, false, 1); }
        private void button2_Click(object sender, EventArgs e) { generate_Buttons(false, true, 2); }
        private void button3_Click(object sender, EventArgs e) { generate_Buttons(true, false, 3); }
        private void generate_Buttons(bool textFile, bool contentType, int outputType)
        {
            CollectNameVariables();
            TextGenerator.GenerateRandomText(titleLength, contentLength, lineLength, paragraphLength, minRange, maxRange, presetRanges.Count != 0 ? presetRanges! : null!, textFile, contentType);
            string message = outputType switch
            {
                1 => TextGenerator.characterString,
                2 => TextGenerator.titleString.ToString(),
                3 => TextGenerator.contentString.ToString(),
                _ => ""
            };
            MessageBox.Show(message);
        }
        private void button4_Click(object sender, EventArgs e) { SendKeys.SendWait("{F11}"); }
        private void Escape_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F11 || e.KeyData == Keys.Escape)
            {
                fullScreen.Toggle();
                if (fullScreen.enabled) { matrix_Enabled(); }
                else { matrix_Disabled(); }
            }
            // specifically for disabling the spawning of new rows of characters, not the entire display.
            // intended use case : video clips of random characters within specific ranges?
            if (e.KeyData == Keys.F1) { matrixRunning = !matrixRunning; }
            // stop dead in its tracks - intended use case : unknown
            if (e.KeyData == Keys.F2) { matrixStop = !matrixStop; }
        }
        private void matrix_Enabled()
        {
            DoubleBuffered = true;
            foreach (Control control in this.Controls) { control.Visible = false; }
            CollectNameVariables();
            timer.Start();
            CreateColumns();
        }
        private void matrix_Disabled()
        {
            timer.Stop();
            DoubleBuffered = false;
            matrixRunning = true;
            matrixStop = false;
            foreach (Control control in this.Controls) { control.Visible = true; }
        }
        private void CreateColumns()
        {
            columns.Clear();
            int count = Width / charSize;
            for (int i = 0; i < count; i++)
            {
                StreamColumn col = new StreamColumn();
                col.X = i * charSize;
                col.Y = -random.Next(0, 200);
                col.Length = random.Next(8, 30);
                for (int k = 0; k < col.Length; k++) { col.Glyphs.Add(RandomGlyph()); }
                columns.Add(col);
            }
        }
        private string RandomGlyph()
        {
            TextGenerator.GenerateRandomText(titleLength, contentLength, lineLength, paragraphLength, minRange, maxRange, presetRanges.Count != 0 ? presetRanges! : null!, false, false);
            return TextGenerator.characterString;
        }
        private void UpdateColumns()
        {
            if (matrixStop) { return; } // Stops all updates and new streams when F2 is pressed (matrixStop is true)
            foreach (var col in columns)
            {
                col.Y += charSize;
                if (!matrixRunning) { continue; } // Stops new characters from spawning when F1 is pressed (matrixRunning is false)
                if (col.Y > Height + col.Length * charSize)
                {
                    col.Y = -random.Next(0, 300);
                    col.Length = random.Next(8, 100);
                    col.Glyphs.Clear();
                    for (int i = 0; i < col.Length; i++) { col.Glyphs.Add(RandomGlyph()); }
                }
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (!DoubleBuffered) { return; }
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
            e.Graphics.Clear(Color.Black);
            for (int i = 0; i < columns.Count; i++)
            {
                var col = columns[i];
                for (int j = 0; j < col.Length; j++)
                {
                    int y = col.Y - j * charSize;
                    if (y < 0 || y > Height) { continue; }
                    string glyphString = col.Glyphs[j];
                    e.Graphics.DrawString(glyphString, GetFontForGlyph(glyphString), j == 0 ? Brushes.White : Brushes.LimeGreen, col.X, y);
                }
            }
            base.OnPaint(e);
        }
        private Font GetFontForGlyph(string glyph)
        {
            int codePoint = char.ConvertToUtf32(glyph, 0); // Get the Unicode Codepoint
            if (codePoint < 0x80) { return font; } // Default to the primary font for everything it should cover (ASCII + Basic Latin)
            string bestFontName = string.Empty;
            foreach (var range in RangeToBestFont) // Find the best font range by name
            {
                if (codePoint >= range.min && codePoint <= range.max)
                {
                    bestFontName = range.fontName;
                    break; // Found the best specialized font, stop checking ranges
                }
            }
            if (string.IsNullOrEmpty(bestFontName)) { return font; } // Fall back to the base font
            if (FontObjectCache.TryGetValue(bestFontName, out Font? cachedFont)) { return cachedFont; }
            else // Create the font object and cache it for future use
            {
                try
                {
                    Font fallbackFont = new Font(bestFontName, font.Size, font.Style);
                    FontObjectCache.Add(bestFontName, fallbackFont);
                    return fallbackFont;
                }
                catch (ArgumentException) { return font; } // Font doesn't exist on the user's system
            }
        }
    }
}