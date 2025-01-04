using System.Windows.Forms;

namespace VideoLOB
{
    public partial class Form1 : Form
    {
        RandomTextGenerator RTG = new RandomTextGenerator();
        public Form1()
        {
            InitializeComponent();
            InitializeButtonHandlers();
        }
        //multiple button
        /*private void button_Click(object? sender, EventArgs e)
        {
            if (sender is Button button)
            {
                var index = int.Parse(button.Name.Replace("button", ""));
                var range = unicodeRanges[index];
                SetUnicodeRange(range.min, range.max);
            }
        }*/
        private void InitializeButtonHandlers()
        {
            //multiple buttons
            /*for (int i = 1; i <= 11; i++)
            {
                var control = Controls.Find("button" + i, true).FirstOrDefault();
                if (control is Button button)
                {
                    control.Click += button_Click;
                }
            }*/
            foreach (var range in unicodeRanges.Values)
            {
                comboBox1.Items.Add(range.language);
            }
        }
        private string GenerateSeedString()
        {
            // Generate a seed string that represents the video
            // This can be done using a combination of algorithms, such as hash functions and lossy compression
            // For simplicity, let's just use a random string
            return Guid.NewGuid().ToString();
        }
        private void GenerateVideoButton_Click(object sender, EventArgs e)
        {
            // Generate the video and seed string
            string seedString = GenerateSeedString();
            videoSeed.Text = seedString;

            string filePath = "random_video.avi";
            int width = 640;
            int height = 480;
            int frameRate = 30;
            int duration = 360;

            VideoGenerator.GenerateVideo(filePath, width, height, frameRate, duration);

            MessageBox.Show("Video Generated!");
        }
        private void GenerateAudioButton_Click(object sender, EventArgs e)
        {
            // Generate the audio and seed string
            string seedString = GenerateSeedString();
            audioSeed.Text = seedString;

            // filename
            string filePath = "random_audio.wav";
            // Define the audio parameters
            int sampleRate = Convert.ToInt32(sampleNumeric.Value);// Sample rate in Hz
            int frequency = Convert.ToInt32(frequencyNumeric.Value);// Frequency in Hz
            int duration = Convert.ToInt32(durationNumeric.Value);// Duration in seconds
            // default numChannels and bitDepth for WAV header
            short numChannels = 1; // Number of channels (mono)
            short bitDepth = 16; // Bit depth (16-bit audio)

            if (channelsBox.SelectedItem != null)
            {
                numChannels = Convert.ToInt16(channelsBox.SelectedItem.ToString());
            }
            if (depthBox.SelectedItem != null)
            {
                bitDepth = Convert.ToInt16(depthBox.SelectedItem.ToString());
            }

            AudioGenerator.GenerateAudio(filePath, sampleRate, frequency, duration, numChannels, bitDepth);

            MessageBox.Show("Audio Generated!");
        }
        private void GenerateImageButton_Click(object sender, EventArgs e)
        {
            string filePath = "random_image.png";

            int width = Convert.ToInt32(widthNumeric.Value);
            int height = Convert.ToInt32(heightNumeric.Value);

            ImageGenerator.GenerateImage(filePath, width, height);

            MessageBox.Show("Image Generated!");
        }

        private void GenerateModelButton_Click(object sender, EventArgs e)
        {
            string filePath = "random_model.obj";

            int numVertices = Convert.ToInt32(verticesNumeric.Value);
            int numFaces = Convert.ToInt32(facesNumeric.Value);
            int scale = Convert.ToInt32(scaleNumeric.Value);
            bool generateSolid = checkBox1.Checked;

            ModelGenerator.GenerateModel(numVertices, numFaces, scale, filePath, generateSolid);

            MessageBox.Show("Model Generated!");
        }

