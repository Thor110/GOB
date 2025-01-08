using static System.Net.WebRequestMethods;

namespace VideoLOB
{
    /// <summary>
    /// Library of Babel generater form.
    /// </summary>
    public partial class Form1 : Form
    {
        RandomTextGenerator TextGenerator = new RandomTextGenerator();
        private System.Windows.Forms.ToolTip tooltip;
        private Type[] excludedControlTypes = new Type[] { typeof(Panel), typeof(TableLayoutPanel), typeof(FlowLayoutPanel), typeof(Label), typeof(Button) };
        private int titleLength;
        private int minRange;
        private int maxRange;
        private bool surrogates;
        public Form1()
        {
            InitializeComponent();
            InitializeTooltips();
            InitializePresets();
        }
        /// <summary>
        /// Initialize tooltips for all controls.
        /// </summary>
        private void InitializeTooltips()
        {
            this.components = new System.ComponentModel.Container();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            foreach (Control control in this.Controls)
            {
                if (excludedControlTypes.Contains(control.GetType()) != true)
                {
                    control.MouseEnter += new EventHandler(tooltip_MouseEnter);
                    control.MouseLeave += new EventHandler(tooltip_MouseLeave);
                }
            }
        }
        /// <summary>
        /// Unicode Preset Initializer.
        /// </summary>
        private void InitializePresets()
        {
            foreach (var range in unicodeRanges.Values)
            {
                comboBox1.Items.Add(range.language);
            }
            comboBox1.SelectedIndex = 58; // "Basic Unicode Range"
        }
        /// <summary>
        /// Tooltip mouse event handlers.
        /// </summary>
        void tooltip_MouseEnter(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            if (control.AccessibleDescription != null)
            {
                this.tooltip.Show(control.AccessibleDescription.ToString(), control);
            }
            else
            {
                this.tooltip.Show("No description available", control);
            }
        }
        void tooltip_MouseLeave(object sender, EventArgs e)
        {
            this.tooltip.Hide((Control)sender);
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
        /// Video generator button.
        /// </summary>
        private void GenerateVideoButton_Click(object sender, EventArgs e)
        {
            //string seedString = GenerateSeedString();
            //seedText.Text = seedString;

            CollectNameVariables();
            string filePath = TextGenerator.GenerateSingleString(titleLength, minRange, maxRange, true, surrogates) + ".avi";
            int width = Convert.ToInt32(videoWidthNumeric.Value);
            int height = Convert.ToInt32(videoHeightNumeric.Value);
            int frameRate = Convert.ToInt32(videoFramerateNumeric.Value);
            int duration = Convert.ToInt32(videoDurationNumeric.Value);

            VideoGenerator.GenerateVideo(filePath, width, height, frameRate, duration);

            MessageBox.Show("Video Generated!");
        }
        /// <summary>
        /// Audio generator button.
        /// </summary>
        private void GenerateAudioButton_Click(object sender, EventArgs e)
        {
            //string seedString = GenerateSeedString();
            //seedText.Text = seedString;

            // filename
            CollectNameVariables();
            string filePath = TextGenerator.GenerateSingleString(titleLength, minRange, maxRange, true, surrogates) + ".wav";

            // Define the audio parameters
            int sampleRate = Convert.ToInt32(sampleNumeric.Value);// Sample rate in Hz
            int frequency = Convert.ToInt32(frequencyNumeric.Value);// Frequency in Hz
            int duration = Convert.ToInt32(durationNumeric.Value);// Duration in seconds
            // default numChannels and bitDepth for WAV header
            short numChannels = Convert.ToInt16(channelsBox.SelectedItem.ToString());
            short bitDepth = Convert.ToInt16(depthBox.SelectedItem.ToString());

            AudioGenerator.GenerateAudio(filePath, sampleRate, frequency, duration, numChannels, bitDepth);

            MessageBox.Show("Audio Generated!");
        }
        /// <summary>
        /// Image generator button.
        /// </summary>
        private void GenerateImageButton_Click(object sender, EventArgs e)
        {
            CollectNameVariables();
            string filePath = TextGenerator.GenerateSingleString(titleLength, minRange, maxRange, true, surrogates) + ".png";

            int width = Convert.ToInt32(widthNumeric.Value);
            int height = Convert.ToInt32(heightNumeric.Value);

            ImageGenerator.GenerateImage(filePath, width, height);

            MessageBox.Show("Image Generated!");
        }
        /// <summary>
        /// 3D Model generator button.
        /// </summary>
        private void GenerateModelButton_Click(object sender, EventArgs e)
        {
            CollectNameVariables();
            string filePath = TextGenerator.GenerateSingleString(titleLength, minRange, maxRange, true, surrogates) + ".obj";

            int numVertices = Convert.ToInt32(verticesNumeric.Value);
            int numFaces = Convert.ToInt32(facesNumeric.Value);
            int scale = Convert.ToInt32(scaleNumeric.Value);
            bool generateSolid = checkBox1.Checked;

            ModelGenerator.GenerateModel(numVertices, numFaces, scale, filePath, generateSolid);

            MessageBox.Show("Model Generated!");
        }
        /// <summary>
        /// Book generator button.
        /// </summary>
        private void CollectNameVariables()
        {
            titleLength = Convert.ToInt32(titleNumeric.Value);
            minRange = trackBarMin.Value;
            maxRange = trackBarMax.Value;
        }
        private void GenerateBookButton_Click(object sender, EventArgs e)
        {
            CollectNameVariables();
            int titleLength = Convert.ToInt32(titleNumeric.Value);
            int contentLength = Convert.ToInt32(contentNumeric.Value);
            int lineLength = Convert.ToInt32(lineNumeric.Value);
            int paragraphLength = Convert.ToInt32(paragraphNumeric.Value);
            //Range []range = { }; // use an array of ranges to pass to the text generation functions
            if (comboBox2.Items.Count >= 1)
            {
                MessageBox.Show("Generating using a list of presets. FUNCTION NOT READY YET!");
                return;
            }
            if (checkBox2.Checked)
            {
                int operations = Convert.ToInt32(textFileNumeric.Value);
                TextGenerator.MultipleTextFiles(titleLength, contentLength, lineLength, paragraphLength, minRange, maxRange, operations, surrogates);
                MessageBox.Show("Books Generated!");
            }
            else
            {
                TextGenerator.GenerateTextFile(titleLength, contentLength, lineLength, paragraphLength, minRange, maxRange, surrogates);
                MessageBox.Show("Book Generated!");
            }
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
            string custom = "Custom";
            comboBox1.Text = custom;
            richTextBox1.Text = custom;
        }
        /// <summary>
        /// Update minimum Unicode range label.
        /// </summary>
        private void updateLabel1()
        {
            label1.Text = trackBarMin.Value.ToString();
        }
        /// <summary>
        /// Update maximum Unicode range label.
        /// </summary>
        private void updateLabel2()
        {
            label2.Text = trackBarMax.Value.ToString();
        }
        /// <summary>
        /// Unicode presets dictionary.
        /// </summary>
        private Dictionary<int, (string language, int min, int max, string description)> unicodeRanges = new Dictionary<int, (string language, int min, int max, string description)>
        {
            // Unicode Consortium
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
            { 58, ("Latin", 0x0081, 0x024F, "The language of ancient Rome and its empire, widely used historically as a language of scholarship and administration.") },
            { 59, ("Basic Unicode Range", 32, 0xFFFD, "The basic range of Unicode characters.") },
            { 60, ("Unified Canadian Aboriginal", 0x1400, 0x167F, "Canadian syllabic writing, or simply syllabics, is a family of writing systems used in a number of indigenous Canadian languages of the Algonquian, Inuit, and Athabaskan language families.") },
            { 61, ("High Surrogates", 0xD800, 0xDBFF, "High Surrogate Code Points. Surrogates are bit patterns used in the UTF-16 encoding of Unicode code points to indicate that a particular 16-bit field does not encode a complete code point by itself, and must be combined with the following or preceding 16-bit field to produce a double-width encoding of some code point.") },
            { 62, ("Low Surrogates", 0xDC00, 0xDFFF, "Low Surrogate Code Points. Surrogates are bit patterns used in the UTF-16 encoding of Unicode code points to indicate that a particular 16-bit field does not encode a complete code point by itself, and must be combined with the following or preceding 16-bit field to produce a double-width encoding of some code point.") },
            { 63, ("All Surrogates", 0xD800, 0xDFFF, "High & Low Surrogate Code Points. Surrogates are bit patterns used in the UTF-16 encoding of Unicode code points to indicate that a particular 16-bit field does not encode a complete code point by itself, and must be combined with the following or preceding 16-bit field to produce a double-width encoding of some code point.") }/*,
            { 64, ("", , "") },
            { 65, ("", , "") },
            { 66, ("", , "") },
            { 67, ("", , "") },
            { 68, ("", , "") },
            { 69, ("", , "") },
            { 70, ("", , "") },
            { 71, ("", , "") }*/
            /*
            //
0x3130, 0x318F   12592, 12687 Hangul Compatibility Jamo
0xAC00, 0xD7A3   44032, 55203 Hangul Syllables
0x0250, 0x02AF   592, 687 IPA Extensions
0x02B0, 0x02FF   688, 767 Spacing Modifier Letters
0x0300, 0x036F   768, 879 Combining Diacritical Marks
0x1E00, 0x1EFF   7680, 7935   Latin Extended Additional
0x2000, 0x206F   8192, 8303   General Punctuation
0x2070, 0x209F   8304, 8351   Superscripts and Subscripts
0x20D0, 0x20FF   8400, 8447   Combining Marks for Symbols
0x2100, 0x214F   8448, 8527   Letterlike Symbols
0x2150, 0x218F   8528, 8591   Number Forms
0x2190, 0x21FF   8592, 8703   Arrows
0x2200, 0x22FF   8704, 8959   Mathematical Operators
0x2300, 0x23FF   8960, 9215   Miscellaneous Technical
0x2400, 0x243F   9216, 9279   Control Pictures
0x2440, 0x245F   9280, 9311   Optical Character Recognition
0x2460, 0x24FF   9312, 9471   Enclosed Alphanumerics
0x2500, 0x257F   9472, 9599   Box Drawing
0x2580, 0x259F   9600, 9631   Block Elements
0x25A0, 0x25FF   9632, 9727   Geometric Shapes
0x2600, 0x26FF   9728, 9983   Miscellaneous Symbols
0x2800, 0x28FF   10240, 10495 Braille Patterns
0x2E80, 0x2EFF   11904, 12031 CJK Radicals Supplement
            //
0x2FF0, 0x2FFF   12272, 12287 Ideographic Description Characters
0x3000, 0x303F   12288, 12351 CJK Symbols and Punctuation
0x3200, 0x32FF   12800, 13055 Enclosed CJK Letters and Months
0x3300, 0x33FF   13056, 13311 CJK Compatibility
0x3400, 0x4DB5   13312, 19893 CJK Unified Ideographs Extension A

0x4E00, 0x9FFF   19968, 40959 CJK Unified Ideographs
            //
0xA000, 0xA48F   40960, 42127 Yi Syllables
0xA490, 0xA4CF   42128, 42191 Yi Radicals
            //
0xE000, 0xF8FF   57344, 63743 Private Use
            //
0xF900, 0xFAFF   63744, 64255 CJK Compatibility Ideographs
0xFB00, 0xFB4F   64256, 64335 Alphabetic Presentation Forms
0xFB50, 0xFDFF   64336, 65023 Arabic Presentation Forms, A
            //
0xFE20, 0xFE2F   65056, 65071 Combining Half Marks
0xFE30, 0xFE4F   65072, 65103 CJK Compatibility Forms
0xFE50, 0xFE6F   65104, 65135 Small Form Variants
0xFE70, 0xFEFE   65136, 65278 Arabic Presentation Forms, B
0xFEFF, 0xFEFF   65279, 65279 Specials
0xFF00, 0xFFEF   65280, 65519 Halfwidth and Fullwidth Forms
0xFFF0, 0xFFFD   65520, 65533 Specials
            */
        };
        /// <summary>
        /// Unicode combobox index changed.
        /// </summary>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 60 && comboBox1.SelectedIndex <= 62) // Surrogate Code Points Dictionary Entries
            {
                surrogates = true;
            }
            else
            {
                surrogates = false;
            }
            var selectedLanguage = (string)comboBox1.SelectedItem;
            var range = unicodeRanges.First(x => x.Value.language == selectedLanguage).Value;
            richTextBox1.Text = range.description;
            SetUnicodeRange(range.min, range.max);
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
            label1.Text = trackBarMin.Value.ToString();
            label2.Text = trackBarMax.Value.ToString();
        }
        /// <summary>
        /// Multiple files checkbox checked.
        /// </summary>
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            textFileNumeric.Enabled = checkBox2.Checked;
        }
        private void addButton_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "Custom")
            {
                if(comboBox2.Items.Contains(comboBox1.SelectedItem))
                {
                    MessageBox.Show("That preset has already been added to the list.");
                }
                else
                {
                    comboBox2.Items.Add(comboBox1.SelectedItem);
                }
            }
            else
            {
                MessageBox.Show("Select a preset to add to the list.");
            }
        }
        private void clearButton_Click(object sender, EventArgs e)
        {
            if (comboBox2.Items.Count >= 1)
            {
                comboBox2.Items.Clear(); // the dropdown stays long?
                comboBox2.Text = "Array of Presets";
            }
            else
            {
                MessageBox.Show("Nothing has been added to the list.");
            }
        }
    }
}