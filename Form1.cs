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
        private void InitializeButtonHandlers()
        {
            for (int i = 1; i <= 11; i++)
            {
                var control = Controls.Find("button" + i, true).FirstOrDefault();
                if (control is Button button)
                {
                    control.Click += button_Click;
                }
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

            RTG.GenerateTextFile(titleLength, contentLength, lineLength, paragraphLength, minRange, maxRange);

            MessageBox.Show("Book Generated!");
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (trackBarMin.Value > trackBarMax.Value)
            {
                trackBarMax.Value = trackBarMin.Value;
                updateLabel2();
            }
            updateLabel1();
        }
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (trackBarMax.Value < trackBarMin.Value)
            {
                trackBarMin.Value = trackBarMax.Value;
                updateLabel1();
            }
            updateLabel2();
        }
        private void updateLabel1()
        {
            label1.Text = trackBarMin.Value.ToString();
        }
        private void updateLabel2()
        {
            label2.Text = trackBarMax.Value.ToString();
        }
        private Dictionary<int, (int min, int max)> unicodeRanges = new Dictionary<int, (int min, int max)>
        {
            { 1, (32, 126) }, // ASCII
            { 2, (0x0600, 0x077F) }, // Arabic
            { 3, (0x4E00, 0x9FFF) }, // Chinese
            { 4, (0x0400, 0x04FF) }, // Cyrillic
            { 5, (0x0900, 0x097F) }, // Devanagari
            { 6, (0x0370, 0x03FF) }, // Greek
            { 7, (0x0590, 0x05FF) }, // Hebrew
            { 8, (0x3040, 0x30FF) }, // Japanese
            { 9, (0xAC00, 0xD7AF) }, // Korean
            { 10, (0x0E00, 0x0E7F) }, // Thai
            { 11, (0x00C0, 0x00FF) }, // Turkish
            { 12, (0x0B80, 0x0BFF) }, // Tamil
            { 13, (0x0980, 0x09FF) }, // Bengali
            { 14, (0x0A00, 0x0A7F) }, // Gurmukhi
            { 15, (0x1780, 0x17FF) }, // Khmer
            { 16, (0x1000, 0x109F) }, // Myanmar
            { 17, (0x0D80, 0x0DFF) }, // Sinhala
            { 18, (0x0C00, 0x0C7F) }, // Telugu
            { 19, (0x0C80, 0x0CFF) }, // Kannada
            { 20, (0x0D00, 0x0D7F) }, // Malayalam
            { 21, (0x10A0, 0x10FF) }, // Georgian
            { 22, (0x1200, 0x137F) }, // Ethiopic
            { 23, (0x0B00, 0x0B7F) }, // Oriya
            { 24, (0x1B80, 0x1BBF) }, // Sundanese
            { 25, (0xA980, 0xA9DF) }, // Javanese
            { 26, (0xAA00, 0xAA5F) }, // Cham
        };
        private void button_Click(object? sender, EventArgs e)
        {
            if (sender is Button button)
            {
                var index = int.Parse(button.Name.Replace("button", ""));
                var range = unicodeRanges[index];
                SetUnicodeRange(range.min, range.max);
            }
        }
        private void SetUnicodeRange(int min, int max)
        {
            trackBarMin.Value = min;
            trackBarMax.Value = max;
            label1.Text = trackBarMin.Value.ToString();
            label2.Text = trackBarMax.Value.ToString();
        }
    }
}