        private void GenerateBookButton_Click(object sender, EventArgs e)
        {
            int titleLength = Convert.ToInt32(titleNumeric.Value);
            int contentLength = Convert.ToInt32(contentNumeric.Value);
            int lineLength = Convert.ToInt32(lineNumeric.Value);
            int paragraphLength = Convert.ToInt32(paragraphNumeric.Value);
            int minRange = trackBarMin.Value;
            int maxRange = trackBarMax.Value;

            int operations = Convert.ToInt32(textFileNumeric.Value);
            if (checkBox2.Checked)
            {
                RTG.MultipleTextFiles(titleLength, contentLength, lineLength, paragraphLength, minRange, maxRange, operations);
                MessageBox.Show("Books Generated!");
            }
            else
            {
                RTG.GenerateTextFile(titleLength, contentLength, lineLength, paragraphLength, minRange, maxRange);
                MessageBox.Show("Book Generated!");
            }
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (trackBarMin.Value > trackBarMax.Value)
            {
                trackBarMax.Value = trackBarMin.Value;
                updateLabel2();
            }
            updateLabel1();
            comboBox1.Text = "Custom";
        }
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (trackBarMax.Value < trackBarMin.Value)
            {
                trackBarMin.Value = trackBarMax.Value;
                updateLabel1();
            }
            updateLabel2();
            comboBox1.Text = "Custom";
        }
        private void updateLabel1()
        {
            label1.Text = trackBarMin.Value.ToString();
        }
        private void updateLabel2()
        {
            label2.Text = trackBarMax.Value.ToString();
        }
        private Dictionary<int, (string language, int min, int max)> unicodeRanges = new Dictionary<int, (string language, int min, int max)>
        {
            // Unicode Consortium
            // International Organization for Standardization (ISO)
            // World Wide Web Consortium (W3C)
            { 1, ("ASCII", 32, 126) },
            { 2, ("Arabic", 0x0600, 0x077F) },
            { 3, ("Chinese", 0x4E00, 0x9FFF) },
            { 4, ("Cyrillic", 0x0400, 0x04FF) },
            { 5, ("Devanagari", 0x0900, 0x097F) },
            { 6, ("Greek", 0x0370, 0x03FF) },
            { 7, ("Hebrew", 0x0590, 0x05FF) },
            { 8, ("Japanese", 0x3040, 0x30FF) },
            { 9, ("Korean", 0xAC00, 0xD7AF) },
            { 10, ("Thai", 0x0E00, 0x0E7F) },
            { 11, ("Turkish", 0x00C0, 0x00FF) },
            { 12, ("Tamil", 0x0B80, 0x0BFF) },
            { 13, ("Bengali", 0x0980, 0x09FF) },
            { 14, ("Gurmukhi", 0x0A00, 0x0A7F) },
            { 15, ("Khmer", 0x1780, 0x17FF) },
            { 16, ("Myanmar", 0x1000, 0x109F) },
            { 17, ("Sinhala", 0x0D80, 0x0DFF) },
            { 18, ("Telugu", 0x0C00, 0x0C7F) },
            { 19, ("Kannada", 0x0C80, 0x0CFF) },
            { 20, ("Malayalam", 0x0D00, 0x0D7F) },
            { 21, ("Georgian", 0x10A0, 0x10FF) },
            { 22, ("Ethiopic", 0x1200, 0x137F) },
            { 23, ("Oriya", 0x0B00, 0x0B7F) },
            { 24, ("Sundanese", 0x1B80, 0x1BBF) },
            { 25, ("Javanese", 0xA980, 0xA9DF) },
            { 26, ("Cham", 0xAA00, 0xAA5F) },
            { 27, ("Egyptian Hieroglyphs", 0x13000, 0x1342F) },
            { 28, ("Cuneiform", 0x12000, 0x123FF) },
            { 29, ("Ancient Greek", 0x0370, 0x1FFF) },
            { 30, ("Ancient Roman", 0x10190, 0x101CF) },
            { 31, ("Mayan Hieroglyphs", 0x13000, 0x1342F) },
            { 32, ("Old Italic", 0x10300, 0x1034F) },
            { 33, ("Gothic", 0x10330, 0x1034F) },
            { 34, ("Runic", 0x16A0, 0x16FF) },
            { 35, ("Old Persian", 0x103A0, 0x103DF) },
            { 36, ("Phoenician", 0x10900, 0x1091F) },
            { 37, ("Sogdian", 0x10F00, 0x10F2F) },
            { 38, ("Old South Arabian", 0x10A60, 0x10A7F) },
            { 39, ("Avestan", 0x10B00, 0x10B3F) }
        };
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedLanguage = (string)comboBox1.SelectedItem;
            var range = unicodeRanges.First(x => x.Value.language == selectedLanguage).Value;
            SetUnicodeRange(range.min, range.max);
        }
        private void SetUnicodeRange(int min, int max)
        {
            trackBarMin.Value = min;
            trackBarMax.Value = max;
            label1.Text = trackBarMin.Value.ToString();
            label2.Text = trackBarMax.Value.ToString();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            textFileNumeric.Enabled = checkBox2.Checked;
        }
    }
}