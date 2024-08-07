using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO.Pipes;

namespace VideoLOB
{
    public partial class Form1 : Form
    {
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
                short shortValue = Convert.ToInt16(channelsBox.SelectedItem.ToString());
                numChannels = shortValue;
            }
            if (depthBox.SelectedItem != null)
            {
                short shortValue = Convert.ToInt16(depthBox.SelectedItem.ToString());
                bitDepth = shortValue;
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
            Console.WriteLine("test");
        }

        private void GenerateModelButton_Click(object sender, EventArgs e)
        {
            string filePath = "random_model.obj";

            int numVertices = Convert.ToInt32(verticesNumeric.Value);
            int numFaces = Convert.ToInt32(facesNumeric.Value);

            ModelGenerator modelGenerator = new ModelGenerator();
            List<Mesh> meshes = modelGenerator.GenerateRandom3DModel(numVertices, numFaces);
            modelGenerator.WriteObjFile(filePath, meshes);

            MessageBox.Show("Model Generated!");
        }
    }
}