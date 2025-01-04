namespace VideoLOB
{
    public partial class Form1 : Form
    {
        RandomTextGenerator RTG = new RandomTextGenerator();
        public Form1()
        {
            InitializeComponent();
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
            int minRange = trackBarA.Value;
            int maxRange = trackBarB.Value;

            RTG.GenerateTextFile(titleLength, contentLength, lineLength, paragraphLength, minRange, maxRange);

            MessageBox.Show("Book Generated!");
        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            //trackBarA Minimum
            if (trackBarA.Value > trackBarB.Value)
            {
                trackBarB.Value = trackBarA.Value;
                updateLabel2();
            }
            updateLabel1();
        }
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            //trackBarB Maximum
            if (trackBarB.Value < trackBarA.Value)
            {
                trackBarA.Value = trackBarB.Value;
                updateLabel1();
            }
            updateLabel2();
        }
        private void updateLabel1()
        {
            label1.Text = trackBarA.Value.ToString();
        }
        private void updateLabel2()
        {
            label2.Text = trackBarB.Value.ToString();
        }
    }
}