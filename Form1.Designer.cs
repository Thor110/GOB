namespace VideoLOB
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            videoSeed = new RichTextBox();
            GenerateVideoButton = new Button();
            GenerateAudioButton = new Button();
            audioSeed = new RichTextBox();
            durationText = new TextBox();
            frequencyText = new TextBox();
            sampleRateText = new TextBox();
            GenerateImageButton = new Button();
            GenerateModelButton = new Button();
            channelsBox = new ComboBox();
            depthBox = new ComboBox();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            audioParametersText = new TextBox();
            textBox4 = new TextBox();
            verticesNumeric = new NumericUpDown();
            facesNumeric = new NumericUpDown();
            textBox5 = new TextBox();
            textBox6 = new TextBox();
            textBox7 = new TextBox();
            textBox8 = new TextBox();
            widthNumeric = new NumericUpDown();
            heightNumeric = new NumericUpDown();
            textBox9 = new TextBox();
            textBox10 = new TextBox();
            textBox11 = new TextBox();
            durationNumeric = new NumericUpDown();
            sampleNumeric = new NumericUpDown();
            frequencyNumeric = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)verticesNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)facesNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)widthNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)heightNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)durationNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sampleNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)frequencyNumeric).BeginInit();
            SuspendLayout();
            // 
            // videoSeed
            // 
            videoSeed.Location = new Point(667, 324);
            videoSeed.Name = "videoSeed";
            videoSeed.Size = new Size(121, 114);
            videoSeed.TabIndex = 0;
            videoSeed.Text = "";
            // 
            // GenerateVideoButton
            // 
            GenerateVideoButton.Location = new Point(667, 295);
            GenerateVideoButton.Name = "GenerateVideoButton";
            GenerateVideoButton.Size = new Size(121, 23);
            GenerateVideoButton.TabIndex = 1;
            GenerateVideoButton.Text = "Generate Video";
            GenerateVideoButton.UseVisualStyleBackColor = true;
            GenerateVideoButton.Click += GenerateVideoButton_Click;
            // 
            // GenerateAudioButton
            // 
            GenerateAudioButton.Location = new Point(104, 187);
            GenerateAudioButton.Name = "GenerateAudioButton";
            GenerateAudioButton.Size = new Size(121, 23);
            GenerateAudioButton.TabIndex = 3;
            GenerateAudioButton.Text = "Generate Audio";
            GenerateAudioButton.UseVisualStyleBackColor = true;
            GenerateAudioButton.Click += GenerateAudioButton_Click;
            // 
            // audioSeed
            // 
            audioSeed.Location = new Point(104, 216);
            audioSeed.Name = "audioSeed";
            audioSeed.Size = new Size(121, 114);
            audioSeed.TabIndex = 4;
            audioSeed.Text = "";
            // 
            // durationText
            // 
            durationText.Enabled = false;
            durationText.Location = new Point(231, 97);
            durationText.Name = "durationText";
            durationText.Size = new Size(149, 23);
            durationText.TabIndex = 7;
            durationText.Text = "1 - 3600 Seconds ( 1 Hour )";
            // 
            // frequencyText
            // 
            frequencyText.Enabled = false;
            frequencyText.Location = new Point(231, 69);
            frequencyText.Name = "frequencyText";
            frequencyText.Size = new Size(149, 23);
            frequencyText.TabIndex = 8;
            frequencyText.Text = "1 - 440 Hz";
            // 
            // sampleRateText
            // 
            sampleRateText.Enabled = false;
            sampleRateText.Location = new Point(231, 41);
            sampleRateText.Name = "sampleRateText";
            sampleRateText.Size = new Size(149, 23);
            sampleRateText.TabIndex = 9;
            sampleRateText.Text = "1 - 44100 Hz";
            // 
            // GenerateImageButton
            // 
            GenerateImageButton.Location = new Point(474, 302);
            GenerateImageButton.Name = "GenerateImageButton";
            GenerateImageButton.Size = new Size(121, 23);
            GenerateImageButton.TabIndex = 11;
            GenerateImageButton.Text = "Generate Image";
            GenerateImageButton.UseVisualStyleBackColor = true;
            GenerateImageButton.Click += GenerateImageButton_Click;
            // 
            // GenerateModelButton
            // 
            GenerateModelButton.Location = new Point(475, 132);
            GenerateModelButton.Name = "GenerateModelButton";
            GenerateModelButton.Size = new Size(121, 23);
            GenerateModelButton.TabIndex = 13;
            GenerateModelButton.Text = "Generate 3D Model";
            GenerateModelButton.UseVisualStyleBackColor = true;
            GenerateModelButton.Click += GenerateModelButton_Click;
            // 
            // channelsBox
            // 
            channelsBox.FormattingEnabled = true;
            channelsBox.Items.AddRange(new object[] { "1", "2" });
            channelsBox.Location = new Point(104, 128);
            channelsBox.Name = "channelsBox";
            channelsBox.Size = new Size(121, 23);
            channelsBox.TabIndex = 14;
            channelsBox.Text = "Channels";
            // 
            // depthBox
            // 
            depthBox.FormattingEnabled = true;
            depthBox.Items.AddRange(new object[] { "8", "16", "24", "32" });
            depthBox.Location = new Point(104, 158);
            depthBox.Name = "depthBox";
            depthBox.Size = new Size(121, 23);
            depthBox.TabIndex = 15;
            depthBox.Text = "Bit Depth";
            // 
            // textBox1
            // 
            textBox1.Enabled = false;
            textBox1.Location = new Point(25, 41);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(73, 23);
            textBox1.TabIndex = 16;
            textBox1.Text = "Sample Rate";
            textBox1.TextAlign = HorizontalAlignment.Right;
            // 
            // textBox2
            // 
            textBox2.Enabled = false;
            textBox2.Location = new Point(25, 69);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(73, 23);
            textBox2.TabIndex = 17;
            textBox2.Text = "Frequency";
            textBox2.TextAlign = HorizontalAlignment.Right;
            // 
            // textBox3
            // 
            textBox3.Enabled = false;
            textBox3.Location = new Point(25, 98);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(73, 23);
            textBox3.TabIndex = 18;
            textBox3.Text = "Duration";
            textBox3.TextAlign = HorizontalAlignment.Right;
            // 
            // audioParametersText
            // 
            audioParametersText.Enabled = false;
            audioParametersText.Location = new Point(104, 12);
            audioParametersText.Name = "audioParametersText";
            audioParametersText.Size = new Size(121, 23);
            audioParametersText.TabIndex = 22;
            audioParametersText.Text = "Audio Parameters";
            audioParametersText.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(476, 12);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(120, 23);
            textBox4.TabIndex = 23;
            textBox4.Text = "3D Model Parameters";
            textBox4.TextAlign = HorizontalAlignment.Center;
            // 
            // verticesNumeric
            // 
            verticesNumeric.Location = new Point(476, 42);
            verticesNumeric.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            verticesNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            verticesNumeric.Name = "verticesNumeric";
            verticesNumeric.Size = new Size(120, 23);
            verticesNumeric.TabIndex = 24;
            verticesNumeric.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // facesNumeric
            // 
            facesNumeric.Location = new Point(476, 71);
            facesNumeric.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            facesNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            facesNumeric.Name = "facesNumeric";
            facesNumeric.Size = new Size(120, 23);
            facesNumeric.TabIndex = 25;
            facesNumeric.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // textBox5
            // 
            textBox5.Enabled = false;
            textBox5.Location = new Point(420, 42);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(50, 23);
            textBox5.TabIndex = 26;
            textBox5.Text = "Vertices";
            textBox5.TextAlign = HorizontalAlignment.Right;
            // 
            // textBox6
            // 
            textBox6.Enabled = false;
            textBox6.Location = new Point(420, 71);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(50, 23);
            textBox6.TabIndex = 27;
            textBox6.Text = "Faces";
            textBox6.TextAlign = HorizontalAlignment.Right;
            // 
            // textBox7
            // 
            textBox7.Enabled = false;
            textBox7.Location = new Point(476, 103);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(120, 23);
            textBox7.TabIndex = 28;
            textBox7.Text = "1 - 1000";
            textBox7.TextAlign = HorizontalAlignment.Center;
            // 
            // textBox8
            // 
            textBox8.Enabled = false;
            textBox8.Location = new Point(476, 187);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(120, 23);
            textBox8.TabIndex = 29;
            textBox8.Text = "Image Parameters";
            textBox8.TextAlign = HorizontalAlignment.Center;
            // 
            // widthNumeric
            // 
            widthNumeric.Increment = new decimal(new int[] { 2, 0, 0, 0 });
            widthNumeric.Location = new Point(475, 216);
            widthNumeric.Maximum = new decimal(new int[] { 1920, 0, 0, 0 });
            widthNumeric.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            widthNumeric.Name = "widthNumeric";
            widthNumeric.Size = new Size(120, 23);
            widthNumeric.TabIndex = 30;
            widthNumeric.Value = new decimal(new int[] { 640, 0, 0, 0 });
            // 
            // heightNumeric
            // 
            heightNumeric.Increment = new decimal(new int[] { 2, 0, 0, 0 });
            heightNumeric.Location = new Point(475, 245);
            heightNumeric.Maximum = new decimal(new int[] { 1080, 0, 0, 0 });
            heightNumeric.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            heightNumeric.Name = "heightNumeric";
            heightNumeric.Size = new Size(120, 23);
            heightNumeric.TabIndex = 31;
            heightNumeric.Value = new decimal(new int[] { 480, 0, 0, 0 });
            // 
            // textBox9
            // 
            textBox9.Enabled = false;
            textBox9.Location = new Point(420, 216);
            textBox9.Name = "textBox9";
            textBox9.Size = new Size(49, 23);
            textBox9.TabIndex = 32;
            textBox9.Text = "Width";
            textBox9.TextAlign = HorizontalAlignment.Right;
            // 
            // textBox10
            // 
            textBox10.Enabled = false;
            textBox10.Location = new Point(420, 244);
            textBox10.Name = "textBox10";
            textBox10.Size = new Size(50, 23);
            textBox10.TabIndex = 33;
            textBox10.Text = "Height";
            textBox10.TextAlign = HorizontalAlignment.Right;
            // 
            // textBox11
            // 
            textBox11.Enabled = false;
            textBox11.Location = new Point(476, 274);
            textBox11.Name = "textBox11";
            textBox11.Size = new Size(119, 23);
            textBox11.TabIndex = 34;
            textBox11.Text = "2 - 1920 x 2 - 1080";
            // 
            // durationNumeric
            // 
            durationNumeric.Location = new Point(105, 98);
            durationNumeric.Maximum = new decimal(new int[] { 3600, 0, 0, 0 });
            durationNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            durationNumeric.Name = "durationNumeric";
            durationNumeric.Size = new Size(120, 23);
            durationNumeric.TabIndex = 35;
            durationNumeric.Value = new decimal(new int[] { 3600, 0, 0, 0 });
            // 
            // sampleNumeric
            // 
            sampleNumeric.Location = new Point(105, 41);
            sampleNumeric.Maximum = new decimal(new int[] { 44100, 0, 0, 0 });
            sampleNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            sampleNumeric.Name = "sampleNumeric";
            sampleNumeric.Size = new Size(120, 23);
            sampleNumeric.TabIndex = 36;
            sampleNumeric.Value = new decimal(new int[] { 44100, 0, 0, 0 });
            // 
            // frequencyNumeric
            // 
            frequencyNumeric.Location = new Point(105, 69);
            frequencyNumeric.Maximum = new decimal(new int[] { 440, 0, 0, 0 });
            frequencyNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            frequencyNumeric.Name = "frequencyNumeric";
            frequencyNumeric.Size = new Size(120, 23);
            frequencyNumeric.TabIndex = 37;
            frequencyNumeric.Value = new decimal(new int[] { 440, 0, 0, 0 });
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(frequencyNumeric);
            Controls.Add(sampleNumeric);
            Controls.Add(durationNumeric);
            Controls.Add(textBox11);
            Controls.Add(textBox10);
            Controls.Add(textBox9);
            Controls.Add(heightNumeric);
            Controls.Add(widthNumeric);
            Controls.Add(textBox8);
            Controls.Add(textBox7);
            Controls.Add(textBox6);
            Controls.Add(textBox5);
            Controls.Add(facesNumeric);
            Controls.Add(verticesNumeric);
            Controls.Add(textBox4);
            Controls.Add(audioParametersText);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(depthBox);
            Controls.Add(channelsBox);
            Controls.Add(GenerateModelButton);
            Controls.Add(GenerateImageButton);
            Controls.Add(sampleRateText);
            Controls.Add(frequencyText);
            Controls.Add(durationText);
            Controls.Add(audioSeed);
            Controls.Add(GenerateAudioButton);
            Controls.Add(GenerateVideoButton);
            Controls.Add(videoSeed);
            Name = "Form1";
            Text = "Gallery of Babel";
            ((System.ComponentModel.ISupportInitialize)verticesNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)facesNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)widthNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)heightNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)durationNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)sampleNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)frequencyNumeric).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        //Buttons
        private Button GenerateVideoButton;
        private Button GenerateAudioButton;
        private Button GenerateImageButton;
        private Button GenerateModelButton;
        //Seed Boxes
        private RichTextBox videoSeed;// Video Seed
        private RichTextBox audioSeed;// Audio Seed
        //Audio Parameters
        private TextBox durationText;
        private TextBox frequencyText;
        private TextBox sampleRateText;
        private ComboBox channelsBox;
        private ComboBox depthBox;
        //Labels
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox audioParametersText;
        private TextBox textBox4;
        private NumericUpDown verticesNumeric;
        private NumericUpDown facesNumeric;
        private TextBox textBox5;
        private TextBox textBox6;
        private TextBox textBox7;
        private TextBox textBox8;
        private NumericUpDown widthNumeric;
        private NumericUpDown heightNumeric;
        private TextBox textBox9;
        private TextBox textBox10;
        private TextBox textBox11;
        private NumericUpDown durationNumeric;
        private NumericUpDown sampleNumeric;
        private NumericUpDown frequencyNumeric;
    }
}