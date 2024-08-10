using System.Text;

public class AudioGenerator
{
    private static Random random = new Random();
    private static byte[] sampleBytes = new byte [0];
    public static void GenerateAudio(string filePath, int sampleRate, int frequency, int duration, short numChannels, short bitDepth)
    {
        // Calculate the number of samples
        int numSamples = sampleRate * duration;
        int bytesPerSample = 1;
        switch(bitDepth)
        {
            case 8:
                bytesPerSample = 1;
                break;
            case 16:
                bytesPerSample = 2;
                break;
            case 24:
                bytesPerSample = 3;
                break;
            case 32:
                bytesPerSample = 4;
                break;
        }

        // Create a byte array to hold the audio data
        byte[] audioData = new byte[numSamples * bytesPerSample]; // 2 bytes per sample (16-bit audio)
        // Generate the audio data
        for (int i = 0; i < numSamples; i++)
        {
            // Generate a random value between -1 and 1
            double value = (random.NextDouble() * 2) - 1;

            // Convert the value to an integer and store it in the audio data array
            int maxValue = (1 << bitDepth) - 1;
            int audioSample = (int)(value * maxValue);

            // Convert the integer to bytes and store it in the audio data array
            if (bitDepth == 32)
            {
                float audioSampleFloat = (float)(random.NextDouble() * 2 - 1);
                sampleBytes = BitConverter.GetBytes(audioSampleFloat);
                Array.Reverse(sampleBytes); // Reverse the byte order
            }
            else
            {
                sampleBytes = BitConverter.GetBytes(audioSample);
            }
            Array.Copy(sampleBytes, 0, audioData, i * bytesPerSample, bytesPerSample);
        }

        /*// Pure Sine Wave Audio
        // Generate the audio data
        for (int i = 0; i < numSamples; i++)
        {
            // Calculate the value of the sine wave at the current sample
            double value = Math.Sin(2 * Math.PI * frequency * i / sampleRate);

            // Convert the value to a 16-bit integer and store it in the audio data array
            short audioSample = (short)(value * short.MaxValue);
            audioData[i * 2] = (byte)(audioSample & 0xFF);
            audioData[i * 2 + 1] = (byte)((audioSample >> 8) & 0xFF);
        }*/

        // Write the audio data to a file
        using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
        {
            //fileStream.Write(wavHeader, 0, wavHeader.Length);
            // RIFF header.
            // Chunk ID.
            fileStream.Write(Encoding.ASCII.GetBytes("RIFF"), 0, 4);
            // Chunk size.
            fileStream.Write(BitConverter.GetBytes(((bitDepth / 8) * sampleRate * duration) + 36), 0, 4);
            // Format.
            fileStream.Write(Encoding.ASCII.GetBytes("WAVE"), 0, 4);
            // Sub-chunk 1.
            // Sub-chunk 1 ID.
            fileStream.Write(Encoding.ASCII.GetBytes("fmt "), 0, 4);
            // Sub-chunk 1 size.
            fileStream.Write(BitConverter.GetBytes(16), 0, 4);
            // Audio format (floating point (3) or PCM (1)). Any other format indicates compression.
            fileStream.Write(BitConverter.GetBytes((ushort)(1)), 0, 2);
            // Channels.
            fileStream.Write(BitConverter.GetBytes(numChannels), 0, 2);
            // Sample rate.
            fileStream.Write(BitConverter.GetBytes(sampleRate), 0, 4);
            // Bytes rate.
            fileStream.Write(BitConverter.GetBytes(sampleRate * numChannels * (bitDepth / 8)), 0, 4);
            // Block align.
            fileStream.Write(BitConverter.GetBytes((ushort)numChannels * (bitDepth / 8)), 0, 2);
            // Bits per sample.
            fileStream.Write(BitConverter.GetBytes(bitDepth), 0, 2);
            // Sub-chunk 2.
            // Sub-chunk 2 ID.
            fileStream.Write(Encoding.ASCII.GetBytes("data"), 0, 4);
            // Sub-chunk 2 size.
            fileStream.Write(BitConverter.GetBytes(((bitDepth / 8) * sampleRate * duration)), 0, 4);
            fileStream.Write(audioData, 0, audioData.Length);
        }
    }
}