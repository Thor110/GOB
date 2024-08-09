using System;
using System.Collections;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Text;

public class VideoGenerator
{
    public static void GenerateVideo(string filePath, int width, int height, int frameRate, int duration)
    {
        Random random = new Random();

        int frameSize = width * height * 3; // 3 bytes per pixel (RGB)
        int numberFrames = frameRate * duration; // Total number of frames

        try
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                // Write the AVI file header
                fileStream.Write(BitConverter.GetBytes((int)0x52494646), 0, 4); // "RIFF"
                fileStream.Write(BitConverter.GetBytes((int)0x00000000), 0, 4); // File size (will be updated later)
                fileStream.Write(BitConverter.GetBytes((int)0x41564920), 0, 4); // "AVI "
                fileStream.Write(BitConverter.GetBytes((int)0x00000000), 0, 4); // List header size

                // Write the LIST header
                fileStream.Write(BitConverter.GetBytes((int)0x4C495354), 0, 4); // "LIST"
                fileStream.Write(BitConverter.GetBytes((int)0x00000000), 0, 4); // List size (will be updated later)
                fileStream.Write(BitConverter.GetBytes((int)0x68640000), 0, 4); // "hdrl"

                // Write the AVI main header
                fileStream.Write(BitConverter.GetBytes((int)0x6176696D), 0, 4); // "avim"
                fileStream.Write(BitConverter.GetBytes((int)0x00000010), 0, 4); // Main header size
                fileStream.Write(BitConverter.GetBytes((int)0x00000001), 0, 4); // Microseconds per frame
                fileStream.Write(BitConverter.GetBytes((int)frameRate), 0, 4); // Frames per second
                fileStream.Write(BitConverter.GetBytes(numberFrames), 0, 4); // Total frames
                fileStream.Write(BitConverter.GetBytes(0), 0, 4); // Initial frames
                fileStream.Write(BitConverter.GetBytes(1), 0, 4); // Streams
                fileStream.Write(BitConverter.GetBytes(width * height * 3), 0, 4); // Suggested buffer size
                fileStream.Write(BitConverter.GetBytes((int)width), 0, 4); // Width
                fileStream.Write(BitConverter.GetBytes((int)height), 0, 4); // Height
                fileStream.Write(BitConverter.GetBytes((int)0x00000000), 0, 4); // Reserved 1
                fileStream.Write(BitConverter.GetBytes((int)0x00000000), 0, 4); // Reserved 2
                fileStream.Write(BitConverter.GetBytes((int)0x00000000), 0, 4); // Reserved 3
                fileStream.Write(BitConverter.GetBytes((int)0x00000000), 0, 4); // Reserved 4

                // Write the video stream header
                fileStream.Write(BitConverter.GetBytes((int)0x73747265), 0, 4); // "stre"
                fileStream.Write(BitConverter.GetBytes((int)0x00000040), 0, 4); // Stream header size
                fileStream.Write(BitConverter.GetBytes((int)0x00000001), 0, 4); // Stream type (video)
                fileStream.Write(BitConverter.GetBytes((int)0x00000000), 0, 4); // Handler type
                fileStream.Write(BitConverter.GetBytes(0x00000001), 0, 4); // Flags
                fileStream.Write(BitConverter.GetBytes((int)0x00000000), 0, 4); // Priority
                fileStream.Write(BitConverter.GetBytes((int)0x00000000), 0, 4); // Language
                fileStream.Write(BitConverter.GetBytes((int)0x00000000), 0, 4); // Initial frames
                fileStream.Write(BitConverter.GetBytes((int)0x00000000), 0, 4); // Scale
                fileStream.Write(BitConverter.GetBytes((int)0x00000000), 0, 4); // Rate
                fileStream.Write(BitConverter.GetBytes((int)0x00000000), 0, 4); // Start
                fileStream.Write(BitConverter.GetBytes((int)0x00000000), 0, 4); // Length
                fileStream.Write(BitConverter.GetBytes((int)0x00000000), 0, 4); // Suggested buffer size
                fileStream.Write(BitConverter.GetBytes(10000), 0, 4); // Quality
                fileStream.Write(BitConverter.GetBytes(width * height * 3), 0, 4); // Sample size
                fileStream.Write(BitConverter.GetBytes(frameRate), 0, 4); // Frame rate

                // Write the video format header
                fileStream.Write(BitConverter.GetBytes((int)0x62697666), 0, 4); // "biff"
                fileStream.Write(BitConverter.GetBytes((int)0x00000040), 0, 4); // Format size
                fileStream.Write(BitConverter.GetBytes(frameSize), 0, 4); // Compression size
                fileStream.Write(BitConverter.GetBytes((int)0x00000000), 0, 4); // Flags
                fileStream.Write(BitConverter.GetBytes((int)0x00000000), 0, 4); // Reserved 1
                fileStream.Write(BitConverter.GetBytes((int)0x00000000), 0, 4); // Reserved 2
                fileStream.Write(BitConverter.GetBytes((int)width), 0, 4); // Width
                fileStream.Write(BitConverter.GetBytes((int)height), 0, 4); // Height
                fileStream.Write(BitConverter.GetBytes((int)0x00000001), 0, 4); // Planes
                fileStream.Write(BitConverter.GetBytes((int)0x00000020), 0, 4); // Bit count
                fileStream.Write(BitConverter.GetBytes((int)0x00000000), 0, 4); // Compression size
                fileStream.Write(BitConverter.GetBytes(frameSize), 0, 4); // Image size
                fileStream.Write(BitConverter.GetBytes((int)0x00000000), 0, 4); // X pixels per meter
                fileStream.Write(BitConverter.GetBytes((int)0x00000000), 0, 4); // Y pixels per meter
                fileStream.Write(BitConverter.GetBytes(0), 0, 4); // Colors used
                fileStream.Write(BitConverter.GetBytes(0), 0, 4); // Important colors

                // Write the movi header
                fileStream.Write(BitConverter.GetBytes((int)0x6D6F7669), 0, 4); // "movi"
                fileStream.Write(BitConverter.GetBytes((int)0x00000000), 0, 4); // List size (will be updated later
                long frameOffset = fileStream.Position;

                // Write the video frames
                for (int i = 0; i < numberFrames; i++) // Write 100 frames
                {
                    Console.WriteLine($"Writing frame {i} of {numberFrames}");
                    Console.WriteLine($"Frame size: {frameSize} bytes");
                    Console.WriteLine($"File stream position: {fileStream.Position} bytes");

                    // Write the frame header
                    fileStream.Write(BitConverter.GetBytes((int)0x00000010), 0, 4); // Frame header
                    fileStream.Write(BitConverter.GetBytes((int)0x00000001), 0, 4); // Frame type (keyframe)
                    fileStream.Write(BitConverter.GetBytes((int)0x00000000), 0, 4); // Reserved
                    fileStream.Write(BitConverter.GetBytes((int)0x00000000), 0, 4); // Reserved

                    // Write the frame size header
                    fileStream.Write(BitConverter.GetBytes(frameSize), 0, 4);

                    // Write the frame data
                    byte[] frameData = new byte[frameSize];
                    for (int j = 0; j < frameSize; j += 3)
                    {
                        frameData[j] = (byte)random.Next(0, 255); // Random red value
                        frameData[j + 1] = (byte)random.Next(0, 255); // Random green value
                        frameData[j + 2] = (byte)random.Next(0, 255); // Random blue value
                    }
                    fileStream.Write(frameData, 0, frameSize);

                    // Update the frameOffset for the next frame
                    frameOffset += frameSize + 32;

                    // add conditional for writing the audio data
                    /*
                    // Write the audio data
                    byte[] audioData = new byte[audioFrameSize];
                    for (int i = 0; i < audioFrameSize; i++)
                    {
                        audioData[i] = (byte)random.Next(0, 256); // Random audio value
                    }
                    fileStream.Write(audioData, 0, audioFrameSize);
                    */
                    Console.WriteLine($"File stream position after writing frame: {fileStream.Position} bytes");
                }

                // Update the movi list size field
                fileStream.Position = fileStream.Length - 8;
                fileStream.Write(BitConverter.GetBytes(fileStream.Length - fileStream.Position - 4), 0, 4);

                // Update the LIST list size field
                fileStream.Position = 16;
                fileStream.Write(BitConverter.GetBytes(fileStream.Length - 20), 0, 4);

                // Write the idx1 header
                fileStream.Write(BitConverter.GetBytes((int)0x69647831), 0, 4); // "idx1"
                fileStream.Write(BitConverter.GetBytes((int)0x00000000), 0, 4); // Chunk size (will be updated later)

                // Write the idx1 entries
                long idxOffset = 0;
                for (int i = 0; i < numberFrames; i++)
                {
                    fileStream.Write(BitConverter.GetBytes(idxOffset), 0, 4); // Offset of the frame
                    fileStream.Write(BitConverter.GetBytes(frameSize), 0, 4); // Size of the frame
                    fileStream.Write(BitConverter.GetBytes((int)0x00000000), 0, 4); // Flags
                    fileStream.Write(BitConverter.GetBytes((int)0x00000000), 0, 4); // Type
                    idxOffset += frameSize + 32; // Update idxOffset
                }

                // Update the idx1 chunk size field
                fileStream.Position = fileStream.Length - 24 * numberFrames;
                fileStream.Write(BitConverter.GetBytes(16 * numberFrames), 0, 4);

                // Update the movi list size field
                fileStream.Position = frameOffset - 4;
                fileStream.Write(BitConverter.GetBytes(fileStream.Length - fileStream.Position - 4), 0, 4);
            }
        }
        catch (Exception ex)
        {
            //throw new Exception();
            Console.WriteLine(ex.Message);
        }
    }
}